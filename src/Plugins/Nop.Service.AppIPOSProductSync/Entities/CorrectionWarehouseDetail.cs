#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CorrectionWarehouseDetail
    {
        public int Id { get; set; }
        public int CorrectionWarehouse { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Existence { get; set; }
        public decimal Input { get; set; }
        public decimal Output { get; set; }
        public decimal QuantityReal { get; set; }
    }
}
