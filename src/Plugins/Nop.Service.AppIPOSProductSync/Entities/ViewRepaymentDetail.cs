#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewRepaymentDetail
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int BillId { get; set; }
        public string NumberBill { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PreviousBalancePayment { get; set; }
        public decimal AfterBalancePayment { get; set; }
        public decimal? Retencion1 { get; set; }
        public decimal? Retencion2 { get; set; }
    }
}
