namespace Nop.Plugin.Payments.CyberSource.Models
{
    /// <summary>
    /// Model received from CyberSource containing transaction result information.
    /// </summary>
    public class TransactionResponseModel
    {

        /// <summary>
        /// CyberSource reason code
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// Credit/Debit card type sent to CyberSource
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Order total
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// CyberSource transaction Id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Id of the customer
        /// </summary>
        public string ConsumerId { get; set; }

        /// <summary>
        /// Currency type which the order was paid with
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// CyberSource decision on the transaction
        /// </summary>
        public string Decision { get; set; }

        /// <summary>
        /// Contains information about invalid fields sent to CyberSource (if any)
        /// </summary>
        public string InvalidFields { get; set; }

        /// <summary>
        /// Contains an error message from CyberSource (if any)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// CyberSource transaction UUID
        /// </summary>
        public string TransactionUuid { get; set; }

        /// <summary>
        /// CyberSource transaction type
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// Reference number sent to CyberSource (NopCommerce order Id)
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Indicates credit card payment information token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Indicates the four digits of the  credit card payment information  
        /// </summary>
        public string CardLastDigits { get; set; }

        /// <summary>
        /// Indicates the date of expiry of the credit card
        /// </summary>
        public string CardExpiryDate { get; set; }
    }

}
