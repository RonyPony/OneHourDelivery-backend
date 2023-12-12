using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMarginsRoom
    {
        public long? IdRow { get; set; }
        public DateTime? BillDate { get; set; }
        public int IdRoom { get; set; }
        public string RoomName { get; set; }
        public int RoomType { get; set; }
        public string NameRoomType { get; set; }
        public decimal? SubTotalSales { get; set; }
        public decimal Cost { get; set; }
        public decimal? Margins { get; set; }
    }
}
