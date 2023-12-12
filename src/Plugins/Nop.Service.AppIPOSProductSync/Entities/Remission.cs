using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Remission
    {
        public int Id { get; set; }
        public int WarehouseId { get; set; }
        public int VendorId { get; set; }
        public int ClientId { get; set; }
        public string Observation { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime Date { get; set; }
        public bool Anulled { get; set; }
        public int? UserAnulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public bool IsPaid { get; set; }
        public int RegisterUser { get; set; }
        public string Number { get; set; }
    }
}
