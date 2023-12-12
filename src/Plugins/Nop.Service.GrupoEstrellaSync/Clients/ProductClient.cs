using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Service.GrupoEstrellaSync.Entities;
using Nop.Service.GrupoEstrellaSync.Helper;
using Nop.Service.GrupoEstrellaSync.Models;
using Nop.Service.GrupoEstrellaSync.Models.Parameters;
using Nop.Service.GrupoEstrellaSync.Models.RootObjets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NUglify.Helpers;

namespace Nop.Service.GrupoEstrellaSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to consume the Product and ProductCategoryMapping controllers of the Nop.Plugin.API  
    /// </summary>
    public class ProductClient
    {

        private static HttpClient client;
        private readonly string urlApi;
        private readonly Service1 _service1;

        public ProductClient(Service1 service1)
        {
            _service1 = service1;
            urlApi = service1._configuration.GetSection("settings").GetSection("NopCommerceApiURL").Value;
            TokenHelper tokenHelper = new TokenHelper(_service1);
            client = new HttpClient() { BaseAddress = new Uri(urlApi), DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue(tokenHelper.AuthenticationScheme, tokenHelper.ApiRequestToken) } };

        }

        private static void ShowProduct(ProductDto product)
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: " +
                $"{product.Price}\tCategory: {product.FullDescription}");
        }

        private static string GetQueryString(object obj)
        {
            IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                             where p.GetValue(obj, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        private async Task<ProductDto> CreateProductAsync(ProductDto product)
        {
            try
            {
                string json = JsonConvert.SerializeObject(product);
                StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/products", stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response + "   " + " call with StatusCode not Success. Error Create product");
                }

                string jsonResult = await response.Content.ReadAsStringAsync();

                ProductsRootObjectDto result = JsonConvert.DeserializeObject<ProductsRootObjectDto>(jsonResult);

                return result.Products.FirstOrDefault();
            }
            catch (Exception e)
            {
                WriteToFile($"ProductId: {product.Id}, ProductSKU: {product.Sku}", LogHelper.Logtype.Error);
                throw e;
            }
        }

        private static bool ValidateProduct(ProductDto product)
        {
            return string.IsNullOrWhiteSpace(product.Sku) ||
                   string.IsNullOrWhiteSpace(product.Name) ||
                   product.Price == null ||
                   product.StockQuantity == null;
        }

        private static async Task<ProductsRootObjectDto> GetProductAsync()
        {
            using (GrupoEstrellaContext db = new GrupoEstrellaContext())
            {
                string path = "api/products";
                ProductsParametersModel parametersModel = new Models.Parameters.ProductsParametersModel();

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

                    ProductsRootObjectDto result = JsonConvert.DeserializeObject<ProductsRootObjectDto>(json.Result);
                    result.Products = result.Products.Where(x => !string.IsNullOrEmpty(x.Sku)).ToList();
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private ProductDto ToProductDto(MaestroProducto tableModel, ref List<Lote> lotes)
        {
            ProductDto resultProductDto = new ProductDto();
            Lote stockInfo = GetStardentStockBySku(tableModel.Product0, ref lotes);

            resultProductDto.Name = tableModel.DescripcionProd.Trim();
            resultProductDto.Sku = tableModel.Product0.Trim();
            resultProductDto.Price = GetProductPrice(resultProductDto.Sku);
            //resultProductDto.Price = 10;
            resultProductDto.ShortDescription = tableModel.DescripcionProd.Trim();
            resultProductDto.Published = !string.IsNullOrEmpty(tableModel.ActivoEnWeb);

            resultProductDto.ManufacturerIds = new List<int>(Convert.ToInt32(tableModel.CodigoMarca));
            resultProductDto.Gtin = tableModel.CodigoArancelario;
            //resultProductDto.IsTaxExempt = !string.IsNullOrEmpty(tableModel.PagaImpuestos);
            resultProductDto.IsTaxExempt = tableModel.PagaImpuestos.ToUpper().Trim() != "S";
            resultProductDto.StockQuantity = (int)stockInfo.CantidadDisponible;

            resultProductDto.ManageInventoryMethodId = (int)ManageInventoryMethod.ManageStock;
            resultProductDto.DisplayStockAvailability = true;
            resultProductDto.DisplayStockQuantity = true;
            resultProductDto.MinStockQuantity = 1;
            resultProductDto.OrderMinimumQuantity = 1;
            resultProductDto.OrderMaximumQuantity = (int)stockInfo.CantidadDisponible;
            resultProductDto.TaxCategoryId = int.Parse(_service1._configuration.GetSection("settings").GetSection("DefaultTaxCategoryId").Value);

            return resultProductDto;
        }

        private static decimal? GetProductPrice(string product0)
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            List<PreciosProducto> preciosProductos = db.PreciosProductos.Where(x => x.Product0 == product0).ToList();

            return preciosProductos.FirstOrDefault()?.PrecioMaximo;
        }

        private async Task<ProductDto> UpdateProductAsync(ProductDto product)
        {
            try
            {
                var requestJson = JsonConvert.SerializeObject(product).ToString();
                StringContent content = new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(
                    $"api/UpdateExistProduct", content);
                response.EnsureSuccessStatusCode();

                // Deserialize the updated product from the response body.
                string json = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductDto>(json);

                return product;
            }
            catch (Exception e)
            {
                WriteToFile($"ProductId: {product.Id}, ProductSKU: {product.Sku}", LogHelper.Logtype.Error);
                throw;
            }
        }

        private static List<MaestroProducto> GetStardenProductos()
        {
            List<MaestroProducto> resultListTipoDeProductos = new List<MaestroProducto>();
            using (GrupoEstrellaContext db = new GrupoEstrellaContext())
            {
                resultListTipoDeProductos = db.MaestroProductos.Where(x => x.EstadoProducto == "A" && x.ActivoEnWeb == "A").ToList();
            }

            return resultListTipoDeProductos;
        }

        private static Lote GetStardentStockBySku(string sku, ref List<Lote> lotes)
        {
            Lote lote = lotes.FirstOrDefault(x => x.Product0.Trim() == sku.Trim() && x.CodigoBodega.Trim() == "3" && x.CodigoDeUbicacion.Trim() == "1" && x.CodigoDeLote.Trim() == "1") ?? new Lote();

            return lote;
        }

        private static bool ProductExist(ref ProductsRootObjectDto list, MaestroProducto product)
        {
            return list.Products.Where(element => !string.IsNullOrWhiteSpace(element.Sku)).Any(item => item.Sku.Trim() == product.Product0.Trim());
        }

        private static int GetProductIdBySku(string sku)
        {
            int result = 0;
            var list = GetProductAsync().Result;

            if (list.Products.Where(element => !string.IsNullOrWhiteSpace(element.Sku)).Any(item => item.Sku.Trim() == sku.Trim()))
            {
                result = list.Products.Where(element => !string.IsNullOrWhiteSpace(element.Sku)).FirstOrDefault(item => item.Sku.Trim() == sku.Trim()).Id;
            }
            return result;
        }

        private static int GetProductIdBySku(string sku, ref ProductsRootObjectDto productsRootObjectDto)
        {
            int result = 0;
            var list = productsRootObjectDto;

            if (list.Products.Where(element => !string.IsNullOrWhiteSpace(element.Sku)).Any(item => item.Sku.Trim() == sku.Trim()))
            {
                result = list.Products.Where(element => !string.IsNullOrWhiteSpace(element.Sku)).FirstOrDefault(item => item.Sku.Trim() == sku.Trim()).Id;
            }
            return result;
        }

        /// <summary>
        /// Method for sync Products
        /// </summary>
        public async Task SyncProducts()
        {
            ProductsRootObjectDto nopCommerceProducts = new ProductsRootObjectDto();
            List<MaestroProducto> stardentProducts = new List<MaestroProducto>();
            List<Lote> lotes = new List<Lote>();
            CategoriesRootObject nopCategories = new CategoriesRootObject();
            List<TipoDeProducto> erpCategories = new List<TipoDeProducto>();


            try
            {
                nopCommerceProducts = await GetProductAsync();
                stardentProducts = GetStardenProductos();
                lotes = GetAllStardentLotes();
                nopCategories = await CategoryClient.GetCategoriesAsync();
                erpCategories = CategoryClient.GetStardenTipoDeProductos();
            }
            catch (Exception e)
            {
                WriteToFile($"{e.Message}{e.StackTrace}238", LogHelper.Logtype.Error);
                throw e;
            }

            foreach (MaestroProducto stardentProduct in stardentProducts)
            {
                string categoryName = CategoryClient.GetStardentCategoryById(stardentProduct.CodigoTipoProducto, ref erpCategories)
                        .DescripcionTipo;

                bool exist = ProductExist(ref nopCommerceProducts, stardentProduct);

                if (!exist)
                {
                    ProductDto product = ToProductDto(stardentProduct, ref lotes);

                    if (ValidateProduct(product))
                    {
                        WriteToFile(
                            "No se pudo sincronizar el producto porque no tiene alguno de los valores que se necesitan para su correcta sicronzación",
                            LogHelper.Logtype.Info);
                        continue;
                    }

                    await CreateProductAsync(product);

                    ProductCategoryMappingDto mapping = new ProductCategoryMappingDto
                    {
                        ProductId = GetProductIdBySku(product.Sku),
                        CategoryId = CategoryClient.GetCategoryIdByName(categoryName, ref nopCategories)
                    };

                    SetProductCategory(mapping);
                }
                else
                {
                    ProductDto product = ToProductDto(stardentProduct, ref lotes);

                    if (ValidateProduct(product))
                    {
                        WriteToFile("No se pudo sincronizar el producto porque no tiene alguno de los valores que se necesitan para su correcta sicronzación", LogHelper.Logtype.Info);
                        continue;
                    }

                    product.Id = GetProductIdBySku(product.Sku, ref nopCommerceProducts);

                    await UpdateProductAsync(product);

                }
            }

            var skusToUnpublish = nopCommerceProducts.Products.Where(x => x.Published == true).Select(p => p.Sku.Trim())
                .Except(stardentProducts.Select(s => s.Product0.Trim()));

            skusToUnpublish.ForEach(async sku =>
            {
                var product = nopCommerceProducts.Products.FirstOrDefault(p => p.Sku.Trim() == sku);

                product.Published = false;

                await UpdateProductAsync(product);
            });
        }

        private static List<Lote> GetAllStardentLotes()
        {
            using GrupoEstrellaContext db = new GrupoEstrellaContext();

            return db.Lotes.ToList();
        }

        private static void CreateProductCategoryAsync(ProductCategoryMappingDto productyCategoryMappingDto)
        {
            try
            {
                string json = JsonConvert.SerializeObject(productyCategoryMappingDto);
                StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("api/product_category_mappings", stringContent).Result;
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

        private static void DeleteProductCategoryAsync(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(
                $"api/product_category_mappings/{id.ToString()}").Result;
        }

        private static void SetProductCategory(ProductCategoryMappingDto productyCategoryMappingDto)
        {
            DeleteProductCategoryAsync((int)productyCategoryMappingDto.ProductId);
            CreateProductCategoryAsync(productyCategoryMappingDto);
        }

        private void WriteToFile(string message, LogHelper.Logtype type)
        {

            var _configuration = _service1._configuration;
            var settings = _configuration.GetSection("settings");


            string _logFolderName = settings.GetSection("LogFolderName").Value;
            string _logFileName = settings.GetSection("LogFileName").Value;


            string path = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\" + _logFolderName + "\\" + _logFileName + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            using StreamWriter sw = File.AppendText(filepath);
            sw.WriteLine(message);
        }

    }
}

