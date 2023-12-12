using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request
{
    /// <summary>
    /// Represents an online register sale message.
    /// </summary>
    public sealed class ORequest
    {
        /// <summary>
        /// Indicates the customer personal identification number.
        /// </summary>
        public string Cip { get; set; }

        /// <summary>
        /// Indicates the sale total.
        /// </summary>
        public decimal TotalPayment { get; set; }

        /// <summary>
        /// Indicates the global discount.
        /// </summary>
        public decimal TotalDiscount { get; set; }

        /// <summary>
        /// Indicates the total taxes.
        /// </summary>
        public decimal TotalTax { get; set; }

        /// <summary>
        /// Indicates the payments quantity of the sale.
        /// </summary>
        public decimal QtyPayment { get; set; }

        /// <summary>
        /// Indicates the items quantity of the sale.
        /// </summary>
        public int QtyItemLine { get; set; }

        /// <summary>
        /// Indictaes the withdrawal of merchandise information.
        /// </summary>
        public MerchandisePickup MerchandisePickup { get; set; }

        /// <summary>
        /// Represents the items of the sale.
        /// </summary>
        public IList<Item> Items { get; set; }

        /// <summary>
        /// Represents the payments of the sale.
        /// </summary>
        public IList<Payment> Payments { get; set; }
    }
}
