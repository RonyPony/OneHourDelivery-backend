#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBreakdownCloseCash
    {
        public long? Id { get; set; }
        public int OpeningCashId { get; set; }
        public int PaymentType { get; set; }
        public string NamePaymentType { get; set; }
        public decimal? TotalPrimaryPaid { get; set; }
        public decimal? TotalSecondaryPaid { get; set; }
    }
}
