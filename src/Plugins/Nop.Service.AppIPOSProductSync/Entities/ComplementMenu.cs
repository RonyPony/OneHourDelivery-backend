#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ComplementMenu
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public int RecipeMenu { get; set; }
    }
}
