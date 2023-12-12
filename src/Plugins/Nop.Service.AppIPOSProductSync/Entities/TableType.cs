#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class TableType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public bool? Active { get; set; }
    }
}
