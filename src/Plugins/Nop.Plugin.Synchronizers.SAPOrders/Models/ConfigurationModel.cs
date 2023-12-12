using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Synchronizers.SAPOrders.Models
{
    /// <summary>
    /// Represents the configuration model for this plug-in
    /// </summary>
    public sealed class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Gets or sets the url used to post orders to SAP API
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersPostUrl")]
        [Required]
        public string ApiPostUrl { get; set; }

        /// <summary>
        /// Gets or sets the url used to consult orders from SAP API
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPOrders.Fields.Api.OrdersGetUrl")]
        [Required]
        public string ApiGetUrl { get; set; }

        /// <summary>
        /// Gets or sets the Auth scheme used for making requests to SAP API
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPOrders.Fields.Api.AuthenticationScheme")]
        [Required]
        public string ApiAuthenticationScheme { get; set; }

        /// <summary>
        /// API Token for authorization using Bearer token scheme
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPOrders.Fields.Api.Token")]
        [Required]
        public string ApiToken { get; set; }
    }
}
