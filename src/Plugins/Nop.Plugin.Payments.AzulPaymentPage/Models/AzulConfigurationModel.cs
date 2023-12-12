using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Payments.AzulPaymentPage.Models
{
    /// <summary>
    /// Represents the model used for configuring the AZUL Payment plug-in.
    /// </summary>
    public sealed class AzulConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of AzulConfigurationModel class.
        /// </summary>
        public AzulConfigurationModel() => Locale = new List<SelectListItem>();

        /// <summary>
        /// Represents the main URL used for requesting the payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.Url")]
        public string Url { get; set; }

        /// <summary>
        /// Represents an alternative URL used for requesting the payment page if the main URL is not available.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.AlternativeUrl")]
        public string AlternativeUrl { get; set; }

        /// <summary>
        /// Represents the store identification number assigned when the store is affiliated with AZUL.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.MerchantId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Represents the name that will be displayed in the payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.MerchantName")]
        public string MerchantName { get; set; }

        /// <summary>
        /// Represents the commerce type (just for information purpose).
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.MerchantType")]
        public string MerchantType { get; set; }

        /// <summary>
        /// Represents the authorization key for hashing and requesting the AZUL payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.AuthKey")]
        public string AuthKey { get; set; }

        /// <summary>
        /// Represents the currency used for a transaction.
        ///
        /// <para>
        ///     Each store transact with one currency.
        ///     This value is supplied by AZUL with the environment data access information.
        /// </para>
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CurrencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the payment has been approved by AZUL.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.ApprovedUrl")]
        public string ApprovedUrl { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the payment has been declined by AZUL.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.DeclinedUrl")]
        public string DeclinedUrl { get; set; }

        /// <summary>
        /// Represents the URL used for redirecting the customer when the transaction is canceled by the customer.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CancelUrl")]
        public string CancelUrl { get; set; }

        /// <summary>
        /// Represents the selected language used for displaying the AZUL payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.Locale")]
        public int LocaleId { get; set; }

        /// <summary>
        /// Represents the languages available for displaying the AZUL payment page.
        /// </summary>
        public IList<SelectListItem> Locale { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.UseCustomField1")]
        public bool UseCustomField1 { get; set; }

        /// <summary>
        /// Represents the description used for displaying this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CustomField1Label")]
        public string CustomField1Label { get; set; }

        /// <summary>
        /// Represents the value used for displaying this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CustomField1Value")]
        public string CustomField1Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.UseCustomField2")]
        public bool UseCustomField2 { get; set; }

        /// <summary>
        /// Represents the description used for displaying this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CustomField2Label")]
        public string CustomField2Label { get; set; }

        /// <summary>
        /// Represents the value used for displaying this custom field.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.CustomField2Value")]
        public string CustomField2Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the transaction result.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.ShowTransactionResult")]
        public bool ShowTransactionResult { get; set; }

        /// <summary>
        /// Represents the selected status an order will has after processing the payment.
        /// </summary>
        public IList<SelectListItem> OrderStatus { get; set; }

        /// <summary>
        /// Represents the selected language used for displaying the AZUL payment page.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.OrderStatus")]
        public int OrderStatusId { get; set; }

        /// <summary>
        /// Represents if an order will be mark as paid or not after processing the payment.
        /// </summary>
        [NopResourceDisplayName("Plugins.Payments.AzulPaymentPage.Fields.MarkAsPaid")]
        public bool MarkAsPaid { get; set; }
    }
}
