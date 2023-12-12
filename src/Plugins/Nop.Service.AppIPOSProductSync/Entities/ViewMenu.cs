#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMenu
    {
        public int Id { get; set; }
        public int MenuItemCategoryId { get; set; }
        public int? MenuItemSubCategoryId { get; set; }
        public string Name { get; set; }
        public int PreparationTime { get; set; }
        public decimal ElaborationCost { get; set; }
        public decimal? TotalCost { get; set; }
        public decimal Price { get; set; }
        public decimal PriceHappy { get; set; }
        public bool Discount { get; set; }
        public bool Promotion { get; set; }
        public decimal QtyMin { get; set; }
        public bool AlwaysAvailable { get; set; }
        public bool Bloked { get; set; }
        public bool Active { get; set; }
        public byte[] Picture { get; set; }
        public int Food { get; set; }
        public bool ChargeValue { get; set; }
        public string Code { get; set; }
        public bool UseCode { get; set; }
        public bool TaxApplies { get; set; }
        public bool HasSpecialDrink { get; set; }
        public decimal SpecialDrinkPrices { get; set; }
        public bool IsOpenBar { get; set; }
        public decimal PromotionCommission { get; set; }
        public string Title { get; set; }
        public bool ServiceApplies { get; set; }
        public bool HasSideOrders { get; set; }
        public bool IsComplement { get; set; }
        public int CommandId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CommandName { get; set; }
    }
}
