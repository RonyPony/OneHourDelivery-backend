using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewBill
    {
        public int IdBill { get; set; }
        public int IdOrder { get; set; }
        public string Number { get; set; }
        public DateTime BillDate { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public string NameClient { get; set; }
        public string AddressClient { get; set; }
        public string CodeClient { get; set; }
        public string PhoneClient { get; set; }
        public string EmailClient { get; set; }
        public string DocumentTypeClient { get; set; }
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
        public string PrimarySimbol { get; set; }
        public string SecondarySimbol { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? SubTotalDiscount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? Tax3 { get; set; }
        public decimal? AmountByService { get; set; }
        public decimal? Total { get; set; }
        public decimal? AmountByCredit { get; set; }
        public int OpeningCashId { get; set; }
        public decimal ExchangeMoney { get; set; }
        public decimal ExchangeMoneySec { get; set; }
        public DateTime? DatePaid { get; set; }
        public string RegionName { get; set; }
        public int RegionId { get; set; }
        public int? VendorId { get; set; }
        public string VendorName { get; set; }
        public string ClientPostalCode { get; set; }
        public string PrimaryName { get; set; }
        public string SecondaryName { get; set; }
    }
}
