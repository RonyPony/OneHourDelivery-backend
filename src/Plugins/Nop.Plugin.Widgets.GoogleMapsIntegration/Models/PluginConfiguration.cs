using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Models
{
    /// <summary>
    /// Represents the configurations for Google Maps Integration plugin.
    /// </summary>
    public sealed class PluginConfiguration : BaseNopEntityModel
    {
        /// <summary>
        /// Unique identifier that authenticates requests associated to Google Maps API's.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.ApiKey")]
        public string ApiKey { get; set; }

        /// <summary>
        /// Indicates that address autocomplete search box is enabled in address forms.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.AddressAutocompleteEnabled")]
        public bool AddressAutocompleteEnabled { get; set; }

        /// <summary>
        /// Indicates that Google Maps is enabled in address forms.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.SelectAddresFromMapEnabled")]
        public bool SelectAddresFromMapEnabled { get; set; }

        /// <summary>
        /// Indicates that addresses' latitude and longitude geocoding is enabled in address forms.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.GetAddressFromCoordinatesEnabled")]
        public bool GetAddressFromCoordinatesEnabled { get; set; }

        /// <summary>
        /// Indicates if the map has boundaries.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.MapBoundariesEnabled")]
        public bool MapBoundariesEnabled { get; set; }

        /// <summary>
        /// Indicates the north bound.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.NorthBound")]
        public decimal NorthBound { get; set; }

        /// <summary>
        /// Indicates the south bound.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.SouthBound")]
        public decimal SouthBound { get; set; }

        /// <summary>
        /// Indicates the west bound.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.WestBound")]
        public decimal WestBound { get; set; }

        /// <summary>
        /// Indicates the east bound.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.EastBound")]
        public decimal EastBound { get; set; }

        /// <summary>
        /// Indicates if a default latitude and longitude is enabled.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DefaultLatLngEnabled")]
        public bool DefaultLatLngEnabled { get; set; }

        /// <summary>
        /// Indicates the default latitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DefaultLatitude")]
        public decimal DefaultLatitude { get; set; }

        /// <summary>
        /// Indicates the default longitude.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DefaultLongitude")]
        public decimal DefaultLongitude { get; set; }
    }
}
