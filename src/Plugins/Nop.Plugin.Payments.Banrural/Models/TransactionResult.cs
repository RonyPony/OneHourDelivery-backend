namespace Nop.Plugin.Payments.Banrural.Models
{
    /// <summary>
    /// Represents the operation result after processing a transaction through Banrural.
    /// </summary>
    public sealed class TransactionResult
    {
        /// <summary>
        /// Represents the number of the order.
        /// The same one used in the initial request for processing the transaction.
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Represents the date of the order.
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// Represents the currency used to process the order.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Represents the order's tax.
        /// </summary>
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Represents the order's shipping price.
        /// </summary>
        public decimal ShippingAmount { get; set; }

        /// <summary>
        /// Represents the order's amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Represents the client's firstname.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents the client's lastname.
        /// </summary>
        public string LastName { get; set; }

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
    }
}