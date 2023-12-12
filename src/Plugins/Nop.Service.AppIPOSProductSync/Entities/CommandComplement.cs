#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CommandComplement
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int? ComplmentMenuId { get; set; }
        public int? Ref { get; set; }
    }
}
