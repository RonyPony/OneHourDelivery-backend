using Nop.Core;
using Nop.Core.Domain.Affiliates;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.ScheduleTasks;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Services.ScheduleTasks;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Plugin.Tax.FixedOrByCountryStateZip.Domain;
using Nop.Plugin.Tax.FixedOrByCountryStateZip.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Discounts;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Models.Home;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Areas.Admin.Models.Reports;
using Nop.Web.Areas.Admin.Models.Shipping;
using System.Xml;
using System.Reflection;

namespace Nop.Plugin.Api.Compatibility
{
    public static class ServiceSyncCompatibilityExtensions
    {
        public static T Await<T>(this Task<T> task) => task.GetAwaiter().GetResult();
        public static void Await(this Task task) => task.GetAwaiter().GetResult();

        public static void AddOrUpdatePluginLocaleResource(this ILocalizationService service, string resourceName, string resourceValue, string languageCulture = null)
            => service.AddOrUpdateLocaleResourceAsync(resourceName, resourceValue, languageCulture).Await();

        public static void DeletePluginLocaleResources(this ILocalizationService service, string resourceNamePrefix)
            => service.DeleteLocaleResourcesAsync(resourceNamePrefix).Await();

        public static string GetLocalizedEnum<TEnum>(this ILocalizationService service, TEnum enumValue, int? languageId = null) where TEnum : struct
            => service.GetLocalizedEnumAsync(enumValue, languageId).Await();

        public static ScheduleTask GetTaskByType(this IScheduleTaskService service, string type)
            => service.GetTaskByTypeAsync(type).Await();

        public static void InsertTask(this IScheduleTaskService service, ScheduleTask task)
            => service.InsertTaskAsync(task).Await();

        public static void DeleteTask(this IScheduleTaskService service, ScheduleTask task)
            => service.DeleteTaskAsync(task).Await();

        public static TPropType GetAttribute<TPropType>(this IGenericAttributeService service, BaseEntity entity, string key, int storeId = 0, TPropType defaultValue = default)
            => service.GetAttributeAsync(entity, key, storeId, defaultValue).Await();

        public static TPropType GetAttribute<TEntity, TPropType>(this IGenericAttributeService service, int entityId, string key, int storeId = 0, TPropType defaultValue = default)
            where TEntity : BaseEntity
            => service.GetAttributeAsync<TEntity, TPropType>(entityId, key, storeId, defaultValue).Await();

        public static void UpdateAttribute(this IGenericAttributeService service, GenericAttribute attribute)
            => service.UpdateAttributeAsync(attribute).Await();

        public static DateTime ConvertToUserTime(this IDateTimeHelper helper, DateTime dateTime)
            => helper.ConvertToUserTimeAsync(dateTime).Await();

        public static DateTime ConvertToUserTime(this IDateTimeHelper helper, DateTime dateTime, DateTimeKind sourceDateTimeKind)
            => helper.ConvertToUserTimeAsync(dateTime, sourceDateTimeKind).Await();

        public static void ErrorNotification(this INotificationService service, Exception exception, bool logException = true, int timeout = 0)
            => service.ErrorNotificationAsync(exception, logException, timeout).Await();

        public static string FormatPrice(this IPriceFormatter formatter, decimal price)
            => formatter.FormatPriceAsync(price).Await();

        public static string FormatPrice(this IPriceFormatter formatter, decimal price, bool showCurrency, bool showTax)
            => formatter.FormatPriceAsync(price, showCurrency, showTax).Await();

        public static string FormatPrice(this IPriceFormatter formatter, decimal price, bool showCurrency, Currency targetCurrency)
            => formatter.FormatPriceAsync(price, showCurrency, targetCurrency).Await();

        public static string FormatPrice(this IPriceFormatter formatter, decimal price, bool showCurrency, Currency targetCurrency, int languageId, bool priceIncludesTax)
            => formatter.FormatPriceAsync(price, showCurrency, targetCurrency, languageId, priceIncludesTax).Await();

