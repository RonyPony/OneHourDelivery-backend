using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class AccountsPayable
    {
        public int Id { get; set; }
        public int Idinvoice { get; set; }
        public decimal PreviousBalancePayment { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal AfterBalancePayment { get; set; }
        public DateTime PayableDate { get; set; }
    }
}
