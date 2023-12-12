#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class RepaymentDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int ReceiptId { get; set; }
        public decimal PreviousBalancePayment { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AfterBalancePayment { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? AfterBalance { get; set; }
        public decimal? Ncprorated { get; set; }
    }
}
