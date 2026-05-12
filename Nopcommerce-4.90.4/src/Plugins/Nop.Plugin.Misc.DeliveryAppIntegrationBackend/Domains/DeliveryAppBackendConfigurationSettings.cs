using Nop.Core;
using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain
{
    public class DeliveryAppBackendConfigurationSettings : BaseEntity, ISettings
    {
        #region API

        public bool EnableApi { get; set; } = true;

        public int TokenExpiryInDays { get; set; } = 0;

        #endregion

        #region Payment

        /// <summary>
        /// Payment Page URL for process transaction.
        /// </summary>
        public string PaymentPageUrl { get; set; }

        /// <summary>
        /// Url to which the client will be redirected to after the payment is successful.
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// key that provides access to the payment page. 
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Company ID for validate transaction.
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Key that encrypt signed_fields for send to payment page.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Key that validates Merchant Permission to use Payment Method
        /// </summary>
        public string ProfileId { get; set; }

        /// <summary>
        /// Indicates the Cybersource transaction type.
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Local Currency, for example: USD.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Manage the language used.
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Additional cost that the merchant adds to the transaction
        /// </summary>
        public decimal AdditionalFee { get; set; }


        #endregion

        /// <summary>
        /// Indicates the delivery profit percentage for the administration.
        /// </summary>
        public decimal AdministrativeProfit { get; set; }

        /// <summary>
        /// Indicates the delivery profit percentage for the messenger.
        /// </summary>
        public decimal MessengerProfit { get; set; }


        /// <summary>
        /// Indicates the notification center url.
        /// </summary>
        public string NotificationCenterUrl { get; set; }

        /// <summary>
        /// Indicate tracking notification center driver url.
        /// </summary>
        public string NotificationDriverTrackingUrl { get; set; }

        /// <summary>
        /// Indicate max money a driver can have
        /// </summary>
        public int MaxMoneyAmountDriverCanCarry { get;set; }
    }
}
