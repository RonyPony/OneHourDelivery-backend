#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMenuAtribb
    {
        public int Id { get; set; }
        public bool Discount { get; set; }
        public bool Promotion { get; set; }
        public bool AlwaysAvailable { get; set; }
        public bool ChargeValue { get; set; }
        public bool TaxApplies { get; set; }
        public bool ServiceApplies { get; set; }
        public bool HasSpecialDrink { get; set; }
        public bool IsOpenBar { get; set; }
        public bool HasSideOrders { get; set; }
        public decimal SpecialDrinkPrices { get; set; }
        public decimal PromotionCommission { get; set; }
    }
}
