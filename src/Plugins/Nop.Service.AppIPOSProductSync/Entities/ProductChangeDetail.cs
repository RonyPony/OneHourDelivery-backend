#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductChangeDetail
    {
        public int Id { get; set; }
        public int ProductChange { get; set; }
        public int Product { get; set; }
        public double Quantity { get; set; }
        public decimal? Cost { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int? Unit { get; set; }
        public bool? IsTakeOff { get; set; }
        public bool? IsAdd { get; set; }
    }
}
