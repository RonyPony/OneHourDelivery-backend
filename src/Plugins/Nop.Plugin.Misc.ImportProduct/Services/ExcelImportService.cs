using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Core.Http;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Plugin.Misc.ImportProduct.Models;
using Nop.Services;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.ExportImport;
using Nop.Services.ExportImport.Help;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Date;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using OfficeOpenXml;
using static Nop.Services.ExportImport.ImportManager;
using ImportProductMetadata = Nop.Plugin.Misc.ImportProduct.Models.ImportProductMetadata;

namespace Nop.Plugin.Misc.ImportProduct.Services
{
    /// <summary>
    /// Represents the service model class for import the products and images.
    /// </summary>
    public class ExcelImportService
    {
        #region Constants
        //it's quite fast hash (to cheaply distinguish between objects)
        private const string IMAGE_HASH_ALGORITHM = "SHA512";

        private const string UPLOADS_TEMP_PATH = "~/wwwroot/TempUploads";
        #endregion

        #region Fields
        private readonly CatalogSettings _catalogSettings;
        private readonly ICategoryService _categoryService;
        private readonly ICountryService _countryService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly INopDataProvider _dataProvider;
        private readonly IDateRangeService _dateRangeService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMeasureService _measureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly INopFileProvider _fileProvider;
        private readonly IPictureService _pictureService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IProductTagService _productTagService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IShippingService _shippingService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly ITaxCategoryService _taxCategoryService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly VendorSettings _vendorSettings;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="ProductSyncTask"/>.
        /// </summary>
        public ExcelImportService(CatalogSettings catalogSettings,
            ICategoryService categoryService,
            ICountryService countryService,
            ICustomerActivityService customerActivityService,
            INopDataProvider dataProvider,
            IDateRangeService dateRangeService,
            IHttpClientFactory httpClientFactory,
            ILocalizationService localizationService,
            ILogger logger,
            IManufacturerService manufacturerService,
            IMeasureService measureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            INopFileProvider fileProvider,
            IPictureService pictureService,
            IProductAttributeService productAttributeService,
            IProductService productService,
            IProductTagService productTagService,
            IProductTemplateService productTemplateService,
            IServiceScopeFactory serviceScopeFactory,
            IShippingService shippingService,
            ISpecificationAttributeService specificationAttributeService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            ITaxCategoryService taxCategoryService,
            IUrlRecordService urlRecordService,
            IVendorService vendorService,
            IWorkContext workContext,
            MediaSettings mediaSettings,
            VendorSettings vendorSettings)
        {
            _catalogSettings = catalogSettings;
            _categoryService = categoryService;
            _countryService = countryService;
            _customerActivityService = customerActivityService;
            _dataProvider = dataProvider;
            _dateRangeService = dateRangeService;
            _httpClientFactory = httpClientFactory;
            _fileProvider = fileProvider;
            _localizationService = localizationService;
            _logger = logger;
            _manufacturerService = manufacturerService;
            _measureService = measureService;
            _newsLetterSubscriptionService = newsLetterSubscriptionService;
            _pictureService = pictureService;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _productTagService = productTagService;
            _productTemplateService = productTemplateService;
            _serviceScopeFactory = serviceScopeFactory;
            _shippingService = shippingService;
            _specificationAttributeService = specificationAttributeService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _taxCategoryService = taxCategoryService;
            _urlRecordService = urlRecordService;
            _vendorService = vendorService;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _vendorSettings = vendorSettings;
        }

        #endregion

        /// <summary>
        /// All the method that use for Download Images and Import the products.
        /// </summary>
        public virtual void ImportProductsFromXlsx(Stream stream)
        {
            using var xlPackage = new ExcelPackage(stream);
            // get the first worksheet in the workbook
            var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new NopException("No worksheet found");

            var downloadedFiles = new List<string>();
            var metadata = PrepareImportProductData(worksheet);

            if (_catalogSettings.ExportImportSplitProductsFile && metadata.CountProductsInFile > _catalogSettings.ExportImportProductsCountInOneFile)
            {
                ImportProductsFromSplitedXlsx(worksheet, metadata);
                return;
            }

            //performance optimization, load all products by SKU in one SQL request
            var allProductsBySku = _productService.GetProductsBySku(metadata.AllSku.ToArray(), _workContext.CurrentVendor?.Id ?? 0);

            //validate maximum number of products per vendor
            if (_vendorSettings.MaximumProductNumber > 0 &&
                _workContext.CurrentVendor != null)
            {
                var newProductsCount = metadata.CountProductsInFile - allProductsBySku.Count;
                if (_productService.GetNumberOfProductsByVendorId(_workContext.CurrentVendor.Id) + newProductsCount > _vendorSettings.MaximumProductNumber)
                    throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.ExceededMaximumNumber"), _vendorSettings.MaximumProductNumber));
            }

            //performance optimization, load all categories IDs for products in one SQL request
            var allProductsCategoryIds = _categoryService.GetProductCategoryIds(allProductsBySku.Select(p => p.Id).ToArray());

            //performance optimization, load all categories in one SQL request
            Dictionary<CategoryKey, Category> allCategories;
            try
            {
                var allCategoryList = _categoryService.GetAllCategories(showHidden: true);

                allCategories = allCategoryList
                    .ToDictionary(c => new CategoryKey(c, _categoryService, allCategoryList, _storeMappingService), c => c);
            }
            catch (ArgumentException)
            {
                //categories with the same name are not supported in the same category level
                throw new ArgumentException(_localizationService.GetResource("Admin.Catalog.Products.Import.CategoriesWithSameNameNotSupported"));
            }

            //performance optimization, load all manufacturers IDs for products in one SQL request
            var allProductsManufacturerIds = _manufacturerService.GetProductManufacturerIds(allProductsBySku.Select(p => p.Id).ToArray());

            //performance optimization, load all manufacturers in one SQL request
            var allManufacturers = _manufacturerService.GetAllManufacturers(showHidden: true);

            //performance optimization, load all stores in one SQL request
            var allStores = _storeService.GetAllStores();

            //product to import images
            var productPictureMetadata = new List<ProductPictureMetadata>();

            Product lastLoadedProduct = null;
            var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;

            for (var iRow = 2; iRow < metadata.EndRow; iRow++)
            {
                //imports product attributes
                if (worksheet.Row(iRow).OutlineLevel != 0)
                {
                    if (lastLoadedProduct == null)
                        continue;

                    var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, metadata.ProductAttributeManager, metadata.SpecificationAttributeManager, iRow);

                    //skip caption row
                    if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified &&
                        newTypeOfExportedAttribute != typeOfExportedAttribute)
                    {
                        typeOfExportedAttribute = newTypeOfExportedAttribute;
                        continue;
                    }

                    switch (typeOfExportedAttribute)
                    {
                        case ExportedAttributeType.ProductAttribute:
                            ImportProductAttribute(metadata.ProductAttributeManager, lastLoadedProduct);
                            break;
                        case ExportedAttributeType.SpecificationAttribute:
                            ImportSpecificationAttribute(metadata.SpecificationAttributeManager, lastLoadedProduct);
                            break;
                        case ExportedAttributeType.NotSpecified:
                        default:
                            continue;
                    }

                    continue;
                }

                metadata.Manager.ReadFromXlsx(worksheet, iRow);

                var product = metadata.SkuCellNum > 0 ? allProductsBySku.FirstOrDefault(p => p.Sku == metadata.Manager.GetProperty("SKU").StringValue) : null;
                var isNew = product == null;

