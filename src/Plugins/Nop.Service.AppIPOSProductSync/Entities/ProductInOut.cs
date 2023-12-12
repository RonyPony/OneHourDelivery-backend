using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductInOut
    {
        public int Id { get; set; }
        public int Warehouse { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceNumber { get; set; }
        public string Observation { get; set; }
        public DateTime Date { get; set; }
        public bool Input { get; set; }
        public bool Anulled { get; set; }
        public int? UserAnulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
    }
}
