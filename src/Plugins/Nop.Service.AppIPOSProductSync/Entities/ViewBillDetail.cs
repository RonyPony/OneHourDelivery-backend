using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBillDetail
    {
        public int IdBillDetail { get; set; }
        public int IdBill { get; set; }
        public int? PackageId { get; set; }
        public string Description { get; set; }
        public string Tipo { get; set; }
        public string Number { get; set; }
        public int? Room { get; set; }
        public int? Service { get; set; }
        public int? MenuItem { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityHours { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal BillDiscount { get; set; }
        public decimal? DiscountTotal { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Tax3 { get; set; }
        public decimal? TaxTotal { get; set; }
        public decimal AmountByService { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal? AdultQuantity { get; set; }
        public decimal? ChildQuantity { get; set; }
        public decimal? QuantityAdultExtra { get; set; }
        public decimal? QuantityChildrenExtra { get; set; }
        public decimal? PriceAdultExtra { get; set; }
        public decimal? PriceChildrenExtra { get; set; }
        public decimal? RatePrice { get; set; }
        public decimal? QuantityByRate { get; set; }
        public int? RateId { get; set; }
        public string RateName { get; set; }
        public int? RoomIdExtra { get; set; }
        public bool IsExtra { get; set; }
        public int? ProductId { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int GiftTableId { get; set; }
        public string GiftTableName { get; set; }
        public int RemissionId { get; set; }
        public decimal CommissionVendorPercent { get; set; }
        public decimal DiscountCardAmount { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductMarcaName { get; set; }
        public string MenuCategoryName { get; set; }
        public string MenuSubCategoryName { get; set; }
        public string RoomTypeName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string ServiceSubCategoryName { get; set; }
        public string TableName { get; set; }
        public int? CertificadoId { get; set; }
        public string CertificadoCode { get; set; }
        public int WorkAreaId { get; set; }
        public decimal ProrrateoNc { get; set; }
        public string UnitName { get; set; }
        public int? TrademarkId { get; set; }
    }
}
