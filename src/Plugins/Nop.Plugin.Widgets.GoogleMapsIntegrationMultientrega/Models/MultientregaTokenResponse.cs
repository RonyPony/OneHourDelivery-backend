using Newtonsoft.Json;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents multientrega Token response.
    /// </summary>
    public class MultientregaTokenResponse
    {
        /// <summary>
        /// Indicates if the request was successfull.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Indicates the data returned by the service.
        /// </summary>
        [JsonProperty("data")]
        public TokenData Data { get; set; }

        /// <summary>
        /// Indicates the error message in case the response isn't success.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }

    /// <summary>
    /// Represents token data.
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// Indicates the token
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
