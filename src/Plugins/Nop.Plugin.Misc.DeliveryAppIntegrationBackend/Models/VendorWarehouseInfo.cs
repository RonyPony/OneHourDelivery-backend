using Newtonsoft.Json;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents vendor schedule by warehouse.
    /// </summary>
    public class VendorWarehouseInfo
    {
        /// <summary>
        /// Vendor's schedule day
        /// </summary>
        [JsonProperty("scheduleDay")]
        public int DayId { get; set; }

        /// <summary>
        /// Vendor's schedule begin time.
        /// </summary>
        [JsonProperty("beginTime")]
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// Vendor's schedule end time.
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Indicates if the vendor is open this day
        /// </summary>
        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }
    }
}
