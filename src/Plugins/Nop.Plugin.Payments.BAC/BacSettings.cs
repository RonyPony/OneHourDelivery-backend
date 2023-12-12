using Nop.Core.Configuration;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.BAC
{
    /// <summary>
    /// Represents the class used for saving the different settings and configuration for BAC payment plugin.
    /// </summary>
    public sealed class BacSettings : ISettings
    {
        /// <summary>
        /// Represents the default gateway URL.
        /// </summary>
        public string GatewayUrl { get; set; }

        /// <summary>
        /// Represents the URL for requesting the token that will be used for working with the transaction process.
        /// </summary>
        public string TokenRequestUrl { get; set; }

        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        public string HostedPageUrl { get; set; }

        /// <summary>
        /// Represents the URL for the page where card holder is redirected here after transaction completion.
        /// </summary>
        public string CardHolderResponseUrl { get; set; }

        /// <summary>
        /// Represents the Id giving by the BAC for this acquirer. According with the documentation this value will be always 464748, but we leave it configurable, jut in case.
        /// </summary>
        public string AcquirerId { get; set; }

        /// <summary>
        /// Represents the Merchant ID provided by BAC
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Represents the Merchant ID provided by BAC
        /// </summary>
        public string MerchantPassword { get; set; }

        /// <summary>
        /// Represents the purchase currency ISO 4217 numeric currency code (ex: USD = 840).
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Represents the number of digits after the decimal point in the purchase amount (i.e. $12.00 = 2)
        /// </summary>
        public int CurrencyExponent { get; set; }

        /// <summary>
        /// Represents the method used for encrypting the signature. According with the documentation this value will be always SHA1, but we leave it configurable, jut in case.
        /// </summary>
        public string SignatureMethod { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        public bool MarkAsPaid { get; set; }
    }
}
