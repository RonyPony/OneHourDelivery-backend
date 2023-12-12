namespace Nop.Plugin.Synchronizers.SAPOrders.Models
{
    /// <summary>
    /// Model used to send OrderItem information to SAP API.
    /// </summary>
    public sealed class SapOrderProductModel
    {
        /// <summary>
        /// Represents the OrderItem's ItemCode.
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Represents the OrderItem's Quantity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Represents the OrderItem's UnitPrice.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Represents the OrderItem's TaxCode.
        /// </summary>
        public string TaxCode { get; set; }
    }
}
