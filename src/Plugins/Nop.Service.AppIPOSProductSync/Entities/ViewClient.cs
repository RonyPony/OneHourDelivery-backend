#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ViewClient
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public decimal Credit { get; set; }
        public decimal? Balance { get; set; }
        public bool Exonerated { get; set; }
        public string CompanyName { get; set; }
    }
}
