using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class Vendor
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int DocumentType { get; set; }
        public string DocumentId { get; set; }
        public int Language { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal Commission { get; set; }
        public decimal ProductCommission { get; set; }
        public bool? Active { get; set; }
        public int? ServiceCategoryId { get; set; }
        public byte[] Picture { get; set; }
    }
}
