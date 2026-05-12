using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the shipping method info to be used on an order delivery.
    /// </summary>
    public class DeliveryAppShippingInfo
    {
        /// <summary>
        /// Indicates the shipping method cost.
        /// </summary>
        [JsonProperty("shippingMethodCost")]
        public double ShippingMethodCost { get; set; }

        /// <summary>
        /// Represents the shipping method name.
        /// </summary>
        [JsonProperty("shippingMethodName")]
        public string ShippingMethodName { get; set; }
    }
}
