#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class WorkOrderProductDetail
    {
        public int Id { get; set; }
        public int WorkOrderDetailId { get; set; }
        public int WorkOrderId { get; set; }
        public int Product { get; set; }
        public int Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax1Percent { get; set; }
        public decimal SubTotal { get; set; }
    }
}
