using System;

namespace Nop.Plugin.Payments.AzulPaymentPage.Models
{
    /// <summary>
    /// Represents the operation result after processing a transaction through AZUL Payment Page.
    /// </summary>
    public sealed class TransactionResult
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
        public DateTime DateTime { get; set; }

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
        /// Represents the token's value.
        ///
        /// <para>
        ///     This value is returned in case we sent in the request the property SaveToDataVault with 1 as value.
        ///     In this way AZUL Payment Page will return a token generated for the customer credit card.
        /// </para>
        /// </summary>
        public string DataVaultToken { get; set; }

        /// <summary>
        /// Represents the expiration date for the Data Vault. This date is in AAAAMM format.
        /// </summary>
        public string DataVaultExpiration { get; set; }

        /// <summary>
        /// Represents the credit card brand (VISA, MC, and so on).
        /// </summary>
        public string DataVaultBrand { get; set; }

        /// <summary>
        /// Represents the AZUL order number.
        /// </summary>
        public string AzulOrderId { get; set; }
    }
}
