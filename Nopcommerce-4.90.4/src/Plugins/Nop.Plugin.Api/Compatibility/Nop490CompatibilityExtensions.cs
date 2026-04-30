using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Factories;
using Nop.Web.Models.Customer;

namespace Nop.Plugin.Api.Compatibility
{
    public static class Nop490CompatibilityExtensions
    {
        private static T Await<T>(Task<T> task) => task.GetAwaiter().GetResult();
        private static void Await(Task task) => task.GetAwaiter().GetResult();

        public static Task SuccessNotificationAsync(this INotificationService service, string message, bool encode = true, int timeout = 0)
        {
            service.SuccessNotification(message, encode, timeout);
            return Task.CompletedTask;
        }

        public static Vendor GetVendorById(this IVendorService service, int vendorId) => Await(service.GetVendorByIdAsync(vendorId));
        public static IList<TaxCategory> GetAllTaxCategories(this ITaxCategoryService service) => Await(service.GetAllTaxCategoriesAsync());
        public static MeasureWeight GetMeasureWeightById(this IMeasureService service, int measureWeightId) => Await(service.GetMeasureWeightByIdAsync(measureWeightId));
        public static IList<CategoryTemplate> GetAllCategoryTemplates(this ICategoryTemplateService service) => Await(service.GetAllCategoryTemplatesAsync());
        public static Country GetCountryById(this ICountryService service, int countryId) => Await(service.GetCountryByIdAsync(countryId));
        public static Currency GetCurrencyById(this ICurrencyService service, int currencyId) => Await(service.GetCurrencyByIdAsync(currencyId));
        public static Language GetLanguageById(this ILanguageService service, int languageId) => Await(service.GetLanguageByIdAsync(languageId));
        public static IList<Language> GetAllLanguages(this ILanguageService service, bool showHidden = false, int storeId = 0) => Await(service.GetAllLanguagesAsync(showHidden, storeId));

        public static Customer GetCustomerById(this ICustomerService service, int customerId) => Await(service.GetCustomerByIdAsync(customerId));
        public static Customer GetCustomerByGuid(this ICustomerService service, Guid customerGuid) => Await(service.GetCustomerByGuidAsync(customerGuid));
        public static Customer GetCustomerByEmail(this ICustomerService service, string email) => Await(service.GetCustomerByEmailAsync(email));
        public static IList<CustomerRole> GetCustomerRoles(this ICustomerService service, Customer customer, bool showHidden = false) => Await(service.GetCustomerRolesAsync(customer, showHidden));
        public static IList<CustomerRole> GetAllCustomerRoles(this ICustomerService service, bool showHidden = false) => Await(service.GetAllCustomerRolesAsync(showHidden));
        public static bool IsInCustomerRole(this ICustomerService service, Customer customer, string customerRoleSystemName, bool onlyActiveCustomerRoles = true) => Await(service.IsInCustomerRoleAsync(customer, customerRoleSystemName, onlyActiveCustomerRoles));
        public static IList<Address> GetAddressesByCustomerId(this ICustomerService service, int customerId) => Await(service.GetAddressesByCustomerIdAsync(customerId));
        public static Address GetCustomerShippingAddress(this ICustomerService service, Customer customer) => Await(service.GetCustomerShippingAddressAsync(customer));
        public static void InsertCustomer(this ICustomerService service, Customer customer) => Await(service.InsertCustomerAsync(customer));
        public static void UpdateCustomer(this ICustomerService service, Customer customer) => Await(service.UpdateCustomerAsync(customer));
        public static void DeleteCustomer(this ICustomerService service, Customer customer) => Await(service.DeleteCustomerAsync(customer));
        public static void InsertCustomerAddress(this ICustomerService service, Customer customer, Address address) => Await(service.InsertCustomerAddressAsync(customer, address));
        public static void AddCustomerRoleMapping(this ICustomerService service, CustomerCustomerRoleMapping roleMapping) => Await(service.AddCustomerRoleMappingAsync(roleMapping));
        public static void RemoveCustomerRoleMapping(this ICustomerService service, Customer customer, CustomerRole role) => Await(service.RemoveCustomerRoleMappingAsync(customer, role));
        public static void InsertCustomerPassword(this ICustomerService service, CustomerPassword customerPassword) => Await(service.InsertCustomerPasswordAsync(customerPassword));

