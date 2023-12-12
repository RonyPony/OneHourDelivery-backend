using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ReportReservation
    {
        public int Idreservation { get; set; }
        public DateTime DateReservation { get; set; }
        public int? Client { get; set; }
        public int? TourOperator { get; set; }
        public string NameClient { get; set; }
        public bool Confirmed { get; set; }
        public bool Canceled { get; set; }
        public int? OrderId { get; set; }
        public bool Applied { get; set; }
        public int IdreservationDetail { get; set; }
        public int Room { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? PackageId { get; set; }
        public decimal AdultQuantity { get; set; }
        public decimal ChildQuantity { get; set; }
        public decimal Quantity { get; set; }
        public int? RateId { get; set; }
        public decimal? RatePrice { get; set; }
        public decimal Price { get; set; }
        public int? NumberPackage { get; set; }
        public decimal? QuantityAdultExtra { get; set; }
        public decimal? QuantityChildrenExtra { get; set; }
        public decimal? PriceAdultExtra { get; set; }
        public decimal? PriceChildrenExtra { get; set; }
        public string NameRoom { get; set; }
        public string StateReservation { get; set; }
    }
}
