#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class SystemOption
    {
        public int Id { get; set; }
        public int Module { get; set; }
        public string Name { get; set; }
        public string Interfaz { get; set; }
        public bool Active { get; set; }
    }
}
