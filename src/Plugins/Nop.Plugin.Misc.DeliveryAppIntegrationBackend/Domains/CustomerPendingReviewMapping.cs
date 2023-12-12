using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents a customer pending review.
    /// </summary>
    public class CustomerPendingReviewMapping : BaseEntity
    {
        /// <summary>
        /// Customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Order's id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Reviewed vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Review's status
        /// </summary>
        public PendingReviewStatus PendingReviewStatus { get; set; }

        /// <summary>
        /// review Registered
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
