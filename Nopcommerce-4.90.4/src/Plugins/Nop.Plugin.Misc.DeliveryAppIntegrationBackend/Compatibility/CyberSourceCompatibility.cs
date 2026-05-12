using Nop.Core;

namespace Nop.Plugin.Payments.CyberSource.Domains
{
    public class CustomerPaymentTokenMapping : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Token { get; set; }
        public string CardType { get; set; }
        public string CardLastFourDigits { get; set; }
        public string CardExpirationDate { get; set; }
        public bool IsDefaultPaymentMethod { get; set; }
    }
}

