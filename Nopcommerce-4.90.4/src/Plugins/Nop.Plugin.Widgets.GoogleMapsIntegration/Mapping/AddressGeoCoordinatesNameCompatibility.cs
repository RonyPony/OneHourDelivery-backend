using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Mapping
{
    /// <summary>
    /// Address GeoCoordinates instance of backward compatibility of table naming.
    /// </summary>
    public sealed class AddressGeoCoordinatesNameCompatibility : INameCompatibility
    {
        /// <summary>
        /// Gets table name for mapping with the type.
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(AddressGeoCoordinatesMapping), "Address_GeoCoordinates_Mapping" }
        };

        /// <summary>
        ///  Gets column name for mapping with the entity's property and type.
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(AddressGeoCoordinatesMapping), "AddressId"), "AddressId"},
            {(typeof(AddressGeoCoordinatesMapping),  "Latitude"), "Latitude"},
            {(typeof(AddressGeoCoordinatesMapping),  "Longitude"), "Longitude"}
        };
    }
}