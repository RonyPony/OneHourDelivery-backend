#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class MyCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string GobId { get; set; }
        public string Address { get; set; }
        public string AutorizacionCode { get; set; }
        public byte[] Image { get; set; }
        public bool? Active { get; set; }
    }
}
