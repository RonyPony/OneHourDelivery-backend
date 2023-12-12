#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProformProductDetail
    {
        public int Id { get; set; }
        public int ProformDetailId { get; set; }
        public int? MenuItemId { get; set; }
        public int Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public int Unit { get; set; }
        public int? ServiceId { get; set; }
        public bool ApplyColorationBox { get; set; }
    }
}
