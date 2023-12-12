using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    public sealed class AddressGeoCoordinateViewModel
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

        public double ProximityInMeters { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }
        public double Rating { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public string EstimatedPreparationTime { get; set; }
    }
}
