using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Areas.Admin.Models.Shipping;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Areas.Admin.Models
{
    /// <summary>
    /// Represents a model for adding and editing warehouses addresses with Google Maps API's integration for geo coordinates.
    /// </summary>
    public partial class CustomWarehouseEditModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CustomWarehouseEditModel"/>.
        /// </summary>
        public CustomWarehouseEditModel()
        {
            PluginConfigurationSettings = new PluginConfigurationSettings();
            AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping();
            WarehouseModel = new WarehouseModel();
        }

        ///<inheritdoc/>
        public PluginConfigurationSettings PluginConfigurationSettings { get; set; }

        /// <inheritdoc/>
        public AddressGeoCoordinatesMapping AddressGeoCoordinatesMapping { get; set; }

        /// <summary>
        /// Represents a nopCommerce warehouse model.
        /// </summary>
        public WarehouseModel WarehouseModel { get; set; }
    }
}
