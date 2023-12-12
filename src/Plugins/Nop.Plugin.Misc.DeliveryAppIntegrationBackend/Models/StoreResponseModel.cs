using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the store summarized info.
    /// </summary>
    public class StoreResponseModel
    {
        /// <summary>
        /// Indicates the vendor identification.
        /// </summary>
        [JsonProperty("vendorId")]
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the vendor name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates the logo url.
        /// </summary>
        [JsonProperty("logoUrl")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Indicates the vendor rating.
        /// </summary>
        [JsonProperty("rating")]
        public double Rating { get; set; }

        /// <summary>
        /// Indicates the vendor store proximity in meters.
        /// </summary>
        [JsonProperty("proximityInMeters")]
        public double ProximityInMeters { get; set; }

        /// <summary>
        /// Indicates the vendor store Estimated Preparation Time
        /// </summary>
        [JsonProperty("estimatedPreparationTime")]
        public string EstimatedPreparationTime { get; set; }

        /// <summary>
        /// Indicates the vendor tags
        /// </summary>
        [JsonProperty("vendorTags")]
        public IEnumerable<string> VendorTags { get; set; }

        /// <summary>
        /// Indicates if the vendor store is for adults only
        /// </summary>
        [JsonProperty("adultsLimitated")]
        public bool AdultsLimitated { get; set; }        
        
        /// <summary>
        /// Indicates if the vendor store has promotions
        /// </summary>
        [JsonProperty("hasPromotions")]
        public bool HasPromotions { get; set; }        
        
        /// <summary>
        /// Indicates if the vendor store is open
        /// </summary>
        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Indicates the store schedule.
        /// </summary>
        [JsonProperty("storeSchedule")]
        public string StoreSchedule { get; set; }

        /// <summary>
        /// Indicates the vendor's category
        /// </summary>
        [JsonProperty("vendorCategory")]
        public string VendorCategory { get; set; }
        
        /// <summary>
        /// Indicates the store created date.
        /// </summary>
        public DateTime CreatedDateOnUtc { get; set; }
    }
}
