#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class WorkOrderDetail
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int RequisitionId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
