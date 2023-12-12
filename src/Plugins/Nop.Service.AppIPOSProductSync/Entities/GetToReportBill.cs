using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GetToReportBill
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime BillDate { get; set; }
        public bool BillPaid { get; set; }
        public bool ToCredit { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public string Comment { get; set; }
        public string NameClient { get; set; }
        public string DescriptionDetails { get; set; }
        public string Tipo { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Tax3 { get; set; }
        public decimal AmountByService { get; set; }
        public decimal? Total { get; set; }
        public bool? IsCardPayment { get; set; }
    }
}
