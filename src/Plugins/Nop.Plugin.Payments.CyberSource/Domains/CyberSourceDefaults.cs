using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Payments.CyberSource.Domains
{
    /// <summary>
    /// Class containing default values for CyberSource plugin
    /// </summary>
    public static class CyberSourceDefaults
    {
        /// <summary>
        /// Plugin system name
        /// </summary>
        public const string SystemName = "Payments.CyberSource";

        /// <summary>
        /// Reason code that represents a SUCCESS operation in CyberSource
        /// </summary>
        public const string SuccessReasonCode = "100";

        /// <summary>
        /// Reason code that represents a General decline by the processor operation in CyberSource
        /// </summary>
        public const string generalDeclined = "233";

        /// <summary>
        /// CyberSource default tax indicator
        /// </summary>
        public const string DefaultTaxIndicator = "Y";

        /// <summary>
        /// Default payment method used
        /// </summary>
        public const string PaymentMethod = "card";

        /// <summary>
        /// Specify the input type of CardNumber and Cvv fields in ConfirmAndPay form
        /// </summary>
        public static String type;

        /// <summary>
        /// Gets the locale resources prefix key.
        /// </summary>
        public static string LocaleResourcesPrefix => "Plugins.Payments.CyberSource";

        /// <summary>
        /// Gets the output directory for this plugin.
        /// </summary>
        public static string OutPutDir => "Plugins/Payments.CyberSource";

        /// <summary>
        /// Gets the subject for the order not paid notification messages.
        /// </summary>
        public static string OrderNotPaidNotificationTemplateSubject => "%Store.Name%. Order #%Order.OrderNumber% payment declined";

        /// <summary>
        /// Gets the name of the template used for order not paid message sent to customers.
        /// </summary>
        public static string OrderNotPaidCustomerNotificationTemplateName => "OrderNotPaid.CustomerNotification";

        /// <summary>
        /// Gets the name of the template used for order not paid message sent to store owner.
        /// </summary>
        public static string OrderNotPaidStoreOwnerNotificationTemplateName => "OrderNotPaid.StoreOwnerNotification";

        /// <summary>
        /// Gets the name of the template used for order not paid message sent to vendors.
        /// </summary>
        public static string OrderNotPaidVendorNotificationTemplateName => "OrderNotPaid.VendorNotification";

        /// <summary>
        /// Gets the name of the template used for order not paid message sent to affiliates.
        /// </summary>
        public static string OrderNotPaidAffiliateNotificationTemplateName => "OrderNotPaid.AffiliateNotification";

        /// <summary>
        /// Gets all the message template names registered by Cybersource plugin.
        /// </summary>
        public static IList<string> MessageTemplateNames => new List<string>
        {
            OrderNotPaidCustomerNotificationTemplateName,
            OrderNotPaidStoreOwnerNotificationTemplateName,
            OrderNotPaidVendorNotificationTemplateName,
            OrderNotPaidAffiliateNotificationTemplateName
        };

        /// <summary>
        /// Gets the bodies for the message templates registered by Cybersource plugin.
        /// </summary>
        public static IDictionary<string, string> MessageTemplateBodies => new Dictionary<string, string>
        {
            [OrderNotPaidStoreOwnerNotificationTemplateName] = "<p>  <a href=\" % Store.URL % \">%Store.Name%</a>  <br />  <br />  Order #%Order.OrderNumber% payment has been just declined  <br />  Date Ordered: %Order.CreatedOn%  </p>",
            [OrderNotPaidCustomerNotificationTemplateName] = "<p>  <a href=\" % Store.URL % \">%Store.Name%</a>  <br />  <br />  Hello %Order.CustomerFullName%,  <br />  Thanks for buying from <a href=\" % Store.URL % \">%Store.Name%</a>. We are having trouble authorizing your payment for the items below. Please verify or update your payment method. If your payment information is correct, please contact your bank for more details.  <br />  <br />  Order Number: %Order.OrderNumber%  <br />  Order Details: <a href=\" % Order.OrderURLForCustomer % \" target=\"_blank\">%Order.OrderURLForCustomer%</a>  <br />  %Order.Product(s)%  </p>",
            [OrderNotPaidVendorNotificationTemplateName] = "<p>  <a href=\" % Store.URL % \">%Store.Name%</a>  <br />  <br />  Order #%Order.OrderNumber% payment has been just declined.  <br />  <br />  Order Number: %Order.OrderNumber%  <br />  Date Ordered: %Order.CreatedOn%  <br />  <br />  %Order.Product(s)%  </p>",
            [OrderNotPaidAffiliateNotificationTemplateName] = "<p>  <a href=\" % Store.URL % \">%Store.Name%</a>  <br />  <br />  Order #%Order.OrderNumber% payment has been just declined.  <br />  <br />  Order Number: %Order.OrderNumber%  <br />  Date Ordered: %Order.CreatedOn%  <br />  <br />  %Order.Product(s)%  </p>"
        };

        /// <summary>
        /// Gets the view component corresponding to the widget zones used by this plugin.
        /// </summary>
        public static IDictionary<string, string> WidgetZonesViewComponentsDictionary => new Dictionary<string, string>
        {
            [PublicWidgetZones.CheckoutCompletedTop] = "CheckoutCompletedPaymentStatus"
        };

        /// <summary>
        /// Gets the retry payment default url.
        /// </summary>
        public static string RetryPaymentBaseUrl => "cybersource/retry-payment/";
    }
}
