using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Categories;
using Nop.Plugin.Api.Models.CategoriesParameters;
using Nop.Service.AppIPOSSync.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to sync categories using controllers of the Nop.Plugin.Api plugin 
    /// </summary>
    public class CategoryClient : BaseClient<ProductCategory, CategoryDto, CategoriesRootObject>
    {
        /// <summary>
        /// Constuctor of the class
        /// </summary>
        public CategoryClient(AppIposContext context) : base(context, "api/categories")
        {
        }

        protected override async Task<CategoriesRootObject> GetAllFromNopCommerceAsync()
        {
            var parametersModel = new CategoriesParametersModel
            {
                Limit = 1000
            };

            string requestUri = "api/categories";

            requestUri += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response}. {requestUri} call with StatusCode {response.StatusCode}");

            string json = await response.Content.ReadAsStringAsync();

            CategoriesRootObject result = JsonConvert.DeserializeObject<CategoriesRootObject>(json);
            return result;
        }

        protected override CategoryDto ToDto(ProductCategory tableModel)
        {
            var resultCategoryDto = new CategoryDto
            {
                Name = tableModel.Name,
                Description = tableModel.Comment,
                Deleted = !tableModel.Active
            };

            return resultCategoryDto;
        }

        protected override bool ExistsInNopCommerce(ref CategoriesRootObject list, string categoryName) => list.Categories.Any(item => item.Name == categoryName && item.ParentCategoryId == 0);

        /// <summary>
        /// Get Erp Category By Id
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="ProductCategory"/></returns>
        public ProductCategory GetErpCategoryById(int id)
        {
            var result = new ProductCategory();

            var list = GetAllFromErp();

            if (list.Any(item => item.Id == id))
            {
                result = list.FirstOrDefault(item => item.Id == id);
            }

            return result;
        }

        /// <summary>
        /// Get nopCommerce category Id by name
        /// </summary>
        /// <param name="name">Category name</param>
        public int GetNopCommerceCategoryIdByName(string name)
        {
            int result = 0;

            IList<CategoryDto> list = GetAllFromNopCommerceAsync().Result.Categories;

            if (list.Any(item => item.Name.Trim() == name.Trim()))
            {
                CategoryDto category = list.FirstOrDefault(item => item.Name.Trim() == name.Trim());

                if (category != null)
                    result = category.Id;
            }

            return result;
        }

        /// <summary>
        /// Method for sync Categories
        /// </summary>
        public override async Task Sync()
        {
            CategoriesRootObject nopCommerceCategories = await GetAllFromNopCommerceAsync();
            List<ProductCategory> erpCategoryList = GetAllFromErp();

            foreach (ProductCategory stardentCategory in erpCategoryList)
            {
                if (!ExistsInNopCommerce(ref nopCommerceCategories, stardentCategory.Name))
                {
                    await CreateAsync(ToDto(stardentCategory));
                }
            }
        }
    }
}
