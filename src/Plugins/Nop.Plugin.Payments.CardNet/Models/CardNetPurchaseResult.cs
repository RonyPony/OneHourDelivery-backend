using System.Collections.Generic;

namespace Nop.Plugin.Payments.CardNet.Models
{
    /// <summary>
    /// Result received from CardNet API
    /// </summary>
    public sealed class CardNetPurchaseResult
    {
        /// <summary>
        /// Contains CardNet transaction information
        /// </summary>
        public CardNetPurchaseResponseModel Response { get; set; }

        /// <summary>
        /// Contains CardNet API error information
        /// </summary>
        public List<CardNetErrorModel> Errors { get; set; }
    }
}
