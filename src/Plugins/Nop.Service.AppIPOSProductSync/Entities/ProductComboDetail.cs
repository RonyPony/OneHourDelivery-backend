#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductComboDetail
    {
        public int Id { get; set; }
        public int ProductComboId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Cost { get; set; }
    }
}
