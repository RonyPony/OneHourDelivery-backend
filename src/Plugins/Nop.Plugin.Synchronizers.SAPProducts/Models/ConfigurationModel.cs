using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Synchronizers.SAPProducts.Models
{
    /// <summary>
    /// Represents the model used for configuring the SAP Products Synchronizer.
    /// </summary>
    public sealed class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// Represents the SAP Products URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.SapProductUrl")]
        [Required]
        public string SapProductUrl { get; set; }

        /// <summary>
        /// Represents the Authentication Header Scheme that will be at the request header.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderScheme")]
        [Required]
        public string AuthenticationHeaderScheme { get; set; }

        /// <summary>
        /// Represents the Authentication Header Parameter that will be at the request header.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.AuthenticationHeaderParameter")]
        [Required]
        public string AuthenticationHeaderParameter { get; set; }

        /// <summary>
        /// Represents if the plugin must sync manufacturers.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.SyncManufacturers")]
        public bool SyncManufacturers { get; set; }

        /// <summary>
        /// Represents if the plugin must sync categories.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.SyncCategories")]
        public bool SyncCategories { get; set; }

        /// <summary>
        /// Represents the SAP Products URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.SapCategoryUrl")]
        public string SapCategoryUrl { get; set; }

        /// <summary>
        /// Represents the SAP Products URL that the plugin will be connecting to.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.SapManufacturerUrl")]
        public string SapManufacturerUrl { get; set; }

        /// <summary>
        /// Represents if the plugin must the ItemCode field as SKU when mapping the products form SAP.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.AssignItemCodeFieldAsSku")]
        public bool AssignItemCodeFieldAsSku { get; set; }

        /// <summary>
        /// Represents if the plugin must update the information of the products that are already synchronized.
        /// </summary>
        [NopResourceDisplayName("Plugins.Synchronizers.SAPProducts.Fields.UpdateProductInformation")]
        public bool UpdateProductInformation { get; set; }
    }
}
