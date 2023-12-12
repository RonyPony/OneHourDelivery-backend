using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ReservationBookDetail
    {
        public int Id { get; set; }
        public int Room { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? PackageId { get; set; }
        public int ReservationBook { get; set; }
        public decimal AdultQuantity { get; set; }
        public decimal ChildQuantity { get; set; }
        public decimal Quantity { get; set; }
        public int? RateId { get; set; }
        public decimal? RatePrice { get; set; }
        public decimal Price { get; set; }
        public int? TypeRoom { get; set; }
        public int? NumberPackage { get; set; }
        public decimal? QuantityAdultExtra { get; set; }
        public decimal? QuantityChildrenExtra { get; set; }
        public decimal? PriceAdultExtra { get; set; }
        public decimal? PriceChildrenExtra { get; set; }
        public int? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal QuantityRate { get; set; }
    }
}
