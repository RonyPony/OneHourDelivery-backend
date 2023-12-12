namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Items
{
    /// <summary>
    /// Represents an item's price list loaded from SAP.
    /// </summary>
    public sealed class ItemPrice
    {
        /// <summary>
        /// Indicates the price list's id.
        /// </summary>
        public int PriceList { get; set; }

        /// <summary>
        /// Idicates the item's price for this price list.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Indicates the price list's currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Indicates the price list's additional currency.
        /// </summary>
        public string AdditionalCurrency2 { get; set; }

        /// <summary>
        /// Indicates the base price list id for this price list.
        /// </summary>
        public int BasePriceList { get; set; }

        /// <summary>
        /// Indicates the multiplicative factor of the price in this price list in relation to the price in the base price list.
        /// </summary>
        public decimal? Factor { get; set; }
    }
}
