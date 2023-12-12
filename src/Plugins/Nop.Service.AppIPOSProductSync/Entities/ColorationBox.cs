#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ColorationBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Oxidant { get; set; }
        public bool? Active { get; set; }
    }
}
