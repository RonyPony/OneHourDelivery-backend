using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a request to get order total taxes for an order.
    /// </summary>
    public sealed class GetOrderTotalTaxRequest
    {
        /// <summary>
        /// Products that are going to be ordered
        /// </summary>
        [JsonProperty("orderProducts")]
        public List<ShoppingCartItemModel> OrderProducts { get; set; }

        /// <summary>
        /// Customer id.
        /// </summary>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }
    }
}