        public static Product GetProductById(this IProductService service, int productId) => Await(service.GetProductByIdAsync(productId));
        public static IList<Product> GetProductsByIds(this IProductService service, int[] productIds) => Await(service.GetProductsByIdsAsync(productIds));
        public static IList<Product> GetAssociatedProducts(this IProductService service, int parentGroupedProductId, int storeId = 0, int vendorId = 0, bool showHidden = false) => Await(service.GetAssociatedProductsAsync(parentGroupedProductId, storeId, vendorId, showHidden));
        public static void InsertProduct(this IProductService service, Product product) => Await(service.InsertProductAsync(product));
        public static void UpdateProduct(this IProductService service, Product product) => Await(service.UpdateProductAsync(product));
        public static void DeleteProduct(this IProductService service, Product product) => Await(service.DeleteProductAsync(product));
        public static IList<ProductPicture> GetProductPicturesByProductId(this IProductService service, int productId) => Await(service.GetProductPicturesByProductIdAsync(productId));
        public static ProductPicture GetProductPictureById(this IProductService service, int productPictureId) => Await(service.GetProductPictureByIdAsync(productPictureId));
        public static void InsertProductPicture(this IProductService service, ProductPicture productPicture) => Await(service.InsertProductPictureAsync(productPicture));
        public static void UpdateProductPicture(this IProductService service, ProductPicture productPicture) => Await(service.UpdateProductPictureAsync(productPicture));
        public static void DeleteProductPicture(this IProductService service, ProductPicture productPicture) => Await(service.DeleteProductPictureAsync(productPicture));
        public static void UpdateHasDiscountsApplied(this IProductService service, Product product) => Await(service.UpdateProductAsync(product));

        public static Category GetCategoryById(this ICategoryService service, int categoryId) => Await(service.GetCategoryByIdAsync(categoryId));
        public static void InsertCategory(this ICategoryService service, Category category) => Await(service.InsertCategoryAsync(category));
        public static void UpdateCategory(this ICategoryService service, Category category) => Await(service.UpdateCategoryAsync(category));
        public static void DeleteCategory(this ICategoryService service, Category category) => Await(service.DeleteCategoryAsync(category));
        public static ProductCategory GetProductCategoryById(this ICategoryService service, int productCategoryId) => Await(service.GetProductCategoryByIdAsync(productCategoryId));
        public static void InsertProductCategory(this ICategoryService service, ProductCategory productCategory) => Await(service.InsertProductCategoryAsync(productCategory));
        public static void UpdateProductCategory(this ICategoryService service, ProductCategory productCategory) => Await(service.UpdateProductCategoryAsync(productCategory));
        public static void DeleteProductCategory(this ICategoryService service, ProductCategory productCategory) => Await(service.DeleteProductCategoryAsync(productCategory));

        public static Manufacturer GetManufacturerById(this IManufacturerService service, int manufacturerId) => Await(service.GetManufacturerByIdAsync(manufacturerId));
        public static void InsertManufacturer(this IManufacturerService service, Manufacturer manufacturer) => Await(service.InsertManufacturerAsync(manufacturer));
        public static void UpdateManufacturer(this IManufacturerService service, Manufacturer manufacturer) => Await(service.UpdateManufacturerAsync(manufacturer));
        public static void DeleteManufacturer(this IManufacturerService service, Manufacturer manufacturer) => Await(service.DeleteManufacturerAsync(manufacturer));
        public static IList<ProductManufacturer> GetProductManufacturersByProductId(this IManufacturerService service, int productId, bool showHidden = false) => Await(service.GetProductManufacturersByProductIdAsync(productId, showHidden));
        public static ProductManufacturer GetProductManufacturerById(this IManufacturerService service, int productManufacturerId) => Await(service.GetProductManufacturerByIdAsync(productManufacturerId));
        public static void InsertProductManufacturer(this IManufacturerService service, ProductManufacturer productManufacturer) => Await(service.InsertProductManufacturerAsync(productManufacturer));
        public static void UpdateProductManufacturer(this IManufacturerService service, ProductManufacturer productManufacturer) => Await(service.UpdateProductManufacturerAsync(productManufacturer));
        public static void DeleteProductManufacturer(this IManufacturerService service, ProductManufacturer productManufacturer) => Await(service.DeleteProductManufacturerAsync(productManufacturer));
        public static void InsertDiscountManufacturerMapping(this IManufacturerService service, DiscountManufacturerMapping mapping) => Await(service.InsertDiscountManufacturerMappingAsync(mapping));
        public static void DeleteDiscountManufacturerMapping(this IManufacturerService service, DiscountManufacturerMapping mapping) => Await(service.DeleteDiscountManufacturerMappingAsync(mapping));

