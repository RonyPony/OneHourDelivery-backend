using Nop.Core;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents an order declined by a messenger.
    /// </summary>
    public sealed class MessengerDeclinedOrderMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the customer id of the messenger.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicates the order id of the rejected order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the date and time of rejection.
        /// </summary>
        public DateTime DeclinedDate { get; set; }

        /// <summary>
        /// Indicates the reason of rejection.
        /// </summary>
        public string DeclinedMessage { get; set; }
    }
}
