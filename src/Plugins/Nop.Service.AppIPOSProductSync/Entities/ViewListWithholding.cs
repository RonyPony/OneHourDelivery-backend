using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewListWithholding
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CashRegister { get; set; }
        public int OpeningCashId { get; set; }
        public string NumberWithholding { get; set; }
        public decimal PorcWithholding { get; set; }
        public string BillNumberWithholding { get; set; }
        public string Simbol1 { get; set; }
        public decimal PrimaryAmount { get; set; }
        public string Simbol2 { get; set; }
        public decimal Amount { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
