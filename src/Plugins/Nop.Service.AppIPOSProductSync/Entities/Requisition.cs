using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Requisition
    {
        public int Id { get; set; }
        public int Warehouse { get; set; }
        public int AgentId { get; set; }
        public string Observation { get; set; }
        public DateTime Date { get; set; }
        public bool Anulled { get; set; }
        public int? UserAnulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public bool IsPaid { get; set; }
        public int RegisterUser { get; set; }
    }
}
