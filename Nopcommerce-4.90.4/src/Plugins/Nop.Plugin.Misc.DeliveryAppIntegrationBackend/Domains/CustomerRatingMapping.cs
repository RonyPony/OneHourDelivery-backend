using Nop.Core;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents a Customer's Rating.
    /// </summary>
    public class CustomerRatingMapping : BaseEntity
    {
        /// <summary>
        /// Indicate  the CreatorCustomer id.
        /// </summary>
        public int CreatorCustomerId { get; set; }
        /// <summary>
        /// Indicate  the RatedCustomer id.
        /// </summary>
        public int RatedCustomerId { get; set; }
        /// <summary>
        /// Indicate the rate of delivery.
        /// </summary>
        public decimal? Rate { get; set; }
        /// <summary>
        /// Indicate the comment about the delivery.
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Indicate the date and time when the rating was created.
        /// </summary>
        public DateTime CreateOnUtc { get; set; }
    }
}
