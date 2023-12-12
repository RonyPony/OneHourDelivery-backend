#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductCombo
    {
        public int Id { get; set; }
        public int ProductCategory { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal Price { get; set; }
        public decimal? Quantity { get; set; }
        public bool DiscountApplies { get; set; }
        public decimal Discount { get; set; }
        public bool Excent { get; set; }
        public bool? Active { get; set; }
    }
}
