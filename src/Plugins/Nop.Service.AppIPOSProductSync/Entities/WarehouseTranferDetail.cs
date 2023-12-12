#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class WarehouseTranferDetail
    {
        public int Id { get; set; }
        public int WareHouseTranfer { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Existence { get; set; }
    }
}
