using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the Vendor restaurant detail model
    /// </summary>
    public sealed class DeliveryAppVendor
    {
        /// <summary>
        /// Indicates the vendor id.
        /// </summary>
        [JsonProperty("vendorId")]
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the vendor name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates the vendor description.
        /// </summary>
        [JsonProperty("info")]
        public string Info { get; set; }

        /// <summary>
        /// Indicates the vendor tags
        /// </summary>
        [JsonProperty("vendorTags")]
        public IEnumerable<string> VendorTags { get; set; }

        /// <summary>
        /// Indicates the vendor image.
        /// </summary>
        [JsonProperty("image")]
        public string Image { get; set; }

        /// <summary>
        /// Indicates the vendor rating
        /// </summary>
        [JsonProperty("rating")]
        public double Rating { get; set; }

        /// <summary>
        /// Indicates the vendor estimated time.
        /// </summary>
        [JsonProperty("estimatedWaitTime")]
        public string EstimatedWaitTime { get; set; }

        /// <summary>
        /// Indicates the vendor's category.
        /// </summary>
        [JsonProperty("vendorCategory")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Indicates if the vendor has free shipping
        /// </summary>
        [JsonProperty("isFreeShipping")]
        public bool IsFreeShipping { get; set; }

        /// <summary>
        /// Indicates the vendor Additional Shipping Charge
        /// </summary>
        [JsonProperty("additionalShippingCharge")]
        public decimal AdditionalShippingCharge { get; set; }

        /// <summary>
        /// Indicates the vendor products group by categories.
        /// </summary>
        [JsonProperty("categories")]
        public IEnumerable<DeliveryAppCategoryWithProducts> Categories { get; set; }

        /// <summary>
        /// Indicates if the vendor store is for adults only
        /// </summary>
        [JsonProperty("adultsLimitated")]
        public bool AdultsLimitated { get; set; }

        /// <summary>
        /// Indicates if the vendor store is open
        /// </summary>
        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Indicates if the vendor is favorite.
        /// </summary>
        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; }
    }
}
