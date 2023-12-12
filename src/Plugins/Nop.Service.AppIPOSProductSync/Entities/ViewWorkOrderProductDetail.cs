#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewWorkOrderProductDetail
    {
        public int IdWorkOrderProductDetail { get; set; }
        public int WorkOrderDetailId { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax1 { get; set; }
        public decimal SubTotal { get; set; }
    }
}
