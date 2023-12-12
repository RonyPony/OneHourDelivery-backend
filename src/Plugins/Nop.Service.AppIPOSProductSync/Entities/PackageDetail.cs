#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class PackageDetail
    {
        public int Id { get; set; }
        public int Package { get; set; }
        public int? Service { get; set; }
        public int? RoomType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
