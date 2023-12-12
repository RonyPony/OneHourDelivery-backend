#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderDetailComplement
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int MenuItemId { get; set; }
        public int ComplementMenu { get; set; }
        public decimal Quantity { get; set; }
    }
}
