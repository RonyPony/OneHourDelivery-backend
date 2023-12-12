using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class AppointmentDetail
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }
        public int? VendorId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
