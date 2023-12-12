using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
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
        /// Indicates if the latitude and longitude fields will be shown on address form.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.DisplayLatLngFields")]
        public bool DisplayLatLngFields { get; set; }

        /// <summary>
        /// Indicates if the latitude and longitude fields are required.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.LatLngFieldsRequired")]
        public bool LatLngFieldsRequired { get; set; }

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

        /// <summary>
        /// Indicates Multientrega's login email.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Email")]
        public string Email { get; set; }

        /// <summary>
        /// Indicates Multientrega's login password.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Password")]
        public string Password { get; set; }

        /// <summary>
        /// Indicates Multientrega's base url.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.BaseUrl")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Indicates Multientrega's branch office.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.BranchOffice")]
        public string BranchOffice { get; set; }

        /// <summary>
        /// Indicates the NIT for the store.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.Nit")]
        public string Nit { get; set; }

        /// <summary>
        /// Indicates the description for the shipping option.
        /// </summary>
        [NopResourceDisplayName("Plugin.Widgets.GoogleMapsIntegration.Fields.ShippingOptionDescription")]
        public string ShippingOptionDescription { get; set; }

        ///<inheritdoc/>
        public MultientregaSearchModel SearchModel { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PluginConfiguration"/>.
        /// </summary>
        public PluginConfiguration()
        {
            SearchModel = new MultientregaSearchModel();
            SearchModel.SetGridPageSize(10);
        }
    }
}