        public static string FormatPrice(this IPriceFormatter formatter, decimal price, bool showCurrency, Currency targetCurrency, int languageId, bool priceIncludesTax, bool showTax)
            => formatter.FormatPriceAsync(price, showCurrency, targetCurrency, languageId, priceIncludesTax, showTax).Await();

        public static string FormatShippingPrice(this IPriceFormatter formatter, decimal price, bool showCurrency)
            => formatter.FormatShippingPriceAsync(price, showCurrency).Await();

        public static string FormatShippingPrice(this IPriceFormatter formatter, decimal price, bool showCurrency, Currency targetCurrency, int languageId, bool priceIncludesTax)
            => formatter.FormatShippingPriceAsync(price, showCurrency, targetCurrency, languageId, priceIncludesTax).Await();

        public static string FormatPaymentMethodAdditionalFee(this IPriceFormatter formatter, decimal price, bool showCurrency)
            => formatter.FormatPaymentMethodAdditionalFeeAsync(price, showCurrency).Await();

        public static string FormatPaymentMethodAdditionalFee(this IPriceFormatter formatter, decimal price, bool showCurrency, Currency targetCurrency, int languageId, bool priceIncludesTax)
            => formatter.FormatPaymentMethodAdditionalFeeAsync(price, showCurrency, targetCurrency, languageId, priceIncludesTax).Await();

        public static IPagedList<ProductAttribute> GetAllProductAttributes(this IProductAttributeService service, string name = null, int pageIndex = 0, int pageSize = int.MaxValue)
            => service.GetAllProductAttributesAsync(name, pageIndex, pageSize).Await();

        public static IList<SpecificationAttributeOption> GetSpecificationAttributeOptionsBySpecificationAttribute(this ISpecificationAttributeService service, int specificationAttributeId)
            => service.GetSpecificationAttributeOptionsBySpecificationAttributeAsync(specificationAttributeId).Await();

        public static void InsertSpecificationAttributeOption(this ISpecificationAttributeService service, SpecificationAttributeOption option)
            => service.InsertSpecificationAttributeOptionAsync(option).Await();

        public static void InsertCustomerRole(this ICustomerService service, CustomerRole customerRole)
            => service.InsertCustomerRoleAsync(customerRole).Await();

        public static void UpdateCustomerRole(this ICustomerService service, CustomerRole customerRole)
            => service.UpdateCustomerRoleAsync(customerRole).Await();

        public static bool IsRegistered(this ICustomerService service, Customer customer, bool onlyActiveCustomerRoles = true)
            => service.IsRegisteredAsync(customer, onlyActiveCustomerRoles).Await();

        public static Customer InsertGuestCustomer(this ICustomerService service)
        {
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow
            };
            service.InsertCustomerAsync(customer).Await();
            return customer;
        }

        public static string GetCustomerFullName(this ICustomerService service, Customer customer)
        {
            if (customer is null)
                return string.Empty;

            var genericAttributeService = Nop.Core.Infrastructure.EngineContext.Current.Resolve<IGenericAttributeService>();
            var firstName = genericAttributeService.GetAttribute<string>(customer, "FirstName");
            var lastName = genericAttributeService.GetAttribute<string>(customer, "LastName");
            return $"{firstName} {lastName}".Trim();
        }

        public static CustomerRegistrationResult RegisterCustomer(this ICustomerRegistrationService service, CustomerRegistrationRequest request)
            => service.RegisterCustomerAsync(request).Await();

        public static ChangePasswordResult ChangePassword(this ICustomerRegistrationService service, ChangePasswordRequest request)
            => service.ChangePasswordAsync(request).Await();

        public static Order GetOrderById(this IOrderService service, int orderId)
            => service.GetOrderByIdAsync(orderId).Await();

        public static OrderItem GetOrderItemById(this IOrderService service, int orderItemId)
            => service.GetOrderItemByIdAsync(orderItemId).Await();

