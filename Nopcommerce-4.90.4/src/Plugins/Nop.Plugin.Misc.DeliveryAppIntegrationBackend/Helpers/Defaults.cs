using System.Collections.Generic;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Security;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Represents the default values used by Contacts Confirmation Plugin.
    /// </summary>
    public static class Defaults
    {
        #region Private Properties

        //static readonly List<string> ClientPermissions = new List<string>
        //{
        //    "Public store. Display Prices",
        //    "Public store. Enable shopping cart",
        //    "Public store. Enable wishlist",
        //    "Public store. Allow navigation",
        //};

        static readonly List<string> ClientPermissions = new List<string>
        {
            StandardPermission.PublicStore.DISPLAY_PRICES,
            StandardPermission.PublicStore.ENABLE_SHOPPING_CART,
            StandardPermission.PublicStore.ENABLE_WISHLIST,
            StandardPermission.PublicStore.PUBLIC_STORE_ALLOW_NAVIGATION
        };

        //static readonly List<string> CommercePermissions = new List<string>
        //{
        //    "Access admin area",
        //    "Admin area. Manage Products",
        //    "Admin area. Manage Product Reviews",
        //    "Admin area. Manage Orders",
        //    "Admin area. Manage Shipping Settings",
        //    "Admin area. Manage Stores",
        //};

        static readonly List<string> CommercePermissions = new List<string>
        {
            StandardPermission.Security.ACCESS_ADMIN_PANEL,
            StandardPermission.Catalog.PRODUCTS_VIEW,
            StandardPermission.Catalog.PRODUCTS_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.PRODUCT_REVIEWS_VIEW,
            StandardPermission.Catalog.PRODUCT_REVIEWS_CREATE_EDIT_DELETE,
            StandardPermission.Orders.ORDERS_VIEW,
            StandardPermission.Orders.ORDERS_CREATE_EDIT_DELETE,
            StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS,
            StandardPermission.Configuration.MANAGE_STORES
        };


        //static readonly List<string> MessengerPermissions = new List<string>
        //{
        //    "Public store. Display Prices",
        //    "Public store. Allow navigation"
        //};

        static readonly List<string> MessengerPermissions = new List<string>
        {
            StandardPermission.PublicStore.DISPLAY_PRICES,
            StandardPermission.PublicStore.PUBLIC_STORE_ALLOW_NAVIGATION
        };

        //static readonly List<string> AgentPermissions = new List<string>
        //{
        //    "Access admin area",
        //    "Admin area. Allow Customer Impersonation",
        //    "Admin area. Manage Products",
        //    "Admin area. Manage Categories",
        //    "Admin area. Manage Manufacturers",
        //    "Admin area. Manage Product Reviews",
        //    "Admin area. Manage Product Tags",
        //    "Admin area. Manage Attributes",
        //    "Admin area. Manage Customers",
        //    "Admin area. Manage Vendors",
        //    "Admin area. Manage Current Carts",
        //    "Admin area. Manage Orders",
        //    "Admin area. Manage Recurring Payments",
        //    "Admin area. Manage Gift Cards",
        //    "Admin area. Manage Return Requests",
        //    "Admin area. Access order country report",
        //    "Admin area. Manage Affiliates",
        //    "Admin area. Manage Campaigns",
        //    "Admin area. Manage Discounts",
        //    "Admin area. Manage Newsletter Subscribers",
        //    "Admin area. Manage Polls",
        //    "Admin area. Manage News",
        //    "Admin area. Manage Blog",
        //    "Admin area. Manage Topics",
        //    "Admin area. Manage Forums",
        //    "Admin area. Manage Message Templates",
        //    "Admin area. Manage Countries",
        //    "Admin area. Manage Languages",
        //    "Admin area. Manage Settings",
        //    "Admin area. Manage Payment Methods",
        //    "Admin area. Manage External Authentication Methods",
        //    "Admin area. Manage Tax Settings",
        //    "Admin area. Manage Shipping Settings",
        //    "Admin area. Manage Currencies",
        //    "Admin area. Manage Activity Log",
        //    "Admin area. Manage ACL",
        //    "Admin area. Manage Email Accounts",
        //    "Admin area. Manage Stores",
        //    "Admin area. Manage System Log",
        //    "Admin area. Manage Message Queue",
        //    "Admin area. Manage Maintenance",
        //    "Admin area. HTML Editor. Manage pictures",
        //    "Public store. Display Prices",
        //    "Public store. Enable shopping cart",
        //    "Public store. Enable wishlist",
        //    "Public store. Allow navigation",
        //    "Public store. Access a closed store",
        //};

        static readonly List<string> AgentPermissions = new List<string>
        {
            StandardPermission.Security.ACCESS_ADMIN_PANEL,
            StandardPermission.Customers.CUSTOMERS_IMPERSONATION,

            StandardPermission.Catalog.PRODUCTS_VIEW,
            StandardPermission.Catalog.PRODUCTS_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.PRODUCTS_IMPORT_EXPORT,

            StandardPermission.Catalog.CATEGORIES_VIEW,
            StandardPermission.Catalog.CATEGORIES_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.CATEGORIES_IMPORT_EXPORT,

            StandardPermission.Catalog.MANUFACTURER_VIEW,
            StandardPermission.Catalog.MANUFACTURER_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.MANUFACTURER_IMPORT_EXPORT,

            StandardPermission.Catalog.PRODUCT_REVIEWS_VIEW,
            StandardPermission.Catalog.PRODUCT_REVIEWS_CREATE_EDIT_DELETE,

            StandardPermission.Catalog.PRODUCT_TAGS_VIEW,
            StandardPermission.Catalog.PRODUCT_TAGS_CREATE_EDIT_DELETE,

            StandardPermission.Catalog.PRODUCT_ATTRIBUTES_VIEW,
            StandardPermission.Catalog.PRODUCT_ATTRIBUTES_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.SPECIFICATION_ATTRIBUTES_VIEW,
            StandardPermission.Catalog.SPECIFICATION_ATTRIBUTES_CREATE_EDIT_DELETE,
            StandardPermission.Catalog.CHECKOUT_ATTRIBUTES_VIEW,
            StandardPermission.Catalog.CHECKOUT_ATTRIBUTES_CREATE_EDIT_DELETE,

            StandardPermission.Customers.CUSTOMERS_VIEW,
            StandardPermission.Customers.CUSTOMERS_CREATE_EDIT_DELETE,
            StandardPermission.Customers.CUSTOMERS_IMPORT_EXPORT,

            StandardPermission.Customers.VENDORS_VIEW,
            StandardPermission.Customers.VENDORS_CREATE_EDIT_DELETE,

            StandardPermission.Orders.CURRENT_CARTS_MANAGE,

            StandardPermission.Orders.ORDERS_VIEW,
            StandardPermission.Orders.ORDERS_CREATE_EDIT_DELETE,
            StandardPermission.Orders.ORDERS_IMPORT_EXPORT,

            StandardPermission.Orders.RECURRING_PAYMENTS_VIEW,
            StandardPermission.Orders.RECURRING_PAYMENTS_CREATE_EDIT_DELETE,

            StandardPermission.Orders.GIFT_CARDS_VIEW,
            StandardPermission.Orders.GIFT_CARDS_CREATE_EDIT_DELETE,

            StandardPermission.Orders.RETURN_REQUESTS_VIEW,
            StandardPermission.Orders.RETURN_REQUESTS_CREATE_EDIT_DELETE,

            StandardPermission.Reports.COUNTRY_SALES,

            StandardPermission.Promotions.AFFILIATES_VIEW,
            StandardPermission.Promotions.AFFILIATES_CREATE_EDIT_DELETE,

            StandardPermission.Promotions.CAMPAIGNS_VIEW,
            StandardPermission.Promotions.CAMPAIGNS_CREATE_EDIT,
            StandardPermission.Promotions.CAMPAIGNS_DELETE,
            StandardPermission.Promotions.CAMPAIGNS_SEND_EMAILS,

            StandardPermission.Promotions.DISCOUNTS_VIEW,
            StandardPermission.Promotions.DISCOUNTS_CREATE_EDIT_DELETE,

            StandardPermission.Promotions.SUBSCRIBERS_VIEW,
            StandardPermission.Promotions.SUBSCRIBERS_CREATE_EDIT_DELETE,
            StandardPermission.Promotions.SUBSCRIBERS_IMPORT_EXPORT,

            StandardPermission.ContentManagement.POLLS_VIEW,
            StandardPermission.ContentManagement.POLLS_CREATE_EDIT_DELETE,

            StandardPermission.ContentManagement.NEWS_VIEW,
            StandardPermission.ContentManagement.NEWS_CREATE_EDIT_DELETE,
            StandardPermission.ContentManagement.NEWS_COMMENTS_VIEW,
            StandardPermission.ContentManagement.NEWS_COMMENTS_CREATE_EDIT_DELETE,

            StandardPermission.ContentManagement.BLOG_VIEW,
            StandardPermission.ContentManagement.BLOG_CREATE_EDIT_DELETE,
            StandardPermission.ContentManagement.BLOG_COMMENTS_VIEW,
            StandardPermission.ContentManagement.BLOG_COMMENTS_CREATE_EDIT_DELETE,

            StandardPermission.ContentManagement.TOPICS_VIEW,
            StandardPermission.ContentManagement.TOPICS_CREATE_EDIT_DELETE,

            StandardPermission.ContentManagement.FORUMS_VIEW,
            StandardPermission.ContentManagement.FORUMS_CREATE_EDIT_DELETE,

            StandardPermission.ContentManagement.MESSAGE_TEMPLATES_VIEW,
            StandardPermission.ContentManagement.MESSAGE_TEMPLATES_CREATE_EDIT_DELETE,

            StandardPermission.Configuration.MANAGE_COUNTRIES,
            StandardPermission.Configuration.MANAGE_LANGUAGES,
            StandardPermission.Configuration.MANAGE_SETTINGS,
            StandardPermission.Configuration.MANAGE_PAYMENT_METHODS,
            StandardPermission.Configuration.MANAGE_EXTERNAL_AUTHENTICATION_METHODS,
            StandardPermission.Configuration.MANAGE_TAX_SETTINGS,
            StandardPermission.Configuration.MANAGE_SHIPPING_SETTINGS,
            StandardPermission.Configuration.MANAGE_CURRENCIES,
            StandardPermission.Configuration.MANAGE_ACL,
            StandardPermission.Configuration.MANAGE_EMAIL_ACCOUNTS,
            StandardPermission.Configuration.MANAGE_STORES,

            StandardPermission.Customers.ACTIVITY_LOG_VIEW,
            StandardPermission.Customers.ACTIVITY_LOG_DELETE,
            StandardPermission.Customers.ACTIVITY_LOG_MANAGE_TYPES,

            StandardPermission.System.MANAGE_SYSTEM_LOG,
            StandardPermission.System.MANAGE_MESSAGE_QUEUE,
            StandardPermission.System.MANAGE_MAINTENANCE,
            StandardPermission.System.HTML_EDITOR_MANAGE_PICTURES,

            StandardPermission.PublicStore.DISPLAY_PRICES,
            StandardPermission.PublicStore.ENABLE_SHOPPING_CART,
            StandardPermission.PublicStore.ENABLE_WISHLIST,
            StandardPermission.PublicStore.PUBLIC_STORE_ALLOW_NAVIGATION,
            StandardPermission.PublicStore.ACCESS_CLOSED_STORE
        };

        #endregion

        #region Public Properties

        public static string FixedByWeightByTotalRateSettingsKey =>
    "ShippingRateComputationMethod.FixedByWeightByTotal.Rate.ShippingMethodId{0}";

        #region Address Attributes

        /// <summary>
        /// Gets the address alias attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute AddressAliasAttribute
            => new DeliveryAppCustomAttribute { Name = "Alias", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the address shipping specification attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute AddressShippingSpecificationAttribute
            => new DeliveryAppCustomAttribute { Name = "Shipping specification", ControlType = AttributeControlType.TextBox };

        #endregion

        #region Checkout Attributes

        #region Payment Method Attribute Options

        /// <summary>
        /// Gets the Credit card payment checkout attribute attribute name
        /// </summary>
        public static string CreditCardPaymentCheckoutAttributeName => "TarjetaCredito";

        /// <summary>
        /// Gets the cash payment checkout attribute attribute name
        /// </summary>
        public static string CashPaymentCheckoutAttributeName => "Efectivo";

        /// <summary>
        /// Gets the clave card payment checkout attribute attribute name
        /// </summary>
        public static string ClaveCardPaymentCheckoutAttributeName => "TarjetaClave";

        #endregion

        /// <summary>
        /// Gets the payment method checkout attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute PaymentMethodCheckoutAttribute => new DeliveryAppCustomAttribute
        {
            Name = "MetodoPago",
            ControlType = AttributeControlType.DropdownList,
            Options = new List<string> { CashPaymentCheckoutAttributeName, ClaveCardPaymentCheckoutAttributeName, CreditCardPaymentCheckoutAttributeName }
        };

        /// <summary>
        /// Gets the order indications checkout attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute OrderDeliveryIndicationsCheckoutAttribute
            => new DeliveryAppCustomAttribute { Name = "OrderDeliveryIndications", ControlType = AttributeControlType.TextBox };

        #endregion

        #region Customer Attributes

        /// <summary>
        /// Gets the general review Customer attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute CustomerRatingAttribute
            => new DeliveryAppCustomAttribute { Name = "Rating", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver Identification Number attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute DriverIdentificationNumberAttribute
            => new DeliveryAppCustomAttribute { Name = "Driver identification number", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver Vehicle Type attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute VehicleTypeAttribute
            => new DeliveryAppCustomAttribute { Name = "Vehicle type", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver Brand attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute VehicleBrandAttribute
            => new DeliveryAppCustomAttribute { Name = "Vehicle brand", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver Model attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute VehicleModelAttribute
            => new DeliveryAppCustomAttribute { Name = "Vehicle model", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver License Plate attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute VehicleLicensePlateAttribute
            => new DeliveryAppCustomAttribute { Name = "Vehicle license plate", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the Driver Color attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute VehicleColorAttribute
            => new DeliveryAppCustomAttribute { Name = "Vehicle color", ControlType = AttributeControlType.TextBox };

        #endregion

        #region Product Attributes

        /// <summary>
        /// Get the especifications attribute.
        /// </summary>
        public static DeliveryAppCustomAttribute ProductSpecialSpecificationAttribute
            => new DeliveryAppCustomAttribute { Name = "Instrucciones especiales", ControlType = AttributeControlType.TextBox };

        #endregion

        #region Vendor Attributes

        /// <summary>
        /// Gets the general review vendor attribute name.
        /// </summary>
        public static DeliveryAppCustomAttribute VendorRatingAttribute
            => new DeliveryAppCustomAttribute { Name = "Rating", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the general vendor category attribute name.
        /// </summary>
        public static DeliveryAppCustomAttribute VendorCategoryAttribute => new DeliveryAppCustomAttribute
        {
            Name = "Category",
            ControlType = AttributeControlType.DropdownList,
            Options = new List<string> { "Restaurantes", "Mercados", "Tiendas", "Salud", "Paquete" }
        };

        /// <summary>
        /// The name of the Restaurants vendor category
        /// </summary>
        public static string RestaurantVendorCategoryName => "Restaurantes";

        /// <summary>
        /// Gets the estimated wait time attribute name
        /// </summary>
        public static DeliveryAppCustomAttribute VendorEstimatedWaitTimeAttribute
            => new DeliveryAppCustomAttribute { Name = "Estimated wait time", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets Vendor popularity index
        /// </summary>
        public static DeliveryAppCustomAttribute VendorPopularityAttribute
            => new DeliveryAppCustomAttribute { Name = "Popularity", ControlType = AttributeControlType.TextBox };

        /// <summary>
        /// Gets the adults limitated attribute name
        /// </summary>
        public static DeliveryAppCustomAttribute VendorAdultsLimitedAttribute => new DeliveryAppCustomAttribute
        {
            Name = "Adults limitated",
            ControlType = AttributeControlType.DropdownList,
            Options = new List<string> { "Si", "No" }
        };

        /// <summary>
        /// Retrieves the order payment percentage attribute name for vendors.
        /// </summary>
        public static DeliveryAppCustomAttribute VendorOrderPaymentPercentageAttribute
            => new DeliveryAppCustomAttribute { Name = "Order payment %", ControlType = AttributeControlType.TextBox };

        #endregion

        /// <summary>
        /// Gets the locale resources prefix.
        /// </summary>
        public static string ResourcesNamePrefix => "Plugin.Misc.DeliveryAppIntegrationBackend";

        /// <summary>
        /// Gets this plug-in's system name.
        /// </summary>
        public static string SystemName => "Misc.DeliveryAppIntegrationBackend";

        /// <summary>
        /// Gets the prefix of the locale resources for this plug-in.
        /// </summary>
        public static string LocaleResourcesPrefix => "Plugin.Misc.DeliveryAppIntegrationBackend";

        /// <summary>
        /// Gets the name of the Schedule task used to sync orders.
        /// </summary>
        public static string TaskName => "Vendor review recalculator";

        /// <summary>
        /// Gets the recalculate vendor review task's type.
        /// </summary>
        public static string TaskType => "Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Tasks.RecalculateReviewsTask";

        /// <summary>
        /// Gets default task duration.
        /// </summary>
        public static int TaskDuration => 43000;

        /// <summary>
        /// Gets the subject for verification code.
        /// </summary>
        public static string VerificationCodeMessageSubject => "Plugin.Misc.DeliveryAppIntegrationBackend.VerificationCodeMessageSubject";

        /// <summary>
        /// Gets the additional shipping charge settings key 
        /// </summary>
        public static string AdditionalShippingChargeKey => "deliveryappshippingsettings.additionaldeliveryshippingcharge";

        /// <summary>
        /// Retrieves the output directory forthis plugin.
        /// </summary>
        public static string OutputDir => "Plugins/Misc.DeliveryAppIntegrationBackend";

        /// <summary>
        /// Gets the defaults shipping methods names and rate.
        /// </summary>
        public static Dictionary<string, decimal> ShippingMethodsByKm => new Dictionary<string, decimal>
        {
            { "Ground: 0km - 2km", 2.00M },
            { "Ground: 2km - 4km", 2.50M },
            { "Ground: 4km - 6km", 3.00M },
            { "Ground: 6km - 8km", 3.50M },
            { "Ground: 8km - 10km", 4.00M },
            { "Ground: 10km - 12km", 4.50M },
            { "Ground: 12km - 14km", 5.00M },
            { "Ground: 14km - 16km", 6.00M },
            { "Ground: 16km - 18km", 6.50M },
            { "Ground: 18km - 20km", 7.50M },
            { "Ground: 20km - 25km", 9.50M },
            { "Ground: 25km - 35km", 15.00M },
            { "Ground: 35km - 50km", 25.00M }
        };

        /// <summary>
        /// Gets the permisions for the customer roles.
        /// </summary>
        public static Dictionary<string, List<string>> CustomerRolesPermissions => new Dictionary<string, List<string>>
        {
            { "Agente One Hour Delivery", AgentPermissions },
            { "Cliente", ClientPermissions},
            { "Comercio", CommercePermissions },
            { "Mensajero", MessengerPermissions }
        };

        /// <summary>
        /// Gets the string template for the Google Direction query params.
        /// </summary>
        public static string GoogleDirectionQueryParamsTemplate => "origin={0},{1}&destination={2},{3}&key={4}&language={5}&units=metric";

        /// <summary>
        /// Gets the key name added when vendor atttributes are inserted on generic attributes table.
        /// </summary>
        public static string GenericAttributeKeyForVendorAttributes => "VendorAttributes";

        /// <summary>
        /// Gets the validation email subject locale resource key.
        /// </summary>
        public static string EmailValidationSubjectLocaleKey => "Plugin.Misc.ContactsConfirmation.Email.Subject";

        /// <summary>
        /// Gets the validation email body locale resource key.
        /// </summary>
        public static string EmailValidationBodyLocaleKey => "Plugin.Misc.ContactsConfirmation.Email.Body";

        /// <summary>
        /// Retrieves all the address attributes.
        /// </summary>
        public static List<DeliveryAppCustomAttribute> AddressAttributes => new List<DeliveryAppCustomAttribute>
        {
            AddressAliasAttribute,
            AddressShippingSpecificationAttribute
        };

        /// <summary>
        /// Retrieves all the checkout attributes.
        /// </summary>
        public static List<DeliveryAppCustomAttribute> CheckoutAttributes => new List<DeliveryAppCustomAttribute>
        {
            PaymentMethodCheckoutAttribute,
            OrderDeliveryIndicationsCheckoutAttribute
        };

        /// <summary>
        /// Retrieves all the customer attributes.
        /// </summary>
        public static List<DeliveryAppCustomAttribute> CustomerAttributes => new List<DeliveryAppCustomAttribute>
        {
            CustomerRatingAttribute,
            DriverIdentificationNumberAttribute,
            VehicleTypeAttribute,
            VehicleBrandAttribute,
            VehicleModelAttribute,
            VehicleLicensePlateAttribute,
            VehicleColorAttribute
        };

        /// <summary>
        /// Retrieves all the customer attributes.
        /// </summary>
        public static List<DeliveryAppCustomAttribute> ProductAttributes => new List<DeliveryAppCustomAttribute>
        {
            ProductSpecialSpecificationAttribute
        };

        /// <summary>
        /// Retrieves all the vendor attributes.
        /// </summary>
        public static List<DeliveryAppCustomAttribute> VendorAttributes => new List<DeliveryAppCustomAttribute>
        {
            VendorRatingAttribute,
            VendorCategoryAttribute,
            VendorEstimatedWaitTimeAttribute,
            VendorPopularityAttribute,
            VendorAdultsLimitedAttribute,
            VendorOrderPaymentPercentageAttribute
        };

        /// <summary>
        /// Retrieves a dictionary containing the name of the view components that should be invoked depending the widget zone.
        /// </summary>
        public static Dictionary<string, string> WidgetZonesViewComponentsDictionary => new Dictionary<string, string>
        {
            [AdminWidgetZones.WarehouseListButtons] = "VendorWarehouse",
            [AdminWidgetZones.VendorDetailsBlock] = "VendorWarehouse",
            [AdminWidgetZones.DiscountListButtons] = "DeliveryAppDiscount",
            [AdminWidgetZones.DashboardTop] = "DeliveryAppDashboardReports",
        };

        /// <summary>
        /// Gets default food task category.
        /// </summary>
        public static decimal FoodTax => 7.0M;

        /// <summary>
        ///  Gets default Beverage task category.
        /// </summary>
        public static decimal BeverageTax => 10.0M;

        /// <summary>
        ///  Gets default country name.
        /// </summary>
        public static string Country = "Panama";

        /// <summary>
        ///  Gets default Display order.
        /// </summary>
        public static int DefaultDisplayOrder => 0;

        /// <summary>
        /// Get default vendor's product.
        /// </summary>
        public static string VendorDefaultProduct = "Vendor Default Product";

        /// <summary>
        /// Get default vendor's product specification value.
        /// </summary>
        public static string VendorDefaultProductAffirmation = "Yes";

        /// <summary>
        /// Get default vendor's product specification value.
        /// </summary>
        public static string VendorDefaultProductNegation = "No";

        #endregion
    }
}
