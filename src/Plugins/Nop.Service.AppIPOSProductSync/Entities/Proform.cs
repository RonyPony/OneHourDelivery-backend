using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Proform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public int SessionId { get; set; }
        public int CashRegister { get; set; }
        public int OpeningCashId { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public bool? InProcess { get; set; }
        public int RegisteredUser { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public bool Applied { get; set; }
        public int TableId { get; set; }
        public string ClientName { get; set; }
        public decimal ClientBalance { get; set; }
        public bool TipsCharge { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool ExtraCharge { get; set; }
        public bool TaxCharge { get; set; }
        public int DiscountOn { get; set; }
        public int PersonsQty { get; set; }
        public decimal SpecialDiscount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? OrderId { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public string Comment { get; set; }
    }
}
