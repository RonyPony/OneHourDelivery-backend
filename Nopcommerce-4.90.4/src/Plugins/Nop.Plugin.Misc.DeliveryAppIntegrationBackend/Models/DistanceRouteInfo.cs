using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the responses when a request is successfull using google distance matrix api
    /// </summary>
    public partial class DistanceRouteInfo
    {
        /// <summary>
        /// Indicates response status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Indicates Source Addresses.
        /// </summary>
        [JsonProperty("origin_addresses")]
        public string[] OriginAddresses { get; set; }

        /// <summary>
        /// Indicates Destination Addresses.
        /// </summary>
        [JsonProperty("destination_addresses")]
        public string[] DestinationAddresses { get; set; }

        /// <summary>
        /// Indicates a collection of routes
        /// </summary>
        [JsonProperty("rows")]
        public Row[] Rows { get; set; }

        /// <summary>
        /// Build an instance of <see cref="DistanceRouteInfoError"/> from a json
        /// </summary>
        /// <param name="json">Json object to convert</param>
        public static DistanceRouteInfo FromJson(string json) => JsonConvert.DeserializeObject<DistanceRouteInfo>(json);
    }

    /// <summary>
    /// Represents a collection of routes
    /// </summary>
    public partial class Row
    {
        /// <summary>
        /// Indicates a collection of routes information.
        /// </summary>
        [JsonProperty("elements")]
        public Element[] Elements { get; set; }
    }

    /// <summary>
    /// Represents a route information
    /// </summary>
    public partial class Element
    {
        /// <summary>
        /// Indicates route status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Indicates route duration to arrive to destination
        /// </summary>
        [JsonProperty("duration")]
        public Distance Duration { get; set; }

        /// <summary>
        /// Indicates route distance from source to destination
        /// </summary>
        [JsonProperty("distance")]
        public Distance Distance { get; set; }
    }

    /// <summary>
    /// Represents a distance information
    /// </summary>
    public partial class Distance
    {
        /// <summary>
        /// Indicates distance in meters
        /// </summary>
        [JsonProperty("value")]
        public long Value { get; set; }

        /// <summary>
        /// Indicates distance text in kilometers
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    /// <summary>
    /// Extension method to serialize to json <see cref="DistanceRouteInfo"/>
    /// </summary>
    public static class Serialize
    {
        public static string ToJson(this DistanceRouteInfo self) => JsonConvert.SerializeObject(self);
    }
}
