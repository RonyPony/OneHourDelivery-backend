using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains
{
    /// <summary>
    /// Represents the relation between an address and it's geo coordinates.
    /// </summary>
    public sealed class AddressGeoCoordinatesMapping : BaseEntity
    {
        /// <summary>
        /// Indicates the address to which the geo coordinates are related.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Indicates the latitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Latitude")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// Indicates the longitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Longitude")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// Indicates the Multientrega's province id.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.ProvinceId")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// Indicates the Multientrega's district id.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DistrictId")]
        public string DistrictId { get; set; }

        /// <summary>
        /// Indicates the Multientrega's township id.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.TownshipId")]
        public string TownshipId { get; set; }

        /// <summary>
        /// Indicates the Multientrega's neighborhood id.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.NeighborhoodId")]
        public string NeighborhoodId { get; set; }
    }
}
