#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ColorationBoxDetail
    {
        public int Id { get; set; }
        public int ColorationBoxId { get; set; }
        public int ProductCategory { get; set; }
        public decimal Cost { get; set; }
    }
}
