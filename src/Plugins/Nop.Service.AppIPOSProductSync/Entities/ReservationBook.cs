using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ReservationBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public DateTime EntryDate { get; set; }
        public bool Confirmed { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public bool Canceled { get; set; }
        public int? CanceledBy { get; set; }
        public int? OrderId { get; set; }
        public bool Applied { get; set; }
        public int? CanceledUser { get; set; }
        public string ReasonCanceled { get; set; }
        public DateTime? DateCanceled { get; set; }
    }
}
