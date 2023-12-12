using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a vendor review model.
    /// </summary>
    public sealed class VendorReviewModel
    {
        /// <summary>
        /// Indicates the vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the customer id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicates the order's id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the rating given by the client.
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Indicates the review comment given by the client.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// indicates customer status.
        /// </summary>
        public PendingReviewStatus ReviewStatus { get; set; }
    }
}
