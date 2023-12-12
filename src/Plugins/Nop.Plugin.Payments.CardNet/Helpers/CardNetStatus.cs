namespace Nop.Plugin.Payments.CardNet.Helpers
{
    /// <summary>
    /// Constant values for CardNet transaction statuses from their API
    /// </summary>
    public static class CardNetStatus
    {
        /// <summary>
        /// Approved transaction status
        /// </summary>
        public const string Approved = "Approved";

        /// <summary>
        /// Rejected transaction status
        /// </summary>
        public const string Rejected = "Rejected";

        /// <summary>
        /// Error transaction status
        /// </summary>
        public const string Error = "Error";
    }
}
