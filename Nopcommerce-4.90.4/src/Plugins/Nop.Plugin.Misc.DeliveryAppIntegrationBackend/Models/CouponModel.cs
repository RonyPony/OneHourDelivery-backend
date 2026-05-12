using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a coupon discount.
    /// </summary>
    public sealed class CouponModel
    {
        /// <summary>
        /// Get or set discount's amount
        /// </summary>
        [JsonProperty("discountAmount")]
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// Get or set discount percentage status
        /// </summary>
        [JsonProperty("usePercentage")]
        public bool UsePercentage { get; set; }
    }
}
