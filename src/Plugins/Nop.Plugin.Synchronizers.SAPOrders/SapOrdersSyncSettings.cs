using Nop.Core.Configuration;

namespace Nop.Plugin.Synchronizers.SAPOrders
{
    /// <summary>
    /// Represents settings class for this plug-in
    /// </summary>
    public sealed class SapOrdersSyncSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the url used to post orders to SAP API
        /// </summary>
        public string ApiPostUrl { get; set; }

        /// <summary>
        /// Gets or sets the url used to consult orders from SAP API
        /// </summary>
        public string ApiGetUrl { get; set; }

        /// <summary>
        /// Gets or sets the Auth scheme used for making requests to SAP API
        /// </summary>
        public string AuthenticationScheme { get; set; }

        /// <summary>
        /// API Token for authorization using Bearer token scheme
        /// </summary>
        public string ApiToken { get; set; }
    }
}
