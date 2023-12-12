using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Payments.CyberSource.Models
{
    public sealed class CyberSourceTokenPaymentInfoModel
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Id of the Order
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// Card expiration date(MM-yyyy)
        /// </summary>
        [Required]
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// The last four digits of the card we want to use
        /// </summary>
        [Required]
        public string CardLastFour { get; set; }
    }
}
