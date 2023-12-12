using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Models
{
    /// <summary>
    /// Represents the model used for configuring the SAP Customer Synchronizer plugin.
    /// </summary> 
    public sealed class SapCustomersSynchronizerConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Represents the SAP Customer URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerUrl")]
        public string SapCustomerUrl { get; set; }

        /// <summary>
        /// Represents the SAP Customer Roles URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerRolesUrl")]
        public string SapCustomerRolesUrl { get; set; }

        /// <summary>
        /// Represents the SAP Address URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPCustomers.Fields.SapCustomerAddressUrl")]
        public string SapCustomerAddressUrl { get; set; }

        /// <summary>
        /// Represents the Authentication Header Scheme that will be at the request header.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderScheme")]
        public string AuthenticationHeaderScheme { get; set; }

        /// <summary>
        /// Represents the Authentication Header Parameter that will be at the request header.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPCustomers.Fields.AuthenticationHeaderParameter")]
        public string AuthenticationHeaderParameter { get; set; }
    }
}
