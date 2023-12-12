#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class SystemAccess
    {
        public int Id { get; set; }
        public int Module { get; set; }
        public string Name { get; set; }
        public string Interfaz { get; set; }
        public string Screen { get; set; }
        public bool AllowAction { get; set; }
        public bool Active { get; set; }
    }
}
