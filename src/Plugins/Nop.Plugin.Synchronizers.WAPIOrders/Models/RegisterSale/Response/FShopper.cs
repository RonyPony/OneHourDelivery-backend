using System;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Response
{
    /// <summary>
    /// Represents the frequent customer response.
    /// </summary>
    public sealed class FShopper
    {
        /// <summary>
        /// Indicates the customer personal identification number.
        /// </summary>
        public string Cip { get; set; }

        /// <summary>
        /// Indicates the level of the customer.
        /// </summary>
        public string CustomerLevel { get; set; }

        /// <summary>
        /// Indicates the point before the sale.
        /// </summary>
        public int PreviousPoints { get; set; }

        /// <summary>
        /// Indicates the points accumulated for the sale.
        /// </summary>
        public int TransactionPoints { get; set; }

        /// <summary>
        /// Indicates the points balance.
        /// </summary>
        public int PointsBalance { get; set; }

        /// <summary>
        /// Indicates the points expiration date.
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}
