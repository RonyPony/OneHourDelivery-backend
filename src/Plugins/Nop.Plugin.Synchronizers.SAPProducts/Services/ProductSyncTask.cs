using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Media;
using Nop.Data;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;
using Nop.Plugin.Synchronizers.SAPProducts.Models;
using Nop.Plugin.Synchronizers.SAPProducts.Models.Items;
using Nop.Plugin.Synchronizers.SAPProducts.Models.Items.Groups;
using Nop.Plugin.Synchronizers.SAPProducts.Models.Manufacturers;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Tasks;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.SAPProducts.Services
{
    /// <summary>
    /// Represents the task model class for synchronizing the products from SAP.
    /// </summary>
    public sealed class ProductSyncTask : IScheduleTask
    {

        #region Fields

        private readonly IProductService _productService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductTagService _productTagService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly ILogger _logger;
        private readonly ProductSyncConfigurationSettings _productSyncSettings;
        private readonly IPictureService _pictureService;
        private readonly IRepository<ProductSapItemMapping> _productSapItemRepository;
        private readonly IRepository<CategorySapItemGroupMapping> _categorySapItemGroupRepository;
        private readonly IRepository<ManufacturerSapManufacturerMapping> _manufacturerSapManufacturerRepository;
        private readonly string _loadedFromSapIdentifierMesage = "Loaded from SAP.";

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ProductSyncTask"/>.
        /// </summary>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="urlRecordService">An implementation of <see cref="IUrlRecordService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="productTagService">An implementation of <see cref="IProductTagService"/>.</param>
        /// <param name="localizedEntityService">An implementation of <see cref="ILocalizedEntityService"/>.</param>
        /// <param name="categoryService">An implementation of <see cref="ICategoryService"/>.</param>
        /// <param name="manufacturerService">An implementation of <see cref="IManufacturerService"/>.</param>
        /// <param name="aclService">An implementation of <see cref="IAclService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="discountService">An implementation of <see cref="IDiscountService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="productSyncSettings">An instance of <see cref="ProductSyncConfigurationSettings"/>.</param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/>.</param>
        /// <param name="productSapItemRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is an instance of <see cref="ProductSapItemMapping"/>.</param>
        /// <param name="categorySapItemGroupRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is an instance of <see cref="CategorySapItemGroupMapping"/>.</param>
        /// <param name="manufacturerSapManufacturerRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is an instance of <see cref="ManufacturerSapManufacturerMapping"/>.</param>
        public ProductSyncTask(IProductService productService,
            IUrlRecordService urlRecordService,
            ILocalizationService localizationService,
            IProductTagService productTagService,
            ILocalizedEntityService localizedEntityService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IAclService aclService,
            ICustomerService customerService,
            IDiscountService discountService,
            ILogger logger,
            ProductSyncConfigurationSettings productSyncSettings,
            IPictureService pictureService,
            IRepository<ProductSapItemMapping> productSapItemRepository,
            IRepository<CategorySapItemGroupMapping> categorySapItemGroupRepository,
            IRepository<ManufacturerSapManufacturerMapping> manufacturerSapManufacturerRepository)
        {
            _productService = productService;
            _urlRecordService = urlRecordService;
            _localizationService = localizationService;
            _productTagService = productTagService;
            _localizedEntityService = localizedEntityService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _aclService = aclService;
            _customerService = customerService;
            _discountService = discountService;
            _logger = logger;
            _productSyncSettings = productSyncSettings;
            _pictureService = pictureService;
            _productSapItemRepository = productSapItemRepository;
            _categorySapItemGroupRepository = categorySapItemGroupRepository;
            _manufacturerSapManufacturerRepository = manufacturerSapManufacturerRepository;
            GetItemGroupsFromSapApi();
            GetManufacturersFromSapApi();
        }

        #endregion

        #region Methods

        private void GetItemGroupsFromSapApi()
        {
            if (!_productSyncSettings.SyncCategories)
                return;

            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_productSyncSettings.AuthenticationHeaderScheme, _productSyncSettings.AuthenticationHeaderParameter)
                }
            };

            Task<HttpResponseMessage> response = client.GetAsync(_productSyncSettings.SapCategoryUrl);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.GettingCategoriesFromSap")} Status Code: {response.Result.StatusCode}");
            }

            Task<string> json = response.Result.Content.ReadAsStringAsync();
            json.Wait();
            var itemGroupsResponse = JsonConvert.DeserializeObject<SapItemGroupResponse>(json.Result);

            System.Threading.Tasks.Task initSapItemsGroupsTask = CreateCategoriesFromItemsGroups(itemGroupsResponse.Extra);
            initSapItemsGroupsTask.Wait();
        }

        private System.Threading.Tasks.Task CreateCategoriesFromItemsGroups(List<SapItemGroupModel> sapItemGroups)
        {
            try
            {
                foreach (SapItemGroupModel sapItemGroup in sapItemGroups)
                {
                    CategorySapItemGroupMapping foundCategorySapItemGroup = _categorySapItemGroupRepository.Table.FirstOrDefault(itemGroup => itemGroup.ItemGroupNumber == sapItemGroup.Number);

                    if (foundCategorySapItemGroup != null)
                    {
                        Category foundCategory = _categoryService.GetCategoryById(foundCategorySapItemGroup.CategoryId);

                        if (foundCategory != null)
                        {
                            foundCategory.Name = sapItemGroup.GroupName;
                            foundCategory.UpdatedOnUtc = DateTime.UtcNow;

                            _categoryService.UpdateCategory(foundCategory);
                        }
                    }
                    else
                    {
                        var defaultCategoryTemplateId = 1;
                        var defaultParentCategoryId = 0;
                        var defaultPictureId = 0;
                        var defaultPageSize = 6;
                        var defaultDisplayOrder = 1;

                        var newCategory = new Category
                        {
                            Name = sapItemGroup.GroupName,
                            Description = _loadedFromSapIdentifierMesage,
                            CategoryTemplateId = defaultCategoryTemplateId,
                            ParentCategoryId = defaultParentCategoryId,
                            PictureId = defaultPictureId,
                            PageSize = defaultPageSize,
                            AllowCustomersToSelectPageSize = true,
                            ShowOnHomepage = false,
                            IncludeInTopMenu = false,
                            SubjectToAcl = false,
                            LimitedToStores = false,
                            Published = true,
                            Deleted = false,
                            DisplayOrder = defaultDisplayOrder,
                            CreatedOnUtc = DateTime.UtcNow,
                            UpdatedOnUtc = DateTime.UtcNow
                        };

                        _categoryService.InsertCategory(newCategory);

                        var newCategorySapItemGroup = new CategorySapItemGroupMapping
                        {
                            CategoryId = newCategory.Id,
                            ItemGroupNumber = sapItemGroup.Number
                        };

                        _categorySapItemGroupRepository.Insert(newCategorySapItemGroup);
                    }
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.Error($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.CategoriesSync")} {e.Message}", e);
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.CategoriesSync")} {e.Message}", e);
            }
        }

        private void GetManufacturersFromSapApi()
        {
            if (!_productSyncSettings.SyncManufacturers)
                return;

            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_productSyncSettings.AuthenticationHeaderScheme, _productSyncSettings.AuthenticationHeaderParameter)
                }
            };

            Task<HttpResponseMessage> response = client.GetAsync(_productSyncSettings.SapManufacturerUrl);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.GettingManufacturersFromSap")} Status Code: {response.Result.StatusCode}");
            }

            Task<string> json = response.Result.Content.ReadAsStringAsync();
            json.Wait();
            var itemGroupsResponse = JsonConvert.DeserializeObject<SapManufacturerResponse>(json.Result);

            System.Threading.Tasks.Task initSapManufacturersTask = CreateManufacturersFromSapManufacturers(itemGroupsResponse.Extra);
            initSapManufacturersTask.Wait();
        }

        private System.Threading.Tasks.Task CreateManufacturersFromSapManufacturers(List<SapManufacturerModel> sapManufacturers)
        {
            try
            {
                foreach (SapManufacturerModel sapManufacturer in sapManufacturers)
                {
                    ManufacturerSapManufacturerMapping manufacturerSapManufacturer =
                        _manufacturerSapManufacturerRepository.Table.Where(manufacturer =>
                        manufacturer.SapManufacturerCode == sapManufacturer.Code).FirstOrDefault();

                    if (manufacturerSapManufacturer != null)
                    {
                        Manufacturer foundManufacturer = _manufacturerService.GetManufacturerById(manufacturerSapManufacturer.ManufacturerId);

                        if (foundManufacturer != null)
                        {
                            foundManufacturer.Name = sapManufacturer.ManufacturerName;
                            foundManufacturer.UpdatedOnUtc = DateTime.UtcNow;

                            _manufacturerService.UpdateManufacturer(foundManufacturer);
                        }
                    }
                    else
                    {
                        var defaultManufacturerTemplateId = 1;
                        var defaultPictureId = 0;
                        var defaultPageSize = 6;
                        var defaultDisplayOrder = 1;

                        var newManufacturer = new Manufacturer
                        {
                            Name = sapManufacturer.ManufacturerName,
                            Description = _loadedFromSapIdentifierMesage,
                            ManufacturerTemplateId = defaultManufacturerTemplateId,
                            PictureId = defaultPictureId,
                            PageSize = defaultPageSize,
                            AllowCustomersToSelectPageSize = true,
                            SubjectToAcl = false,
                            LimitedToStores = false,
                            Published = true,
                            Deleted = false,
                            DisplayOrder = defaultDisplayOrder,
                            CreatedOnUtc = DateTime.UtcNow,
                            UpdatedOnUtc = DateTime.UtcNow
                        };

                        _manufacturerService.InsertManufacturer(newManufacturer);

                        var newManufacturerSapManufacturer = new ManufacturerSapManufacturerMapping
                        {
                            ManufacturerId = newManufacturer.Id,
                            SapManufacturerCode = sapManufacturer.Code
                        };

                        _manufacturerSapManufacturerRepository.Insert(newManufacturerSapManufacturer);
                    }
                }

                return System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.Error($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.ManufacturersSync")} {e.Message}", e);
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.ManufacturersSync")} {e.Message}", e);
            }
        }

        /// <summary>
        /// Executes the task of synchronizing the products from SAP.
        /// </summary>
        public void Execute()
        {
            if (string.IsNullOrWhiteSpace(_productSyncSettings.AuthenticationHeaderParameter)
                || string.IsNullOrWhiteSpace(_productSyncSettings.SapProductUrl)
                || string.IsNullOrWhiteSpace(_productSyncSettings.AuthenticationHeaderScheme))
                throw new NopException(_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.PluginNotConfigured"));

            if (_productSyncSettings.SyncCategories && string.IsNullOrWhiteSpace(_productSyncSettings.SapCategoryUrl))
                throw new NopException(_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.PluginNotConfigured"));

            if (_productSyncSettings.SyncManufacturers && string.IsNullOrWhiteSpace(_productSyncSettings.SapManufacturerUrl))
                throw new NopException(_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.PluginNotConfigured"));

            try
            {
                Task<List<ProductModel>> productsTask = GetProductsFromApi();
                productsTask.Wait();
                List<ProductModel> products = productsTask.Result;

                foreach (ProductModel model in products)
                {
                    var product = model.ToEntity<Product>();

                    if (ProductAlreadyExists(product.Sku, out int productId) && _productSyncSettings.UpdateProductInformation)
                    {
                        product.Id = productId;
                        product.Sku = _productSyncSettings.AssignItemCodeFieldAsSku ? product.Sku : string.Empty;
                        product.UpdatedOnUtc = DateTime.UtcNow;

                        _productService.UpdateProduct(product);

                        AddProductAdditionalInfo(model, product);
                    }
                    else
                    {
                        string sapItemCode = product.Sku;
                        product.Sku = _productSyncSettings.AssignItemCodeFieldAsSku ? product.Sku : string.Empty;
                        product.CreatedOnUtc = DateTime.UtcNow;
                        product.UpdatedOnUtc = DateTime.UtcNow;

                        _productService.InsertProduct(product);

                        var productSapItem = new ProductSapItemMapping
                        {
                            ProductId = product.Id,
                            SapItemCode = sapItemCode
                        };

                        _productSapItemRepository.Insert(productSapItem);

                        AddProductAdditionalInfo(model, product);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.ProductsSync")} {e.Message}", e);
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.ProductsSync")} {e.Message}", e);
            }
        }

        private void AddProductAdditionalInfo(ProductModel model, Product product)
        {
            //images
            AddProductImages(model, product.Id);

            //search engine name
            model.SeName = _urlRecordService.ValidateSeName(product, model.SeName, product.Name, true);
            _urlRecordService.SaveSlug(product, model.SeName, 0);

            //locales
            UpdateLocales(product, model);

            //categories
            SaveCategoryMappings(product, model);

            //manufacturers
            SaveManufacturerMappings(product, model);

            //ACL (customer roles)
            SaveProductAcl(product, model);

            //stores
            _productService.UpdateProductStoreMappings(product, model.SelectedStoreIds);

            //discounts
            SaveDiscountMappings(product, model);

            //tags
            _productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));

            //quantity change history
            _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity,
                product.WarehouseId,
                _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));
        }

        private async Task<List<ProductModel>> GetProductsFromApi()
        {
            using HttpClient client = new HttpClient
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue(_productSyncSettings.AuthenticationHeaderScheme,
                    _productSyncSettings.AuthenticationHeaderParameter)
                }
            };

            HttpResponseMessage response = await client.GetAsync(_productSyncSettings.SapProductUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new NopException($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.GettingProductsFromSap")} Status code: {response.StatusCode}");
            }

            string json = await response.Content.ReadAsStringAsync();
            var productsResponse = JsonConvert.DeserializeObject<SapItemResponse>(json);

            var products = new List<ProductModel>();

            foreach (SapItemModel model in productsResponse.Extra)
            {
                try
                {
                    products.Add(new ProductModel
                    {
                        Name = model.ItemName,
                        // Meanwhile, we save ItemCode as Sku only not to lose that data.
                        // This is saved correctly later on, while creating the product.
                        Sku = model.ItemCode,
                        Price = GetMainPriceFromItemPriceList(model.ItemPrices),
                        ShortDescription = _loadedFromSapIdentifierMesage,
                        Published = true,
                        VisibleIndividually = true,
                        ManageInventoryMethodId = 1,
                        ProductTypeId = (int)ProductType.SimpleProduct,
                        OrderMinimumQuantity = 1,
                        OrderMaximumQuantity = (int)model.TotalInStock.GetValueOrDefault(),
                        Length = model.SalesUnitLength.GetValueOrDefault(),
                        Width = model.SalesUnitWidth.GetValueOrDefault(),
                        Height = model.SalesUnitHeight.GetValueOrDefault(),
                        Weight = model.SalesUnitWeight.GetValueOrDefault(),
                        StockQuantity = (int)model.TotalInStock.GetValueOrDefault(),
                        AddPictureModel = new ProductPictureModel
                        {
                            PictureUrl = $"{model.PicturePath}{model.PictureName}"
                        },
                        IsShipEnabled = true,
                        SelectedCategoryIds = GetProductCategoriesIdsByItemsGroupCode(model.ItemsGroupCode)
                    });
                }
                catch (Exception e)
                {
                    _logger.Error($"{_localizationService.GetResource("Plugins.Synchronizers.SAPProducts.Exceptions.ProductsSync")} {e.Message}", e);
                }
            }

            return products;
        }

        private decimal GetMainPriceFromItemPriceList(List<ItemPrice> itemPrices)
        {
            decimal mainPriceFactor = 1.0M;
            int firtsPriceListId = 1;
            ItemPrice mainPriceList = itemPrices.Where(price => price.Factor == mainPriceFactor
                                                                && price.BasePriceList == firtsPriceListId
                                                                && price.PriceList == firtsPriceListId)
                                                .FirstOrDefault();

            return mainPriceList != null ? mainPriceList.Price.GetValueOrDefault() : 0.0M;
        }

        private List<int> GetProductCategoriesIdsByItemsGroupCode(string itemsGroupCode)
        {
            var categoriesIds = new List<int>();

            if (!_productSyncSettings.SyncCategories)
                return categoriesIds;

            bool result = int.TryParse(itemsGroupCode, out int itemGroupNumber);

            if (result)
            {
                CategorySapItemGroupMapping foundCategorySapItemGroup = _categorySapItemGroupRepository.Table.Where(itemGroup => itemGroup.ItemGroupNumber == itemGroupNumber).FirstOrDefault();

                if (foundCategorySapItemGroup != null)
                    categoriesIds.Add(foundCategorySapItemGroup.CategoryId);
            }

            return categoriesIds;
        }

        private bool ProductAlreadyExists(string itemCode, out int productId)
        {
            ProductSapItemMapping productSapItem = _productSapItemRepository.Table
                .Where(product => product.SapItemCode == itemCode).FirstOrDefault();

            productId = productSapItem == null ? 0 : productSapItem.ProductId;

            return productSapItem != null;
        }

        private void UpdateLocales(Product product, ProductModel model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.Name,
                    localized.Name,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.ShortDescription,
                    localized.ShortDescription,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.FullDescription,
                    localized.FullDescription,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.MetaKeywords,
                    localized.MetaKeywords,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.MetaDescription,
                    localized.MetaDescription,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(product,
                    x => x.MetaTitle,
                    localized.MetaTitle,
                    localized.LanguageId);

                //search engine name
                var seName = _urlRecordService.ValidateSeName(product, localized.SeName, localized.Name, false);
                _urlRecordService.SaveSlug(product, seName, localized.LanguageId);
            }
        }

        private void SaveCategoryMappings(Product product, ProductModel model)
        {
            var existingProductCategories = _categoryService.GetProductCategoriesByProductId(product.Id, true);

            foreach (var categoryId in model.SelectedCategoryIds)
            {
                if (_categoryService.FindProductCategory(existingProductCategories, product.Id, categoryId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingCategoryMapping =
                        _categoryService.GetProductCategoriesByCategoryId(categoryId, showHidden: true);
                    if (existingCategoryMapping.Any())
                        displayOrder = existingCategoryMapping.Max(x => x.DisplayOrder) + 1;
                    _categoryService.InsertProductCategory(new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                        DisplayOrder = displayOrder
                    });
                }
            }
        }

        private void SaveManufacturerMappings(Product product, ProductModel model)
        {
            var existingProductManufacturers =
                _manufacturerService.GetProductManufacturersByProductId(product.Id, true);

            //delete manufacturers
            foreach (var existingProductManufacturer in existingProductManufacturers)
                if (!model.SelectedManufacturerIds.Contains(existingProductManufacturer.ManufacturerId))
                    _manufacturerService.DeleteProductManufacturer(existingProductManufacturer);

            //add manufacturers
            foreach (var manufacturerId in model.SelectedManufacturerIds)
            {
                if (_manufacturerService.FindProductManufacturer(existingProductManufacturers, product.Id,
                        manufacturerId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingManufacturerMapping =
                        _manufacturerService.GetProductManufacturersByManufacturerId(manufacturerId, showHidden: true);
                    if (existingManufacturerMapping.Any())
                        displayOrder = existingManufacturerMapping.Max(x => x.DisplayOrder) + 1;
                    _manufacturerService.InsertProductManufacturer(new ProductManufacturer
                    {
                        ProductId = product.Id,
                        ManufacturerId = manufacturerId,
                        DisplayOrder = displayOrder
                    });
                }
            }
        }

        private void SaveProductAcl(Product product, ProductModel model)
        {
            product.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            _productService.UpdateProduct(product);

            var existingAclRecords = _aclService.GetAclRecords(product);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(product, customerRole.Id);
                }
                else
                {
                    //remove role
                    var aclRecordToDelete =
                        existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        _aclService.DeleteAclRecord(aclRecordToDelete);
                }
            }
        }

        private void SaveDiscountMappings(Product product, ProductModel model)
        {
            var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);

            foreach (var discount in allDiscounts)
            {
                if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                {
                    //new discount
                    if (_productService.GetDiscountAppliedToProduct(product.Id, discount.Id) is null)
                        _productService.InsertDiscountProductMapping(new DiscountProductMapping
                        { EntityId = product.Id, DiscountId = discount.Id });
                }
                else
                {
                    //remove discount
                    if (_productService.GetDiscountAppliedToProduct(product.Id, discount.Id) is DiscountProductMapping
                        discountProductMapping)
                        _productService.DeleteDiscountProductMapping(discountProductMapping);
                }
            }

            _productService.UpdateProduct(product);
            _productService.UpdateHasDiscountsApplied(product);
        }

        private string[] ParseProductTags(string productTags)
        {
            var result = new List<string>();
            if (string.IsNullOrWhiteSpace(productTags))
                return result.ToArray();

            var values = productTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var val in values)
                if (!string.IsNullOrEmpty(val.Trim()))
                    result.Add(val.Trim());

            return result.ToArray();
        }

        private void AddProductImages(ProductModel model, int productId)
        {
            byte[] imageBytes = null;
            string picturePath = model.AddPictureModel.PictureUrl;

            if (!string.IsNullOrWhiteSpace(picturePath) && File.Exists(picturePath))
            {
                imageBytes = File.ReadAllBytes(picturePath);
            }

            if (imageBytes == null)
                return;

            string imageMimeType = GetImageMimeType(picturePath);

            Picture picture = _pictureService.InsertPicture(imageBytes, imageMimeType, "");

            _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(model.Name));

            _productService.InsertProductPicture(new ProductPicture
            {
                PictureId = picture.Id,
                ProductId = productId,
                DisplayOrder = 1
            });
        }

        private string GetImageMimeType(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            switch (fileExtension)
            {
                case ".bmp":
                    return MimeTypes.ImageBmp;
                case ".gif":
                    return MimeTypes.ImageGif;
                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".jfif":
                case ".pjpeg":
                case ".pjp":
                    return MimeTypes.ImageJpeg;
                case ".png":
                    return MimeTypes.ImagePng;
                case ".tiff":
                case ".tif":
                    return MimeTypes.ImageTiff;
            }

            return "";
        }

        #endregion
    }
}