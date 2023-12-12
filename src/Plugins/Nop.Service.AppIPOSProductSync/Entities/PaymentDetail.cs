using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class PaymentDetail
    {
        public int Id { get; set; }
        public int Payment { get; set; }
        public int PaymentType { get; set; }
        public int PrimaryCurrency { get; set; }
        public int Currency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public decimal PrimaryAmount { get; set; }
        public string CreditCardNumber { get; set; }
        public int? CreditCardType { get; set; }
        public string AuthorizationNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string NumberTransfer { get; set; }
        public DateTime? TransferDate { get; set; }
        public string BankAccount { get; set; }
        public string Bank { get; set; }
        public string CheckNumber { get; set; }
        public int? CreditNote { get; set; }
        public string NumberWithholding { get; set; }
        public int WithholdingId { get; set; }
        public decimal PorcWithholding { get; set; }
        public string BillIdwithholding { get; set; }
        public string BillNumberWithholding { get; set; }
    }
}
