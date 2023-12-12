using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class OrderDetailActivesList
    {
        public int OrderDetailId { get; set; }
        public string Id { get; set; }
        public int OrderId { get; set; }
        public int? PackageId { get; set; }
        public int? Room { get; set; }
        public int? Service { get; set; }
        public int? MenuItem { get; set; }
        public string Description { get; set; }
        public string Tipo { get; set; }
        public string Number { get; set; }
        public DateTime? StarDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal QuantityHours { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal? MontoFact { get; set; }
        public decimal CustomerDiscount { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal? DiscountTotal { get; set; }
        public decimal? SubTotalConDescuento { get; set; }
        public decimal Tax1 { get; set; }
        public decimal Tax2 { get; set; }
        public decimal Tax3 { get; set; }
        public decimal? SubTotalGeneral { get; set; }
        public decimal AmountByService { get; set; }
        public bool IsExtra { get; set; }
        public int? OrderDetailExtra { get; set; }
        public int? RoomIdExtra { get; set; }
        public int TypeExtra { get; set; }
    }
}
