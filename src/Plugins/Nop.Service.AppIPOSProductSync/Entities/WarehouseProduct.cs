#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class WarehouseProduct
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int Warehouse { get; set; }
        public decimal Quantity { get; set; }
        public decimal? SubUnitQty { get; set; }
    }
}
