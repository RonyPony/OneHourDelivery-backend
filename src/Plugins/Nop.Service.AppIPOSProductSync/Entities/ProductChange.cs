using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ProductChange
    {
        public int Id { get; set; }
        public int ProductOrigin { get; set; }
        public int Client { get; set; }
        public int Bill { get; set; }
        public double QuantityOrigin { get; set; }
        public double PriceOrigin { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public DateTime? ChangeDate { get; set; }
        public decimal? QuantityToTakeOff { get; set; }
        public decimal? QuantityToAdd { get; set; }
        public decimal? AmountToTakeOff { get; set; }
        public decimal? AmountToAdd { get; set; }
        public decimal? Difference { get; set; }
    }
}
