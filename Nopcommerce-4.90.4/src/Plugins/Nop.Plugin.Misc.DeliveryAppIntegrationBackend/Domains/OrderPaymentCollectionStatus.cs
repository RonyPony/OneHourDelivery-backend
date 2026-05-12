using Nop.Core;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains
{
    /// <summary>
    /// Represents the payment collection status of an order.
    /// </summary>
    public sealed class OrderPaymentCollectionStatus : BaseEntity
    {
        /// <summary>
        /// Indicates the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the customer id of the driver assigned to the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicates the total of the order.
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Indicates the payment collection status id.
        /// </summary>
        public int PaymentCollectionStatusId { get; set; }

        /// <summary>
        /// Indicates the date of creation (should be the same as the date the order was accepted by the driver).
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Indicates the customer id of the person who collected the order payment from the driver.
        /// </summary>
        public int? CollectedByCustomerId { get; set; }

        /// <summary>
        /// Indicates the date of collection of the order payment.
        /// </summary>
        public DateTime? CollectedOnUtc { get; set; }

        /// <summary>
        /// Indicates the payment collection status.
        /// </summary>
        public PaymentCollectionStatus PaymentCollectionStatus
        {
            get => (PaymentCollectionStatus)PaymentCollectionStatusId;
            set => PaymentCollectionStatusId = (int)value;
        }
    }
}
