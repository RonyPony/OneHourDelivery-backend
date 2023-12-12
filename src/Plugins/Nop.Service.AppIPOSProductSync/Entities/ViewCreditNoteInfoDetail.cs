using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCreditNoteInfoDetail
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int OpeningCashId { get; set; }
        public string NumberInfoDetail { get; set; }
        public DateTime PaymentDate { get; set; }
        public int PrimaryCurrency { get; set; }
        public string CurrencySimbol { get; set; }
        public decimal Amount { get; set; }
        public int? CreditNote { get; set; }
    }
}
