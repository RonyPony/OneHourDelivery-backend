using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Common;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models
{
    /// <summary>
    /// Represents a model for adding and editing customer addresses with Google Maps API's integration for geo-coordinates.
    /// </summary>
    public partial class AddressGeoCoordinatesEditModel : BaseNopModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AddressGeoCoordinatesEditModel"/>.
        /// </summary>
        public AddressGeoCoordinatesEditModel()
        {
            PluginConfigurationSettings = new PluginConfigurationSettings();
            AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping();
            Address = new AddressModel();
        }

        ///<inheritdoc/>
        public PluginConfigurationSettings PluginConfigurationSettings { get; set; }

        /// <inheritdoc/>
        public AddressGeoCoordinatesMapping AddressGeoCoordinatesMapping { get; set; }

        /// <summary>
        /// Represents a nopCommerce address.
        /// </summary>
        public AddressModel Address { get; set; }

        /// <summary>
        /// Indicates if Google Maps JavaScript API needs to be rendered.
        /// </summary>
        public bool RenderGoogleMapsJavaScript { get; set; }

        /// <summary>
        /// Indicates the id for the html div tag that contains the map.
        /// </summary>
        public string MapDivId { get; set; }

        /// <summary>
        /// Indicates the id for the html search address input.
        /// </summary>
        public string AutocompleteInputId { get; set; }

        /// <summary>
        /// Indicates the id for the html search geo coordinates input.
        /// </summary>
        public string GeoCoordinatesSearchInputId { get; set; }

        /// <summary>
        /// An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.
        /// </summary>
        public IList<SelectListItem> AvailableProvinces { get; set; }

        /// <summary>
        /// An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.
        /// </summary>
        public IList<SelectListItem> AvailableDistricts { get; set; }

        /// <summary>
        /// An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.
        /// </summary>
        public IList<SelectListItem> AvailableTownships { get; set; }

        /// <summary>
        /// An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.
        /// </summary>
        public IList<SelectListItem> AvailableNeighborhoods { get; set; }
    }
}
