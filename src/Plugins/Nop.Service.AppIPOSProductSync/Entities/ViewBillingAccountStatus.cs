using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBillingAccountStatus
    {
        public string RowId { get; set; }
        public string NumberReceipt { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int BillId { get; set; }
        public bool BillPaid { get; set; }
        public bool ToCredit { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public decimal? PreviousBalancePayment { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? AfterBalancePayment { get; set; }
        public bool? Annulled { get; set; }
    }
}
