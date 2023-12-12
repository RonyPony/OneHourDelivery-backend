using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class GetExistenceAverageInKardex
    {
        public DateTime Date { get; set; }
        public int MenuItemId { get; set; }
        public int ProductId { get; set; }
        public decimal? Quantity { get; set; }
    }
}
