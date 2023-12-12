using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CommandSended
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TableId { get; set; }
        public string UserId { get; set; }
        public decimal Quantity { get; set; }
        public string Product { get; set; }
        public string Comment { get; set; }
        public DateTime Start { get; set; }
        public int PreparationTime { get; set; }
        public int MenuItemId { get; set; }
        public bool IsPromotion { get; set; }
        public string PromotionName { get; set; }
        public int PromotionId { get; set; }
        public bool PrePayment { get; set; }
        public int CommandId { get; set; }
        public DateTime? End { get; set; }
        public int State { get; set; }
    }
}
