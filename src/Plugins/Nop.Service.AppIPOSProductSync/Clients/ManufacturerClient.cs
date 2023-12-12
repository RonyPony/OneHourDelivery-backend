using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Manufacturers;
using Nop.Plugin.Api.Models.ManufacturersParameters;
using Nop.Service.AppIPOSSync.Entities;
using Nop.Service.AppIPOSSync.Models;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to sync manufacturers using controllers of the Nop.Plugin.Api plugin 
    /// </summary>
    public class ManufacturerClient : BaseClient<Supplyer, ManufacturerDto, ManufacturersRootObject>
    {
        /// <summary>
        /// Constuctor of the class
        /// </summary>
        public ManufacturerClient(AppIposContext context) : base(context, "api/manufacturers")
        {
        }

        protected override async Task<ManufacturersRootObject> CreateAsync(ManufacturerDto manufacturer)
        {
            var manufacturerModel = new ManufacturerDelta
            {
                manufacturer = manufacturer
            };

            string json = JsonConvert.SerializeObject(manufacturerModel);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync("api/manufacturers", stringContent);

            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}. Error creating manufacturers with StatusCode {response.StatusCode}");
            }

            string responseJson = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ManufacturersRootObject>(responseJson);
        }

        protected override async Task<ManufacturersRootObject> GetAllFromNopCommerceAsync()
        {
            var parametersModel = new ManufacturersParametersModel
            {
                Limit = 1000
            };

            string requestUri = ApiControllerRoute;

            requestUri += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response}. {requestUri} call with StatusCode {response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();

            ManufacturersRootObject result = JsonConvert.DeserializeObject<ManufacturersRootObject>(json);
            return result;
        }

        protected override ManufacturerDto ToDto(Supplyer tableModel)
        {
            var resultManufacturerDto = new ManufacturerDto
            {
                Name = tableModel.Name,
                Description = tableModel.Comment,
                Deleted = !tableModel.Active,
                Published = tableModel.Active
            };

            return resultManufacturerDto;
        }

        protected override bool ExistsInNopCommerce(ref ManufacturersRootObject list, string categoryName) => list.Manufacturers.Any(item => item.Name == categoryName);

        /// <summary>
        /// Get manufacturer Ids by names
        /// </summary>
        /// <param name="manufacturerNames">Manufacturer names</param>
        public List<int> GetNopCommerceManufacturerIdsByNames(List<string> manufacturerNames)
        {
            IList<ManufacturerDto> list = GetAllFromNopCommerceAsync().Result.Manufacturers;

            return list.Where(manufacturer => manufacturerNames.Contains(manufacturer.Name))
                .Select(filteredManufacturers => filteredManufacturers.Id)
                .ToList();
        }

        /// <summary>
        /// Get a list of manufacturer names by product Id
        /// </summary>
        /// <param name="productId">Id of the product to search manufacturers for</param>
        public List<string> GetErpProductManufacturersByProductId(int productId)
        {
            IQueryable<string> query = from supplier in Context.Supplyers
                join productSupplier in Context.ProductSuppliers on supplier.Id equals productSupplier.SupplyerId
                where productSupplier.ProductId == productId
                select supplier.Name;

            return query.ToList();
        }

        /// <summary>
        /// Method for synching manufacturers
        /// </summary>
        public override async Task Sync()
        {
            ManufacturersRootObject nopCommerceManufacturers = await GetAllFromNopCommerceAsync();
            List<Supplyer> erpManufacturerList = GetAllFromErp();

            foreach (Supplyer manufacturer in erpManufacturerList)
            {
                if (!ExistsInNopCommerce(ref nopCommerceManufacturers, manufacturer.Name))
                {
                    await CreateAsync(ToDto(manufacturer));
                }
            }
        }
    }
}
