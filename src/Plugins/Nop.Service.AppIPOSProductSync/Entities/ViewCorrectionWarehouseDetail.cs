#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCorrectionWarehouseDetail
    {
        public int IdCorrectionWarehouseDetail { get; set; }
        public int IdCorrectionWarehouse { get; set; }
        public int Product { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public decimal Existence { get; set; }
        public decimal Input { get; set; }
        public decimal Output { get; set; }
        public decimal QuantityReal { get; set; }
    }
}
