using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Payments.CyberSource.Models
{
    /// <summary>
    /// Contains credit card information in order to process a payment
    /// </summary>
    public sealed class CyberSourcePaymentInfoModel
    {
        /// <summary>
        /// Credit/Debit Card type
        /// </summary>
        [Required]
        public string CardType { get; set; }

        /// <summary>
        /// Credit/Debit Card number
        /// </summary>
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Credit/Debit Card cvv
        /// </summary>
        [Required]
        [StringLength(4)]
        public string Cvv { get; set; }

        /// <summary>
        /// Credit/Debit Card expiration month
        /// </summary>
        [Required]
        public string ExpirationMonth { get; set; }

        /// <summary>
        /// Credit/Debit Card expiration year
        /// </summary>
        [Required]
        public string ExpirationYear { get; set; }

        /// <summary>
        /// Device fingerprint to be sent to CyberSource
        /// </summary>
        [Required]
        public string DeviceFingerprintId { get; set; }

        /// <summary>
        /// The order id of the order being paid.
        /// </summary>
        public int OrderId { get; set; } = 0;
    }
}
