#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CommandDetailCopy
    {
        public int Id { get; set; }
        public int OriginalCommandId { get; set; }
        public int CopyCommandId { get; set; }
    }
}
