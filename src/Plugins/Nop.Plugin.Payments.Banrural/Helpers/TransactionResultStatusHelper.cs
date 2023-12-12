namespace Nop.Plugin.Payments.Banrural.Helpers
{
    /// <summary>
    /// Represents Banrural's transaction result status.
    /// </summary>
    public static class TransactionResultStatusHelper
    {
        /// <summary>
        /// Represents when the order is aproved.
        /// </summary>
        public const string Approved = "APROBADA";

        /// <summary>
        /// Represents when the order is declined.
        /// </summary>
        public const string Declined = "DECLINADA";

        /// <summary>
        /// Represents when the order has an error.
        /// </summary>
        public const string Error = "ERROR";
    }
}