        public static ProductAttribute GetProductAttributeById(this IProductAttributeService service, int productAttributeId) => Await(service.GetProductAttributeByIdAsync(productAttributeId));
        public static IList<ProductAttributeMapping> GetProductAttributeMappingsByProductId(this IProductAttributeService service, int productId) => Await(service.GetProductAttributeMappingsByProductIdAsync(productId));
        public static ProductAttributeMapping GetProductAttributeMappingById(this IProductAttributeService service, int productAttributeMappingId) => Await(service.GetProductAttributeMappingByIdAsync(productAttributeMappingId));
        public static IList<ProductAttributeValue> GetProductAttributeValues(this IProductAttributeService service, int productAttributeMappingId) => Await(service.GetProductAttributeValuesAsync(productAttributeMappingId));
        public static ProductAttributeValue GetProductAttributeValueById(this IProductAttributeService service, int productAttributeValueId) => Await(service.GetProductAttributeValueByIdAsync(productAttributeValueId));
        public static void InsertProductAttribute(this IProductAttributeService service, ProductAttribute productAttribute) => Await(service.InsertProductAttributeAsync(productAttribute));
        public static void UpdateProductAttribute(this IProductAttributeService service, ProductAttribute productAttribute) => Await(service.UpdateProductAttributeAsync(productAttribute));
        public static void DeleteProductAttribute(this IProductAttributeService service, ProductAttribute productAttribute) => Await(service.DeleteProductAttributeAsync(productAttribute));
        public static void InsertProductAttributeMapping(this IProductAttributeService service, ProductAttributeMapping mapping) => Await(service.InsertProductAttributeMappingAsync(mapping));
        public static void UpdateProductAttributeMapping(this IProductAttributeService service, ProductAttributeMapping mapping) => Await(service.UpdateProductAttributeMappingAsync(mapping));
        public static void DeleteProductAttributeMapping(this IProductAttributeService service, ProductAttributeMapping mapping) => Await(service.DeleteProductAttributeMappingAsync(mapping));
        public static void InsertProductAttributeValue(this IProductAttributeService service, ProductAttributeValue value) => Await(service.InsertProductAttributeValueAsync(value));
        public static void UpdateProductAttributeValue(this IProductAttributeService service, ProductAttributeValue value) => Await(service.UpdateProductAttributeValueAsync(value));
        public static void DeleteProductAttributeValue(this IProductAttributeService service, ProductAttributeValue value) => Await(service.DeleteProductAttributeValueAsync(value));

        public static IPagedList<SpecificationAttribute> GetSpecificationAttributes(this ISpecificationAttributeService service, int pageIndex = 0, int pageSize = int.MaxValue) => Await(service.GetAllSpecificationAttributesAsync(pageIndex, pageSize));
        public static SpecificationAttribute GetSpecificationAttributeById(this ISpecificationAttributeService service, int specificationAttributeId) => Await(service.GetSpecificationAttributeByIdAsync(specificationAttributeId));
        public static void InsertSpecificationAttribute(this ISpecificationAttributeService service, SpecificationAttribute specificationAttribute) => Await(service.InsertSpecificationAttributeAsync(specificationAttribute));
        public static void UpdateSpecificationAttribute(this ISpecificationAttributeService service, SpecificationAttribute specificationAttribute) => Await(service.UpdateSpecificationAttributeAsync(specificationAttribute));
        public static void DeleteSpecificationAttribute(this ISpecificationAttributeService service, SpecificationAttribute specificationAttribute) => Await(service.DeleteSpecificationAttributeAsync(specificationAttribute));
        public static IList<ProductSpecificationAttribute> GetProductSpecificationAttributes(this ISpecificationAttributeService service, int productId = 0, int specificationAttributeOptionId = 0, bool? allowFiltering = null, bool? showOnProductPage = null, int? specificationAttributeGroupId = 0) => Await(service.GetProductSpecificationAttributesAsync(productId, specificationAttributeOptionId, allowFiltering, showOnProductPage, specificationAttributeGroupId));
        public static ProductSpecificationAttribute GetProductSpecificationAttributeById(this ISpecificationAttributeService service, int productSpecificationAttributeId) => Await(service.GetProductSpecificationAttributeByIdAsync(productSpecificationAttributeId));
        public static int GetProductSpecificationAttributeCount(this ISpecificationAttributeService service, int productId = 0, int specificationAttributeOptionId = 0) => Await(service.GetProductSpecificationAttributeCountAsync(productId, specificationAttributeOptionId));
        public static void InsertProductSpecificationAttribute(this ISpecificationAttributeService service, ProductSpecificationAttribute productSpecificationAttribute) => Await(service.InsertProductSpecificationAttributeAsync(productSpecificationAttribute));
        public static void UpdateProductSpecificationAttribute(this ISpecificationAttributeService service, ProductSpecificationAttribute productSpecificationAttribute) => Await(service.UpdateProductSpecificationAttributeAsync(productSpecificationAttribute));
        public static void DeleteProductSpecificationAttribute(this ISpecificationAttributeService service, ProductSpecificationAttribute productSpecificationAttribute) => Await(service.DeleteProductSpecificationAttributeAsync(productSpecificationAttribute));

