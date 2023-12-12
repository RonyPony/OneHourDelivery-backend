#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewRemissionDetail
    {
        public int IdRemisionDetail { get; set; }
        public int IdRemision { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxPorc { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal? PriceTotal { get; set; }
    }
}
