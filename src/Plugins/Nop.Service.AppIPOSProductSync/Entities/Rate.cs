#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Rate
    {
        public int Id { get; set; }
        public int RoomType { get; set; }
        public bool TourOperator { get; set; }
        public int Season { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ExtraAdult { get; set; }
        public decimal ExtraChild { get; set; }
        public bool? Active { get; set; }
        public decimal QuantityHour { get; set; }
        public bool Reused { get; set; }
    }
}