        public static Picture InsertPicture(this IPictureService service, byte[] pictureBinary, string mimeType, string seoFilename, string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true) => Await(service.InsertPictureAsync(pictureBinary, mimeType, seoFilename, altAttribute, titleAttribute, isNew, validateBinary));
        public static Picture GetPictureById(this IPictureService service, int pictureId) => Await(service.GetPictureByIdAsync(pictureId));
        public static void DeletePicture(this IPictureService service, Picture picture) => Await(service.DeletePictureAsync(picture));
        public static byte[] ValidatePicture(this IPictureService service, byte[] pictureBinary, string mimeType, string fileName = null) => Await(service.ValidatePictureAsync(pictureBinary, mimeType, fileName));
        public static string GetPictureUrl(this IPictureService service, int pictureId, int targetSize = 0, bool showDefaultPicture = true, string storeLocation = null, PictureType defaultPictureType = PictureType.Entity) => Await(service.GetPictureUrlAsync(pictureId, targetSize, showDefaultPicture, storeLocation, defaultPictureType));
        public static string GetPictureSeName(this IPictureService service, string name) => Await(service.GetPictureSeNameAsync(name));
        public static Picture SetSeoFilename(this IPictureService service, int pictureId, string seoFilename) => Await(service.SetSeoFilenameAsync(pictureId, seoFilename));

        public static string ValidateSeName<T>(this IUrlRecordService service, T entity, string seName, string name, bool ensureNotEmpty) where T : BaseEntity, ISlugSupported => Await(service.ValidateSeNameAsync(entity, seName, name, ensureNotEmpty));
        public static void SaveSlug<T>(this IUrlRecordService service, T entity, string slug, int languageId) where T : BaseEntity, ISlugSupported => Await(service.SaveSlugAsync(entity, slug, languageId));
        public static string GetSeName<T>(this IUrlRecordService service, T entity, int? languageId = null, bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true) where T : BaseEntity, ISlugSupported => Await(service.GetSeNameAsync(entity, languageId, returnDefaultValue, ensureTwoPublishedLanguages));

        public static string GetResource(this ILocalizationService service, string resourceKey) => Await(service.GetResourceAsync(resourceKey));
        public static string GetResource(this ILocalizationService service, string resourceKey, int languageId, bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false) => Await(service.GetResourceAsync(resourceKey, languageId, logIfNotFound, defaultValue, returnEmptyIfNotFound));
        public static TPropType GetLocalized<TEntity, TPropType>(this ILocalizationService service, TEntity entity, Expression<Func<TEntity, TPropType>> keySelector, int? languageId = null, bool returnDefaultValue = true, bool ensureTwoPublishedLanguages = true) where TEntity : BaseEntity, ILocalizedEntity => Await(service.GetLocalizedAsync(entity, keySelector, languageId, returnDefaultValue, ensureTwoPublishedLanguages));

