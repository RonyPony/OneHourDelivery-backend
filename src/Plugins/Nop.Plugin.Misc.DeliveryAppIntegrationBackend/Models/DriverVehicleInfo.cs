

using Newtonsoft.Json;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents Driver Vehicle info model.
    /// </summary>
    public sealed class DriverVehicleInfo
    {
        /// <summary>
        /// Vehicle's brand.
        /// </summary>
        [JsonProperty("vehicleBrand")]
        public string VehicleBrand { get; set; }

        /// <summary>
        /// Vehicle's model.
        /// </summary>
        [JsonProperty("vehicleModel")]
        public string VehicleModel { get; set; }

        /// <summary>
        /// Vehicle's color.
        /// </summary>
        [JsonProperty("vehicleColor")]
        public string  VehicleColor { get; set; }

        /// <summary>
        /// Vehicle's plate.
        /// </summary>
        [JsonProperty("vehiclePlate")]
        public string  VehiclePlate { get; set; }
    }
}
