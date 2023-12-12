namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts
{
    /// <summary>
    /// Represents the contracts to manage the results of the operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperationResult<T>
    {
        /// <summary>
        /// Represents the message's result
        /// </summary>
        string Message { get; }

        public string MessageDetail { get; set; }

        /// <summary>
        /// Represents if the operation was successful
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Represents the operation's result
        /// </summary>
        T Entity { get; }

    }
}
