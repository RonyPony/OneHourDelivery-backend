using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBill1
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int SessionId { get; set; }
        public int CashRegisterId { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public int TableId { get; set; }
        public DateTime BillDate { get; set; }
        public bool ToCredit { get; set; }
        public int PersonQty { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public int RegisteredUser { get; set; }
        public decimal BillSubTotal { get; set; }
        public decimal BillDiscount { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal BillTax { get; set; }
        public decimal BillTax2 { get; set; }
        public decimal BillTips { get; set; }
        public decimal TotalExtra { get; set; }
        public decimal BillTotal { get; set; }
        public bool IsDetail { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public bool BillPaid { get; set; }
        public bool PrePayment { get; set; }
        public string Comment { get; set; }
        public decimal SendCost { get; set; }
        public string TableName { get; set; }
        public string CashName { get; set; }
        public string ClientName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
    }
}
