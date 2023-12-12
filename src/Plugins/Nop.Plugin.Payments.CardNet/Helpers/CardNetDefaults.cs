namespace Nop.Plugin.Payments.CardNet.Helpers
{
    /// <summary>
    /// CardNet plugin constants
    /// </summary>
    public static class CardNetDefaults
    {
        /// <summary>
        /// PaymentInfo view component name
        /// </summary>
        public const string PaymentInfoViewComponentName = "CardNetPaymentInfoPage";

        /// <summary>
        /// ScriptViewComponent view component name
        /// </summary>
        public const string ScriptViewComponentName = "CardNetPaymentInfoPageScripts";

        /// <summary>
        /// CardNet plug-in system name
        /// </summary>
        public const string SystemName = "Payments.CardNet";

        /// <summary>
        /// OneTimeToken session variable name
        /// </summary>
        public const string CacheTokenName = "OTToken";

        /// <summary>
        /// CardNet default currency code
        /// </summary>
        public const string DefaultCurrencyCode = "DOP";
    }
}
