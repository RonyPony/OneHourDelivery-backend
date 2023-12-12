using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents the names of territorial structure from Multientrega
    /// </summary>
    public sealed class MultientregaAddressStructureModel : BaseNopModel
    {
        /// <summary>
        /// Indicates the province name.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.ProvinceId")]
        public string ProvinceName { get; set; }

        /// <summary>
        /// Indicates the district name.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DistrictId")]
        public string DistrictName { get; set; }

        /// <summary>
        /// Indicates the township name.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.TownshipId")]
        public string TownshipName { get; set; }

        /// <summary>
        /// Indicates the neighborhood name.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.NeighborhoodId")]
        public string NeighborhoodName { get; set; }
    }
}
