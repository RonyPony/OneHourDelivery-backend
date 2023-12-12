using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Shipping;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Represents the model used for configuring the SAP Customer Synchronizer plugin.
    /// </summary> 
    public sealed class DeliveryAsapConfigurationModel : BaseNopEntityModel
    {
        /// <summary>
        /// Represents the api key in ASAP service.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.ApiKey")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Represents the ASAP service URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.ServiceUrl")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Represents the shared secret in ASAP service.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.SharedSecret")]
        public string SharedSecret { get; set; }

        /// <summary>
        /// Represent the user token in ASAP service.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.UserToken")]
        public string UserToken { get; set; }

        /// <summary>
        /// Represent the email of user account in ASAP service.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.Email")]
        public string Email { get; set; }

        /// <summary>
        /// Represents the rate given to Delivery ASAP shipping.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.Rate")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Rpresents default Warehouse of Delivery ASAP plugin.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.DefaultWarehoseId")]
        public int DefaultWarehouseId { get; set; }

        /// <summary>
        /// Represents a description of Delivery ASAP plugin.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.ASAP.Fields.Description")]
        public string Description { get; set; }

        /// <summary>
        /// Represents avaible warehouses list.
        /// </summary>
        public List<SelectListItem> Warehouses { get; set; }
    }
}