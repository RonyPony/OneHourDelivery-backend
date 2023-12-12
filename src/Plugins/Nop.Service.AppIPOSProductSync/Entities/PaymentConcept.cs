#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class PaymentConcept
    {
        public int Id { get; set; }
        public int Payment { get; set; }
        public int? Bill { get; set; }
        public int? Escrow { get; set; }
        public decimal Amount { get; set; }
    }
}
