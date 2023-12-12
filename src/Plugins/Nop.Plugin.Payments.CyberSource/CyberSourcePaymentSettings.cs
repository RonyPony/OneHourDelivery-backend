using Nop.Core.Configuration;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.CyberSource
{
    /// <summary>
    /// Read and write Configuration PaymentParameters.
    /// </summary>
    public sealed class CyberSourcePaymentSettings : ISettings
    {
        /// <summary>
        /// Specify the url of the address of the payment page of the plugin.
        /// </summary>
        public string PaymentPageUrl { get; set; }

        /// <summary>
        /// Url to which the client will be redirected to after the payment is successful.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Password to access the payment page.
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Identifier of the company that is using the payment method.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Key to encrypt the fields that are sent to the payment page.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Key that validates Merchant Permission to use Payment Method
        /// </summary>
        public string ProfileId { get; set; }

        /// <summary>
        /// Key provided by the client which validates if the company is authorized to use said payment method.
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Define the transaction as authorized or denied.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Local currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Local language
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Additional fee that the company decides to place.
        /// </summary>
        public decimal AdditionalFee { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        public bool MarkAsPaid { get; set; }

        /// <summary>
        /// Represents whether or not you have chosen to mask the card.
        /// </summary>
        public bool CreditCardIsMasked { get; set; }

        /// <summary>
        /// Indicates the environment for Cybersource request. Either 'test' or 'live'.
        /// </summary>
        public string CybersourceEnvironment { get; set; }
    }
}