using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RoomStatusRegister
    {
        public int Id { get; set; }
        public int Room { get; set; }
        public int RoomState { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string Comment { get; set; }
        public bool IsCancel { get; set; }
    }
}
