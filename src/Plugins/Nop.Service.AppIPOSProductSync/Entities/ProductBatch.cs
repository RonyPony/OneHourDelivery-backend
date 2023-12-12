using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductBatch
    {
        public int Id { get; set; }
        public string Batch { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public decimal Quantity { get; set; }
    }
}