                product ??= new Product();

                //some of previous values
                var previousStockQuantity = product.StockQuantity;
                var previousWarehouseId = product.WarehouseId;

                if (isNew)
                    product.CreatedOnUtc = DateTime.UtcNow;

                foreach (var property in metadata.Manager.GetProperties)
                {
                    switch (property.PropertyName)
                    {
                        case "ProductType":
                            product.ProductTypeId = property.IntValue;
                            break;
                        case "ParentGroupedProductId":
                            product.ParentGroupedProductId = property.IntValue;
                            break;
                        case "VisibleIndividually":
                            product.VisibleIndividually = property.BooleanValue;
                            break;
                        case "Name":
                            product.Name = property.StringValue;
                            break;
                        case "ShortDescription":
                            product.ShortDescription = property.StringValue;
                            break;
                        case "FullDescription":
                            product.FullDescription = property.StringValue;
                            break;
                        case "Vendor":
                            //vendor can't change this field
                            if (_workContext.CurrentVendor == null)
                                product.VendorId = property.IntValue;
                            break;
                        case "ProductTemplate":
                            product.ProductTemplateId = property.IntValue;
                            break;
                        case "ShowOnHomepage":
                            //vendor can't change this field
                            if (_workContext.CurrentVendor == null)
                                product.ShowOnHomepage = property.BooleanValue;
                            break;
                        case "DisplayOrder":
                            //vendor can't change this field
                            if (_workContext.CurrentVendor == null)
                                product.DisplayOrder = property.IntValue;
                            break;
                        case "MetaKeywords":
                            product.MetaKeywords = property.StringValue;
                            break;
                        case "MetaDescription":
                            product.MetaDescription = property.StringValue;
                            break;
                        case "MetaTitle":
                            product.MetaTitle = property.StringValue;
                            break;
                        case "AllowCustomerReviews":
                            product.AllowCustomerReviews = property.BooleanValue;
                            break;
                        case "Published":
                            product.Published = property.BooleanValue;
                            break;
                        case "SKU":
                            product.Sku = property.StringValue;
                            break;
                        case "ManufacturerPartNumber":
                            product.ManufacturerPartNumber = property.StringValue;
                            break;
                        case "Gtin":
                            product.Gtin = property.StringValue;
                            break;
                        case "IsGiftCard":
                            product.IsGiftCard = property.BooleanValue;
                            break;
                        case "GiftCardType":
                            product.GiftCardTypeId = property.IntValue;
                            break;
                        case "OverriddenGiftCardAmount":
                            product.OverriddenGiftCardAmount = property.DecimalValue;
                            break;
                        case "RequireOtherProducts":
                            product.RequireOtherProducts = property.BooleanValue;
                            break;
                        case "RequiredProductIds":
                            product.RequiredProductIds = property.StringValue;
                            break;
                        case "AutomaticallyAddRequiredProducts":
                            product.AutomaticallyAddRequiredProducts = property.BooleanValue;
                            break;
                        case "IsDownload":
                            product.IsDownload = property.BooleanValue;
                            break;
                        case "DownloadId":
                            product.DownloadId = property.IntValue;
                            break;
                        case "UnlimitedDownloads":
                            product.UnlimitedDownloads = property.BooleanValue;
                            break;
                        case "MaxNumberOfDownloads":
                            product.MaxNumberOfDownloads = property.IntValue;
                            break;
                        case "DownloadActivationType":
                            product.DownloadActivationTypeId = property.IntValue;
                            break;
                        case "HasSampleDownload":
                            product.HasSampleDownload = property.BooleanValue;
                            break;
                        case "SampleDownloadId":
                            product.SampleDownloadId = property.IntValue;
                            break;
                        case "HasUserAgreement":
                            product.HasUserAgreement = property.BooleanValue;
                            break;
                        case "UserAgreementText":
                            product.UserAgreementText = property.StringValue;
                            break;
                        case "IsRecurring":
                            product.IsRecurring = property.BooleanValue;
                            break;
                        case "RecurringCycleLength":
                            product.RecurringCycleLength = property.IntValue;
                            break;
                        case "RecurringCyclePeriod":
                            product.RecurringCyclePeriodId = property.IntValue;
                            break;
                        case "RecurringTotalCycles":
                            product.RecurringTotalCycles = property.IntValue;
                            break;
                        case "IsRental":
                            product.IsRental = property.BooleanValue;
                            break;
                        case "RentalPriceLength":
                            product.RentalPriceLength = property.IntValue;
                            break;
                        case "RentalPricePeriod":
                            product.RentalPricePeriodId = property.IntValue;
                            break;
                        case "IsShipEnabled":
                            product.IsShipEnabled = property.BooleanValue;
                            break;
                        case "IsFreeShipping":
                            product.IsFreeShipping = property.BooleanValue;
                            break;
                        case "ShipSeparately":
                            product.ShipSeparately = property.BooleanValue;
                            break;
                        case "AdditionalShippingCharge":
                            product.AdditionalShippingCharge = property.DecimalValue;
                            break;
                        case "DeliveryDate":
                            product.DeliveryDateId = property.IntValue;
                            break;
                        case "IsTaxExempt":
                            product.IsTaxExempt = property.BooleanValue;
                            break;
                        case "TaxCategory":
                            product.TaxCategoryId = property.IntValue;
                            break;
                        case "IsTelecommunicationsOrBroadcastingOrElectronicServices":
                            product.IsTelecommunicationsOrBroadcastingOrElectronicServices = property.BooleanValue;
                            break;
                        case "ManageInventoryMethod":
                            product.ManageInventoryMethodId = property.IntValue;
                            break;
                        case "ProductAvailabilityRange":
                            product.ProductAvailabilityRangeId = property.IntValue;
                            break;
                        case "UseMultipleWarehouses":
                            product.UseMultipleWarehouses = property.BooleanValue;
                            break;
                        case "WarehouseId":
                            product.WarehouseId = property.IntValue;
                            break;
                        case "StockQuantity":
                            product.StockQuantity = property.IntValue;
                            break;
                        case "DisplayStockAvailability":
                            product.DisplayStockAvailability = property.BooleanValue;
                            break;
                        case "DisplayStockQuantity":
                            product.DisplayStockQuantity = property.BooleanValue;
                            break;
                        case "MinStockQuantity":
                            product.MinStockQuantity = property.IntValue;
                            break;
                        case "LowStockActivity":
                            product.LowStockActivityId = property.IntValue;
                            break;
                        case "NotifyAdminForQuantityBelow":
                            product.NotifyAdminForQuantityBelow = property.IntValue;
                            break;
                        case "BackorderMode":
                            product.BackorderModeId = property.IntValue;
                            break;
                        case "AllowBackInStockSubscriptions":
                            product.AllowBackInStockSubscriptions = property.BooleanValue;
                            break;
                        case "OrderMinimumQuantity":
                            product.OrderMinimumQuantity = property.IntValue;
                            break;
                        case "OrderMaximumQuantity":
                            product.OrderMaximumQuantity = property.IntValue;
                            break;
                        case "AllowedQuantities":
                            product.AllowedQuantities = property.StringValue;
                            break;
                        case "AllowAddingOnlyExistingAttributeCombinations":
                            product.AllowAddingOnlyExistingAttributeCombinations = property.BooleanValue;
                            break;
                        case "NotReturnable":
                            product.NotReturnable = property.BooleanValue;
                            break;
                        case "DisableBuyButton":
                            product.DisableBuyButton = property.BooleanValue;
                            break;
                        case "DisableWishlistButton":
                            product.DisableWishlistButton = property.BooleanValue;
                            break;
                        case "AvailableForPreOrder":
                            product.AvailableForPreOrder = property.BooleanValue;
                            break;
                        case "PreOrderAvailabilityStartDateTimeUtc":
                            product.PreOrderAvailabilityStartDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "CallForPrice":
                            product.CallForPrice = property.BooleanValue;
                            break;
                        case "Price":
                            product.Price = property.DecimalValue;
                            break;
                        case "OldPrice":
                            product.OldPrice = property.DecimalValue;
                            break;
                        case "ProductCost":
                            product.ProductCost = property.DecimalValue;
                            break;
                        case "CustomerEntersPrice":
                            product.CustomerEntersPrice = property.BooleanValue;
                            break;
                        case "MinimumCustomerEnteredPrice":
                            product.MinimumCustomerEnteredPrice = property.DecimalValue;
                            break;
                        case "MaximumCustomerEnteredPrice":
                            product.MaximumCustomerEnteredPrice = property.DecimalValue;
                            break;
                        case "BasepriceEnabled":
                            product.BasepriceEnabled = property.BooleanValue;
                            break;
                        case "BasepriceAmount":
                            product.BasepriceAmount = property.DecimalValue;
                            break;
                        case "BasepriceUnit":
                            product.BasepriceUnitId = property.IntValue;
                            break;
                        case "BasepriceBaseAmount":
                            product.BasepriceBaseAmount = property.DecimalValue;
                            break;
                        case "BasepriceBaseUnit":
                            product.BasepriceBaseUnitId = property.IntValue;
                            break;
                        case "MarkAsNew":
                            product.MarkAsNew = property.BooleanValue;
                            break;
                        case "MarkAsNewStartDateTimeUtc":
                            product.MarkAsNewStartDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "MarkAsNewEndDateTimeUtc":
                            product.MarkAsNewEndDateTimeUtc = property.DateTimeNullable;
                            break;
                        case "Weight":
                            product.Weight = property.DecimalValue;
                            break;
                        case "Length":
                            product.Length = property.DecimalValue;
                            break;
                        case "Width":
                            product.Width = property.DecimalValue;
                            break;
                        case "Height":
                            product.Height = property.DecimalValue;
                            break;
                        case "IsLimitedToStores":
                            product.LimitedToStores = property.BooleanValue;
                            break;
                    }
                }