        public static IList<OrderItem> GetOrderItems(this IOrderService service, int orderId, bool? isNotReturnable = null, bool? isShipEnabled = null, int vendorId = 0)
            => service.GetOrderItemsAsync(orderId, isNotReturnable, isShipEnabled, vendorId).Await();

        public static IPagedList<Order> SearchOrders(this IOrderService service, int storeId = 0, int vendorId = 0, int customerId = 0,
            int productId = 0, int affiliateId = 0, int warehouseId = 0, int billingCountryId = 0, string paymentMethodSystemName = null,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null, List<int> osIds = null, List<int> psIds = null, List<int> ssIds = null,
            string billingPhone = null, string billingEmail = null, string billingLastName = "", string orderNotes = null,
            int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
            => service.SearchOrdersAsync(storeId, vendorId, customerId, productId, affiliateId, warehouseId, billingCountryId, paymentMethodSystemName,
                createdFromUtc, createdToUtc, osIds, psIds, ssIds, billingPhone, billingEmail, billingLastName, orderNotes, pageIndex, pageSize, getOnlyTotalCount).Await();

        public static OrderNote GetOrderNoteById(this IOrderService service, int orderNoteId)
            => service.GetOrderNoteByIdAsync(orderNoteId).Await();

        public static IList<OrderNote> GetOrderNotesByOrderId(this IOrderService service, int orderId, bool? displayToCustomer = null)
            => service.GetOrderNotesByOrderIdAsync(orderId, displayToCustomer).Await();

        public static void InsertOrderNote(this IOrderService service, OrderNote orderNote)
            => service.InsertOrderNoteAsync(orderNote).Await();

        public static void DeleteOrderNote(this IOrderService service, OrderNote orderNote)
            => service.DeleteOrderNoteAsync(orderNote).Await();

        public static void InsertDiscount(this IDiscountService service, Discount discount)
            => service.InsertDiscountAsync(discount).Await();

        public static void UpdateDiscount(this IDiscountService service, Discount discount)
            => service.UpdateDiscountAsync(discount).Await();

        public static void DeleteDiscount(this IDiscountService service, Discount discount)
            => service.DeleteDiscountAsync(discount).Await();

        public static Discount GetDiscountById(this IDiscountService service, int discountId)
            => service.GetDiscountByIdAsync(discountId).Await();

        public static IList<DiscountRequirement> GetAllDiscountRequirements(this IDiscountService service, int discountId = 0, bool topLevelOnly = false)
            => service.GetAllDiscountRequirementsAsync(discountId, topLevelOnly).Await();

        public static DiscountRequirement GetDiscountRequirementById(this IDiscountService service, int discountRequirementId)
            => service.GetDiscountRequirementByIdAsync(discountRequirementId).Await();

        public static void InsertDiscountRequirement(this IDiscountService service, DiscountRequirement discountRequirement)
            => service.InsertDiscountRequirementAsync(discountRequirement).Await();

        public static void UpdateDiscountRequirement(this IDiscountService service, DiscountRequirement discountRequirement)
            => service.UpdateDiscountRequirementAsync(discountRequirement).Await();

        public static void DeleteDiscountRequirement(this IDiscountService service, DiscountRequirement discountRequirement, bool recursively = false)
            => service.DeleteDiscountRequirementAsync(discountRequirement, recursively).Await();

        public static IPagedList<DiscountUsageHistory> GetAllDiscountUsageHistory(this IDiscountService service, int? discountId = null,
            int? customerId = null, int? orderId = null, bool includeCancelledOrders = true, int pageIndex = 0, int pageSize = int.MaxValue)
            => service.GetAllDiscountUsageHistoryAsync(discountId, customerId, orderId, includeCancelledOrders, pageIndex, pageSize).Await();

        public static void ClearDiscountProductMapping(this IProductService service, Discount discount)
            => service.ClearDiscountProductMappingAsync(discount).Await();

        public static IPagedList<Product> GetProductsWithAppliedDiscount(this IProductService service, int? discountId = null,
            bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
            => service.GetProductsWithAppliedDiscountAsync(discountId, showHidden, pageIndex, pageSize).Await();

        public static DiscountProductMapping GetDiscountAppliedToProduct(this IProductService service, int productId, int discountId)
            => service.GetDiscountAppliedToProductAsync(productId, discountId).Await();

        public static string FormatSku(this IProductService service, Product product, string attributesXml = null)
            => service.FormatSkuAsync(product, attributesXml).Await();

        public static IList<DiscountProductMapping> GetAllDiscountsAppliedToProduct(this IProductService service, int productId)
            => service.GetAllDiscountsAppliedToProductAsync(productId).Await();

        public static void ClearDiscountCategoryMapping(this ICategoryService service, Discount discount)
            => service.ClearDiscountCategoryMappingAsync(discount).Await();

        public static DiscountCategoryMapping GetDiscountAppliedToCategory(this ICategoryService service, int categoryId, int discountId)
            => service.GetDiscountAppliedToCategoryAsync(categoryId, discountId).Await();

        public static void InsertDiscountCategoryMapping(this ICategoryService service, DiscountCategoryMapping mapping)
            => service.InsertDiscountCategoryMappingAsync(mapping).Await();

        public static void DeleteDiscountCategoryMapping(this ICategoryService service, DiscountCategoryMapping mapping)
            => service.DeleteDiscountCategoryMappingAsync(mapping).Await();

        public static IList<ProductCategory> GetProductCategoriesByProductId(this ICategoryService service, int productId, bool showHidden = false)
            => service.GetProductCategoriesByProductIdAsync(productId, showHidden).Await();

        public static string GetFormattedBreadCrumb(this ICategoryService service, Category category, IList<Category> allCategories = null, string separator = ">>", int languageId = 0)
            => service.GetFormattedBreadCrumbAsync(category, allCategories, separator, languageId).Await();

        public static void ClearDiscountManufacturerMapping(this IManufacturerService service, Discount discount)
            => service.ClearDiscountManufacturerMappingAsync(discount).Await();

        public static DiscountManufacturerMapping GetDiscountAppliedToManufacturer(this IManufacturerService service, int manufacturerId, int discountId)
            => service.GetDiscountAppliedToManufacturerAsync(manufacturerId, discountId).Await();

        public static Store GetStoreById(this IStoreService service, int storeId)
            => service.GetStoreByIdAsync(storeId).Await();

        public static Country GetCountryByAddress(this ICountryService service, Address address)
            => address is null ? null : service.GetCountryByIdAsync(address.CountryId ?? 0).Await();

        public static void InsertActivity(this ICustomerActivityService service, string systemKeyword, string comment, BaseEntity entity = null)
            => service.InsertActivityAsync(systemKeyword, comment, entity).Await();

        public static void InsertActivity(this ICustomerActivityService service, string systemKeyword, string comment, params object[] commentParams)
            => service.InsertActivityAsync(systemKeyword, string.Format(comment, commentParams)).Await();

        public static void InsertAddress(this IAddressService service, Address address)
            => service.InsertAddressAsync(address).Await();

        public static void UpdateAddress(this IAddressService service, Address address)
            => service.UpdateAddressAsync(address).Await();

        public static Affiliate GetAffiliateById(this IAffiliateService service, int affiliateId)
            => service.GetAffiliateByIdAsync(affiliateId).Await();

        public static string GetAffiliateFullName(this IAffiliateService service, Affiliate affiliate)
            => affiliate is null ? string.Empty : service.GetAffiliateFullNameAsync(affiliate).Await();

        public static Download GetDownloadById(this IDownloadService service, int downloadId)
            => service.GetDownloadByIdAsync(downloadId).Await();

        public static byte[] GetDownloadBits(this IDownloadService service, IFormFile file)
            => service.GetDownloadBitsAsync(file).Await();

        public static IList<ShipmentItem> GetShipmentItemsByShipmentId(this IShipmentService service, int shipmentId)
            => service.GetShipmentItemsByShipmentIdAsync(shipmentId).Await();

        public static void UpdateShipment(this IShipmentService service, Shipment shipment)
            => service.UpdateShipmentAsync(shipment).Await();

        public static void InsertShipment(this IShipmentService service, Shipment shipment)
            => service.InsertShipmentAsync(shipment).Await();

        public static void InsertShipmentItem(this IShipmentService service, ShipmentItem shipmentItem)
            => service.InsertShipmentItemAsync(shipmentItem).Await();

        public static void InsertDiscountProductMapping(this IProductService service, DiscountProductMapping mapping)
            => service.InsertDiscountProductMappingAsync(mapping).Await();

        public static void DeleteDiscountProductMapping(this IProductService service, DiscountProductMapping mapping)
            => service.DeleteDiscountProductMappingAsync(mapping).Await();

        public static DiscountUsageHistory GetDiscountUsageHistoryById(this IDiscountService service, int discountUsageHistoryId)
            => service.GetDiscountUsageHistoryByIdAsync(discountUsageHistoryId).Await();

        public static void DeleteDiscountUsageHistory(this IDiscountService service, DiscountUsageHistory discountUsageHistory)
            => service.DeleteDiscountUsageHistoryAsync(discountUsageHistory).Await();

        public static IList<Customer> GetCustomersByIds(this ICustomerService service, int[] customerIds)
            => service.GetCustomersByIdsAsync(customerIds).Await();

        public static void ApplyDiscountCouponCode(this ICustomerService service, Customer customer, string couponCode)
            => service.ApplyDiscountCouponCodeAsync(customer, couponCode).Await();

        public static IPagedList<Customer> GetAllCustomers(this ICustomerService service,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
            int[] customerRoleIds = null, string email = null, string username = null, string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0, string company = null, string phone = null, string zipPostalCode = null,
            string ipAddress = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
            => service.GetAllCustomersAsync(createdFromUtc, createdToUtc, null, null, affiliateId, vendorId, customerRoleIds, email, username,
                firstName, lastName, dayOfBirth, monthOfBirth, company, phone, zipPostalCode, ipAddress, null, pageIndex, pageSize, getOnlyTotalCount).Await();

        public static TPlugin LoadPluginBySystemName<TPlugin>(this IPluginManager<TPlugin> manager, string systemName, Customer customer = null, int storeId = 0)
            where TPlugin : class, IPlugin
            => manager.LoadPluginBySystemNameAsync(systemName, customer, storeId).Await();

        public static IList<TPlugin> LoadAllPlugins<TPlugin>(this IPluginManager<TPlugin> manager, Customer customer = null, int storeId = 0)
            where TPlugin : class, IPlugin
            => manager.LoadAllPluginsAsync(customer, storeId).Await();

        public static IList<DiscountRequirementRuleModel> PrepareDiscountRequirementRuleModels(this IDiscountModelFactory factory,
            ICollection<DiscountRequirement> requirements, Discount discount, RequirementGroupInteractionType groupInteractionType)
            => factory.PrepareDiscountRequirementRuleModelsAsync(requirements, discount, groupInteractionType).Await();

        public static DiscountProductListModel PrepareDiscountProductListModel(this IDiscountModelFactory factory, DiscountProductSearchModel searchModel, Discount discount)
            => factory.PrepareDiscountProductListModelAsync(searchModel, discount).Await();

        public static AddProductToDiscountListModel PrepareAddProductToDiscountListModel(this IDiscountModelFactory factory, AddProductToDiscountSearchModel searchModel)
            => factory.PrepareAddProductToDiscountListModelAsync(searchModel).Await();

        public static DiscountCategoryListModel PrepareDiscountCategoryListModel(this IDiscountModelFactory factory, DiscountCategorySearchModel searchModel, Discount discount)
            => factory.PrepareDiscountCategoryListModelAsync(searchModel, discount).Await();

        public static AddCategoryToDiscountSearchModel PrepareAddCategoryToDiscountSearchModel(this IDiscountModelFactory factory, AddCategoryToDiscountSearchModel searchModel)
            => factory.PrepareAddCategoryToDiscountSearchModelAsync(searchModel).Await();

        public static AddCategoryToDiscountListModel PrepareAddCategoryToDiscountListModel(this IDiscountModelFactory factory, AddCategoryToDiscountSearchModel searchModel)
            => factory.PrepareAddCategoryToDiscountListModelAsync(searchModel).Await();

        public static DiscountManufacturerListModel PrepareDiscountManufacturerListModel(this IDiscountModelFactory factory, DiscountManufacturerSearchModel searchModel, Discount discount)
            => factory.PrepareDiscountManufacturerListModelAsync(searchModel, discount).Await();

        public static AddManufacturerToDiscountSearchModel PrepareAddManufacturerToDiscountSearchModel(this IDiscountModelFactory factory, AddManufacturerToDiscountSearchModel searchModel)
            => factory.PrepareAddManufacturerToDiscountSearchModelAsync(searchModel).Await();

        public static AddManufacturerToDiscountListModel PrepareAddManufacturerToDiscountListModel(this IDiscountModelFactory factory, AddManufacturerToDiscountSearchModel searchModel)
            => factory.PrepareAddManufacturerToDiscountListModelAsync(searchModel).Await();

        public static DiscountUsageHistoryListModel PrepareDiscountUsageHistoryListModel(this IDiscountModelFactory factory, DiscountUsageHistorySearchModel searchModel, Discount discount)
            => factory.PrepareDiscountUsageHistoryListModelAsync(searchModel, discount).Await();

        public static void PrepareVendors(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareVendorsAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareCategories(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareCategoriesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareManufacturers(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareManufacturersAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareStores(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareStoresAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareProductTypes(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareProductTypesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareOrderStatuses(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareOrderStatusesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PreparePaymentStatuses(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PreparePaymentStatusesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareShippingStatuses(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareShippingStatusesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareWarehouses(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareWarehousesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareCountries(this IBaseAdminModelFactory factory, IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareCountriesAsync(items, withSpecialDefaultItem, defaultItemText).Await();

        public static void PrepareStatesAndProvinces(this IBaseAdminModelFactory factory, IList<SelectListItem> items, int? countryId,
            bool withSpecialDefaultItem = true, string defaultItemText = null)
            => factory.PrepareStatesAndProvincesAsync(items, countryId, withSpecialDefaultItem, defaultItemText).Await();

        public static IList<SystemWarningModel> PrepareSystemWarningModels(this ICommonModelFactory factory)
            => factory.PrepareSystemWarningModelsAsync().Await();

        public static DashboardModel PrepareDashboardModel(this IHomeModelFactory factory, DashboardModel model)
            => factory.PrepareDashboardModelAsync(model).Await();

        public static OrderNoteListModel PrepareOrderNoteListModel(this IOrderModelFactory factory, OrderNoteSearchModel searchModel, Order order)
            => factory.PrepareOrderNoteListModelAsync(searchModel, order).Await();

        public static OrderShipmentListModel PrepareOrderShipmentListModel(this IOrderModelFactory factory, OrderShipmentSearchModel searchModel, Order order)
            => factory.PrepareOrderShipmentListModelAsync(searchModel, order).Await();

        public static OrderAverageReportListModel PrepareOrderAverageReportListModel(this IOrderModelFactory factory, OrderAverageReportSearchModel searchModel)
            => factory.PrepareOrderAverageReportListModelAsync(searchModel).Await();

        public static OrderIncompleteReportListModel PrepareOrderIncompleteReportListModel(this IOrderModelFactory factory, OrderIncompleteReportSearchModel searchModel)
            => factory.PrepareOrderIncompleteReportListModelAsync(searchModel).Await();

        public static void InsertTaxCategory(this ITaxCategoryService service, Nop.Core.Domain.Tax.TaxCategory taxCategory)
            => service.InsertTaxCategoryAsync(taxCategory).Await();

        public static void InsertTaxRate(this ICountryStateZipService service, TaxRate taxRate)
            => service.InsertTaxRateAsync(taxRate).Await();

        public static IList<Country> GetAllCountriesForBilling(this ICountryService service, int languageId = 0, bool showHidden = false)
            => service.GetAllCountriesForBillingAsync(languageId, showHidden).Await();

        public static IList<Category> GetAllCategories(this ICategoryService service, int storeId = 0, bool showHidden = false)
            => service.GetAllCategoriesAsync(storeId, showHidden).Await();

        public static StateProvince GetStateProvinceById(this IStateProvinceService service, int stateProvinceId)
            => service.GetStateProvinceByIdAsync(stateProvinceId).Await();

        public static IList<StateProvince> GetStateProvincesByCountryId(this IStateProvinceService service, int countryId, int languageId = 0, bool showHidden = false)
            => service.GetStateProvincesByCountryIdAsync(countryId, languageId, showHidden).Await();

        public static IList<Nop.Core.Domain.Security.PermissionRecord> GetAllPermissionRecords(this IPermissionService service)
            => service.GetAllPermissionRecordsAsync().Await();

        public static void InsertPermissionRecordCustomerRoleMapping(this IPermissionService service, Nop.Core.Domain.Security.PermissionRecordCustomerRoleMapping mapping)
            => service.InsertPermissionRecordCustomerRoleMappingAsync(mapping).Await();

        public static void DeletePermissionRecordCustomerRoleMapping(this IPermissionService service, int permissionId, int customerRoleId)
            => service.DeletePermissionRecordCustomerRoleMappingAsync(permissionId, customerRoleId).Await();

        public static void InsertShippingMethod(this IShippingService service, ShippingMethod shippingMethod)
            => Nop.Core.Infrastructure.EngineContext.Current.Resolve<IRepository<ShippingMethod>>().Insert(shippingMethod);

        public static Warehouse GetWarehouseById(this IShippingService service, int warehouseId)
            => Nop.Core.Infrastructure.EngineContext.Current.Resolve<IWarehouseService>().GetWarehouseByIdAsync(warehouseId).Await();

        public static Nop.Core.Domain.Directory.StateProvince GetStateProvinceByAddress(this IStateProvinceService service, Address address)
            => address is null ? null : service.GetStateProvinceByIdAsync(address.StateProvinceId ?? 0).Await();

        public static IList<int> SendCustomerEmailValidationMessage(this IWorkflowMessageService service, Customer customer, int languageId)
            => service.SendCustomerEmailValidationMessageAsync(customer, languageId).Await();

        public static string FormatAttributes(this ICheckoutAttributeFormatter formatter, string attributesXml, Customer customer,
            string separator = "<br />", bool htmlEncode = true, bool renderPrices = true, bool allowHyperlinks = true)
            => formatter.FormatAttributesAsync(attributesXml, customer, separator, htmlEncode, renderPrices, allowHyperlinks).Await();

        public static Nop.Web.Areas.Admin.Models.Shipping.WarehouseListModel PrepareWarehouseListModel(this IShippingModelFactory factory,
            Nop.Web.Areas.Admin.Models.Shipping.WarehouseSearchModel searchModel)
            => factory.PrepareWarehouseListModelAsync(searchModel).Await();

        public static string GetDefaultPictureUrl(this IPictureService service, int targetSize = 0, PictureType defaultPictureType = PictureType.Entity, string storeLocation = null)
            => service.GetDefaultPictureUrlAsync(targetSize, defaultPictureType, storeLocation).Await();

        public static IList<Picture> GetPicturesByProductId(this IPictureService service, int productId, int recordsToReturn = 0)
            => service.GetPicturesByProductIdAsync(productId, recordsToReturn).Await();

        public static Picture UpdatePicture(this IPictureService service, int pictureId, byte[] pictureBinary, string mimeType, string seoFilename,
            string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true)
            => service.UpdatePictureAsync(pictureId, pictureBinary, mimeType, seoFilename, altAttribute, titleAttribute, isNew, validateBinary).Await();

        public static EmailAccount GetEmailAccountById(this IEmailAccountService service, int emailAccountId)
            => service.GetEmailAccountByIdAsync(emailAccountId).Await();

        public static IList<EmailAccount> GetAllEmailAccounts(this IEmailAccountService service)
            => service.GetAllEmailAccountsAsync().Await();

        public static void InsertQueuedEmail(this IQueuedEmailService service, QueuedEmail queuedEmail)
            => service.InsertQueuedEmailAsync(queuedEmail).Await();

        public static decimal GetTaxTotal(this IOrderTotalCalculationService service,
            IList<ShoppingCartItem> cart, bool usePaymentMethodAdditionalFee = true)
            => service.GetTaxTotalAsync(cart, usePaymentMethodAdditionalFee).Await().taxTotal;

        public static IPagedList<Vendor> GetAllVendors(this IVendorService service, string name = "", string email = "", int pageIndex = 0,
            int pageSize = int.MaxValue, bool showHidden = false)
            => service.GetAllVendorsAsync(name, email, pageIndex, pageSize, showHidden).Await();

        public static IList<CustomerPassword> GetCustomerPasswords(this ICustomerService service, int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
            => service.GetCustomerPasswordsAsync(customerId, passwordFormat, passwordsToReturn).Await();

        public static SpecificationAttributeOption GetSpecificationAttributeOptionById(this ISpecificationAttributeService service, int specificationAttributeOptionId)
            => service.GetSpecificationAttributeOptionByIdAsync(specificationAttributeOptionId).Await();
        public static IList<WarehouseScheduleMappingSnapshot> GetWarehouseScheduleByReflection(int warehouseId)
        {
            var serviceType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException exception)
                    {
                        return exception.Types.Where(type => type != null);
                    }
                })
                .FirstOrDefault(type => type.FullName == "Nop.Plugin.Widgets.WarehouseSchedule.Services.IWarehouseScheduleService");

            if (serviceType == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var service = Nop.Core.Infrastructure.EngineContext.Current.Resolve(serviceType);

            if (service == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var method = serviceType.GetMethod("GetWarehouseScheduleAsync");

            if (method == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var task = method.Invoke(service, new object[] { warehouseId }) as Task;

            if (task == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            task.GetAwaiter().GetResult();

            var resultProperty = task.GetType().GetProperty("Result");

            if (resultProperty == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var result = resultProperty.GetValue(task) as System.Collections.IEnumerable;

            if (result == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var mappings = new List<WarehouseScheduleMappingSnapshot>();

            foreach (var item in result)
            {
                var itemType = item.GetType();

                mappings.Add(new WarehouseScheduleMappingSnapshot
                {
                    Id = GetPropertyValue<int>(item, itemType, "Id"),
                    WarehouseId = GetPropertyValue<int>(item, itemType, "WarehouseId"),
                    DayOfWeekId = GetPropertyValue<int>(item, itemType, "DayOfWeekId"),
                    OpeningTime = GetPropertyValue<TimeSpan>(item, itemType, "OpeningTime"),
                    ClosingTime = GetPropertyValue<TimeSpan>(item, itemType, "ClosingTime"),
                    IsClosed = GetPropertyValue<bool>(item, itemType, "IsClosed")
                });
            }

            return mappings;
        }

        private static T GetPropertyValue<T>(object instance, Type instanceType, string propertyName)
        {
            var property = instanceType.GetProperty(propertyName);

            if (property == null)
                return default;

            var value = property.GetValue(instance);

            if (value == null)
                return default;

            return (T)value;
        }
    }
}

namespace System.Xml
{
    public static class XmlWriterCompatibilityExtensions
    {
        public static void WriteString(this XmlWriter writer, string name, object value)
            => writer.WriteElementString(name, value?.ToString() ?? string.Empty);
    }
}
