using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nop.Core.Configuration;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Factories;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Attributes;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Pickup;
using Nop.Web.Factories;
using Nop.Web.Models.Checkout;
using Nop.Web.Models.Common;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Controllers
{
    internal static class StandardPermissionProvider
    {
        public const string ManageWidgets = StandardPermission.Configuration.MANAGE_WIDGETS;

        public const string ManageShippingSettings = StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS;
    }

    internal static class NopCommerce490CompatibilityExtensions
    {
        public static bool Authorize(this IPermissionService permissionService, string permission)
            => permissionService.AuthorizeAsync(permission).GetAwaiter().GetResult();

        public static string GetResource(this ILocalizationService localizationService, string resourceKey)
            => localizationService.GetResourceAsync(resourceKey).GetAwaiter().GetResult();

        public static T LoadSetting<T>(this ISettingService settingService, int storeId = 0) where T : ISettings, new()
            => settingService.LoadSettingAsync<T>(storeId).GetAwaiter().GetResult();

        public static void SaveSetting<T>(this ISettingService settingService, T settings, int storeId = 0) where T : ISettings, new()
            => settingService.SaveSettingAsync(settings, storeId).GetAwaiter().GetResult();

        public static void ClearCache(this ISettingService settingService)
            => settingService.ClearCacheAsync().GetAwaiter().GetResult();

        public static bool IsRegistered(this ICustomerService customerService, Customer customer)
            => customerService.IsRegisteredAsync(customer).GetAwaiter().GetResult();

        public static bool IsGuest(this ICustomerService customerService, Customer customer)
            => customerService.IsGuestAsync(customer).GetAwaiter().GetResult();

        public static IList<Address> GetAddressesByCustomerId(this ICustomerService customerService, int customerId)
            => customerService.GetAddressesByCustomerIdAsync(customerId).GetAwaiter().GetResult();

        public static Address GetCustomerAddress(this ICustomerService customerService, int customerId, int addressId)
            => customerService.GetCustomerAddressAsync(customerId, addressId).GetAwaiter().GetResult();

        public static Address GetCustomerBillingAddress(this ICustomerService customerService, Customer customer)
            => customerService.GetCustomerBillingAddressAsync(customer).GetAwaiter().GetResult();

        public static Address GetCustomerShippingAddress(this ICustomerService customerService, Customer customer)
            => customerService.GetCustomerShippingAddressAsync(customer).GetAwaiter().GetResult();

        public static void InsertCustomerAddress(this ICustomerService customerService, Customer customer, Address address)
            => customerService.InsertCustomerAddressAsync(customer, address).GetAwaiter().GetResult();

        public static void RemoveCustomerAddress(this ICustomerService customerService, Customer customer, Address address)
            => customerService.RemoveCustomerAddressAsync(customer, address).GetAwaiter().GetResult();

        public static void UpdateCustomer(this ICustomerService customerService, Customer customer)
            => customerService.UpdateCustomerAsync(customer).GetAwaiter().GetResult();

        public static Address GetAddressById(this IAddressService addressService, int addressId)
            => addressService.GetAddressByIdAsync(addressId).GetAwaiter().GetResult();

        public static void InsertAddress(this IAddressService addressService, Address address)
            => addressService.InsertAddressAsync(address).GetAwaiter().GetResult();

        public static void UpdateAddress(this IAddressService addressService, Address address)
            => addressService.UpdateAddressAsync(address).GetAwaiter().GetResult();

        public static void DeleteAddress(this IAddressService addressService, Address address)
            => addressService.DeleteAddressAsync(address).GetAwaiter().GetResult();

        public static IList<Country> GetAllCountries(this ICountryService countryService, int languageId = 0)
            => countryService.GetAllCountriesAsync(languageId).GetAwaiter().GetResult();

        public static Country GetCountryByAddress(this ICountryService countryService, Address address)
            => countryService.GetCountryByAddressAsync(address).GetAwaiter().GetResult();

        public static void PrepareAddressModel(this IAddressModelFactory addressModelFactory, AddressModel model,
            Address address, bool excludeProperties, AddressSettings addressSettings,
            Func<IList<Country>> loadCountries = null, bool prePopulateWithCustomerFields = false,
            Customer customer = null, string overrideAttributesXml = "")
        {
            Func<Task<IList<Country>>> loadCountriesAsync = loadCountries == null
                ? null
                : () => Task.FromResult(loadCountries());

            addressModelFactory.PrepareAddressModelAsync(model, address, excludeProperties, addressSettings,
                loadCountriesAsync, prePopulateWithCustomerFields, customer, overrideAttributesXml).GetAwaiter().GetResult();
        }

        public static string ParseCustomAddressAttributes(this IAttributeParser<AddressAttribute, AddressAttributeValue> attributeParser, IFormCollection form)
            => attributeParser.ParseCustomAttributesAsync(form, NopCommonDefaults.AddressAttributeControlName).GetAwaiter().GetResult();

        public static IList<string> GetAttributeWarnings(this IAttributeParser<AddressAttribute, AddressAttributeValue> attributeParser, string attributesXml)
            => attributeParser.GetAttributeWarningsAsync(attributesXml).GetAwaiter().GetResult();

        public static void SaveAttribute<TPropType>(this IGenericAttributeService genericAttributeService, Customer customer, string key, TPropType value, int storeId = 0)
            => genericAttributeService.SaveAttributeAsync(customer, key, value, storeId).GetAwaiter().GetResult();

        public static IList<ShoppingCartItem> GetShoppingCart(this IShoppingCartService shoppingCartService, Customer customer, ShoppingCartType shoppingCartType, int storeId = 0)
            => shoppingCartService.GetShoppingCartAsync(customer, shoppingCartType, storeId).GetAwaiter().GetResult();

        public static bool ShoppingCartRequiresShipping(this IShoppingCartService shoppingCartService, IList<ShoppingCartItem> shoppingCart)
            => shoppingCartService.ShoppingCartRequiresShippingAsync(shoppingCart).GetAwaiter().GetResult();

        public static GetPickupPointsResponse GetPickupPoints(this IShippingService shippingService, int addressId, Customer customer,
            string providerSystemName = null, int storeId = 0)
            => shippingService.GetPickupPointsAsync(new List<ShoppingCartItem>(), null, customer, providerSystemName, storeId).GetAwaiter().GetResult();

        public static CheckoutShippingMethodModel PrepareShippingMethodModel(this ICheckoutModelFactory checkoutModelFactory, IList<ShoppingCartItem> cart, Address shippingAddress)
            => checkoutModelFactory.PrepareShippingMethodModelAsync(cart, shippingAddress).GetAwaiter().GetResult();

        public static CheckoutPaymentMethodModel PreparePaymentMethodModel(this ICheckoutModelFactory checkoutModelFactory, IList<ShoppingCartItem> cart, int filterByCountryId)
            => checkoutModelFactory.PreparePaymentMethodModelAsync(cart, filterByCountryId).GetAwaiter().GetResult();

        public static CheckoutPaymentInfoModel PreparePaymentInfoModel(this ICheckoutModelFactory checkoutModelFactory, IPaymentMethod paymentMethod)
            => checkoutModelFactory.PreparePaymentInfoModelAsync(paymentMethod).GetAwaiter().GetResult();

        public static CheckoutConfirmModel PrepareConfirmOrderModel(this ICheckoutModelFactory checkoutModelFactory, IList<ShoppingCartItem> cart)
            => checkoutModelFactory.PrepareConfirmOrderModelAsync(cart).GetAwaiter().GetResult();

        public static CustomOnePageCheckoutModel PrepareOnePageCheckoutModel(this ICheckoutModelCustomFactory checkoutModelFactory, IList<ShoppingCartItem> cart)
            => checkoutModelFactory.PrepareOnePageCheckoutModelAsync(cart).GetAwaiter().GetResult();

        public static CheckoutBillingGeoCoordinatesAddressModel PrepareCheckoutBillingGeoCoordinatesAddressModel(this ICheckoutModelCustomFactory checkoutModelFactory,
            IList<ShoppingCartItem> cart, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
            => checkoutModelFactory.PrepareCheckoutBillingGeoCoordinatesAddressModelAsync(cart, addressId, countryId, attributesXml, latitude, longitude).GetAwaiter().GetResult();

        public static CheckoutShippingGeoCoordinatesAddressModel PrepareCheckoutShippingGeoCoordinatesAddressModel(this ICheckoutModelCustomFactory checkoutModelFactory,
            IList<ShoppingCartItem> cart, bool renderMapsJS, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
            => checkoutModelFactory.PrepareCheckoutShippingGeoCoordinatesAddressModelAsync(cart, renderMapsJS, addressId, countryId, attributesXml, latitude, longitude).GetAwaiter().GetResult();

        public static AddressGeoCoordinatesMapping GetAddressGeoCoordinates(this IAddressGeoCoordinatesService addressGeoCoordinatesService, int addressId)
            => addressGeoCoordinatesService.GetAddressGeoCoordinatesAsync(addressId).GetAwaiter().GetResult();

        public static void InsertAddressGeoCoordinates(this IAddressGeoCoordinatesService addressGeoCoordinatesService, AddressGeoCoordinatesMapping addressGeoCoordinates, int addressId)
            => addressGeoCoordinatesService.InsertAddressGeoCoordinatesAsync(addressGeoCoordinates, addressId).GetAwaiter().GetResult();

        public static void RemoveAddressGeoCoordinates(this IAddressGeoCoordinatesService addressGeoCoordinatesService, int addressId)
            => addressGeoCoordinatesService.RemoveAddressGeoCoordinatesAsync(addressId).GetAwaiter().GetResult();

        public static bool IsPaymentWorkflowRequired(this IOrderProcessingService orderProcessingService, IList<ShoppingCartItem> cart, bool ignoreRewardPoints = false)
            => orderProcessingService.IsPaymentWorkflowRequiredAsync(cart, ignoreRewardPoints).GetAwaiter().GetResult();

        public static IPaymentMethod LoadPluginBySystemName(this IPaymentPluginManager paymentPluginManager, string systemName, Customer customer = null, int storeId = 0)
            => paymentPluginManager.LoadPluginBySystemNameAsync(systemName, customer, storeId).GetAwaiter().GetResult();

        public static void Set<T>(this ISession session, string key, T value)
            => session.SetAsync(key, value).GetAwaiter().GetResult();

        public static void Warning(this ILogger logger, string message, Exception exception = null, Customer customer = null)
            => logger.WarningAsync(message, exception, customer).GetAwaiter().GetResult();
    }
}
