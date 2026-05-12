using Newtonsoft.Json;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents to customer pending review model.
    /// </summary>
    public sealed class CustomerPendingReviewsModel
    {
        /// <summary>
        /// Customer's id.
        /// </summary>
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Order's id.
        /// </summary>
        [JsonProperty("orderId")]
        public int OrderId { get; set; }

        /// <summary>
        /// Reviewed vendor id.
        /// </summary>
        [JsonProperty("vendorId")]
        public int VendorId { get; set; }

        /// <summary>
        /// Review's status
        /// </summary>
        [JsonProperty("pendingReviewStatus")]
        public PendingReviewStatus PendingReviewStatus { get; set; }
    }
}
