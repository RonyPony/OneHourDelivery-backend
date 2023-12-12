using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents driver's cash payment collection information.
    /// </summary>
    public sealed class DriverPaymentCollection
    {
        /// <summary>
        /// Indicates the total amount of money the driver has pending to deliver to OHD administration.
        /// </summary>
        [JsonProperty("totalAmountPendingToDeliver")]
        public decimal TotalAmountPendingToDeliver { get; set; }

        /// <summary>
        /// Indicates the limit amount of cash money the driver can carry. 
        /// </summary>
        [JsonProperty("totalLimitAmount")]
        public decimal TotalLimitAmount { get; set; }
    }
}
