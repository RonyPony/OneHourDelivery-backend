#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class MenuItemSubcategory
    {
        public int Id { get; set; }
        public int MenuItemCategoryId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? Color { get; set; }
        public bool? Active { get; set; }
    }
}
