using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWorkOrderDetail
    {
        public int IdWorkOrderDetail { get; set; }
        public int IdWorkOrder { get; set; }
        public int RequisitionId { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public DateTime Date { get; set; }
        public decimal SubTotal { get; set; }
    }
}
