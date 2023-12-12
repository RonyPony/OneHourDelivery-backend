#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public decimal Cost { get; set; }
        public bool? Active { get; set; }
    }
}
