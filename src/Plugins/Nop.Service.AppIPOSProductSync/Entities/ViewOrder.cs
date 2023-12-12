using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewOrder
    {
        public int IdOrder { get; set; }
        public string NameClient { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public int RegisteredUser { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public int? CreditPaymentUser { get; set; }
        public bool IsCreditPayment { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserNameCreditPayment { get; set; }
        public bool Closed { get; set; }
    }
}
