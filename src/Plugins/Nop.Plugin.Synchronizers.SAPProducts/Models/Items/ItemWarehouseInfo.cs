namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Items
{
    /// <summary>
    /// Represents an item's warehouse loaded from SAP.
    /// </summary>
    public sealed class ItemWarehouseInfo
    {
        /// <summary>
        /// Indicates the warehouse's minimal stock for the item.
        /// </summary>
        public decimal? MinimalStock { get; set; }

        /// <summary>
        /// Indicates the warehouse's maximal stock for the item.
        /// </summary>
        public decimal? MaximalStock { get; set; }

        /// <summary>
        /// Indicates the warehouse's minimal order for the item.
        /// </summary>
        public decimal? MinimalOrder { get; set; }

        /// <summary>
        /// Indicates the warehouse's standart average price for the item.
        /// </summary>
        public decimal? StandardAveragePrice { get; set; }

        /// <summary>
        /// Indicates the warehouse's locked status for the item.
        /// </summary>
        public string Locked { get; set; }

        /// <summary>
        /// Indicates the warehouse's code.
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// Indicates the warehouse's stock quantity for the item.
        /// </summary>
        public decimal? InStock { get; set; }

        /// <summary>
        /// Indicates the warehouse's committed quantity for the item.
        /// </summary>
        public decimal? Committed { get; set; }

        /// <summary>
        /// Indicates the warehouse's ordered quantity for the item.
        /// </summary>
        public decimal? Ordered { get; set; }
    }
}
