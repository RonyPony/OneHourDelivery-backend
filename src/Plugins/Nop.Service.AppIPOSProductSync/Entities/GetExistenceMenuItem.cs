#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GetExistenceMenuItem
    {
        public int MenuItemId { get; set; }
        public int Warehouse { get; set; }
        public int ProductId { get; set; }
        public decimal? Quantity { get; set; }
    }
}
