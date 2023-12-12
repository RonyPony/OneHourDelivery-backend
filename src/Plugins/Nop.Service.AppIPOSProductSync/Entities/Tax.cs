#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Tax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int Country { get; set; }
    }
}