                //set some default values if not specified
                if (isNew && metadata.Properties.All(p => p.PropertyName != "ProductType"))
                    product.ProductType = ProductType.SimpleProduct;

                if (isNew && metadata.Properties.All(p => p.PropertyName != "VisibleIndividually"))
                    product.VisibleIndividually = true;

                if (isNew && metadata.Properties.All(p => p.PropertyName != "Published"))
                    product.Published = true;

                //sets the current vendor for the new product
                if (isNew && _workContext.CurrentVendor != null)
                    product.VendorId = _workContext.CurrentVendor.Id;

                product.UpdatedOnUtc = DateTime.UtcNow;

                if (isNew)
                {
                    _productService.InsertProduct(product);
                }
                else
                {
                    _productService.UpdateProduct(product);
                }

                //quantity change history
                if (isNew || previousWarehouseId == product.WarehouseId)
                {
                    _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity - previousStockQuantity, product.StockQuantity,
                        product.WarehouseId, _localizationService.GetResource("Admin.StockQuantityHistory.Messages.ImportProduct.Edit"));
                }
                //warehouse is changed 
                else
                {
                    //compose a message
                    var oldWarehouseMessage = string.Empty;
                    if (previousWarehouseId > 0)
                    {
                        var oldWarehouse = _shippingService.GetWarehouseById(previousWarehouseId);
                        if (oldWarehouse != null)
                            oldWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.Old"), oldWarehouse.Name);
                    }

                    var newWarehouseMessage = string.Empty;
                    if (product.WarehouseId > 0)
                    {
                        var newWarehouse = _shippingService.GetWarehouseById(product.WarehouseId);
                        if (newWarehouse != null)
                            newWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.New"), newWarehouse.Name);
                    }

                    var message = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.ImportProduct.EditWarehouse"), oldWarehouseMessage, newWarehouseMessage);

                    //record history
                    _productService.AddStockQuantityHistoryEntry(product, -previousStockQuantity, 0, previousWarehouseId, message);
                    _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId, message);
                }

                var tempProperty = metadata.Manager.GetProperty("SeName");

                //search engine name
                var seName = tempProperty?.StringValue ?? (isNew ? string.Empty : _urlRecordService.GetSeName(product, 0));
                _urlRecordService.SaveSlug(product, _urlRecordService.ValidateSeName(product, seName, product.Name, true), 0);

                tempProperty = metadata.Manager.GetProperty("Categories");

                if (tempProperty != null)
                {
                    var categoryList = tempProperty.StringValue;

                    //category mappings
                    var categories = isNew || !allProductsCategoryIds.ContainsKey(product.Id) ? Array.Empty<int>() : allProductsCategoryIds[product.Id];

                    var importedCategories = categoryList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(categoryName => new CategoryKey(categoryName))
                        .Select(categoryKey =>
                        {
                            var rez = allCategories.ContainsKey(categoryKey) ? allCategories[categoryKey].Id : allCategories.Values.FirstOrDefault(c => c.Name == categoryKey.Key)?.Id;

                            if (!rez.HasValue && int.TryParse(categoryKey.Key, out var id))
                            {
                                rez = id;
                            }

                            if (!rez.HasValue)
                            {
                                //database doesn't contain the imported category
                                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.DatabaseNotContainCategory"), categoryKey.Key));
                            }

                            return rez.Value;
                        }).ToList();

                    foreach (var categoryId in importedCategories)
                    {
                        if (categories.Any(c => c == categoryId))
                            continue;

                        var productCategory = new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId,
                            IsFeaturedProduct = false,
                            DisplayOrder = 1
                        };
                        _categoryService.InsertProductCategory(productCategory);
                    }

                    //delete product categories
                    var deletedProductCategories = categories.Where(categoryId => !importedCategories.Contains(categoryId))
                        .Select(categoryId => _categoryService.GetProductCategoriesByProductId(product.Id).First(pc => pc.CategoryId == categoryId));

                    foreach (var deletedProductCategory in deletedProductCategories)
                    {
                        _categoryService.DeleteProductCategory(deletedProductCategory);
                    }
                }

                tempProperty = metadata.Manager.GetProperty("Manufacturers");

                if (tempProperty != null)
                {
                    var manufacturerList = tempProperty.StringValue;

                    //manufacturer mappings
                    var manufacturers = isNew || !allProductsManufacturerIds.ContainsKey(product.Id) ? Array.Empty<int>() : allProductsManufacturerIds[product.Id];
                    var importedManufacturers = manufacturerList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => allManufacturers.FirstOrDefault(m => m.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList();

                    foreach (var manufacturerId in importedManufacturers)
                    {
                        if (manufacturers.Any(c => c == manufacturerId))
                            continue;

                        var productManufacturer = new ProductManufacturer
                        {
                            ProductId = product.Id,
                            ManufacturerId = manufacturerId,
                            IsFeaturedProduct = false,
                            DisplayOrder = 1
                        };

                        _manufacturerService.InsertProductManufacturer(productManufacturer);
                    }

                    //delete product manufacturers
                    var deletedProductsManufacturers = manufacturers.Where(manufacturerId => !importedManufacturers.Contains(manufacturerId))
                        .Select(manufacturerId => _manufacturerService.GetProductManufacturersByProductId(product.Id).First(pc => pc.ManufacturerId == manufacturerId));

                    foreach (var deletedProductManufacturer in deletedProductsManufacturers)
                    {
                        _manufacturerService.DeleteProductManufacturer(deletedProductManufacturer);
                    }
                }

                tempProperty = metadata.Manager.GetProperty("ProductTags");
                if (tempProperty != null)
                {
                    var productTags = tempProperty.StringValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

                    //searching existing product tags by their id
                    var productTagIds = productTags.Where(pt => int.TryParse(pt, out var _)).Select(int.Parse);
                    var pruductTagsByIds = _productTagService.GetAllProductTagsByProductId(product.Id).Where(pt => productTagIds.Contains(pt.Id)).ToList();

                    productTags.AddRange(pruductTagsByIds.Select(pt => pt.Name));

                    var filter = pruductTagsByIds.Select(pt => pt.Id.ToString()).ToList();

                    //product tag mappings
                    _productTagService.UpdateProductTags(product, productTags.Where(pt => !filter.Contains(pt)).ToArray());
                }

                tempProperty = metadata.Manager.GetProperty("LimitedToStores");
                if (tempProperty != null)
                {
                    var limitedToStoresList = tempProperty.StringValue;

                    var importedStores = product.LimitedToStores ? limitedToStoresList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => allStores.FirstOrDefault(store => store.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList() : new List<int>();

                    _productService.UpdateProductStoreMappings(product, importedStores);
                }

                var picture1 = DownloadFile(metadata.Manager.GetProperty("Picture1")?.StringValue, downloadedFiles);
                var picture2 = DownloadFile(metadata.Manager.GetProperty("Picture2")?.StringValue, downloadedFiles);
                var picture3 = DownloadFile(metadata.Manager.GetProperty("Picture3")?.StringValue, downloadedFiles);

                productPictureMetadata.Add(new ProductPictureMetadata
                {
                    ProductItem = product,
                    Picture1Path = picture1,
                    Picture2Path = picture2,
                    Picture3Path = picture3,
                    IsNew = isNew
                });

                lastLoadedProduct = product;
            }

            if (_mediaSettings.ImportProductImagesUsingHash && _pictureService.StoreInDb)
                ImportProductImagesUsingHash(productPictureMetadata, allProductsBySku);
            else
            {
                ImportProductImagesUsingServices(productPictureMetadata);
            }


            foreach (var downloadedFile in downloadedFiles)
            {
                if (!_fileProvider.FileExists(downloadedFile))
                    continue;

                try
                {
                    _fileProvider.DeleteFile(downloadedFile);
                }
                catch
                {
                    // ignored
                }
            }

            //activity log
            _customerActivityService.InsertActivity("ImportProducts", string.Format(_localizationService.GetResource("ActivityLog.ImportProducts"), metadata.CountProductsInFile));
        }

        private ImportProductMetadata PrepareImportProductData(ExcelWorksheet worksheet)
        {
            //the columns
            var properties = GetPropertiesByExcelCells<Product>(worksheet);
            var manager = new PropertyManager<Product>(properties, _catalogSettings);
            var productAttributeProperties = new[]
            {
                new PropertyByName<ExportProductAttribute>("AttributeId"),
                new PropertyByName<ExportProductAttribute>("AttributeName"),
                new PropertyByName<ExportProductAttribute>("AttributeTextPrompt"),
                new PropertyByName<ExportProductAttribute>("AttributeIsRequired"),
                new PropertyByName<ExportProductAttribute>("AttributeControlType"),
                new PropertyByName<ExportProductAttribute>("AttributeDisplayOrder"),
                new PropertyByName<ExportProductAttribute>("ProductAttributeValueId"),
                new PropertyByName<ExportProductAttribute>("ValueName"),
                new PropertyByName<ExportProductAttribute>("AttributeValueType"),
                new PropertyByName<ExportProductAttribute>("AssociatedProductId"),
                new PropertyByName<ExportProductAttribute>("ColorSquaresRgb"),
                new PropertyByName<ExportProductAttribute>("ImageSquaresPictureId"),
                new PropertyByName<ExportProductAttribute>("PriceAdjustment"),
                new PropertyByName<ExportProductAttribute>("PriceAdjustmentUsePercentage"),
                new PropertyByName<ExportProductAttribute>("WeightAdjustment"),
                new PropertyByName<ExportProductAttribute>("Cost"),
                new PropertyByName<ExportProductAttribute>("CustomerEntersQty"),
                new PropertyByName<ExportProductAttribute>("Quantity"),
                new PropertyByName<ExportProductAttribute>("IsPreSelected"),
                new PropertyByName<ExportProductAttribute>("DisplayOrder"),
                new PropertyByName<ExportProductAttribute>("PictureId")
            };

            var productAttributeManager = new PropertyManager<ExportProductAttribute>(productAttributeProperties, _catalogSettings);
            var specificationAttributeProperties = new[]
            {
                new PropertyByName<ExportSpecificationAttribute>("AttributeType", p => p.AttributeTypeId),
                new PropertyByName<ExportSpecificationAttribute>("SpecificationAttribute", p => p.SpecificationAttributeId),
                new PropertyByName<ExportSpecificationAttribute>("CustomValue", p => p.CustomValue),
                new PropertyByName<ExportSpecificationAttribute>("SpecificationAttributeOptionId", p => p.SpecificationAttributeOptionId),
                new PropertyByName<ExportSpecificationAttribute>("AllowFiltering", p => p.AllowFiltering),
                new PropertyByName<ExportSpecificationAttribute>("ShowOnProductPage", p => p.ShowOnProductPage),
                new PropertyByName<ExportSpecificationAttribute>("DisplayOrder", p => p.DisplayOrder)
            };

            var specificationAttributeManager = new PropertyManager<ExportSpecificationAttribute>(specificationAttributeProperties, _catalogSettings);

            var endRow = 2;
            var allCategories = new List<string>();
            var allSku = new List<string>();
            var tempProperty = manager.GetProperty("Categories");
            var categoryCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            tempProperty = manager.GetProperty("SKU");
            var skuCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            var allManufacturers = new List<string>();
            tempProperty = manager.GetProperty("Manufacturers");

            var manufacturerCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            var allStores = new List<string>();
            tempProperty = manager.GetProperty("LimitedToStores");

            var limitedToStoresCellNum = tempProperty?.PropertyOrderPosition ?? -1;

            if (_catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities)
            {
                productAttributeManager.SetSelectList("AttributeControlType", AttributeControlType.TextBox.ToSelectList(useLocalization: false));
                productAttributeManager.SetSelectList("AttributeValueType", AttributeValueType.Simple.ToSelectList(useLocalization: false));

                specificationAttributeManager.SetSelectList("AttributeType", SpecificationAttributeType.Option.ToSelectList(useLocalization: false));
                specificationAttributeManager.SetSelectList("SpecificationAttribute", _specificationAttributeService
                    .GetSpecificationAttributes()
                    .Select(sa => sa as BaseEntity)
                    .ToSelectList(p => (p as SpecificationAttribute)?.Name ?? string.Empty));

                manager.SetSelectList("ProductType", ProductType.SimpleProduct.ToSelectList(useLocalization: false));
                manager.SetSelectList("GiftCardType", GiftCardType.Virtual.ToSelectList(useLocalization: false));
                manager.SetSelectList("DownloadActivationType",
                    DownloadActivationType.Manually.ToSelectList(useLocalization: false));
                manager.SetSelectList("ManageInventoryMethod",
                    ManageInventoryMethod.DontManageStock.ToSelectList(useLocalization: false));
                manager.SetSelectList("LowStockActivity",
                    LowStockActivity.Nothing.ToSelectList(useLocalization: false));
                manager.SetSelectList("BackorderMode", BackorderMode.NoBackorders.ToSelectList(useLocalization: false));
                manager.SetSelectList("RecurringCyclePeriod",
                    RecurringProductCyclePeriod.Days.ToSelectList(useLocalization: false));
                manager.SetSelectList("RentalPricePeriod", RentalPricePeriod.Days.ToSelectList(useLocalization: false));

                manager.SetSelectList("Vendor",
                    _vendorService.GetAllVendors(showHidden: true).Select(v => v as BaseEntity)
                        .ToSelectList(p => (p as Vendor)?.Name ?? string.Empty));
                manager.SetSelectList("ProductTemplate",
                    _productTemplateService.GetAllProductTemplates().Select(pt => pt as BaseEntity)
                        .ToSelectList(p => (p as ProductTemplate)?.Name ?? string.Empty));
                manager.SetSelectList("DeliveryDate",
                    _dateRangeService.GetAllDeliveryDates().Select(dd => dd as BaseEntity)
                        .ToSelectList(p => (p as DeliveryDate)?.Name ?? string.Empty));
                manager.SetSelectList("ProductAvailabilityRange",
                    _dateRangeService.GetAllProductAvailabilityRanges().Select(range => range as BaseEntity)
                        .ToSelectList(p => (p as ProductAvailabilityRange)?.Name ?? string.Empty));
                manager.SetSelectList("TaxCategory",
                    _taxCategoryService.GetAllTaxCategories().Select(tc => tc as BaseEntity)
                        .ToSelectList(p => (p as TaxCategory)?.Name ?? string.Empty));
                manager.SetSelectList("BasepriceUnit",
                    _measureService.GetAllMeasureWeights().Select(mw => mw as BaseEntity)
                        .ToSelectList(p => (p as MeasureWeight)?.Name ?? string.Empty));
                manager.SetSelectList("BasepriceBaseUnit",
                    _measureService.GetAllMeasureWeights().Select(mw => mw as BaseEntity)
                        .ToSelectList(p => (p as MeasureWeight)?.Name ?? string.Empty));
            }

            var allAttributeIds = new List<int>();
            var allSpecificationAttributeOptionIds = new List<int>();
            var attributeIdCellNum = 1 + ExportProductAttribute.ProducAttributeCellOffset;
            var specificationAttributeOptionIdCellNum =
                specificationAttributeManager.GetIndex("SpecificationAttributeOptionId") +
                ExportProductAttribute.ProducAttributeCellOffset;
            var productsInFile = new List<int>();

            //find end of data
            var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

                if (allColumnsAreEmpty)
                    break;

                if (new[] { 1, 2 }.Select(cellNum => worksheet.Cells[endRow, cellNum])
                        .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString())) &&
                    worksheet.Row(endRow).OutlineLevel == 0)
                {
                    var cellValue = worksheet.Cells[endRow, attributeIdCellNum].Value;
                    SetOutLineForProductAttributeRow(cellValue, worksheet, endRow);
                    SetOutLineForSpecificationAttributeRow(cellValue, worksheet, endRow);
                }

                if (worksheet.Row(endRow).OutlineLevel != 0)
                {
                    var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, productAttributeManager, specificationAttributeManager, endRow);

                    //skip caption row
                    if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified && newTypeOfExportedAttribute != typeOfExportedAttribute)
                    {
                        typeOfExportedAttribute = newTypeOfExportedAttribute;
                        endRow++;
                        continue;
                    }

                    switch (typeOfExportedAttribute)
                    {
                        case ExportedAttributeType.ProductAttribute:
                            productAttributeManager.ReadFromXlsx(worksheet, endRow,
                                ExportProductAttribute.ProducAttributeCellOffset);
                            if (int.TryParse((worksheet.Cells[endRow, attributeIdCellNum].Value ?? string.Empty).ToString(), out var aid))
                            {
                                allAttributeIds.Add(aid);
                            }

                            break;
                        case ExportedAttributeType.SpecificationAttribute:
                            specificationAttributeManager.ReadFromXlsx(worksheet, endRow, ExportProductAttribute.ProducAttributeCellOffset);

                            if (int.TryParse((worksheet.Cells[endRow, specificationAttributeOptionIdCellNum].Value ?? string.Empty).ToString(), out var saoid))
                            {
                                allSpecificationAttributeOptionIds.Add(saoid);
                            }

                            break;
                    }

                    endRow++;
                    continue;
                }

                if (categoryCellNum > 0)
                {
                    var categoryIds = worksheet.Cells[endRow, categoryCellNum].Value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(categoryIds))
                        allCategories.AddRange(categoryIds
                            .Split(new[] { ";", ">>" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
                            .Distinct());
                }

                if (skuCellNum > 0)
                {
                    var sku = worksheet.Cells[endRow, skuCellNum].Value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(sku))
                        allSku.Add(sku);
                }

                if (manufacturerCellNum > 0)
                {
                    var manufacturerIds = worksheet.Cells[endRow, manufacturerCellNum].Value?.ToString() ??
                                          string.Empty;
                    if (!string.IsNullOrEmpty(manufacturerIds))
                        allManufacturers.AddRange(manufacturerIds
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                }

                if (limitedToStoresCellNum > 0)
                {
                    var storeIds = worksheet.Cells[endRow, limitedToStoresCellNum].Value?.ToString() ??
                                          string.Empty;
                    if (!string.IsNullOrEmpty(storeIds))
                        allStores.AddRange(storeIds
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                }

                //counting the number of products
                productsInFile.Add(endRow);

                endRow++;
            }

            //performance optimization, the check for the existence of the categories in one SQL request
            var notExistingCategories = _categoryService.GetNotExistingCategories(allCategories.ToArray());

            if (notExistingCategories.Any())
            {
                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.CategoriesDontExist"), string.Join(", ", notExistingCategories)));
            }

            //performance optimization, the check for the existence of the manufacturers in one SQL request
            var notExistingManufacturers = _manufacturerService.GetNotExistingManufacturers(allManufacturers.ToArray());
            if (notExistingManufacturers.Any())
            {
                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.ManufacturersDontExist"), string.Join(", ", notExistingManufacturers)));
            }

            //performance optimization, the check for the existence of the product attributes in one SQL request
            var notExistingProductAttributes = _productAttributeService.GetNotExistingAttributes(allAttributeIds.ToArray());
            if (notExistingProductAttributes.Any())
            {
                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.ProductAttributesDontExist"), string.Join(", ", notExistingProductAttributes)));
            }

            //performance optimization, the check for the existence of the specification attribute options in one SQL request
            var notExistingSpecificationAttributeOptions = _specificationAttributeService.GetNotExistingSpecificationAttributeOptions(allSpecificationAttributeOptionIds.Where(saoId => saoId != 0).ToArray());
            if (notExistingSpecificationAttributeOptions.Any())
            {
                throw new ArgumentException($"The following specification attribute option ID(s) don't exist - {string.Join(", ", notExistingSpecificationAttributeOptions)}");
            }

            //performance optimization, the check for the existence of the stores in one SQL request
            var notExistingStores = _storeService.GetNotExistingStores(allStores.ToArray());
            if (notExistingStores.Any())
            {
                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.StoresDontExist"), string.Join(", ", notExistingStores)));
            }

            return new ImportProductMetadata
            {
                EndRow = endRow,
                Manager = manager,
                Properties = properties,
                ProductsInFile = productsInFile,
                ProductAttributeManager = productAttributeManager,
                SpecificationAttributeManager = specificationAttributeManager,
                SkuCellNum = skuCellNum,
                AllSku = allSku
            };
        }

        private void ImportProductsFromSplitedXlsx(ExcelWorksheet worksheet, ImportProductMetadata metadata)
        {
            foreach (var path in SplitProductFile(worksheet, metadata))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    // Resolve
                    var importManager = scope.ServiceProvider.GetRequiredService<IImportManager>();

                    using var sr = new StreamReader(path);
                    importManager.ImportProductsFromXlsx(sr.BaseStream);
                }

                try
                {
                    _fileProvider.DeleteFile(path);
                }
                catch
                {
                    // ignored
                }
            }
        }

        protected virtual void ImportProductAttribute(PropertyManager<ExportProductAttribute> productAttributeManager, Product lastLoadedProduct)
        {
            if (!_catalogSettings.ExportImportProductAttributes || lastLoadedProduct == null || productAttributeManager.IsCaption)
                return;

            var productAttributeId = productAttributeManager.GetProperty("AttributeId").IntValue;
            var attributeControlTypeId = productAttributeManager.GetProperty("AttributeControlType").IntValue;
            var productAttributeValueId = productAttributeManager.GetProperty("ProductAttributeValueId").IntValue;
            var associatedProductId = productAttributeManager.GetProperty("AssociatedProductId").IntValue;
            var valueName = productAttributeManager.GetProperty("ValueName").StringValue;
            var attributeValueTypeId = productAttributeManager.GetProperty("AttributeValueType").IntValue;
            var colorSquaresRgb = productAttributeManager.GetProperty("ColorSquaresRgb").StringValue;
            var imageSquaresPictureId = productAttributeManager.GetProperty("ImageSquaresPictureId").IntValue;
            var priceAdjustment = productAttributeManager.GetProperty("PriceAdjustment").DecimalValue;
            var priceAdjustmentUsePercentage = productAttributeManager.GetProperty("PriceAdjustmentUsePercentage").BooleanValue;
            var weightAdjustment = productAttributeManager.GetProperty("WeightAdjustment").DecimalValue;
            var cost = productAttributeManager.GetProperty("Cost").DecimalValue;
            var customerEntersQty = productAttributeManager.GetProperty("CustomerEntersQty").BooleanValue;
            var quantity = productAttributeManager.GetProperty("Quantity").IntValue;
            var isPreSelected = productAttributeManager.GetProperty("IsPreSelected").BooleanValue;
            var displayOrder = productAttributeManager.GetProperty("DisplayOrder").IntValue;
            var pictureId = productAttributeManager.GetProperty("PictureId").IntValue;
            var textPrompt = productAttributeManager.GetProperty("AttributeTextPrompt").StringValue;
            var isRequired = productAttributeManager.GetProperty("AttributeIsRequired").BooleanValue;
            var attributeDisplayOrder = productAttributeManager.GetProperty("AttributeDisplayOrder").IntValue;

            var productAttributeMapping = _productAttributeService.GetProductAttributeMappingsByProductId(lastLoadedProduct.Id)
                .FirstOrDefault(pam => pam.ProductAttributeId == productAttributeId);

            if (productAttributeMapping == null)
            {
                //insert mapping
                productAttributeMapping = new ProductAttributeMapping
                {
                    ProductId = lastLoadedProduct.Id,
                    ProductAttributeId = productAttributeId,
                    TextPrompt = textPrompt,
                    IsRequired = isRequired,
                    AttributeControlTypeId = attributeControlTypeId,
                    DisplayOrder = attributeDisplayOrder
                };

                _productAttributeService.InsertProductAttributeMapping(productAttributeMapping);
            }
            else
            {
                productAttributeMapping.AttributeControlTypeId = attributeControlTypeId;
                productAttributeMapping.TextPrompt = textPrompt;
                productAttributeMapping.IsRequired = isRequired;
                productAttributeMapping.DisplayOrder = attributeDisplayOrder;
                _productAttributeService.UpdateProductAttributeMapping(productAttributeMapping);
            }

            var pav = _productAttributeService.GetProductAttributeValueById(productAttributeValueId);

            var attributeControlType = (AttributeControlType)attributeControlTypeId;

            if (pav == null)
            {
                switch (attributeControlType)
                {
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    case AttributeControlType.MultilineTextbox:
                    case AttributeControlType.TextBox:
                        return;
                }

                pav = new ProductAttributeValue
                {
                    ProductAttributeMappingId = productAttributeMapping.Id,
                    AttributeValueType = (AttributeValueType)attributeValueTypeId,
                    AssociatedProductId = associatedProductId,
                    Name = valueName,
                    PriceAdjustment = priceAdjustment,
                    PriceAdjustmentUsePercentage = priceAdjustmentUsePercentage,
                    WeightAdjustment = weightAdjustment,
                    Cost = cost,
                    IsPreSelected = isPreSelected,
                    DisplayOrder = displayOrder,
                    ColorSquaresRgb = colorSquaresRgb,
                    ImageSquaresPictureId = imageSquaresPictureId,
                    CustomerEntersQty = customerEntersQty,
                    Quantity = quantity,
                    PictureId = pictureId
                };

                _productAttributeService.InsertProductAttributeValue(pav);
            }
            else
            {
                pav.AttributeValueTypeId = attributeValueTypeId;
                pav.AssociatedProductId = associatedProductId;
                pav.Name = valueName;
                pav.ColorSquaresRgb = colorSquaresRgb;
                pav.ImageSquaresPictureId = imageSquaresPictureId;
                pav.PriceAdjustment = priceAdjustment;
                pav.PriceAdjustmentUsePercentage = priceAdjustmentUsePercentage;
                pav.WeightAdjustment = weightAdjustment;
                pav.Cost = cost;
                pav.CustomerEntersQty = customerEntersQty;
                pav.Quantity = quantity;
                pav.IsPreSelected = isPreSelected;
                pav.DisplayOrder = displayOrder;
                pav.PictureId = pictureId;

                _productAttributeService.UpdateProductAttributeValue(pav);
            }
        }

        private void ImportSpecificationAttribute(PropertyManager<ExportSpecificationAttribute> specificationAttributeManager, Product lastLoadedProduct)
        {
            if (!_catalogSettings.ExportImportProductSpecificationAttributes || lastLoadedProduct == null || specificationAttributeManager.IsCaption)
                return;

            var attributeTypeId = specificationAttributeManager.GetProperty("AttributeType").IntValue;
            var allowFiltering = specificationAttributeManager.GetProperty("AllowFiltering").BooleanValue;
            var specificationAttributeOptionId = specificationAttributeManager.GetProperty("SpecificationAttributeOptionId").IntValue;
            var productId = lastLoadedProduct.Id;
            var customValue = specificationAttributeManager.GetProperty("CustomValue").StringValue;
            var displayOrder = specificationAttributeManager.GetProperty("DisplayOrder").IntValue;
            var showOnProductPage = specificationAttributeManager.GetProperty("ShowOnProductPage").BooleanValue;

            //if specification attribute option isn't set, try to get first of possible specification attribute option for current specification attribute
            if (specificationAttributeOptionId == 0)
            {
                var specificationAttribute = specificationAttributeManager.GetProperty("SpecificationAttribute").IntValue;
                specificationAttributeOptionId = _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(specificationAttribute)
                                                     .FirstOrDefault()?.Id ?? specificationAttributeOptionId;
            }

            var productSpecificationAttribute = specificationAttributeOptionId == 0
                ? null
                : _specificationAttributeService.GetProductSpecificationAttributes(productId, specificationAttributeOptionId).FirstOrDefault();

            var isNew = productSpecificationAttribute == null;

            if (isNew)
            {
                productSpecificationAttribute = new ProductSpecificationAttribute();
            }

            if (attributeTypeId != (int)SpecificationAttributeType.Option)
            {
                //we allow filtering only for "Option" attribute type
                allowFiltering = false;
            }

            //we don't allow CustomValue for "Option" attribute type
            if (attributeTypeId == (int)SpecificationAttributeType.Option)
            {
                customValue = null;
            }

            productSpecificationAttribute.AttributeTypeId = attributeTypeId;
            productSpecificationAttribute.SpecificationAttributeOptionId = specificationAttributeOptionId;
            productSpecificationAttribute.ProductId = productId;
            productSpecificationAttribute.CustomValue = customValue;
            productSpecificationAttribute.AllowFiltering = allowFiltering;
            productSpecificationAttribute.ShowOnProductPage = showOnProductPage;
            productSpecificationAttribute.DisplayOrder = displayOrder;

            if (isNew)
            {
                _specificationAttributeService.InsertProductSpecificationAttribute(productSpecificationAttribute);
            }
            else
            {
                _specificationAttributeService.UpdateProductSpecificationAttribute(productSpecificationAttribute);
            }

        }

        private static ExportedAttributeType GetTypeOfExportedAttribute(ExcelWorksheet worksheet, PropertyManager<ExportProductAttribute> productAttributeManager, PropertyManager<ExportSpecificationAttribute> specificationAttributeManager, int iRow)
        {
            productAttributeManager.ReadFromXlsx(worksheet, iRow, ExportProductAttribute.ProducAttributeCellOffset);

            if (productAttributeManager.IsCaption)
            {
                return ExportedAttributeType.ProductAttribute;
            }

            specificationAttributeManager.ReadFromXlsx(worksheet, iRow, ExportProductAttribute.ProducAttributeCellOffset);

            if (specificationAttributeManager.IsCaption)
            {
                return ExportedAttributeType.SpecificationAttribute;
            }

            return ExportedAttributeType.NotSpecified;

        }

        private string DownloadFile(string urlString, IList<string> downloadedFiles)
        {
            bool isADropBoxUrl = urlString.Contains("?dl=0") || urlString.Contains("dropbox");

            if (isADropBoxUrl)
            {
                urlString = urlString.Replace("?dl=0", "?dl=1");
            }

            if (string.IsNullOrEmpty(urlString))
            {
                _logger.Error($"Download image failed. urlString can't be empty {urlString}");
                return string.Empty;
            }

            if (!Uri.IsWellFormedUriString(urlString, UriKind.Absolute))
            {
                _logger.Error($"Download image failed. urlString can't be bad form {urlString}");
                return urlString;
            }

            if (!_catalogSettings.ExportImportAllowDownloadImages)
            {
                _logger.Error($"Download image failed. ExportImportAllowDownloadImages permission is not active.");
                return string.Empty;
            }

            //ensure that temp directory is created
            var tempDirectory = _fileProvider.MapPath(UPLOADS_TEMP_PATH);
            _fileProvider.CreateDirectory(tempDirectory);

            var fileName = _fileProvider.GetFileName(urlString);

            if (urlString.Contains("?dl=1"))
            {
                fileName = fileName.Replace("?dl=1", "");
            }

            if (string.IsNullOrEmpty(fileName))
            {
                _logger.Error($"Download image failed. fileName can't be empty {fileName}");
                return string.Empty;
            }

            var filePath = _fileProvider.Combine(tempDirectory, fileName);
            bool isADropBoxUrlWithForceDownload = urlString.Contains("?dl=1") || urlString.Contains("dropbox");

            var client = _httpClientFactory.CreateClient(NopHttpDefaults.DefaultHttpClient);

            try
            {
                var fileData = client.GetByteArrayAsync(urlString).Result;
                var fileExtension = Path.GetExtension(fileName);

                bool isATiffFile = fileExtension.Equals(ImageFormat.Tiff) || fileExtension.Equals(".tif") || fileExtension.Equals("tif");
                if (isATiffFile)
                {
                    Stream imageStream = client.GetStreamAsync(urlString).Result;
                    Image img = Bitmap.FromStream(imageStream);
                    SaveImage(filePath, img);
                }
                else if (isADropBoxUrlWithForceDownload)
                {
                    Stream imageStream = client.GetStreamAsync(urlString).Result;
                    Image img = Bitmap.FromStream(imageStream);
                    SaveImage(filePath, img);
                }
                else
                {
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fs.Write(fileData, 0, fileData.Length);
                    }
                }

                downloadedFiles?.Add(filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                _logger.Error("Download image failed", ex);
            }
            finally
            {
                client.Dispose();
            }

            return string.Empty;
        }

        private static void SaveImage(string filePath, Image img)
        {
            using (var bitmap = new Bitmap(img))
            {
                bitmap.Save(filePath, ImageFormat.Jpeg);
            }
        }
        protected virtual void ImportProductImagesUsingServices(IList<ProductPictureMetadata> productPictureMetadata)
        {
            foreach (var product in productPictureMetadata)
            {
                foreach (var picturePath in new[] { product.Picture1Path, product.Picture2Path, product.Picture3Path })
                {
                    if (string.IsNullOrEmpty(picturePath))
                        continue;

                    var mimeType = GetMimeTypeFromFilePath(picturePath);
                    var newPictureBinary = _fileProvider.ReadAllBytes(picturePath);
                    var pictureAlreadyExists = false;
                    if (!product.IsNew)
                    {
                        //compare with existing product pictures
                        var existingPictures = _pictureService.GetPicturesByProductId(product.ProductItem.Id);
                        foreach (var existingPicture in existingPictures)
                        {
                            var existingBinary = _pictureService.LoadPictureBinary(existingPicture);
                            //picture binary after validation (like in database)
                            var validatedPictureBinary = _pictureService.ValidatePicture(newPictureBinary, mimeType);
                            if (!existingBinary.SequenceEqual(validatedPictureBinary) &&
                                !existingBinary.SequenceEqual(newPictureBinary))
                                continue;
                            //the same picture content
                            pictureAlreadyExists = true;
                            break;
                        }
                    }

                    if (pictureAlreadyExists)
                        continue;

                    try
                    {
                        var newPicture = _pictureService.InsertPicture(newPictureBinary, mimeType, _pictureService.GetPictureSeName(product.ProductItem.Name));
                        _productService.InsertProductPicture(new ProductPicture
                        {
                            //EF has some weird issue if we set "Picture = newPicture" instead of "PictureId = newPicture.Id"
                            //pictures are duplicated
                            //maybe because entity size is too large
                            PictureId = newPicture.Id,
                            DisplayOrder = 1,
                            ProductId = product.ProductItem.Id
                        });
                        _productService.UpdateProduct(product.ProductItem);
                    }
                    catch (Exception ex)
                    {
                        LogPictureInsertError(picturePath, ex);
                    }
                }
            }
        }

        protected virtual void ImportProductImagesUsingHash(IList<ProductPictureMetadata> productPictureMetadata, IList<Product> allProductsBySku)
        {
            //performance optimization, load all pictures hashes
            //it will only be used if the images are stored in the SQL Server database (not compact)
            var trimByteCount = _dataProvider.SupportedLengthOfBinaryHash - 1;
            var productsImagesIds = _productService.GetProductsImagesIds(allProductsBySku.Select(p => p.Id).ToArray());
            var allPicturesHashes = _pictureService.GetPicturesHash(productsImagesIds.SelectMany(p => p.Value).ToArray());

            foreach (var product in productPictureMetadata)
            {
                foreach (var picturePath in new[] { product.Picture1Path, product.Picture2Path, product.Picture3Path })
                {
                    if (string.IsNullOrEmpty(picturePath))
                        continue;
                    try
                    {
                        var mimeType = GetMimeTypeFromFilePath(picturePath);
                        var newPictureBinary = _fileProvider.ReadAllBytes(picturePath);
                        var pictureAlreadyExists = false;
                        if (!product.IsNew)
                        {
                            var newImageHash = HashHelper.CreateHash(
                                newPictureBinary,
                                IMAGE_HASH_ALGORITHM,
                                trimByteCount);

                            var newValidatedImageHash = HashHelper.CreateHash(
                                _pictureService.ValidatePicture(newPictureBinary, mimeType),
                                IMAGE_HASH_ALGORITHM,
                                trimByteCount);

                            var imagesIds = productsImagesIds.ContainsKey(product.ProductItem.Id)
                                ? productsImagesIds[product.ProductItem.Id]
                                : Array.Empty<int>();

                            pictureAlreadyExists = allPicturesHashes.Where(p => imagesIds.Contains(p.Key))
                                .Select(p => p.Value)
                                .Any(p =>
                                    p.Equals(newImageHash, StringComparison.OrdinalIgnoreCase) ||
                                    p.Equals(newValidatedImageHash, StringComparison.OrdinalIgnoreCase));
                        }

                        if (pictureAlreadyExists)
                            continue;

                        var newPicture = _pictureService.InsertPicture(newPictureBinary, mimeType, _pictureService.GetPictureSeName(product.ProductItem.Name));

                        _productService.InsertProductPicture(new ProductPicture
                        {
                            //EF has some weird issue if we set "Picture = newPicture" instead of "PictureId = newPicture.Id"
                            //pictures are duplicated
                            //maybe because entity size is too large
                            PictureId = newPicture.Id,
                            DisplayOrder = 1,
                            ProductId = product.ProductItem.Id
                        });

                        _productService.UpdateProduct(product.ProductItem);
                    }
                    catch (Exception ex)
                    {
                        LogPictureInsertError(picturePath, ex);
                    }
                }
            }
        }

        protected virtual void SetOutLineForProductAttributeRow(object cellValue, ExcelWorksheet worksheet, int endRow)
        {
            try
            {
                var aid = Convert.ToInt32(cellValue ?? -1);

                var productAttribute = _productAttributeService.GetProductAttributeById(aid);

                if (productAttribute != null)
                    worksheet.Row(endRow).OutlineLevel = 1;
            }
            catch (FormatException)
            {
                if ((cellValue ?? string.Empty).ToString() == "AttributeId")
                    worksheet.Row(endRow).OutlineLevel = 1;
            }

        }

        private static void SetOutLineForSpecificationAttributeRow(object cellValue, ExcelWorksheet worksheet, int endRow)
        {
            var attributeType = (cellValue ?? string.Empty).ToString();
            if (attributeType.Equals("AttributeType", StringComparison.InvariantCultureIgnoreCase))
            {
                worksheet.Row(endRow).OutlineLevel = 1;
            }
            else
            {
                if (SpecificationAttributeType.Option.ToSelectList(useLocalization: false)
                    .Any(p => p.Text.Equals(attributeType, StringComparison.InvariantCultureIgnoreCase)))
                    worksheet.Row(endRow).OutlineLevel = 1;
                else if (int.TryParse(attributeType, out var attributeTypeId) && Enum.IsDefined(typeof(SpecificationAttributeType), attributeTypeId))
                    worksheet.Row(endRow).OutlineLevel = 1;
            }

        }

        private IList<string> SplitProductFile(ExcelWorksheet worksheet, ImportProductMetadata metadata)
        {
            var fileIndex = 1;
            var fileName = Guid.NewGuid().ToString();
            var endCell = metadata.Properties.Max(p => p.PropertyOrderPosition);
            var filePaths = new List<string>();

            while (true)
            {
                var curIndex = fileIndex * _catalogSettings.ExportImportProductsCountInOneFile;
                var startRow = metadata.ProductsInFile[(fileIndex - 1) * _catalogSettings.ExportImportProductsCountInOneFile];
                var endRow = metadata.CountProductsInFile > curIndex + 1
                    ? metadata.ProductsInFile[curIndex - 1]
                    : metadata.EndRow;

                var filePath = $"{_fileProvider.MapPath(UPLOADS_TEMP_PATH)}/{fileName}_part_{fileIndex}.xlsx";

                CopyDataToNewFile(metadata, worksheet, filePath, startRow, endRow, endCell);

                filePaths.Add(filePath);
                fileIndex += 1;

                if (endRow == metadata.EndRow)
                    break;
            }

            return filePaths;
        }

        protected virtual string GetMimeTypeFromFilePath(string filePath)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out var mimeType);

            //set to jpeg in case mime type cannot be found
            return mimeType ?? MimeTypes.ImageJpeg;
        }

        private void LogPictureInsertError(string picturePath, Exception ex)
        {
            var extension = _fileProvider.GetFileExtension(picturePath);
            var name = _fileProvider.GetFileNameWithoutExtension(picturePath);
            var point = string.IsNullOrEmpty(extension) ? string.Empty : ".";
            var fileName = _fileProvider.FileExists(picturePath) ? $"{name}{point}{extension}" : string.Empty;
            _logger.Error($"Insert picture failed (file name: {fileName})", ex);
        }

        private static void CopyDataToNewFile(ImportProductMetadata metadata, ExcelWorksheet worksheet, string filePath, int startRow, int endRow, int endCell)
        {
            using var stream = new FileStream(filePath, FileMode.OpenOrCreate);
            // ok, we can run the real code of the sample now

            using var xlPackage = new ExcelPackage(stream);
            // uncomment this line if you want the XML written out to the outputDir
            //xlPackage.DebugMode = true; 

            // get handles to the worksheets
            var outWorksheet = xlPackage.Workbook.Worksheets.Add(typeof(Product).Name);
            metadata.Manager.WriteCaption(outWorksheet);
            var outRow = 2;

            for (var row = startRow; row <= endRow; row++)
            {
                outWorksheet.Row(outRow).OutlineLevel = worksheet.Row(row).OutlineLevel;
                for (var cell = 1; cell <= endCell; cell++)
                {
                    outWorksheet.Cells[outRow, cell].Value = worksheet.Cells[row, cell].Value;
                }

                outRow += 1;
            }

            xlPackage.Save();
        }

        protected class ProductPictureMetadata
        {
            /// <summary>
            /// Represents the product.
            /// </summary>
            public Product ProductItem { get; set; }
            /// <summary>
            /// Represents the picture 1.
            /// </summary>
            public string Picture1Path { get; set; }
            /// <summary>
            /// Represents the picture 2.
            /// </summary>
            public string Picture2Path { get; set; }
            /// <summary>
            /// Represents the picture 3.
            /// </summary>
            public string Picture3Path { get; set; }
            /// <summary>
            /// Represents the the new product.
            /// </summary>
            public bool IsNew { get; set; }
        }
    }
}
