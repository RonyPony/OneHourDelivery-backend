using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMarginsProduct
    {
        public long? IdRow { get; set; }
        public DateTime? BillDate { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int TrademarkId { get; set; }
        public string NameCategory { get; set; }
        public string NameTrademark { get; set; }
        public decimal? SubTotalSales { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Margins { get; set; }
    }
}
