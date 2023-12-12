using Newtonsoft.Json;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a model for <see cref="OrderRatingMapping"/>.
    /// </summary>
    public sealed class OrderRatingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the rate of the order.
        /// </summary>
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Indicates a comment abour the order.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
