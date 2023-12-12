#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RequisitionDetail
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal SubTotal { get; set; }
    }
}
