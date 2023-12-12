#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int Color { get; set; }
        public int OrdenCategory { get; set; }
    }
}
