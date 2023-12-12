using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillRefund
    {
        public int Id { get; set; }
        public int OpeningCashId { get; set; }
        public int UserId { get; set; }
        public int BillId { get; set; }
        public DateTime Date { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? AfterBalance { get; set; }
    }
}
