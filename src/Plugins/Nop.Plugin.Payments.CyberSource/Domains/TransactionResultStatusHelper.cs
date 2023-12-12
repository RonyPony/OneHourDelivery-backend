namespace Nop.Plugin.Payments.CyberSource.Domains
{
    /// <summary>
    /// CyberSource transaction result statuses
    /// </summary>
    public static class TransactionResultStatusHelper
    {
        /// <summary>
        /// Accepted status
        /// </summary>
        public const string Accepted = "ACCEPT";

        /// <summary>
        /// Declined status
        /// </summary>
        public const string Declined = "DECLINE";

        /// <summary>
        /// Error status
        /// </summary>
        public const string Error = "ERROR";

        /// <summary>
        /// Canceled status
        /// </summary>
        public const string Cancel = "CANCEL";
    }
}
