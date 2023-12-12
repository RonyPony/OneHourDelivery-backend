#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public int ProductCategory { get; set; }
        public int Unit { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public decimal AlertQuantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public bool Locked { get; set; }
        public bool? Active { get; set; }
        public string BarCode { get; set; }
        public decimal Discount { get; set; }
        public bool Excent { get; set; }
        public bool AlwaysAvailable { get; set; }
        public bool UseSubUnit { get; set; }
        public int? SubUnit { get; set; }
        public decimal? Portion { get; set; }
        public decimal? PricePortion { get; set; }
        public decimal? QuantityUnit { get; set; }
        public decimal? QuantitySubUnit { get; set; }
        public bool ApplyBillMaterials { get; set; }
        public int TrademarkId { get; set; }
        public bool DiscountApplies { get; set; }
        public byte[] Picture { get; set; }
        public decimal Commission { get; set; }
        public string Presentation { get; set; }
        public string Treatment { get; set; }
        public string Location { get; set; }
        public bool UseMarginPrice { get; set; }
        public decimal MarginPrice { get; set; }
        public int FlagMarginPrice { get; set; }
        public decimal QuantityBill { get; set; }
    }
}
