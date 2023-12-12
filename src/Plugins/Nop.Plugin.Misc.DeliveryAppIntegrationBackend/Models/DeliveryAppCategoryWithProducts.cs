using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a list products group by CategoryName
    /// </summary>
    public sealed class DeliveryAppCategoryWithProducts
    {
        /// <summary>
        /// Indicates the category name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates the products present in category.
        /// </summary>
        [JsonProperty("products")]
        public IEnumerable<DeliveryAppProduct> Products { get; set; }
    }
}
