using Nop.Core;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represent a customer discount mapping.
    /// </summary>
    public class CustomerDiscountMapping : BaseEntity
    {
        /// <summary>
        /// Get or set customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or set discount's id.
        /// </summary>
        public int DiscountId { get; set; }

        /// <summary>
        /// Get or set register Date.
        /// </summary>
        public DateTime CreatedAtUtc { get; set; }
    }
}
