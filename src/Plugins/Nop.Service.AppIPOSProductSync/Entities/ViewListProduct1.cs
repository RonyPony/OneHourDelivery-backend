#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewListProduct1
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
        public bool Active { get; set; }
        public string BarCode { get; set; }
        public decimal Discount { get; set; }
        public bool Excent { get; set; }
        public bool AlwaysAvailable { get; set; }
        public bool UseSubUnit { get; set; }
        public int? SubUnit { get; set; }
        public decimal? Portion { get; set; }
        public decimal? PricePortion { get; set; }
        public string ProductCategoryName { get; set; }
        public string UnitName { get; set; }
        public string SubUnitName { get; set; }
        public byte[] Picture { get; set; }
    }
}
