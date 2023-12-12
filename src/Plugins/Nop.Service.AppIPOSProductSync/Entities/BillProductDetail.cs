#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillProductDetail
    {
        public int Id { get; set; }
        public int BillDetailId { get; set; }
        public int? MenuItemId { get; set; }
        public int Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public int Unit { get; set; }
        public int? ServiceId { get; set; }
        public bool ApplyColorationBox { get; set; }
    }
}
