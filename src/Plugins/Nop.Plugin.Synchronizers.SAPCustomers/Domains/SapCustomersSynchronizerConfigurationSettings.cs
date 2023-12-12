using Nop.Core.Configuration;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Domains
{
    /// <summary>
    /// Represents the entity for storing the SAP Customers Synchronizer configuration for requesting synchronizing the customers.
    /// </summary>
    public sealed class SapCustomersSynchronizerConfigurationSettings : ISettings
    {
        /// <summary>
        /// Represents the SAP Customers URL that the plugin will be connecting to.
        /// </summary>
        public string SapCustomerUrl { get; set; }

        /// <summary>
        /// Represents the SAP Customers roles URL that the plugin will be connecting to.
        /// </summary>
        public string SapCustomerRolesUrl { get; set; }

        /// <summary>
        /// Represents the SAP Address roles URL that the plugin will be connecting to.
        /// </summary>
        public string SapCustomerAddressUrl { get; set; }

        /// <summary>
        /// Represents the Authentication Header Scheme that will be at the request header.
        /// </summary>
        public string AuthenticationHeaderScheme { get; set; }

        /// <summary>
        /// Represents the Authentication Header Parameter that will be at the request header.
        /// </summary>
        public string AuthenticationHeaderParameter { get; set; }
    }
}
