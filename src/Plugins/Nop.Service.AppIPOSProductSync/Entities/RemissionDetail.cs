#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RemissionDetail
    {
        public int Id { get; set; }
        public int RemissionId { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal TaxPorc { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
