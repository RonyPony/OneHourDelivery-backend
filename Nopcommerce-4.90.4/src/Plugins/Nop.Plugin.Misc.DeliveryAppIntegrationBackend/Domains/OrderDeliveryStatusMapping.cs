using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents the relationship between an order and the delivery app delivery status.
    /// </summary>
    public sealed class OrderDeliveryStatusMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the customer id of the messenger.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Indicates the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the delivery status.
        /// </summary>
        public int DeliveryStatusId { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was ready for pickup.
        /// </summary>
        public DateTime? AwaitingForMessengerDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was accepted by a messenger.
        /// </summary>
        public DateTime? AcceptedByMessengerDate { get; set; }

        /// <summary>
        /// Message to indicate why store declined the order.
        /// </summary>
        public string MessageToDeclinedOrder { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was retrieved by the messenger from the store.
        /// </summary>
        public DateTime? DeliveryInProgressDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was delivered.
        /// </summary>
        public DateTime? DeliveredDate { get; set; }

        /// <summary>
        /// Indicates the date and time when the order was declined by the store.
        /// </summary>
        public DateTime? DeclinedByStoreDate { get; set; }
    }
}
