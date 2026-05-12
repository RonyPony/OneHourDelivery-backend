using Nop.Core;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents a rating of an order.
    /// </summary>
    public sealed class OrderRatingMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the rate of the order.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Indicates a comment abour the order.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Indicates the date and time when the order w
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }
    }
}
