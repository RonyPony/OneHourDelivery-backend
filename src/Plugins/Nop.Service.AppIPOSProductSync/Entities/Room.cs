#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Room
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
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int OrderRoom { get; set; }
        public bool? Active { get; set; }
    }
}
