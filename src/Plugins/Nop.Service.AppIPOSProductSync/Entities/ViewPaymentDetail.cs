using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewPaymentDetail
    {
        public int IdPaymentDetail { get; set; }
        public int IdPayment { get; set; }
        public int Currency { get; set; }
        public decimal Amount { get; set; }
        public int PrimaryCurrency { get; set; }
        public decimal PrimaryAmount { get; set; }
        public decimal ExchangeRate { get; set; }
        public int PaymentType { get; set; }
        public string CurrencyName { get; set; }
        public string PrimaryCurrencyName { get; set; }
        public int? CreditCardType { get; set; }
        public string Name { get; set; }
        public string CreditCardNumber { get; set; }
        public string AuthorizationNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string NumberTransfer { get; set; }
        public DateTime? TransferDate { get; set; }
        public string Bank { get; set; }
        public string BankAccount { get; set; }
        public string CheckNumber { get; set; }
        public string Simbol { get; set; }
        public int? CreditNote { get; set; }
        public string CertificateRealCode { get; set; }
    }
}
