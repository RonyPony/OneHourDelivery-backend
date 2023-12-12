using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWorkOrder
    {
        public int IdWorkOrder { get; set; }
        public string AgentName { get; set; }
        public int AgentId { get; set; }
        public DateTime Date { get; set; }
        public string Observation { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? UserAnnulled { get; set; }
        public int UserId { get; set; }
        public string ReferenceName { get; set; }
        public bool Applied { get; set; }
        public DateTime? AppliedDate { get; set; }
        public int? AppliedUser { get; set; }
        public string UserName { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
