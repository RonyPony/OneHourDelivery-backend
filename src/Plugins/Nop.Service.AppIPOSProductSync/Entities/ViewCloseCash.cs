using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewCloseCash
    {
        public int Id { get; set; }
        public int CashRegister { get; set; }
        public string NameCashRegister { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Closed { get; set; }
        public string NameUserI { get; set; }
        public string NameUserF { get; set; }
        public decimal Rate { get; set; }
        public int MainCurrency { get; set; }
        public int SecondaryCurrency { get; set; }
        public string NameMainCurrency { get; set; }
        public string SimbolMainCurrency { get; set; }
        public string NameSecondaryCurrency { get; set; }
        public string SimbolSecondaryCurrency { get; set; }
        public decimal MainCashFund { get; set; }
        public decimal SecondaryCashFund { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Tax3 { get; set; }
        public decimal AmountByService { get; set; }
        public decimal Total { get; set; }
        public decimal TotalContado { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal? SubTotalSec { get; set; }
        public decimal? DiscountSec { get; set; }
        public decimal? Tax1Sec { get; set; }
        public decimal? Tax2Sec { get; set; }
        public decimal? Tax3Sec { get; set; }
        public decimal? AmountByServiceSec { get; set; }
        public decimal? TotalSec { get; set; }
        public decimal? TotalContadoSec { get; set; }
        public decimal? TotalCreditSec { get; set; }
        public decimal ExchangeMoney { get; set; }
        public decimal ExchangeMoneySec { get; set; }
        public int QtyContado { get; set; }
        public int QtyCredit { get; set; }
        public int QtyAnnulled { get; set; }
        public int QtyBilling { get; set; }
        public string MinBill { get; set; }
        public string MaxBill { get; set; }
        public decimal TotalPrimaryPaid { get; set; }
        public decimal TotalSecondaryPaid { get; set; }
        public decimal? TotalInCashRegisterPri { get; set; }
        public decimal? TotalInCashRegisterSec { get; set; }
    }
}
