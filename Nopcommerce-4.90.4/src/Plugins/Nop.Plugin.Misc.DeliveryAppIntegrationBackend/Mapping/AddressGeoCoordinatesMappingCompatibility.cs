using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// Represents name compatibility for the Google Maps geo-coordinates table used by this plugin.
    /// </summary>
    public sealed class AddressGeoCoordinatesMappingCompatibility : INameCompatibility
    {
        /// <inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(AddressGeoCoordinatesMapping), "Address_GeoCoordinates_Mapping" }
        };

        /// <inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            { (typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.AddressId)), "AddressId" },
            { (typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.Latitude)), "Latitude" },
            { (typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.Longitude)), "Longitude" }
        };
    }
}
