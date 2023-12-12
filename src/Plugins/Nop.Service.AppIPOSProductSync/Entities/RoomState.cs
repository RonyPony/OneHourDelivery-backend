#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RoomState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Color { get; set; }
        public bool? Occupied { get; set; }
        public bool? Reserved { get; set; }
        public bool? Cleanning { get; set; }
        public bool? OtherState { get; set; }
        public bool CheckOut { get; set; }
    }
}
