using Newtonsoft.Json;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Api.DTO.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the vendor profit earned by orders.
    /// </summary>
    public sealed class ProfitVendorModel
    {
        /// <summary>
        ///  Orders associated with the vendor
        /// </summary>
        [JsonProperty("orders")]
        public IList<OrderProfitModel> Orders { get; set; }

        /// <summary>
        /// vendor profit earned value.
        /// </summary>
        [JsonProperty("earnedProfitTotal")]
        public decimal? EarnedProfitTotal { get; set; }

        /// <summary>
        /// Vendor's name
        /// </summary>
        [JsonProperty("vendorName")]
        public string VendorName { get; set; }

        /// <summary>
        /// Vendor's rating
        /// </summary>
        [JsonProperty("vendorRating")]
        public decimal? VendorRating { get; set; }

        /// <summary>
        /// Vendor's image
        /// </summary>
        [JsonProperty("vendorPictureUrl")]
        public string VendorPictureUrl { get; set; }
    }
}
