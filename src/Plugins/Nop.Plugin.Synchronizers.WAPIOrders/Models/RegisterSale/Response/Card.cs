using System;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Models.RegisterSale.Response
{
    /// <summary>
    /// Represents a gift card.
    /// </summary>
    public sealed class Card
    {
        /// <summary>
        /// Indicates the code of the gift card.
        /// </summary>
        public string CodGCard { get; set; }

        /// <summary>
        /// Indicates the authorization code obtained from authorizeConsume.
        /// </summary>
        public string AuthCod { get; set; }

        /// <summary>
        /// Indicates the amount before the consumption.
        /// </summary>
        public decimal PreviousBalance { get; set; }

        /// <summary>
        /// Indicates the consumption of the gift card.
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// Indicates the balance of the gift card.
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Indicates the expiration date of the gift card.
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}
