#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewListProductByWarehouseFastBilling
    {
        public int Id { get; set; }
        public int Unit { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductCategoryName { get; set; }
        public int Warehouse { get; set; }
        public decimal Quantity { get; set; }
        public string UnitName { get; set; }
        public string Presentation { get; set; }
        public string Treatment { get; set; }
        public string Location { get; set; }
        public string TrademarkName { get; set; }
    }
}
