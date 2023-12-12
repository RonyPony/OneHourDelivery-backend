using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillDetailProductBatch
    {
        public int Id { get; set; }
        public int BillDetailId { get; set; }
        public int ProductId { get; set; }
        public string Batch { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUnit { get; set; }
        public decimal Quantity { get; set; }
    }
}
