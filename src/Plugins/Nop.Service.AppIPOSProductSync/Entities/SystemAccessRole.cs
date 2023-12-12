#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class SystemAccessRole
    {
        public int Id { get; set; }
        public int SystemRole { get; set; }
        public int SystemAccess { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Lock { get; set; }
        public bool Print { get; set; }
        public bool Export { get; set; }
    }
}
