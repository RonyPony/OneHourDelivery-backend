#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Package
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? QuantityHour { get; set; }
        public int? Season { get; set; }
        public bool IsPromotion { get; set; }
        public int RegisteredUser { get; set; }
        public bool? Active { get; set; }
    }
}
