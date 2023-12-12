using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Payments.CyberSource.Models
{
    public sealed class CyberSourceRegisterSingleCard
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        public string CustomerId { get; set; }
        
        /// <summary>
        /// Customer email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Name in the card
        /// </summary>
        public string CardHolderName { get; set; }
        
        /// <summary>
        /// Last name of card holder
        /// </summary>
        public string CardHolderLastName { get; set; }

        /// <summary>
        /// Full address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Card expiration month
        /// </summary>
        [Required]
        public string CardExpirationMonth { get; set; }

        /// <summary>
        /// Card expiration year
        /// </summary>
        [Required]
        public string CardExpirationYear { get; set; }

        /// <summary>
        /// CVV of the card
        /// </summary>
        public string CVV { get; set; }

        /// <summary>
        /// Type of the card processed by CyberSource (Visa, MasterCard, etc.)
        /// </summary>
        [Required]
        public string CardType { get; set; }

        /// <summary>
        /// The las four digits of the card we want to use
        /// </summary>
        [Required]
        public string CardNumber { get; set; }
    }
}
