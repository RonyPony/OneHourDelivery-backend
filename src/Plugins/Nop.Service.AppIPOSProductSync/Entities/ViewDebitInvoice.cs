using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewDebitInvoice
    {
        public int IdInvoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal ExchangeRate { get; set; }
        public int PrimaryCurrency { get; set; }
        public string PrimaryCurrencyName { get; set; }
        public string PrimaryCurrencySimbol { get; set; }
        public int Currency { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySimbol { get; set; }
        public string Reference { get; set; }
        public int Warehouse { get; set; }
        public string WarehouseName { get; set; }
        public int Supplyer { get; set; }
        public string SupplyerName { get; set; }
        public DateTime? DateEndCredit { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal? UnpaidBalance { get; set; }
    }
}
