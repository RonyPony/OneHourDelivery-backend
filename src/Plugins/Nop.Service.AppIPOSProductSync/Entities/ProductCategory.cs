#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
        public bool ApplyColorationBox { get; set; }
        public bool IsOxidant { get; set; }
    }
}
