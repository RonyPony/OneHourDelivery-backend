#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderAnnulledDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
