using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the error response when something fails using google directions API
    /// </summary>
    public partial class GoogleDirectionRouteError
    {
        /// <summary>
        /// Indicates error message
        /// </summary>
        [JsonProperty("error_message")]
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
        public static GoogleDirectionRouteError FromJson(string json) => JsonConvert.DeserializeObject<GoogleDirectionRouteError>(json);
    }
}
