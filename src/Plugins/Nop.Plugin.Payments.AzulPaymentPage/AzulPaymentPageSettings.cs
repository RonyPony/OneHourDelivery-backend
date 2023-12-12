using Nop.Core.Configuration;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.AzulPaymentPage.Enums;

namespace Nop.Plugin.Payments.AzulPaymentPage
{
    public sealed class AzulPaymentPageSettings : ISettings
    {
        /// <summary>
        /// Represents the main URL used for requesting the payment page.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Represents an alternative URL used for requesting the payment page if the main URL is not available.
        /// </summary>
        public string AlternativeUrl { get; set; }

        /// <summary>
        /// Represents the store identification number assigned when the store is affiliated with AZUL.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Represents the name that will be displayed in the payment page.
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// Represents the commerce type (just for information purpose).
        /// </summary>
        public string MerchantType { get; set; }

        /// <summary>
        /// Represents the authorization key for hashing and requesting the AZUL payment page.
        /// </summary>
        public string AuthKey { get; set; }

        /// <summary>
        /// Represents the currency used for a transaction.
        ///
        /// <para>Each store transact with one currency.
        /// This value is supplied by AZUL with the environment data access information.</para>
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the payment has been approved by AZUL.
        /// </summary>
        public string ApprovedUrl { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the payment has been declined by AZUL.
        /// </summary>
        public string DeclinedUrl { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the transaction is canceled by the customer.
        /// </summary>
        public string CancelUrl { get; set; }

        /// <summary>
        /// Represents the language used for displaying the AZUL payment page.
        ///
        /// For default this page is in Spanish (ES).
        /// </summary>
        public Locale Locale { get; set; }

        #region Advanced settings
        /// <summary>
        /// Gets or sets a value indicating whether to use this custom field.
        /// </summary>
        public bool UseCustomField1 { get; set; }

        /// <summary>
        /// Represents the description used for displaying this custom field.
        /// </summary>
        public string CustomField1Label { get; set; }

        /// <summary>
        /// Represents the value used for displaying this custom field.
        /// </summary>
        public string CustomField1Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use this custom field.
        /// </summary>
        public bool UseCustomField2 { get; set; }

        /// <summary>
        /// Represents the description used for displaying this custom field.
        /// </summary>
        public string CustomField2Label { get; set; }

        /// <summary>
        /// Represents the value used for displaying this custom field.
        /// </summary>
        public string CustomField2Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the transaction result.
        /// </summary>
        public bool ShowTransactionResult { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        public bool MarkAsPaid { get; set; }
        #endregion
    }
}
