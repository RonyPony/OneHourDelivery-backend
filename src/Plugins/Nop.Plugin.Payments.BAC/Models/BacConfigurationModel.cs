using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.BAC.Models
{
    /// <summary>
    /// Represents the model used for configuring the BAC Payment plug-in.
    /// </summary>
    public sealed class BacConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.GatewayUrl")]
        public string GatewayUrl { get; set; }

        /// <summary>
        /// Represents the URL for requesting the token that will be used for working with the transaction process.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.TokenRequestUrl")]
        public string TokenRequestUrl { get; set; }

        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.HostedPageUrl")]
        public string HostedPageUrl { get; set; }

        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.CardHolderResponseUrl")]
        public string CardHolderResponseUrl { get; set; }

        /// <summary>
        /// Represents the Id giving by the BAC for this acquirer. According with the documentation this value will be always 464748, but we leave it configurable, jut in case.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.AcquirerId")]
        public string AcquirerId { get; set; }

        /// <summary>
        /// Represents the Merchant ID provided by BAC
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.MerchantId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Represents the Merchant password provided by BAC
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.MerchantPassword")]
        public string MerchantPassword { get; set; }

        /// <summary>
        /// Represents the purchase currency ISO 4217 numeric currency code (ex: USD = 840).
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.Currency")]
        public int Currency { get; set; }

        /// <summary>
        /// Represents the number of digits after the decimal point in the purchase amount (i.e. $12.00 = 2)
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.CurrencyExponent")]
        public int CurrencyExponent { get; set; }

        /// <summary>
        /// Represents the method used for encrypting the signature. According with the documentation this value will be always SHA1, but we leave it configurable, jut in case.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.SignatureMethod")]
        public string SignatureMethod { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.OrderStatus")]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Represents available status that an order could has after processing the payment.
        /// </summary>
        public IList<SelectListItem> OrderStatus { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.BAC.Fields.MarkAsPaid")]
        public bool MarkAsPaid { get; set; }
    }
}
