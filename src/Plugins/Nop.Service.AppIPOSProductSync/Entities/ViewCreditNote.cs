using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCreditNote
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Code { get; set; }
        public int IdRefund { get; set; }
        public string Comment { get; set; }
        public int Client { get; set; }
        public string NameClient { get; set; }
        public decimal Amount { get; set; }
        public decimal? Balance { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public string NameUserAnnuller { get; set; }
        public string NameUser { get; set; }
    }
}
