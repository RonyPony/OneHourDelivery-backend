namespace Nop.Plugin.Payments.BAC.Helpers
{
    public sealed class DefaultsInfo
    {
        /// <summary>
        /// Gets the plug-in system name
        /// </summary>
        public static string SystemName => "Payments.BAC";

        /// <summary>
        /// Gets the configuration route name
        /// </summary>
        public static string ConfigurationRouteName => "Plugin.Payments.BAC.Configure";

        /// <summary>
        /// Gets transaction result page route name
        /// </summary>
        public static string TransactionResultRouteName => "Plugin.Payments.BAC.TransactionResult";

        /// <summary>
        /// Represents the name for the public view component.
        /// </summary>
        public const string PaymentInfoViewComponentName = "BacPaymentInfo";

        /// <summary>
        /// Represents the default signature encryption method used for encrypting the info send to BAC. 
        /// </summary>
        public const string DefaultSignatureEncryptionMethod = "SHA1";
    }
}
