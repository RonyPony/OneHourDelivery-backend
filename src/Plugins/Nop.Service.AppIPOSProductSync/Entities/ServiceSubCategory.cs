#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ServiceSubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceCategory { get; set; }
        public string Descrption { get; set; }
        public bool Active { get; set; }
    }
}
