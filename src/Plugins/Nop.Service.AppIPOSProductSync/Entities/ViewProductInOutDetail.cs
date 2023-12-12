#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewProductInOutDetail
    {
        public int IdDetail { get; set; }
        public int IdproductInOut { get; set; }
        public int Product { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int Unit { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal SubTotal { get; set; }
    }
}
