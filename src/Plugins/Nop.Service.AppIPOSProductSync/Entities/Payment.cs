using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CashRegister { get; set; }
        public int OpeningCashId { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public int? Bill { get; set; }
        public int? Escrow { get; set; }
        public bool? InitialPayment { get; set; }
        public int? Receipt { get; set; }
    }
}
