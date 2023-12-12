#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public byte[] Picture { get; set; }
    }
}
