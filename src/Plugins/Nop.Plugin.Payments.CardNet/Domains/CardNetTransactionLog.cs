using System;
using System.Net.Http;
using LinqToDB.Mapping;
using Nop.Core;

namespace Nop.Plugin.Payments.CardNet.Domains
{
    /// <summary>
    /// Entity used to store CardNet transaction history/information on the database
    /// </summary>
    public class CardNetTransactionLog : BaseEntity
    {
        /// <summary>
        /// NopCommerce order id that corresponds to the CardNet transaction
        /// </summary>
        public int? OrderId { get; set; }

        /// <summary>
        /// Transaction's amount sent to CardNet for processing
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Transaction's currency type sent to CardNet for processing
        /// </summary>
        [Nullable]
        public string Currency { get; set; }

        /// <summary>
        /// Transaction's approval code received from CardNet after processing
        /// </summary>
        [Nullable]
        public string ApprovalCode { get; set; }

        /// <summary>
        /// <see cref="HttpResponseMessage.ReasonPhrase"/> received from CardNet's API
        /// </summary>
        [Nullable]
        public string ResultType { get; set; }

        /// <summary>
        /// Any error message received from CardNet's API or failed process from this plug-in
        /// </summary>
        [Nullable]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Full error message
        /// </summary>
        [Nullable]
        public string FullException { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> logged
        /// </summary>
        public DateTime DateLogged { get; set; }
    }
}
