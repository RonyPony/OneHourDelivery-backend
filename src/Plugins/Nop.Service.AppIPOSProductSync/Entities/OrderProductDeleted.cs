using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderProductDeleted
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int UserId { get; set; }
        public decimal Quantity { get; set; }
        public string Motive { get; set; }
        public int TableId { get; set; }
        public int CashRegisterId { get; set; }
        public DateTime Date { get; set; }
        public int? SessionId { get; set; }
    }
}
