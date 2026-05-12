using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents the order profit earned.
    /// </summary>
    public sealed class OrderProfitModel
    {
        /// <summary>
        /// Order's id
        /// </summary>
        [JsonProperty("orderId")]
        public int OrderId {get; set;}

        /// <summary>
        /// Vendor's profit
        /// </summary>
        [JsonProperty("vendorProfit")]
        public decimal? VendorProfit { get; set; }

        /// <summary>
        /// Order's created date
        /// </summary>
        [JsonProperty("orderCreated")]
        public DateTime CreatedOn { get; set; }
    }
}
