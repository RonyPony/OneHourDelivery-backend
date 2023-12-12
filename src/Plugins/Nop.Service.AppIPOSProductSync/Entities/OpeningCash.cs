using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OpeningCash
    {
        public int Id { get; set; }
        public int Session { get; set; }
        public int User { get; set; }
        public int UserEnd { get; set; }
        public int CashRegister { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Closed { get; set; }
        public int MainCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public decimal Rate { get; set; }
        public decimal MainCurrencyValue { get; set; }
        public decimal SecondaryCurrencyValue { get; set; }
        public decimal Recipts { get; set; }
        public decimal Payments { get; set; }
        public decimal Cash { get; set; }
        public decimal Credit { get; set; }
        public decimal CreditCard { get; set; }
        public decimal Check { get; set; }
        public decimal CreditNote { get; set; }
        public decimal GiftCertificate { get; set; }
        public decimal TotalBilling { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax1 { get; set; }
        public decimal TotalTax2 { get; set; }
        public decimal TotalTax3 { get; set; }
        public decimal ExchangeMoney { get; set; }
        public decimal ExchangeMoneySec { get; set; }
    }
}
