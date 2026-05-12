using Nop.Web.Framework.Models;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a driver profit.
    /// </summary>
    public sealed record DriverProfitModel : BaseNopModel
    {
        /// <summary>
        /// Indicates the order identifier.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Indicates the order number.
        /// </summary>
        public string CustomOrderNumber { get; set; }

        /// <summary>
        /// Indicates the total shipping tax amount of the order.
        /// </summary>
        public decimal ShippingTaxAmount { get; set; }

        /// <summary>
        /// Indicates the profit amount for the driver.
        /// </summary>
        public decimal DriverProfitAmount { get; set; }

        /// <summary>
        /// Indicates the payment status id for the driver order pending payment.
        /// </summary>
        public int PaymentStatusId { get; set; }

        /// <summary>
        /// Indicates the payment status name for the driver order pending payment.
        /// </summary>
        public string PaymentStatusName { get; set; }

        /// <summary>
        /// Indicates the store name where the orderwas made.
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// Indicates the date and time when the delivery was completed.
        /// </summary>
        public DateTime? DeliveredOnUtc { get; set; }
    }
}
