using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCreditBill
    {
        public int IdBill { get; set; }
        public int IdOrder { get; set; }
        public string Number { get; set; }
        public DateTime BillDate { get; set; }
        public int? TourOperator { get; set; }
        public int? Client { get; set; }
        public string CodeClient { get; set; }
        public string NameClient { get; set; }
        public string AddressClient { get; set; }
        public string PhoneClient { get; set; }
        public string DocumentIdclient { get; set; }
        public bool ToCredit { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public int RegisteredUser { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public string AnnulledUserName { get; set; }
        public bool BillPaid { get; set; }
        public string Comment { get; set; }
        public bool? IsCardPayment { get; set; }
        public decimal ExchangeRate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? Tax3 { get; set; }
        public decimal? AmountByService { get; set; }
        public decimal? Total { get; set; }
        public decimal PaidBalance { get; set; }
        public decimal? PaymentAmount { get; set; }
        public decimal? UnpaidBalance { get; set; }
        public int? Dias { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
