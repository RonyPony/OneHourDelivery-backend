namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a send validation email operation result.
    /// </summary>
    public sealed class SendEmailResult
    {
        /// <summary>
        /// Indicates if the operation was successfully done.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Indicates the error message in case the operation wasn't successfully done.
        /// </summary>
        public string Message { get; set; }
    }
}
