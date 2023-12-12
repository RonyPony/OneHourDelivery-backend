using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Api.DTO.Images;
using Nop.Plugin.Api.DTO.ProductCategoryMappings;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.Models.ProductsParameters;
using Nop.Service.AppIPOSSync.Entities;
using Nop.Service.AppIPOSSync.Helpers;
using Nop.Service.AppIPOSSync.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Product = Nop.Service.AppIPOSSync.Entities.Product;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// This class contains all the methods to be able to sync products using controllers of the Nop.Plugin.Api plugin 
    /// </summary>
    public class ProductClient : BaseClient<Product, ProductDto, ProductsRootObjectDto>
    {
        private readonly CategoryClient _categoryClient;
        private readonly ManufacturerClient _manufacturerClient;

        private readonly IConfiguration _configurationSection = ConfigurationHelper.GetConfiguration().GetSection("Settings");

        /// <summary>
        /// Constuctor of the class
        /// <param name="categoryClient">Instance of <see cref="CategoryClient"/></param>
        /// <param name="manufacturerClient">Instance of <see cref="ManufacturerClient"/></param>
        /// <param name="context">Instance of <see cref="AppIposContext"/></param>
        /// </summary>
        public ProductClient(CategoryClient categoryClient, ManufacturerClient manufacturerClient,
            AppIposContext context) : base(
            context, "api/products")
        {
            _categoryClient = categoryClient;
            _manufacturerClient = manufacturerClient;
        }

        private async Task AddProductPicture(Product erpProduct, int nopCommerceProductId)
        {
            var imageMappingDto = new ImageMappingDtoDelta
            {
                image = new ImageMappingDto
                {
                    Attachment = Convert.ToBase64String(erpProduct.Picture),
                    ProductId = nopCommerceProductId,
                    SeoFilename = erpProduct.Name.Trim().ToLower().Replace(" ", "-"),
                    MimeType = "image/jpeg"
                }
            };

            string json = JsonConvert.SerializeObject(imageMappingDto);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync("api/productpictures", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}. Error creating product image with StatusCode {response.StatusCode}");
            }
        }

        protected override async Task<ProductsRootObjectDto> GetAllFromNopCommerceAsync()
        {
            string requestUri = "api/products";
            var parametersModel = new ProductsParametersModel
            {
                Limit = 1000
            };

            requestUri += "?" + GetQueryString(parametersModel);

            HttpResponseMessage response = await Client.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}. {requestUri} call with StatusCode {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();

            ProductsRootObjectDto result = JsonConvert.DeserializeObject<ProductsRootObjectDto>(json);
            return result;
        }

        protected override ProductDto ToDto(Product tableModel)
        {
            List<string> manufacturerNames = _manufacturerClient.GetErpProductManufacturersByProductId(tableModel.Id);

            int? stockQuantity = (int?)GetProductStockByProductId(tableModel.Id);

            var resultProductDto = new ProductDto
            {
                Name = tableModel.Name,
                ShortDescription = tableModel.Comment,
                Sku = tableModel.Code,
                Price = tableModel.Price,
                Published = tableModel.Active,
                ProductCost = tableModel.Cost,
                ManufacturerIds = _manufacturerClient.GetNopCommerceManufacturerIdsByNames(manufacturerNames),
                IsTaxExempt = tableModel.Excent,
                StockQuantity = stockQuantity,
                ManageInventoryMethodId = (int)ManageInventoryMethod.ManageStock,
                DisplayStockAvailability = true,
                DisplayStockQuantity = true,
                MinStockQuantity = 1,
                OrderMinimumQuantity = 1,
                OrderMaximumQuantity = stockQuantity,
                TaxCategoryId = int.Parse(_configurationSection["DefaultTaxCategoryId"]),
            };

            return resultProductDto;
        }

        protected override async Task<ProductDto> UpdateOnNopCommerceAsync(ProductDto product)
        {
            var requestJson = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync("api/UpdateExistProduct", content);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            ProductsRootObjectDto result = JsonConvert.DeserializeObject<ProductsRootObjectDto>(jsonResponse);

            return result?.Products?.FirstOrDefault();
        }

        protected override bool ExistsInNopCommerce(ref ProductsRootObjectDto list, string productSku) =>
            list.Products.Where(item => !string.IsNullOrWhiteSpace(item.Sku))
                .Any(item => item.Sku.Trim() == productSku);

        private int GetProductIdBySku(string sku)
        {
            int result = 0;
            ProductsRootObjectDto list = GetAllFromNopCommerceAsync().Result;
            
            if (list.Products.Any(item => item.Sku?.Trim() == sku.Trim()))
            {
                ProductDto product = list.Products.FirstOrDefault(item => item.Sku?.Trim() == sku.Trim());

                if (product != null)
                    result = product.Id;
            }

            return result;
        }

        /// <summary>
        /// Method for synching Products
        /// </summary>
        public override async Task Sync()
        {
            ProductsRootObjectDto nopCommerceProducts = await GetAllFromNopCommerceAsync();
            List<Product> productList = GetAllFromErp().Where(p => p.Active == true).ToList();

            foreach (var product in productList)
            {
                string categoryName = _categoryClient.GetErpCategoryById(product.ProductCategory).Name;

                if (!ExistsInNopCommerce(ref nopCommerceProducts, product.Code.Trim()))
                {
                    ProductsRootObjectDto createdProduct = await CreateAsync(ToDto(product));

                    int? productId = createdProduct.Products.FirstOrDefault()?.Id;

                    if (product.Picture != default)
                        await AddProductPicture(product, productId.GetValueOrDefault());

                    var mapping = new ProductCategoryMappingDto
                    {
                        ProductId = productId,
                        CategoryId = _categoryClient.GetNopCommerceCategoryIdByName(categoryName)
                    };

                    SetProductCategory(mapping);

                    await CreateNopCommerceErpProductMapping(productId.GetValueOrDefault(), product.Id);
                }
                else
                {
                    ProductDto productDto = ToDto(product);
                    productDto.Id = GetProductIdBySku(productDto.Sku);

                    ProductDto updatedProduct = await UpdateOnNopCommerceAsync(productDto);

                    int? productId = updatedProduct?.Id;

                    if (ShouldAddImage(updatedProduct, product))
                        await AddProductPicture(product, productId.GetValueOrDefault());

                    ProductCategoryMappingDto mapping = new ProductCategoryMappingDto
                    {
                        ProductId = productId,
                        CategoryId = _categoryClient.GetNopCommerceCategoryIdByName(categoryName)
                    };

                    SetProductCategory(mapping);
                }
            }
        }

        private bool ShouldAddImage(ProductDto updatedProduct, Product erpProduct)
        {
            List<ImageMappingDto> imageList = updatedProduct?.Images;

            if (imageList != null)
                return !imageList.Any() && erpProduct.Picture != default;

            return erpProduct.Picture != default;
        }

        private void CreateProductCategoryAsync(ProductCategoryMappingDto productyCategoryMappingDto)
        {
            string json = JsonConvert.SerializeObject(productyCategoryMappingDto);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = Client.PostAsync("api/product_category_mappings", stringContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response}. Error creating categories with StatusCode {response.StatusCode}");
            }
        }

        private void DeleteProductCategoryAsync(int id)
        {
            HttpResponseMessage response = Client.DeleteAsync($"api/product_category_mappings/{id}").Result;
        }

        private void SetProductCategory(ProductCategoryMappingDto productyCategoryMappingDto)
        {
            DeleteProductCategoryAsync(productyCategoryMappingDto.ProductId.GetValueOrDefault());
            CreateProductCategoryAsync(productyCategoryMappingDto);
        }

        private decimal GetProductStockByProductId(int productId)
        {
            int.TryParse(ConfigurationHelper.GetConfiguration().GetSection("Settings")["DefaultWarehouseId"],
                out int defaultWarehouseId);

            WarehouseProduct warehouseProduct = Context.WarehouseProducts
                .FirstOrDefault(whProduct =>
                    whProduct.Product == productId && whProduct.Warehouse == defaultWarehouseId);

            return warehouseProduct?.Quantity ?? decimal.Zero;
        }

        private async Task CreateNopCommerceErpProductMapping(int nopCommerceProductId, int erpProductId)
        {
            HttpResponseMessage response = await Client.PostAsync(
                $"erp/products?ProductId={nopCommerceProductId}&ErpProductId={erpProductId}", new StringContent(""));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"{response}. Error creating nopCommerce and erp product mappings with StatusCode {response.StatusCode}");
            }
        }
    }
}