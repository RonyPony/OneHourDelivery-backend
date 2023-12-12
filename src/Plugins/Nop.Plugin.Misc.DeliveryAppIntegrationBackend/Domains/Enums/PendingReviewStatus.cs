using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums
{
    /// <summary>
    /// Represents a status of the pending reviews of the customers.
    /// </summary>
    public enum PendingReviewStatus
    {
        //When the review is registered
        PendingToRate = 1,

        //when a customer reviewed.
        Reviewed = 2,

        //When a customer ignored the review
        Ignored = 3
    }
}
