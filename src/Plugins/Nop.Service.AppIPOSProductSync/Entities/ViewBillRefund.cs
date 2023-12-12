using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBillRefund
    {
        public int IdbillRefund { get; set; }
        public DateTime BillRefundDate { get; set; }
        public int BillId { get; set; }
        public int IdOrder { get; set; }
        public string NumberBill { get; set; }
        public DateTime BillDate { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public string NameClient { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? AmountByService { get; set; }
        public decimal? Total { get; set; }
    }
}
