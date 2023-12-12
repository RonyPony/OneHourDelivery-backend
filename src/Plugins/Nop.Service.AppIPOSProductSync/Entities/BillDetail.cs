using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillDetail
    {
        public int Id { get; set; }
        public int Bill { get; set; }
        public int? PackageId { get; set; }
        public int? Room { get; set; }
        public int? Service { get; set; }
        public int? MenuItem { get; set; }
        public string Name { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal BillDiscount { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Tax3 { get; set; }
        public decimal SubTotal { get; set; }
        public decimal AmountByService { get; set; }
        public int? IdreservationDetail { get; set; }
        public decimal? QuantityAdultExtra { get; set; }
        public decimal? QuantityChildrenExtra { get; set; }
        public decimal? PriceAdultExtra { get; set; }
        public decimal? PriceChildrenExtra { get; set; }
        public decimal? RatePrice { get; set; }
        public decimal? AdultQuantity { get; set; }
        public decimal? ChildQuantity { get; set; }
        public int? RateId { get; set; }
        public decimal QuantityHours { get; set; }
        public bool IsExtra { get; set; }
        public int? RoomIdExtra { get; set; }
        public bool Promotion { get; set; }
        public int PlayTimeId { get; set; }
        public bool IsSpecialDrink { get; set; }
        public bool IsOpenBar { get; set; }
        public int? ProductId { get; set; }
        public decimal DiscountCardAmount { get; set; }
        public decimal ProductDiscountPercent { get; set; }
        public decimal CustomerDiscountPercent { get; set; }
        public decimal AmountByServicePercent { get; set; }
        public decimal DiscountCardPercent { get; set; }
        public decimal BillDiscountPercent { get; set; }
        public decimal Tax1Percent { get; set; }
        public decimal Tax2Percent { get; set; }
        public decimal Tax3Percent { get; set; }
        public int? Unit { get; set; }
        public decimal CommissionVendorPercent { get; set; }
        public int VendorId { get; set; }
        public int? CertificateId { get; set; }
        public int WorkAreaId { get; set; }
        public string Comment { get; set; }
        public bool IsOffice { get; set; }
        public int GiftTableId { get; set; }
        public int RemissionId { get; set; }
        public int WarehouseId { get; set; }
    }
}
