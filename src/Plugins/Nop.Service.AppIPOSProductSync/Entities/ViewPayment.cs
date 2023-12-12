using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewPayment
    {
        public int IdPayment { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public int? IdBill { get; set; }
        public int? IdEscrow { get; set; }
        public int SessionId { get; set; }
        public int OpeningCashId { get; set; }
        public int IdCashRegister { get; set; }
        public DateTime PaymentDate { get; set; }
        public string NameCashRegister { get; set; }
        public bool InitialPayment { get; set; }
        public int? IdReceipt { get; set; }
    }
}
