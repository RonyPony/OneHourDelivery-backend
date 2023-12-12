#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class PromotionDetail
    {
        public int Id { get; set; }
        public int Promotion { get; set; }
        public int MenuItem { get; set; }
        public decimal PromotionPrice { get; set; }
    }
}
