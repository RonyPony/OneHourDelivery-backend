using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Models
{
    /// <summary>
    /// Represents the configuration model for this plugin.
    /// </summary>
    public sealed class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Indicates the name of the request authorization key.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.WAPIOrders.Fields.AuthKeyName")]
        [Required]
        public string AuthKeyName { get; set; }

        /// <summary>
        /// Indicates the value of the request authorization key.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.WAPIOrders.Fields.AuthKeyValue")]
        [Required]
        public string AuthKeyValue { get; set; }

        /// <summary>
        /// Indicates the url used to post orders to WAPI.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.WAPIOrders.Fields.ApiPostUrl")]
        [Required]
        public string ApiPostUrl { get; set; }

        /// <summary>
        /// Indicates the default store pickup code to be used when pickup point is not defined.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.WAPIOrders.Fields.DefaultStorePickupCode")]
        [Required]
        public string DefaultStorePickupCode { get; set; }
    }
}
