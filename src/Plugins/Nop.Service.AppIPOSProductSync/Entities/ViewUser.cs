#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewUser
    {
        public int Id { get; set; }
        public int? Company { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Locked { get; set; }
        public bool LogedIn { get; set; }
        public bool Active { get; set; }
        public int SystemRole { get; set; }
        public string RolName { get; set; }
        public string CompanyName { get; set; }
    }
}
