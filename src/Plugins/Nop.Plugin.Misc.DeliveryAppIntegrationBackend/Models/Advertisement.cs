using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents an advertisement image.
    /// </summary>
    public sealed class Advertisement
    {
        /// <summary>
        /// Indicates image url.
        /// </summary>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
