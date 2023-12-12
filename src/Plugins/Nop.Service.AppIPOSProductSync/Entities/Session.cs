using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Closed { get; set; }
        public int? ClosedBy { get; set; }
    }
}
