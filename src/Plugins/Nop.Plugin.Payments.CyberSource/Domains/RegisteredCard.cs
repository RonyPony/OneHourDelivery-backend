namespace Nop.Plugin.Payments.CyberSource.Domains
{
    public class RegisteredCard
    {
        /// <summary>
        /// Get customer's payment card last digits
        /// </summary>
        public string CardLastFourDigits { get; set; }

        /// <summary>
        /// Get customer's payment card type.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Get customer's payment card expiration date.
        /// </summary>
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Get customer's credit card is defualt.
        /// </summary>
        public bool IsDefaultPaymentMethod { get; set; }

        /// <summary>
        /// Returns whether or not the card is expired
        /// </summary>
        public bool IsExpired { get; set; }
    }
}
