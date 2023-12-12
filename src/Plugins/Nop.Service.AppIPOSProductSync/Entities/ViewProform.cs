using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewProform
    {
        public int IdProform { get; set; }
        public string NameClient { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public int RegisteredUser { get; set; }
        public bool Annulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public int? AnnulledUser { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Applied { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal SpecialDiscount { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PrimaryCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public string PrimarySimbol { get; set; }
        public string SecondarySimbol { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax1 { get; set; }
        public decimal? Tax2 { get; set; }
        public decimal? Tax3 { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? AmountByService { get; set; }
        public decimal? Total { get; set; }
        public string Comment { get; set; }
    }
}
