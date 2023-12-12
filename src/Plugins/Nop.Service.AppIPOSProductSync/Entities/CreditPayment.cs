using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CreditPayment
    {
        public int Id { get; set; }
        public int OpeningCashId { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
    }
}
