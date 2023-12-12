using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class DiscountCard
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Discount { get; set; }
        public bool Applied { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime? DateBill { get; set; }
        public bool? Active { get; set; }
    }
}
