using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Data;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;
using Nop.Plugin.Synchronizers.SAPCustomers.Helpers;
using Nop.Plugin.Synchronizers.SAPCustomers.Models;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Tasks;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Services
{
    /// <summary>
    /// Represents the task model class for synchronizing the customers.
    /// </summary>
    public sealed class CustomerSyncTask : IScheduleTask
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        private readonly CustomerSettings _customerSettings;
        private readonly IStoreContext _storeContext;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IAddressService _addressService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly List<SapCustomerRoleModel> _sapCustomerRoles;
        private readonly IRepository<CustomerSapCustomerMapping> _customerSapCustomerRepository;

        private readonly SapCustomersSynchronizerConfigurationSettings _customersSynchronizerConfigurationSettings;

        /// <summary>
        /// Initializes a new instance of CustomerSyncTask class with the value indicated as parameters.
        /// </summary>
        /// <param name="customerService">Represents an implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="logger">Represents an implementation of <see cref="ILogger"/>.</param>
        /// <param name="customerSettings">Represents an instance of <see cref="CustomerSettings"/>.</param>
        /// <param name="storeContext">Represents an implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="dateTimeSettings">Represents an instance of <see cref="DateTimeSettings"/>.</param>
        /// <param name="genericAttributeService">Represents an implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="addressService">Represents an implementation of <see cref="IAddressService"/>.</param>
        /// <param name="customerActivityService">Represents an implementation of <see cref="ICustomerActivityService"/>.</param>
        /// <param name="customerRegistrationService">Represents an implementation of <see cref="ICustomerRegistrationService"/>.</param>
        /// <param name="customersSynchronizerConfigurationSettings">Represents an instance of <see cref="SapCustomersSynchronizerConfigurationSettings"/>.</param>
        /// <param name="customerSapCustomerRepository">Represents an implementation of <see cref="IRepository{CustomerSapCustomerMapping}"/>.</param>
        public CustomerSyncTask(
            ICustomerService customerService,
            ILogger logger,
            CustomerSettings customerSettings,
            IStoreContext storeContext,
            DateTimeSettings dateTimeSettings,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IAddressService addressService,
            ICustomerActivityService customerActivityService,
            ICustomerRegistrationService customerRegistrationService,
            SapCustomersSynchronizerConfigurationSettings customersSynchronizerConfigurationSettings,
            IRepository<CustomerSapCustomerMapping> customerSapCustomerRepository)
        {
            _customerService = customerService;
            _logger = logger;
            _customerSettings = customerSettings;
            _storeContext = storeContext;
            _dateTimeSettings = dateTimeSettings;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _addressService = addressService;
            _customerActivityService = customerActivityService;
            _customerRegistrationService = customerRegistrationService;
            _customersSynchronizerConfigurationSettings = customersSynchronizerConfigurationSettings;
            _customerSapCustomerRepository = customerSapCustomerRepository;

            _sapCustomerRoles = GetCustomerRolesFromApi();

            InitializeSapCustomerRoles();
        }

        void IScheduleTask.Execute()
        {
            try
            {
                Task<List<CustomerModel>> customersTask = GetCustomersFromApi();
                customersTask.Wait();

                List<CustomerModel> customers = customersTask.Result;

                foreach (var customer in customers)
                {
                    Task createCustomerTask = RegisterCustomer(customer);
                    createCustomerTask.Wait();
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error syncing customers. {e.Message}", e);
            }
        }

        private List<SapCustomerRoleModel> GetCustomerRolesFromApi()
        {
            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_customersSynchronizerConfigurationSettings.AuthenticationHeaderScheme, _customersSynchronizerConfigurationSettings.AuthenticationHeaderParameter)
                }
            };

            Task<HttpResponseMessage> response = client.GetAsync(_customersSynchronizerConfigurationSettings.SapCustomerRolesUrl);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting customer roles from SAP API. Status code: {response.Result.StatusCode}");
            }

            Task<string> json = response.Result.Content.ReadAsStringAsync();
            json.Wait();
            var customerRoles = JsonConvert.DeserializeObject<SapCustomerRoleResponseModel>(json.Result);

            return customerRoles.Extra;
        }

        private void InitializeSapCustomerRoles()
        {
            try
            {
                foreach (var sapCustomerRole in _sapCustomerRoles)
                {
                    string customerRoleName = $"{sapCustomerRole.Name.Replace(" ", "").Trim()}{DefaultsInfo.SapPrefix}";
                    CustomerRole foundCustomerRole = _customerService.GetCustomerRoleBySystemName(customerRoleName);

                    if (foundCustomerRole == null)
                    {
                        _customerService.InsertCustomerRole(new CustomerRole
                        {
                            Name = $"{sapCustomerRole.Name}{DefaultsInfo.SapPrefix}",
                            SystemName = customerRoleName,
                            Active = true,
                            IsSystemRole = true
                        });
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error syncing customers. {e.Message}", e);
            }
        }

        private async Task RegisterCustomer(CustomerModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                _logger.Error($"Error while registering the customer from CustomerSyncTask class. The email for the customer with full name: {model.FullName} is null or white space.");
                return;
            }

            if (_customerService.GetCustomerByEmail(model.Email) != null)
            {
                _logger.Information($"The customer with the email {model.Email} already exists.");
                return;
            }

            //fill entity from model
            var customer = model.ToEntity<Customer>();

            customer.CustomerGuid = Guid.NewGuid();
            customer.CreatedOnUtc = DateTime.UtcNow;
            customer.LastActivityDateUtc = DateTime.UtcNow;
            customer.RegisteredInStoreId = _storeContext.CurrentStore.Id;

            _customerService.InsertCustomer(customer);

            // Customer password
            var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
            _customerRegistrationService.ChangePassword(changePassRequest);

            // Customer role
            var customerRoles = new List<CustomerCustomerRoleMapping>
            {
                new CustomerCustomerRoleMapping
                {
                    CustomerId = customer.Id,
                    CustomerRoleId = GetCustomerRoleIdBySapGroupCode(model.SelectedCustomerRoleIds.First())
                },
                new CustomerCustomerRoleMapping
                {
                    CustomerId = customer.Id,
                    CustomerRoleId = model.SelectedCustomerRoleIds.Last()
                }
            };

            foreach (var customerRole in customerRoles)
            {
                _customerService.AddCustomerRoleMapping(customerRole);
            }

            //form fields
            if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.TimeZoneIdAttribute,
                    model.TimeZoneId);
            if (_customerSettings.GenderEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.GenderAttribute, model.Gender);
            if (_customerSettings.FirstNameEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.FirstNameAttribute,
                    model.FirstName);
            if (_customerSettings.LastNameEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.LastNameAttribute, model.LastName);
            if (_customerSettings.DateOfBirthEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.DateOfBirthAttribute,
                    model.DateOfBirth);
            if (_customerSettings.CompanyEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CompanyAttribute, model.Company);
            if (_customerSettings.StreetAddressEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.StreetAddressAttribute,
                    model.StreetAddress);
            if (_customerSettings.StreetAddress2Enabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.StreetAddress2Attribute,
                    model.StreetAddress2);
            if (_customerSettings.ZipPostalCodeEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.ZipPostalCodeAttribute,
                    model.ZipPostalCode);
            if (_customerSettings.CityEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CityAttribute, model.City);
            if (_customerSettings.CountyEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CountyAttribute, model.County);
            if (_customerSettings.CountryEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CountryIdAttribute,
                    model.CountryId);
            if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.StateProvinceIdAttribute,
                    model.StateProvinceId);
            if (_customerSettings.PhoneEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.PhoneAttribute, model.Phone);
            if (_customerSettings.FaxEnabled)
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.FaxAttribute, model.Fax);

            var customerSapCustomerMapping = new CustomerSapCustomerMapping
            {
                CustomerId = customer.Id,
                SapCustomerId = customer.AdminComment
            };

            _customerSapCustomerRepository.Insert(customerSapCustomerMapping);

            List<KeyValuePair<string, Address>> customerAddressesDictionary = await GetCustomerAddressesFromApi(model);

            foreach (var customerAddressDictionary in customerAddressesDictionary)
            {
                //insert default address (if possible)
                var customerAddress = customerAddressDictionary.Value;

                if (customerAddress.CountryId == 0)
                {
                    customerAddress.CountryId = null;
                }
                if (customerAddress.StateProvinceId == 0)
                {
                    customerAddress.StateProvinceId = null;
                }

                _addressService.InsertAddress(customerAddress);

                _customerService.InsertCustomerAddress(customer, customerAddress);

                // In case that it's a billing address, AddressType should be "B", otherwise, it's a shipping address
                if (customerAddressDictionary.Key.Equals("B"))
                {
                    customer.BillingAddressId = customerAddress.Id;
                }
                else
                {
                    customer.ShippingAddressId = customerAddress.Id;
                }
            }

            _customerService.UpdateCustomer(customer);

            //ensure that a customer in the Vendors role has a vendor account associated.
            //otherwise, he will have access to ALL products
            if (_customerService.IsVendor(customer) && customer.VendorId == 0)
            {
                var vendorRole = _customerService.GetCustomerRoleBySystemName(NopCustomerDefaults.VendorsRoleName);
                _customerService.RemoveCustomerRoleMapping(customer, vendorRole);
            }

            _customerActivityService.InsertActivity("AddNewCustomer", string.Format(_localizationService.GetResource("ActivityLog.AddNewCustomer"), customer.Id), customer);
        }

        private int GetCustomerRoleIdBySapGroupCode(int sapGroupCode)
        {
            int defaultCustomerRole = 3;
            string sapCustomerRoleName = _sapCustomerRoles.FirstOrDefault(cr => cr.Code == sapGroupCode).Name;

            if (string.IsNullOrWhiteSpace(sapCustomerRoleName))
            {
                return defaultCustomerRole;
            }

            string customerRoleSysname = $"{sapCustomerRoleName.Replace(" ", "").Trim()}{DefaultsInfo.SapPrefix}";
            CustomerRole foundCustomerRole = _customerService.GetCustomerRoleBySystemName(customerRoleSysname);

            return foundCustomerRole.Id;
        }

        private async Task<List<CustomerModel>> GetCustomersFromApi()
        {
            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_customersSynchronizerConfigurationSettings.AuthenticationHeaderScheme, _customersSynchronizerConfigurationSettings.AuthenticationHeaderParameter)
                }
            };

            HttpResponseMessage response = await client.GetAsync(_customersSynchronizerConfigurationSettings.SapCustomerUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error getting customers from SAP API. Status code: {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();
            var customersResponse = JsonConvert.DeserializeObject<SapCustomerResponse>(json);

            List<CustomerModel> customers = new List<CustomerModel>();

            foreach (var model in customersResponse.Extra)
            {
                customers.Add(new CustomerModel
                {
                    Email = model.EmailAddress,
                    Username = model.EmailAddress,
                    Active = true,
                    AdminComment = model.CardCode,
                    CreatedOn = DateTime.Now,
                    FullName = model.CardName,
                    Phone = model.Phone1,
                    SelectedCustomerRoleIds = new List<int> { model.GroupCode, DefaultsInfo.RegisteredRoleId },
                    Password = DefaultsInfo.DefaultUserPassword
                });
            }

            return customers;
        }

        private async Task<List<KeyValuePair<string, Address>>> GetCustomerAddressesFromApi(CustomerModel customer)
        {
            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_customersSynchronizerConfigurationSettings.AuthenticationHeaderScheme, _customersSynchronizerConfigurationSettings.AuthenticationHeaderParameter)
                }
            };

            HttpResponseMessage response = await client.GetAsync(_customersSynchronizerConfigurationSettings.SapCustomerAddressUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<KeyValuePair<string, Address>>();
            }

            string json = await response.Content.ReadAsStringAsync();
            SapCustomerAddressResponse customersResponse = JsonConvert.DeserializeObject<SapCustomerAddressResponse>(json);

            List<KeyValuePair<string, Address>> customerAddresses = new List<KeyValuePair<string, Address>>();

            foreach (var model in customersResponse.Extra)
            {
                customerAddresses.Add(new KeyValuePair<string, Address>(model.AddressType, new Address
                {
                    Company = customer.FullName,
                    Email = customer.Email,
                    County = model.County,
                    City = $"{model.City}, {model.Country}",
                    Address1 = $"{model.Street}, {model.Block}",
                    ZipPostalCode = model.ZipCode,
                    PhoneNumber = customer.Phone,
                    CreatedOnUtc = DateTime.UtcNow
                }));
            }

            return customerAddresses;
        }
    }
}
