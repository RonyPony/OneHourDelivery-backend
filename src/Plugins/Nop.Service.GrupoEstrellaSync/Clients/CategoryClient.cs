using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using iTextSharp.text;
using Newtonsoft.Json;
using Nop.Service.GrupoEstrellaSync.Entities;
using Nop.Service.GrupoEstrellaSync.Helper;
using Nop.Service.GrupoEstrellaSync.Models;
using Nop.Service.GrupoEstrellaSync.Models.Parameters;
using Nop.Service.GrupoEstrellaSync.Models.RootObjets;

namespace Nop.Service.GrupoEstrellaSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to consume the Category controller of the Nop.Plugin.API 
    /// </summary>
    public class CategoryClient
    {
        private static HttpClient client;
        private readonly string urlApi;
        private readonly Service1 _service1;


        public CategoryClient(Service1 service1)
        {
            _service1 = service1;
            urlApi = service1._configuration.GetSection("settings").GetSection("NopCommerceApiURL").Value;
            TokenHelper tokenHelper = new TokenHelper(_service1);
            client = new HttpClient() { BaseAddress = new Uri(urlApi), DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue(tokenHelper.AuthenticationScheme, tokenHelper.ApiRequestToken) } };
        }

        private static string GetQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                             where p.GetValue(obj, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        private static void CreateCategoryAsync(CategoryDto category)
        {
            try
            {
                string json = JsonConvert.SerializeObject(category);
                StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("api/categories", stringContent).Result;
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ToString() + "   " + " call with StatusCode not Success. Error Create category");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<CategoriesRootObject> GetCategoriesAsync()
        {
            CategoriesParametersModel parametersModel = new CategoriesParametersModel();
            string path = "api/categories";
            path += "?" + GetQueryString(parametersModel);

            Task<HttpResponseMessage> response = client.GetAsync(path);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception(response.ToString() + "   " + path + " call with StatusCode not Success. "); //new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.GettingCategoriesFromSap")} Status Code: {response.Result.StatusCode}");
            }
            try
            {
                Task<string> json = response.Result.Content.ReadAsStringAsync();
                json.Wait();

                CategoriesRootObject result = JsonConvert.DeserializeObject<CategoriesRootObject>(json.Result);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Get Category Id By Name
        /// </summary>
        /// <param name="Name"> string name</param>
        /// <returns><see cref="int"/></returns>
        public static int GetCategoryIdByName(string Name, ref CategoriesRootObject list)
        {
            int result = 0;

            if (list.Categories.Where(element => !string.IsNullOrWhiteSpace(element.Name)).Any(item => item.Name.Trim() == Name.Trim()))
            {
                result = list.Categories.FirstOrDefault(item => item.Name.Trim() == Name.Trim()).Id;
            }

            return result;
        }
        /// <summary>
        /// Get Stardent Category By Id
        /// </summary>
        /// <param name="id"><see cref="int"/></param>
        /// <returns><see cref="TipoDeProducto"/></returns>
        public static TipoDeProducto GetStardentCategoryById(int id, ref List<TipoDeProducto> list)
        {
            TipoDeProducto result = new TipoDeProducto();
            if (list.Any(item => item.CodigoTipoProducto == id))
            {
                result = list.FirstOrDefault(item => item.CodigoTipoProducto == id);
            }

            return result;
        }

        private static async Task<CategoriesRootObject> GetCategoryByIdAsync(int Id)
        {
            string path = $"/api/categories/{Id}";

            Task<HttpResponseMessage> response = client.GetAsync(path);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new Exception(response.Result.ToString() + "   " + path + " call with StatusCode not Success. "); //new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.GettingCategoriesFromSap")} Status Code: {response.Result.StatusCode}");
            }

            Task<string> json = response.Result.Content.ReadAsStringAsync();
            json.Wait();

            return JsonConvert.DeserializeObject<CategoriesRootObject>(json.Result);
        }

        private static CategoryDto ToCategoryDto(TipoDeProducto tableModel)
        {
            CategoryDto resultCategoryDto = new CategoryDto();

            resultCategoryDto.Name = tableModel.DescripcionTipo;
            resultCategoryDto.Description = tableModel.DescripcionTipo;

            return resultCategoryDto;
        }

        public static List<TipoDeProducto> GetStardenTipoDeProductos()
        {
            List<TipoDeProducto> resultListTipoDeProductos = new List<TipoDeProducto>();
            using (GrupoEstrellaContext db = new GrupoEstrellaContext())
            {
                resultListTipoDeProductos = db.TipoDeProductos.ToList();
            }

            return resultListTipoDeProductos;
        }

        private static bool CategoryExist(ref CategoriesRootObject list, string Categoryname) => list.Categories.Any(item => item.Name == Categoryname && item.ParentCategoryId == 0);

        /// <summary>
        /// Method for sync Categories
        /// </summary>
        public async Task SyncCategories()
        {
            try
            {
                CategoriesRootObject nopCommerceCategories = await GetCategoriesAsync();
                List<TipoDeProducto> StardentCategories = GetStardenTipoDeProductos();

                foreach (TipoDeProducto stardentCategory in StardentCategories)
                {
                    if (!CategoryExist(ref nopCommerceCategories, stardentCategory.DescripcionTipo))
                    {
                        CreateCategoryAsync(ToCategoryDto(stardentCategory));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static async Task<CategoryDto> UpdateCategoryAsync(CategoryDto category)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(category).ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(
                $"api/categories/{category.Id}", content);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            string json = await response.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<CategoryDto>(json);

            return category;
        }

        private static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/categories/{id}");
            return response.StatusCode;
        }



    }
}
