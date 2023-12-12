#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CreditCardProcess { get; set; }
        public bool CashLessProcess { get; set; }
        public bool IsCheck { get; set; }
        public bool IsTranfer { get; set; }
    }
}
