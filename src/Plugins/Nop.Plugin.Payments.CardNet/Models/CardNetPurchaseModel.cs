using Nop.Plugin.Payments.CardNet.Extensions;
using Nop.Plugin.Payments.CardNet.Helpers;

namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Purchase model sent to CardNet API
    /// </summary>
    public sealed class CardNetPurchaseModel
    {
        /// <summary>
        /// OneTimeToken from CardNet's capture page
        /// </summary>
        public string TrxToken { get; set; }

        /// <summary>
        /// NopCommerce invoice (order) number
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Amount that will be purchased (formatted by <see cref="CardNetExtensions.FormatAsCardNetAmount"/>)
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Purchase currency type, must be <see cref="CardNetDefaults.DefaultCurrencyCode"/>
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Determines if purchase is done in one or two steps
        /// </summary>
        public bool Capture { get; set; }

        /// <summary>
        /// Contains information about invoice number and tax amount
        /// </summary>
        public CardNetDataDoModel DataDo { get; set; }
    }
}
