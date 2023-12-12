namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Request
{
    /// <summary>
    /// Represents a sale item.
    /// </summary>
    public sealed class Item
    {
        /// <summary>
        /// Idicates the correlative line number of item.
        /// </summary>
        public int ItemLine { get; set; }

        /// <summary>
        /// Indicates the code of item.
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Indicates the item description.
        /// </summary>
        public string ItemDescription { get; set; }

        /// <summary>
        /// Indicates the price of the item.
        /// </summary>
        public decimal ExtOriginalPrice { get; set; }

        /// <summary>
        /// Indicates the sell price of the item.
        /// </summary>
        public decimal ExtSellPrice { get; set; }

        /// <summary>
        /// Indicates the item sold quantity.
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// Indicates the item tax code.
        /// </summary>
        public int TaxCod { get; set; }

        /// <summary>
        /// Indicates the item tax amount.
        /// </summary>
        public decimal TaxAmt { get; set; }

        /// <summary>
        /// Indicates the item discount amount.
        /// </summary>
        public decimal ItemDiscount { get; set; }
    }
}
