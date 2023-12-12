#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public bool IsPrincipal { get; set; }
    }
}
