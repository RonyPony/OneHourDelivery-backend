using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewRemission
    {
        public int IdRemission { get; set; }
        public string NameClient { get; set; }
        public string Name { get; set; }
        public string NameVendor { get; set; }
        public string Observation { get; set; }
        public DateTime RemisionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int? UserAnulled { get; set; }
        public DateTime? AnnulledDate { get; set; }
        public bool IsPaid { get; set; }
        public int RegisterUser { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
    }
}
