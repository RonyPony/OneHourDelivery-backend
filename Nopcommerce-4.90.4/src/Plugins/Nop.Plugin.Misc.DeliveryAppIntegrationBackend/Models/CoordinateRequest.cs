using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Present Coordinate request for Store and Delivery order.
    /// </summary>
    public sealed class CoordinateRequest
    {
        /// <summary>
        /// Get or set store latitude value.
        /// </summary>
        [JsonProperty("storeLatitude")]
        public decimal? StoreLatitude { get; set; }

        /// <summary>
        /// Get or set store longitude value.
        /// </summary>
        [JsonProperty("storeLongitude")]
        public decimal? StoreLongitude { get; set; }
        /// <summary>
        /// Get or set delivery order latitude value.
        /// </summary>
        [JsonProperty("deliveryOrderLatitude")]
        public decimal? DeliveryOrderLatitude { get; set; }
        /// <summary>
        /// Get or set delivery order longitude value.
        /// </summary>
        [JsonProperty("deliveryOrderLongitude")]
        public decimal? DeliveryOrderLongitude { get; set; }
    }
}

