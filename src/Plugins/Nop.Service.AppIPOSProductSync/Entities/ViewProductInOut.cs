using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewProductInOut
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ReferenceName { get; set; }
        public string ReferenceNumber { get; set; }
        public int WareHouse { get; set; }
        public string WarehouseName { get; set; }
        public bool Input { get; set; }
        public bool Anulled { get; set; }
        public string Observation { get; set; }
        public decimal Total { get; set; }
    }
}
