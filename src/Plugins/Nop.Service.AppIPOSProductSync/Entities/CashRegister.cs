#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class CashRegister
    {
        public int Id { get; set; }
        public int? Company { get; set; }
        public int? Warehouse { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string AccessCode { get; set; }
        public bool Locked { get; set; }
        public bool? Active { get; set; }
        public bool Assigned { get; set; }
        public bool Closed { get; set; }
        public int TypeCashRegister { get; set; }
        public int? ParentCashRegister { get; set; }
        public bool Main { get; set; }
        public int SeriesBillingId { get; set; }
        public int SeriesReceiptId { get; set; }
        public int SeriesRemisionId { get; set; }
    }
}
