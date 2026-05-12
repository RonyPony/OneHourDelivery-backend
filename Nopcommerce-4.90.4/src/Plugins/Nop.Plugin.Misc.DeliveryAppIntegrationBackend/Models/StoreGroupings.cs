using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a store groupings
    /// </summary>
    public sealed class StoreGroupings
    {
        /// <summary>
        /// Indicates a list of stores that has been created aproximately one month ago
        /// </summary>
        [JsonProperty("newStores")]
        public IEnumerable<StoreResponseModel> NewStores { get; set; }

        /// <summary>
        /// Indicates a list of stores that have better score on its reviews.
        /// 
        /// Can also be influencitated using Popularity vendor attribute.
        /// </summary>
        [JsonProperty("popularStores")]
        public IEnumerable<StoreResponseModel> PopularStores { get; set; }

        /// <summary>
        /// Indicates a list of stores that a near the customer.
        /// </summary>
        [JsonProperty("nearStores")]
        public IEnumerable<StoreResponseModel> NearStores { get; set; }

        /// <summary>
        /// Indicates a list of stores that a near the customer.
        /// </summary>
        [JsonProperty("storesWithPromotions")]
        public IEnumerable<StoreResponseModel> StoresWithPromotions { get; set; }
    }
}
