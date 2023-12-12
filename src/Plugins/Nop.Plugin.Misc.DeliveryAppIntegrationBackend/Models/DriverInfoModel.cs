using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a driver info model
    /// </summary>
    public sealed class DriverInfoModel
    {
        /// <summary>
        /// Driver's id.
        /// </summary>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Driver's name.
        /// </summary>\
        [JsonProperty("driverName")]
        public string DriverName { get; set; }

        /// <summary>
        /// Driver's profil picture
        /// </summary>
        [JsonProperty("driverPicture")]
        public string DriverPicture { get; set; }

        /// <summary>
        /// Driver's rating
        /// </summary>
        [JsonProperty("driverRating")]
        public decimal DriverRating { get; set; }

        /// <summary>
        /// Driver's orders delivered count
        /// </summary>
        [JsonProperty("orderDeliveredCount")]
        public int OrdersDeliveredCount { get; set; }

        /// <summary>
        /// Driver's registered date.
        /// </summary>
        [JsonProperty("driverRegisteredDate")]
        public int DriverRegisteredDate { get; set; }

        /// <summary>
        /// Driver's vehicle info.
        /// </summary>
        [JsonProperty("driverVehicleInfo")]
        public DriverVehicleInfo DriverVehicleInfo { get; set; } 
    }
}
