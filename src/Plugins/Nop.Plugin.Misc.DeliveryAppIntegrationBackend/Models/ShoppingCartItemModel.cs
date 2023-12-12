using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the products selected for the order.
    /// </summary>
    public sealed class ShoppingCartItemModel
    {
        /// <summary>
        /// Get or set product's id.
        /// </summary>
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Get or set product's quantity.
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity {get; set;}
    }
}
