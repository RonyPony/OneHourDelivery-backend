using Nop.Core;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Domains
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
        public decimal Latitude { get; set; }

        /// <summary>
        /// Indicates the longitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Longitude")]
        public decimal Longitude { get; set; }
    }
}
