using Nop.Core;
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Domains
{
    /// <summary>
    /// Represents the entity that stores the plugin settings.
    /// </summary>
    public sealed class PluginConfigurationSettings : BaseEntity, ISettings
    {
        /// <summary>
        /// Unique identifier that authenticates requests associated to Google Maps API's.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Indicates that address autocomplete search box is enabled in address forms.
        /// </summary>
        public bool AddressAutocompleteEnabled { get; set; }

        /// <summary>
        /// Indicates that Google Maps is enabled in address forms.
        /// </summary>
        public bool SelectAddresFromMapEnabled { get; set; }

        /// <summary>
        /// Indicates that addresses' latitude and longitude geocoding is enabled in address forms.
        /// </summary>
        public bool GetAddressFromCoordinatesEnabled { get; set; }

        /// <summary>
        /// Indicates if the map has boundaries.
        /// </summary>
        public bool MapBoundariesEnabled { get; set; }

        /// <summary>
        /// Indicates the north bound.
        /// </summary>
        public decimal NorthBound { get; set; }

        /// <summary>
        /// Indicates the south bound.
        /// </summary>
        public decimal SouthBound { get; set; }

        /// <summary>
        /// Indicates the west bound.
        /// </summary>
        public decimal WestBound { get; set; }

        /// <summary>
        /// Indicates the east bound.
        /// </summary>
        public decimal EastBound { get; set; }

        /// <summary>
        /// Indicates if a default latitude and longitude is enabled.
        /// </summary>
        public bool DefaultLatLngEnabled { get; set; }

        /// <summary>
        /// Indicates the default latitude.
        /// </summary>
        public decimal DefaultLatitude { get; set; }

        /// <summary>
        /// Indicates the default longitude.
        /// </summary>
        public decimal DefaultLongitude { get; set; }
    }
}
