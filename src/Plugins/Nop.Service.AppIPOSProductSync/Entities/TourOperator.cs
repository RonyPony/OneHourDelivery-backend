#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class TourOperator
    {
        public int Id { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public decimal Credit { get; set; }
        public decimal? Balance { get; set; }
        public int? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Commission { get; set; }
        public bool? Active { get; set; }
    }
}
