using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.CyberSource.Models
{
    /// <summary>
    /// Manage the values of the parameters that will be shown in the plugin configuration.
    /// </summary>
    public sealed class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Payment Page URL for process transaction.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.CyberSourcePaymentUrl")]
        public string PaymentPageUrl { get; set; }

        /// <summary>
        /// Url to which the client will be redirected to after the payment is successful.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.RedirectUrl")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// key that provides access to the payment page. 
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.Access_key")]
        public string Access_key { get; set; }

        /// <summary>
        /// Company ID for validate transaction.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.MerchantId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Key that encrypt signed_fields for send to payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.Secret_key")]
        public string Secret_key { get; set; }

        /// <summary>
        /// Key that validates Merchant Permission to use Payment Method
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.SerialNumber")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Mark the permission as authorized or denied.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.Transaction_type")]
        public string Transaction_type { get; set; }

        /// <summary>
        /// Local Currency, for example: USD.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Manage the language used.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.Locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Additional cost that the merchant adds to the transaction
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.AdditionalFee")]
        public decimal AdditionalFee { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public IList<SelectListItem> OrderStatus { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.OrderStatus")]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.MarkAsPaid")]
        public bool MarkAsPaid { get; set; }

        /// <summary>
        /// Represents whether or not you have chosen to mask the card.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.CreditCardIsMasked")]
        public bool CreditCardIsMasked { get; set; }

        /// <summary>
        /// Indicates the environment for Cybersource request. Either 'test' or 'live'.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.CyberSource.CybersourceEnvironment")]
        public string CybersourceEnvironment { get; set; }
    }
}
