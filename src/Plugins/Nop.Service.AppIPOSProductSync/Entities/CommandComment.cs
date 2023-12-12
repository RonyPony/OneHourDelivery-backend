#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CommandComment
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public string Comment { get; set; }
        public bool? Active { get; set; }
        public bool? IsFood { get; set; }
    }
}
