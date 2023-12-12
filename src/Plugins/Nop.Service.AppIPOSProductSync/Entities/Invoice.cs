using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public int Supplyer { get; set; }
        public int Warehouse { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public int Currency { get; set; }
        public int PrimaryCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal? UnpaidAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public DateTime? DateEndCredit { get; set; }
        public bool IsCredit { get; set; }
        public bool? IsPaid { get; set; }
    }
}
