using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewListRemision
    {
        public int Id { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public bool Anulled { get; set; }
        public bool IsPaid { get; set; }
        public DateTime Date { get; set; }
        public string Vendor { get; set; }
        public string Client { get; set; }
        public int IdClient { get; set; }
        public int WarehouseId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? IsExpired { get; set; }
        public string Bill { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalBilled { get; set; }
        public decimal? TotalSaldo { get; set; }
        public string CodeClient { get; set; }
        public string AddressClient { get; set; }
        public string PhoneClient { get; set; }
        public string DocumentTypeClient { get; set; }
        public string DocumentIdclient { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string Number { get; set; }
    }
}
