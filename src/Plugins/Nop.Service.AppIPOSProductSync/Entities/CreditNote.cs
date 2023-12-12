using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CreditNote
    {
        public int Id { get; set; }
        public int Client { get; set; }
        public int Refound { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Amount { get; set; }
        public bool? Active { get; set; }
        public int Bill { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Comment { get; set; }
        public int Code { get; set; }
        public string RealCode { get; set; }
        public decimal? Balance { get; set; }
        public bool IsRefound { get; set; }
        public bool Annulled { get; set; }
        public int? AnnulledUser { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int RegisteredUser { get; set; }
        public int PrimaryCurrency { get; set; }
        public int FromClient { get; set; }
    }
}
