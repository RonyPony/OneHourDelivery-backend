using Newtonsoft.Json;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a model for <see cref="CustomerRatingMapping"/>.
    /// </summary>
    public sealed class CustomerRatingMappingRequest
    {
        /// <summary>
        /// Indicate the creator customer id.
        /// </summary>
        public int CreatorCustomerId { get; set; }

        /// <summary>
        /// Indicate the rated customer id.
        /// </summary>
        public int RatedCustomerId { get; set; }

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
