using Newtonsoft.Json;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO
{
    /// <summary>
    /// Represent orders history by vendor
    /// </summary>
    [JsonObject(Title = "order")]
    public class HistoricOrdersByVendorDto
    {
        /// <summary>
        /// Gets or sets the order id
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the vendor name
        /// </summary>
        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        [JsonProperty("order_status")]
        public int OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the order item length
        /// </summary>
        [JsonProperty("order_items_length")]
        public int OrderItemsLength { get; set; }

        /// <summary>
        /// Gets or sets the order subtotal (incl tax)
        /// </summary>
        [JsonProperty("order_subtotal_incl_tax")]
        public decimal? OrderSubtotalInclTax { get; set; }

        /// <summary>
        /// Gets or sets the delivery date and time
        /// </summary>
        [JsonProperty("delivery_date_utc")]
        public DateTime? DeliveryDateUtc { get; set; }
    }
}
