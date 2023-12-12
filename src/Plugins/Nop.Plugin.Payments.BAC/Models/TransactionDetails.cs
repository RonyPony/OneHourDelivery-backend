namespace Nop.Plugin.Payments.BAC.Models
{
    /// <summary>
    /// This request object needs to get passed when calling the HostedPageAuthorize method when requesting the Hosted Page from the BAC.
    /// </summary>
    public sealed class TransactionDetails
    {
        /// <summary>
        /// Represents the Id giving by the BAC for this acquirer. According with the documentation this value will be always 464748, but we leave it configurable, jut in case.
        /// </summary>
        public string AcquirerId { get; set; }

        /// <summary>
        /// Represents the total amount of purchase.
        /// Note: The purchase amount must be presented as a character string that is 12  characters long. (i.e. $12.00 should be provided as  "000000001200").
        /// Can be excluded if PurchaseAmt field is on the form.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Represents the purchase currency ISO 4217 numeric currency code (ex: USD = 840).
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Represents the number of digits after the decimal point in the purchase amount (i.e. $12.00 = 2)
        /// </summary>
        public int CurrencyExponent { get; set; }

        /// <summary>
        /// Represents the customer IP address.
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Represents the Merchant ID provided by BAC
        /// </summary>
        public string MerchantId { get; set; }

        /// <summary>
        /// Represents the order number for a transaction.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Represents the signature for requesting the Hosted Page
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Represents the signature encryption method used for requesting the Hosted Page
        /// </summary>
        public string SignatureMethod { get; set; }

        /// <summary>
        /// Represents the <see cref="TransactionCode"/> used for this transaction.
        /// </summary>
        public int TransactionCode { get; set; }

        /// <summary>
        /// Represents a custom reference field that will be used in the future.
        /// </summary>
        public string CustomerReference { get; set; }

        
    }
}
