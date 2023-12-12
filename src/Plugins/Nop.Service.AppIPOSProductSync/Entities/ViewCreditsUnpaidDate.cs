using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCreditsUnpaidDate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string DocumentId { get; set; }
        public string NameClient { get; set; }
        public string Number { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public decimal? UnpaidBalance { get; set; }
        public int? Dias { get; set; }
    }
}
