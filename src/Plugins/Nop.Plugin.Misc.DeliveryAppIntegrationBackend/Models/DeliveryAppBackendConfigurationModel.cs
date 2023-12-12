using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Manage the values of the parameters that will be shown in the plugin configuration.
    /// </summary>
    public sealed class DeliveryAppBackendConfigurationModel : BaseNopEntityModel
    {
        #region Payment

        /// <summary>
        /// Payment Page URL for process transaction.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.PaymentPageUrl")]
        public string PaymentPageUrl { get; set; }

        /// <summary>
        /// Url to which the client will be redirected to after the payment is successful.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.RedirectUrl")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// key that provides access to the payment page. 
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.AccessKey")]
        public string AccessKey { get; set; }

        /// <summary>
        /// Company ID for validate transaction.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.MerchantId")]
        public string MerchantId { get; set; }

        /// <summary>
        /// Key that encrypt signed_fields for send to payment page.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.SecretKey")]
        public string SecretKey { get; set; }

        /// <summary>
        /// Key that validates Merchant Permission to use Payment Method
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.ProfileId")]
        public string ProfileId { get; set; }

        /// <summary>
        /// Indicates the Cybersource transaction type.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.TransactionType")]
        public string TransactionType { get; set; }

        /// <summary>
        /// Local Currency, for example: USD.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.Currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Manage the language used.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.Locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Additional cost that the merchant adds to the transaction
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Payment.AdditionalFee")]
        public decimal AdditionalFee { get; set; }

        #endregion

        #region Generics

        /// <summary>
        /// Indicates the delivery profit percentage for the administration.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Generics.AdministrativeProfit")]
        public decimal AdministrativeProfit { get; set; }

        /// <summary>
        /// Indicates the delivery profit percentage for the messenger.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Generics.MessengerProfit")]
        public decimal MessengerProfit { get; set; }

        /// <summary>
        /// Indicates the notification center url.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Generics.NotificationCenterUrl")]
        public string NotificationCenterUrl { get; set; }

        /// <summary>
        /// Indicates tracking driver notification center url.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Generics.NotificationDriverTrackingUrl")]
        public string NotificationDriverTrackingUrl { get; set; }

        /// <summary>
        /// Indicates the max amount of money a driver can carry with him.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.Generics.MaxMoneyAmountDriverCanCarry")]
        public decimal MaxMoneyAmountDriverCanCarry { get; set; }

        #endregion
    }
}
