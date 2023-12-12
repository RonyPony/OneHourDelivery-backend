using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ExchangeRate
    {
        public int Id { get; set; }
        public int CurrencyTypePrincipal { get; set; }
        public int CurrencyType { get; set; }
        public DateTime ExchangeRateDate { get; set; }
        public decimal ExchangeRateValue { get; set; }
    }
}
