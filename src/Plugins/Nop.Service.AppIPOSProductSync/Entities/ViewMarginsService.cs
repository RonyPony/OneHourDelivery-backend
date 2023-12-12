using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewMarginsService
    {
        public long? IdRow { get; set; }
        public DateTime? BillDate { get; set; }
        public int IdService { get; set; }
        public string ServiceName { get; set; }
        public int? CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string NameCategory { get; set; }
        public string NameSubCategory { get; set; }
        public decimal? SubTotalSales { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Margins { get; set; }
    }
}
