using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.SAPProducts.Models.Items
{
    /// <summary>
    /// Represents the items included in the response from the SAP API.
    /// </summary>
    public sealed class SapItemModel
    {
        /// <summary>
        /// Indicates the item's code.
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Indicates the item's name.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Indicates the item's group code.
        /// </summary>
        public string ItemsGroupCode { get; set; }

        /// <summary>
        /// Indicates the item's firm code.
        /// </summary>
        public string FirmCode { get; set; }

        /// <summary>
        /// Indicates the item's vat status.
        /// </summary>
        public string VatStatus { get; set; }

        /// <summary>
        /// Indicates the item's inventory item.
        /// </summary>
        public string InventoryItem { get; set; }

        /// <summary>
        /// Indicates the item's main supplier.
        /// </summary>
        public string Mainsupplier { get; set; }

        /// <summary>
        /// Indicates the item's supplier catalog number.
        /// </summary>
        public string SupplierCatalogNo { get; set; }

        /// <summary>
        /// Indicates the item's tax type.
        /// </summary>
        public string TaxType { get; set; }

        /// <summary>
        /// Indicates the item's custom group name.
        /// </summary>
        public string CustomGroupName { get; set; }

        /// <summary>
        /// Indicates the item's custom group name percentage.
        /// </summary>
        public string CustomGroupNamePercentage { get; set; }

        /// <summary>
        /// Indicates the item's sales unit length.
        /// </summary>
        public decimal? SalesUnitLength { get; set; }

        /// <summary>
        /// Indicates the item's sales unit width.
        /// </summary>
        public decimal? SalesUnitWidth { get; set; }

        /// <summary>
        /// Indicates the item's sales unit height.
        /// </summary>
        public decimal? SalesUnitHeight { get; set; }

        /// <summary>
        /// Indicates the item's sales unit weight.
        /// </summary>
        public decimal? SalesUnitWeight { get; set; }

        /// <summary>
        /// Indicates the item's sales items per unit.
        /// </summary>
        public decimal? SalesItemsPerUnit { get; set; }

        /// <summary>
        /// Indicates the item's sales quantity per package unit.
        /// </summary>
        public decimal? SalesQtyPerPackUnit { get; set; }

        /// <summary>
        /// Indicates the item's total in stock.
        /// </summary>
        public decimal? TotalInStock { get; set; }

        /// <summary>
        /// Indicates the item's total committed.
        /// </summary>
        public decimal? TotalCommitted { get; set; }

        /// <summary>
        /// Indicates the item's total on order.
        /// </summary>
        public decimal? TotalOnOrder { get; set; }

        /// <summary>
        /// Indicates the item's total available.
        /// </summary>
        public decimal? TotalAvailable { get; set; }

        /// <summary>
        /// Indicates the item's total average cost.
        /// </summary>
        public decimal? TotalAvgcost { get; set; }

        /// <summary>
        /// Indicates the item's picture path.
        /// </summary>
        public string PicturePath { get; set; }

        /// <summary>
        /// Indicates the item's picture name.
        /// </summary>
        public string PictureName { get; set; }

        /// <summary>
        /// Indicates the item's default price list.
        /// </summary>
        public string DefaultPriceList { get; set; }

        /// <summary>
        /// Indicates the item's default price list name.
        /// </summary>
        public string DefaultPriceListName { get; set; }

        /// <summary>
        /// Indicates the item's prices list.
        /// </summary>
        public List<ItemPrice> ItemPrices { get; set; }

        /// <summary>
        /// Indicates the item's warehouses list.
        /// </summary>
        public List<ItemWarehouseInfo> ItemWarehouseInfoCollection { get; set; }
    }
}
