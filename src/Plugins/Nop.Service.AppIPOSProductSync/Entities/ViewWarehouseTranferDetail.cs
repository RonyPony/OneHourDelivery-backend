#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWarehouseTranferDetail
    {
        public int IdDetail { get; set; }
        public int IdwareHouseTranfer { get; set; }
        public int Product { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public decimal Existence { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
