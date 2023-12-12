using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GuestBook
    {
        public int Id { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public int Room { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public bool Busy { get; set; }
        public int RegisteredUser { get; set; }
        public int NumberAdults { get; set; }
        public int NumberChildren { get; set; }
        public int? Reservation { get; set; }
        public int OrderDetailId { get; set; }
    }
}
