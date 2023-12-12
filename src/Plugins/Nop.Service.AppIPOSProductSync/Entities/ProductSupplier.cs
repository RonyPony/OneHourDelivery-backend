#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductSupplier
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SupplyerId { get; set; }
    }
}
