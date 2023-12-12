#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class MenuItemDetail
    {
        public int Id { get; set; }
        public int MenuItem { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public bool SubUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Cost { get; set; }
    }
}
