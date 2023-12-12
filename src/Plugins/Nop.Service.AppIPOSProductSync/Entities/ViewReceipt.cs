using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewReceipt
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Concept { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public int SessionId { get; set; }
        public int CashRegisterId { get; set; }
        public int OpeningCashId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public string AnnulledUserName { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal AfterBalance { get; set; }
        public decimal TotalWithholding { get; set; }
        public decimal? TotalAmount { get; set; }
        public string NamePc { get; set; }
        public string SimbolPc { get; set; }
        public string NameSc { get; set; }
        public string SimbolPsc { get; set; }
        public int Idcliente { get; set; }
        public string Code { get; set; }
    }
}
