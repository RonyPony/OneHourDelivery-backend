using Nop.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Payments.CyberSource.Domains
{
    /// <summary>
    /// Represents the customer payment token info.
    /// </summary>
    public class CustomerPaymentTokenMapping : BaseEntity
    {
        /// <summary>
        /// Get or set customer's id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Get or set customer's payment Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Get or set customer's payment card last digits
        /// </summary>
        public string CardLastFourDigits { get; set; }

        /// <summary>
        /// Get or set customer's payment card type.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Get or set customer's payment card expiration date.
        /// </summary>
        public string CardExpirationDate { get; set; }

        /// <summary>
        /// Get or set customer's credit card is defualt.
        /// </summary>
        public bool IsDefaultPaymentMethod { get; set; }
    }
}
