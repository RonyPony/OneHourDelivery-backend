#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
    }
}
