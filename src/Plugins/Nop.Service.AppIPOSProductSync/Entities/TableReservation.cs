using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class TableReservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public bool Applied { get; set; }
        public bool Cancelled { get; set; }
        public string Comment { get; set; }
    }
}
