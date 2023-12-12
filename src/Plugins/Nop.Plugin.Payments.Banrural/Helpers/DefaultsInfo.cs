namespace Nop.Plugin.Payments.Banrural.Helpers
{
    /// <summary>
    /// Represents the default info to the Banrural plugin.
    /// </summary>
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the plug-in system name
        /// </summary>
        public static string SystemName => "Payments.Banrural";

        /// <summary>
        /// Gets the configuration route name
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Payments.Banrural.Configure";

        /// <summary>
        /// Gets a name of the view component to display payment info in public store
        /// </summary>
        public const string PAYMENT_INFO_VIEW_COMPONENT_NAME = "BanruralPaymentInfo";

        /// <summary>
        /// Gets the session key to get process payment request
        /// </summary>
        public static string PaymentRequestSessionKey => "OrderPaymentInfo";

        /// <summary>
        /// Banrural default currency code
        /// </summary>
        public const string DefaultUSDCurrencyCode = "HNL";

        /// <summary>
        /// Gets transaction result page route name
        /// </summary>
        public static string TransactionResultRouteName => "Plugin.Payments.Banrural.TransactionResult";

        /// <summary>
        /// Gets completed result page route name
        /// </summary>
        public static string CompletedRouteName => "Plugin.Payments.Banrural.Completed";
    }
}
