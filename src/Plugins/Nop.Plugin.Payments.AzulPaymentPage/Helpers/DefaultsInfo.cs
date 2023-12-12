namespace Nop.Plugin.Payments.AzulPaymentPage.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the plug-in system name
        /// </summary>
        public static string SystemName => "Payments.AzulPaymentPage";

        /// <summary>
        /// Gets the configuration route name
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Payments.AzulPaymentPage.Configure";

        /// <summary>
        /// Gets transaction result page route name
        /// </summary>
        public static string TransactionResultRouteName => "Plugin.Payments.AzulPaymentPage.TransactionResult";

        /// <summary>
        /// Gets the session key to get process payment request
        /// </summary>
        public static string PaymentRequestSessionKey => "OrderPaymentInfo";

        /// <summary>
        /// Gets a name of the view component to display payment info in public store
        /// </summary>
        public const string PaymentInfoViewComponentName = "AzulPaymentPagePaymentInfo";

        /// <summary>
        /// AZUL default currency code
        /// </summary>
        public const string DefaultDominicanPesoCurrencyCode = "DOP";
    }
}
