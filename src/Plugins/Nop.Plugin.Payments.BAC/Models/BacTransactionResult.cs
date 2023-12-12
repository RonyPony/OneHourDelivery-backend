namespace Nop.Plugin.Payments.BAC.Models
{
    /// <summary>
    /// Represents the result model when a transaction is completed.
    /// </summary>
    public sealed class BacTransactionResult
    {
        /// <summary>
        /// Represents the transaction result identification.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Represents the response code.
        /// </summary>
        public int RespCode { get; set; }

        /// <summary>
        /// Represents the reason code.
        /// </summary>
        public int ReasonCode { get; set; }
    }
}
