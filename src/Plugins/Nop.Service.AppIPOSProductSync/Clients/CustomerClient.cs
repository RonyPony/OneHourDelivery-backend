using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Customers;
using Nop.Plugin.Api.Models.CustomersParameters;
using Nop.Service.AppIPOSSync.Entities;
using Nop.Service.AppIPOSSync.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// Represents the main class for handling the client sync process for the APP IPOS Erp.
    /// </summary>
    public sealed class CustomerClient : BaseClient<Client, CustomerDto, CustomersRootObject>
    {
        private readonly string _logFolderName;
        private readonly string _logFileName;
        private readonly LogHelper _logHelper;
        private readonly IConfiguration _configuration = ConfigurationHelper.GetConfiguration();

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="context">Represents the database context used for the sync process.</param>
        public CustomerClient(AppIposContext context) : base(context, "api/customers")
        {
            var settings = _configuration.GetSection("Settings");

            _logFolderName = settings["LogFolderName"];
            _logFileName = settings["LogFileName"];
            _logHelper = new LogHelper(settings["EventViewerName"]);
        }

        /// <summary>
        /// Syncs the customer from nopCommerce to the ERP.
        /// </summary>
        /// <returns>This method does not return anything. It just perform the sync task.</returns>
        public override async Task Sync()
        {
            CustomersRootObject customers = await GetCustomersAsync();
            List<Client> customersErp = GetErpClients();

            foreach (var customer in customers.Customers)
            {
                if (!ClientExist(customersErp, customer))
                {
                    try
                    {
                        int customerErpId = SaveCustomerToErp(customer);
                        SaveErpCode(customer.Id, customerErpId.ToString());
                    }
                    catch (Exception e)
                    {
                        WriteToFile($"There was an error while syncing the customer with the email: {customer.Email } {Environment.NewLine} {e.Message} {Environment.NewLine} {e.StackTrace}", EventLogEntryType.Error);
                        continue;
                    }
                }
                else
                {
                    try
                    {
                        string customerErpId = GetErpClientIdByEmail(customer.Email);
                        SaveErpCode(customer.Id, customerErpId);
                    }
                    catch (Exception e)
                    {
                        WriteToFile($"There was an error while updating the customer with the email: {customer.Email } {Environment.NewLine} {e.Message} {Environment.NewLine} {e.StackTrace}", EventLogEntryType.Error);
                        continue;
                    }
                }
            }
        }

        private async Task<CustomersRootObject> GetCustomersAsync()
        {
            string path = "api/customers";
            CustomersParametersModel parametersModel = new CustomersParametersModel { Limit = int.MaxValue };

            path += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await Client.GetAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}  {path} " + "   " + path + " call with StatusCode not Success.");
            }

            Task<string> json = response.Content.ReadAsStringAsync();

            CustomersRootObject result = JsonConvert.DeserializeObject<CustomersRootObject>(json.Result);

            return result;
        }

        private void SaveErpCode(int nopId, string erpId)
        {
            string path = $"erp/customers?CustomerId={nopId}&ErpCustomerId={erpId}";

            Task<HttpResponseMessage> response = Client.PostAsync(path, new StringContent(""));
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception($"There was an error saving client ERPcode in NopCommerce: {response.Result.RequestMessage} at {DateTime.Now}");
            }
        }

        private List<Client> GetErpClients() => Context.Clients.ToList();

        private static bool ClientExist(List<Client> list, CustomerDto clientDto)
            => list.Any(item => item.Email.Trim() == clientDto.Email.Trim());

        private int SaveCustomerToErp(CustomerDto customer)
        {
            string regionName = customer.CustomerInfo.CustomerAttributes
                    .FirstOrDefault(x => x.Name == "REGION")?.Values
                    .FirstOrDefault(x => x.IsPreSelected)?.Name;

            int stateId = customer.CustomerInfo.StateProvinceId;

            string cityName = customer.CustomerInfo.AvailableStates.FirstOrDefault(x => x.Value == stateId.ToString())?.Text;
            var clientDefaultSettings = _configuration.GetSection("ClientDefaultSettings");

            var client = new Client
            {
                Email = customer.Email,
                Name = customer.FirstName ?? "N/A",
                LastName = customer.LastName ?? "N/A",
                Gender = customer.Gender,
                BirthDate = customer.DateOfBirth,
                Active = customer.Active,
                DocumentType = int.Parse(clientDefaultSettings["DocumentType"]),
                DocumentId = clientDefaultSettings["DocumentId"],
                Language = int.Parse(clientDefaultSettings["Language"]),
                DiscountType = int.Parse(clientDefaultSettings["DiscountType"]),
                DiscountValue = int.Parse(clientDefaultSettings["DiscountValue"]),
                Credit = decimal.Parse(clientDefaultSettings["Credit"]),
                Exonerated = bool.Parse(clientDefaultSettings["Exonerated"]),
                RecordDate = DateTime.Now,
                Region = GetRegionErpIdByName(regionName),
                Phone = customer.CustomerInfo.Phone,
                Country = int.Parse(clientDefaultSettings["Country"]),
                City = GetCityErpIdByName(cityName)
            };

            Context.Clients.Add(client);
            Context.SaveChanges();

            return client.Id;
        }

        /// <summary>
        /// Gets ERP client id by its email.
        /// </summary>
        /// <param name="email">Represents the client's email</param>
        /// <returns>The client id.</returns>
        public string GetErpClientIdByEmail(string email)
            => Context.Clients.FirstOrDefault(x => x.Email == email)?.Id.ToString();

        private int? GetRegionErpIdByName(string name)
            => Context.Regions.FirstOrDefault(x => x.Name == name)?.Id;

        private int GetCityErpIdByName(string name)
        {
            var clientDefaultSettings = _configuration.GetSection("ClientDefaultSettings");
            int defaultCountry = int.Parse(clientDefaultSettings["Country"]);

            City city = Context.Cities.FirstOrDefault(x => x.Name == name && x.Country == defaultCountry);

            if (city != null)
            {
                return city.Id;
            }

            return defaultCountry;
        }

        private void WriteToFile(string message, EventLogEntryType type)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName + "\\" + _logFileName + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            
            if (!File.Exists(filepath))
            { 
                using StreamWriter sw = File.CreateText(filepath);
                sw.WriteLine(message);
            }
            else
            {
                using StreamWriter sw = File.AppendText(filepath);
                sw.WriteLine(message);
            }

            _logHelper.LogEventViewer(message, type);
        }
    }
}
