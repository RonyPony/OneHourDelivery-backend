#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ServiceItemDetail
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public bool SubUnit { get; set; }
        public decimal Quantity { get; set; }
        public decimal? Cost { get; set; }
    }
}