        public static IList<AclRecord> GetAclRecords<TEntity>(this IAclService service, TEntity entity) where TEntity : BaseEntity, IAclSupported => Await(service.GetAclRecordsAsync(entity));
        public static void InsertAclRecord<TEntity>(this IAclService service, TEntity entity, int customerRoleId) where TEntity : BaseEntity, IAclSupported => Await(service.InsertAclRecordAsync(entity, customerRoleId));
        public static void DeleteAclRecord(this IAclService service, AclRecord aclRecord) => Await(service.DeleteAclRecordAsync(aclRecord));

        public static bool Authorize<TEntity>(this IStoreMappingService service, TEntity entity) where TEntity : BaseEntity, IStoreMappingSupported => service.Authorize(entity, 0);
        public static IList<StoreMapping> GetStoreMappings<TEntity>(this IStoreMappingService service, TEntity entity) where TEntity : BaseEntity, IStoreMappingSupported => Await(service.GetStoreMappingsAsync(entity));
        public static void InsertStoreMapping<TEntity>(this IStoreMappingService service, TEntity entity, int storeId) where TEntity : BaseEntity, IStoreMappingSupported => Await(service.InsertStoreMappingAsync(entity, storeId));
        public static void DeleteStoreMapping(this IStoreMappingService service, StoreMapping storeMapping) => Await(service.DeleteStoreMappingAsync(storeMapping));

        public static IList<Discount> GetAllDiscounts(this IDiscountService service, DiscountType? discountType = null, string couponCode = null, string discountName = null, bool showHidden = false, DateTime? startDateUtc = null, DateTime? endDateUtc = null, bool? isActive = true, int vendorId = 0) => Await(service.GetAllDiscountsAsync(discountType, couponCode, discountName, showHidden, startDateUtc, endDateUtc, isActive, vendorId));
        public static IList<Discount> GetAppliedDiscounts<T>(this IDiscountService service, IDiscountSupported<T> entity) where T : DiscountMapping => Await(service.GetAppliedDiscountsAsync(entity));

        public static IList<OrderItem> GetOrderItems(this IOrderService service, int orderId) => Await(service.GetOrderItemsAsync(orderId));
        public static OrderItem GetOrderItemById(this IOrderService service, int orderItemId) => Await(service.GetOrderItemByIdAsync(orderItemId));
        public static void InsertOrderItem(this IOrderService service, OrderItem orderItem) => Await(service.InsertOrderItemAsync(orderItem));
        public static void DeleteOrderItem(this IOrderService service, OrderItem orderItem) => Await(service.DeleteOrderItemAsync(orderItem));
        public static void UpdateOrder(this IOrderService service, Order order) => Await(service.UpdateOrderAsync(order));
        public static void DeleteOrder(this IOrderProcessingService service, Order order) => Await(service.DeleteOrderAsync(order));
        public static PlaceOrderResult PlaceOrder(this IOrderProcessingService service, ProcessPaymentRequest processPaymentRequest) => Await(service.PlaceOrderAsync(processPaymentRequest));
        public static IList<Shipment> GetShipmentsByOrderId(this IShipmentService service, int orderId, bool shipped = false) => Await(service.GetShipmentsByOrderIdAsync(orderId, shipped));

        public static IList<ShoppingCartItem> GetShoppingCart(this IShoppingCartService service, Customer customer, ShoppingCartType? shoppingCartType = null, int storeId = 0, int? productId = null, DateTime? createdFromUtc = null, DateTime? createdToUtc = null, int? customWishlistId = null) => Await(service.GetShoppingCartAsync(customer, shoppingCartType, storeId, productId, createdFromUtc, createdToUtc, customWishlistId));
        public static IList<string> AddToCart(this IShoppingCartService service, Customer customer, Product product, ShoppingCartType shoppingCartType, int storeId, string attributesXml = null, decimal customerEnteredPrice = decimal.Zero, DateTime? rentalStartDate = null, DateTime? rentalEndDate = null, int quantity = 1, bool addRequiredProducts = true, int? wishlistId = null) => Await(service.AddToCartAsync(customer, product, shoppingCartType, storeId, attributesXml, customerEnteredPrice, rentalStartDate, rentalEndDate, quantity, addRequiredProducts, wishlistId));
        public static IList<string> UpdateShoppingCartItem(this IShoppingCartService service, Customer customer, int shoppingCartItemId, string attributesXml, decimal customerEnteredPrice, DateTime? rentalStartDate = null, DateTime? rentalEndDate = null, int quantity = 1, bool resetCheckoutData = true) => Await(service.UpdateShoppingCartItemAsync(customer, shoppingCartItemId, attributesXml, customerEnteredPrice, rentalStartDate, rentalEndDate, quantity, resetCheckoutData));
        public static void DeleteShoppingCartItem(this IShoppingCartService service, ShoppingCartItem shoppingCartItem, bool resetCheckoutData = true, bool ensureOnlyActiveCheckoutAttributes = false) => Await(service.DeleteShoppingCartItemAsync(shoppingCartItem, resetCheckoutData, ensureOnlyActiveCheckoutAttributes));

