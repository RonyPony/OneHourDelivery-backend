using Newtonsoft.Json;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents the response model for tax rate from Multientrega.
    /// </summary>
    public sealed class MultientregaTaxRateResponse
    {
        /// <summary>
        /// Indicates the tax rate.
        /// </summary>
        [JsonProperty("tarifa")]
        public decimal Tarifa { get; set; }
    }
}
