#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinAdults { get; set; }
        public int MaxAdults { get; set; }
        public int MinChildren { get; set; }
        public int MaxChildren { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public int Color { get; set; }
        public string Comment { get; set; }
    }
}
