#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RoomList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomType { get; set; }
        public int Floor { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string PhoneExtension { get; set; }
        public int Width { get; set; }
        public int Lenght { get; set; }
        public int OrderRoom { get; set; }
        public bool Active { get; set; }
        public string RoomTypeName { get; set; }
        public int MinAdults { get; set; }
        public int MaxAdults { get; set; }
        public int MinChildren { get; set; }
        public int MaxChildren { get; set; }
        public int TypeColor { get; set; }
        public string RoomStateName { get; set; }
        public int StateColor { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool? Reserved { get; set; }
        public bool? Cleanning { get; set; }
        public bool? OtherState { get; set; }
        public bool? Occupied { get; set; }
    }
}
