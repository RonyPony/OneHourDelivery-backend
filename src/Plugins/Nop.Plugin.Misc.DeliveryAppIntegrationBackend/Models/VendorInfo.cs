using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents vendor info model.
    /// </summary>
    public sealed class VendorInfo
    {
        /// <summary>
        /// Vendor's id
        /// </summary>
        [JsonProperty("vendorId")]
        public int VendorId { get; set; }

        /// <summary>
        /// Vendor's name
        /// </summary>
        [JsonProperty("vendorName")]
        public string Name { get; set; }

        /// <summary>
        /// Vendor's picture
        /// </summary>
        [JsonProperty("vendorPictureUrl")]
        public string PictureUrl { get; set; }

        /// <summary>
        /// Vendor's rating
        /// </summary>
        [JsonProperty("vendorRating")]
        public decimal Rating { get; set; }

        /// <summary>
        /// Vendor's stimated time.
        /// </summary>
        [JsonProperty("vendorEstimatedTime")]
        public string EstimatedTime { get; set; }

        /// <summary>
        /// Vendor's specialities
        /// </summary>
        [JsonProperty("vendorSpecialities")]
        public IList<string> Specialities { get; set; }

        /// <summary>
        /// Vendor's schedule.
        /// </summary>
        [JsonProperty("vendorSchedules")]
        public IList<VendorWarehouseInfo> VendorSchedules { get; set; }
    }
}
