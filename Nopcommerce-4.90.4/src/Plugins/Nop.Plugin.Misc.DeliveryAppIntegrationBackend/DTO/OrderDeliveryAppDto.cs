using Newtonsoft.Json;
using Nop.Plugin.Api.DTO.Orders;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO
{
    /// <summary>
    /// Representes order delivery 
    /// </summary>
    [JsonObject(Title = "order")]
    public class OrderDeliveryAppDto : OrderDto
    {
        /// <summary>
        /// Indicates a vendor id.
        /// </summary>
        [JsonProperty("vendor_id")]
        public int VendorID { get; set; }

        /// <summary>
        /// Indicates the vendor image
        /// </summary>
        [JsonProperty("vendor_image")]
        public string VendorImage { get; set; }

        /// <summary>
        /// Indicates the vendor estimated delivery time
        /// </summary>
        [JsonProperty("estimated_delivery_time")]
        public string EstimatedDeliveryTime { get; set; }

    }
}
