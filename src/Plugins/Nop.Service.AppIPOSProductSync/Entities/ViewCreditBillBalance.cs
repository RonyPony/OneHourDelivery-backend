#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCreditBillBalance
    {
        public int BillId { get; set; }
        public int? Client { get; set; }
        public decimal? UnpaidBalance { get; set; }
    }
}
