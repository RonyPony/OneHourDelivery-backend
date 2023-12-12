using Nop.Core.Configuration;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Helpers
{
    /// <summary>
    /// Represents settings class for this plugin
    /// </summary>
    public sealed class ConfigurationSettings : ISettings
    {
        /// <summary>
        /// Indicates the name of the request authorization key.
        /// </summary>
        public string AuthKeyName { get; set; }

        /// <summary>
        /// Indicates the value of the request authorization key.
        /// </summary>
        public string AuthKeyValue { get; set; }

        /// <summary>
        /// Indicates the url used to post orders to WAPI.
        /// </summary>
        public string ApiPostUrl { get; set; }

        /// <summary>
        /// Indicates the default store pickup code to be used when pickup point is not defined.
        /// </summary>
        public string DefaultStorePickupCode { get; set; }
    }
}
