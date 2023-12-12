#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewTotalDetailByBill
    {
        public int Bill { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? SubTotalAfterDiscount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? Tax3 { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? AmountByService { get; set; }
        public decimal? Total { get; set; }
        public decimal? SubTotalServiceG { get; set; }
        public decimal? SubTotalServiceE { get; set; }
        public decimal? SubTotalProductG { get; set; }
        public decimal? SubTotalProductE { get; set; }
        public decimal? SubTotalG { get; set; }
        public decimal? SubTotalE { get; set; }
        public decimal? SubTotalGAfterDiscount { get; set; }
        public decimal? SubTotalEAfterDiscount { get; set; }
        public decimal? CostTotal { get; set; }
    }
}
