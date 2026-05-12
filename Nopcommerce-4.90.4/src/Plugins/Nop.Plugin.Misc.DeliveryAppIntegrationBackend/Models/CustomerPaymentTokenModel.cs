using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a customer payment token model for the payment info.
    /// </summary>
    public sealed class CustomerPaymentTokenModel
    {
        /// <summary>
        /// Get or set customer's id.
        /// </summary>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or set customer's payment token
        /// </summary>
        [JsonProperty("paymenToken")]
        public string Token { get; set; }

        /// <summary>
        /// Get or set customer's payment card last four digits.
        /// </summary>
        [JsonProperty("cardLastFourDigits")]
        public string CardLastFourDigits { get; set; }

        /// <summary>
        /// Get or set customer' card type.
        /// </summary>
        [JsonProperty("cardType")]
        public string CardType { get; set; }

        /// <summary>
        /// Get or set customer's payment card expiration date.
        /// </summary>
        [JsonProperty("cardExpirationDate")]
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Get or ser customer's payment is default payment method
        /// </summary>
        [JsonProperty("isDefaultPaymentMethod")]
        public bool IsDefaultPaymentMethod { get; set; }
       
    }
}
