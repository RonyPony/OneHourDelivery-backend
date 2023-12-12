using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ReservationBookDetailList
    {
        public int ReservationBookId { get; set; }
        public int ReservationBookDetailId { get; set; }
        public int RoomId { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string NumberRoom { get; set; }
        public string NameRoom { get; set; }
        public decimal PriceRoom { get; set; }
        public string NameFloor { get; set; }
        public string NumberFloor { get; set; }
        public decimal Quantity { get; set; }
    }
}
