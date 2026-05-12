using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the error response when something fails using google distance matrix api
    /// </summary>
    public partial class DistanceRouteInfoError
    {
        /// <summary>
        /// Indicates error message
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Indicates response status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Build an instance of <see cref="DistanceRouteInfoError"/> from a json
        /// </summary>
        /// <param name="json">Json object to convert<</param>
        public static DistanceRouteInfoError FromJson(string json) => JsonConvert.DeserializeObject<DistanceRouteInfoError>(json);
    }
}
