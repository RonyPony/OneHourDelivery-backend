#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Service
    {
        public int Id { get; set; }
        public int? ServiceCategory { get; set; }
        public int? ServiceSubCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Cost { get; set; }
        public decimal Price { get; set; }
        public bool? Active { get; set; }
        public int RegisteredUser { get; set; }
        public decimal ElaborationCost { get; set; }
        public int PreparationTime { get; set; }
        public decimal PromotionPrice { get; set; }
        public byte[] Picture { get; set; }
        public string Code { get; set; }
        public bool TaxApplies { get; set; }
        public bool DiscountApplies { get; set; }
        public decimal Discount { get; set; }
        public bool ServiceApplies { get; set; }
        public bool ItemDetailApplies { get; set; }
        public bool ApplyColorationBox { get; set; }
        public int? ColorationBox { get; set; }
        public bool OtherService { get; set; }
    }
}
