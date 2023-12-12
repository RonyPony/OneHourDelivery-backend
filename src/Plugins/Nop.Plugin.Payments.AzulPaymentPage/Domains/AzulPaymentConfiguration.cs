using System;
using Nop.Core;

namespace Nop.Plugin.Payments.AzulPaymentPage.Domains
{
    /// <summary>
    /// Represents the entity for storing the AZUL configuration for requesting the Payment Page.
    /// </summary>
    public partial class AzulPaymentTransactionLog : BaseEntity
    {
        /// <summary>
        /// Represents the number of the order.
        /// The same one used in the initial request for processing the transaction.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Represents the transaction's original amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Represents the taxes amount.
        /// </summary>
        public decimal Itbis { get; set; }

        /// <summary>
        /// Represents the authorization code in case the transaction has been approved.
        /// </summary>
        public string AuthorizationCode { get; set; }

        /// <summary>
        /// Represents the date when the transaction was processed.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Represents the transaction response code.
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// Represents the transaction response message.
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Represents a clear message with the information of the error if there is any.
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Represents the transaction response ISO code.
        /// </summary>
        public string IsoCode { get; set; }

        /// <summary>
        /// Represents the Reference Referral Number.
        /// </summary>
        public string Rrn { get; set; }

        /// <summary>
        /// <see cref="DateTime"/> logged
        /// </summary>
        public DateTime DateLogged { get; set; }
    }
}
