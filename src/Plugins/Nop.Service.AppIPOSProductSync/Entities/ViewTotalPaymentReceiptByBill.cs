#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewTotalPaymentReceiptByBill
    {
        public int BillId { get; set; }
        public decimal? PaymentAmount { get; set; }
    }
}
