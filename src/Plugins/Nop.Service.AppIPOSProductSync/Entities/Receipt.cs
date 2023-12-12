using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Receipt
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Concept { get; set; }
        public int SessionId { get; set; }
        public int CashRegisterId { get; set; }
        public int OpeningCashId { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public int UserId { get; set; }
        public string Reference { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public bool IsRepayment { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal AfterBalance { get; set; }
        public decimal TotalWithholding { get; set; }
        public int Idcliente { get; set; }
    }
}
