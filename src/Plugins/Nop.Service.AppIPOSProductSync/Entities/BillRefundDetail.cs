#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillRefundDetail
    {
        public int Id { get; set; }
        public int BillRefundId { get; set; }
        public int BillDetailId { get; set; }
        public decimal QuantityTmp { get; set; }
        public decimal QuantityRefund { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal SubTotal { get; set; }
        public decimal CommissionVendor { get; set; }
        public int VendorId { get; set; }
        public decimal AmountByService { get; set; }
    }
}
