using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMarginsMenuItem
    {
        public long? IdRow { get; set; }
        public DateTime? BillDate { get; set; }
        public int IdMenuItem { get; set; }
        public string MenuName { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string NameCategory { get; set; }
        public string NameSubCategory { get; set; }
        public decimal? SubTotalSales { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Margins { get; set; }
    }
}
