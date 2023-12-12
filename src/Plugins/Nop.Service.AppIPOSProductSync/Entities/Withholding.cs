#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Withholding
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PorcAmount { get; set; }
        public bool? Active { get; set; }
        public string Comment { get; set; }
    }
}
