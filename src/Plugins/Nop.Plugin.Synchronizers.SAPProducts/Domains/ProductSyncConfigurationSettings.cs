using Nop.Core.Configuration;

namespace Nop.Plugin.Synchronizers.SAPProducts.Domains
{
    /// <summary>
    /// Represents the plugin configurations.
    /// </summary>
    public class ProductSyncConfigurationSettings : ISettings
    {
        /// <summary>
        /// Indicates the URL for getting products from SAP.
        /// </summary>
        public string SapProductUrl { get; set; }

        /// <summary>
        /// Indicates the Authentication Header Scheme that will be at the request header.
        /// </summary>
        public string AuthenticationHeaderScheme { get; set; }

        /// <summary>
        /// Indicates the Authentication Header Parameter that will be at the request header.
        /// </summary>
        public string AuthenticationHeaderParameter { get; set; }

        /// <summary>
        /// Indicates if the plugin must sync the categories from SAP.
        /// </summary>
        public bool SyncCategories { get; set; }

        /// <summary>
        /// Indicates the URL for getting categories from SAP.
        /// </summary>
        public string SapCategoryUrl { get; set; }

        /// <summary>
        /// Indicates if the plugin must sync the manufacturers from SAP.
        /// </summary>
        public bool SyncManufacturers { get; set; }

        /// <summary>
        /// Indicates the URL for getting manufacturers from SAP.
        /// </summary>
        public string SapManufacturerUrl { get; set; }

        /// <summary>
        /// Indicates if the plugin must assign the ItemCode field as SKU when mapping the products form SAP.
        /// </summary>
        public bool AssignItemCodeFieldAsSku { get; set; }

        /// <summary>
        /// Represents if the plugin must update the information of the products that are already synchronized.
        /// </summary>
        public bool UpdateProductInformation { get; set; }
    }
}