        public static GetShippingOptionResponse GetShippingOptions(this IShippingService service, IList<ShoppingCartItem> cart, Address shippingAddress, Customer customer = null, string allowedShippingRateComputationMethodSystemName = "", int storeId = 0) => Await(service.GetShippingOptionsAsync(cart, shippingAddress, customer, allowedShippingRateComputationMethodSystemName, storeId));
        public static decimal GetFinalPrice(this IPriceCalculationService service, Product product, Customer customer, decimal additionalCharge = 0, bool includeDiscounts = true, int quantity = 1) => Await(service.GetFinalPriceAsync(product, customer, null, additionalCharge, includeDiscounts, quantity)).finalPrice;
        public static decimal GetProductCost(this IPriceCalculationService service, Product product, string attributesXml) => Await(service.GetProductCostAsync(product, attributesXml));
        public static decimal GetProductPrice(this ITaxService service, Product product, decimal price, bool includingTax, Customer customer, out decimal taxRate)
        {
            var result = Await(service.GetProductPriceAsync(product, price, includingTax, customer));
            taxRate = result.taxRate;
            return result.price;
        }

        public static Download GetDownloadByGuid(this IDownloadService service, Guid downloadGuid) => Await(service.GetDownloadByGuidAsync(downloadGuid));
        public static Address GetAddressById(this IAddressService service, int addressId) => Await(service.GetAddressByIdAsync(addressId));
        public static IList<GenericAttribute> GetAttributesForEntity(this IGenericAttributeService service, int entityId, string keyGroup) => Await(service.GetAttributesForEntityAsync(entityId, keyGroup));
        public static void SaveAttribute<TPropType>(this IGenericAttributeService service, BaseEntity entity, string key, TPropType value, int storeId = 0) => Await(service.SaveAttributeAsync(entity, key, value, storeId));
        public static NewsLetterSubscription GetNewsLetterSubscriptionByEmailAndStoreId(this INewsLetterSubscriptionService service, string email, int storeId) => Await(service.GetNewsLetterSubscriptionsByEmailAsync(email, storeId)).FirstOrDefault();
        public static void UpdateNewsLetterSubscription(this INewsLetterSubscriptionService service, NewsLetterSubscription subscription) => Await(service.UpdateNewsLetterSubscriptionAsync(subscription));
        public static void DeleteNewsLetterSubscription(this INewsLetterSubscriptionService service, NewsLetterSubscription subscription) => Await(service.DeleteNewsLetterSubscriptionAsync(subscription));
        public static CustomerInfoModel PrepareCustomerInfoModel(this ICustomerModelFactory service, CustomerInfoModel model, Customer customer, bool excludeProperties, string overrideCustomCustomerAttributesXml = "") => Await(service.PrepareCustomerInfoModelAsync(model, customer, excludeProperties, overrideCustomCustomerAttributesXml));

        public static IList<ProductTag> GetAllProductTagsByProductId(this IProductTagService service, int productId) => Await(service.GetAllProductTagsByProductIdAsync(productId));
        public static ProductTag GetProductTagByName(this IProductTagService service, string name) => Await(service.GetAllProductTagsAsync(name)).FirstOrDefault();
        public static void UpdateProductTags(this IProductTagService service, Product product, string[] productTags) => Await(service.UpdateProductTagsAsync(product, productTags));
        public static void InsertProductProductTagMapping(this IProductTagService service, ProductProductTagMapping mapping) => Await(service.InsertProductProductTagMappingAsync(mapping));
        public static void InsertProductTag(this IProductTagService service, ProductTag productTag)
        {
            var method = service.GetType().GetMethod("InsertProductTagAsync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (method?.Invoke(service, new object[] { productTag }) is Task task)
                Await(task);
        }
    }
}
