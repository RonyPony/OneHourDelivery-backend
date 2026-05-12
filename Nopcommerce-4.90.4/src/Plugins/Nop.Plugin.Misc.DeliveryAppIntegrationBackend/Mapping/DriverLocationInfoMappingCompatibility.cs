using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="DriverLocationInfoMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public class DriverLocationInfoMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(DriverLocationInfoMapping) , "Driver_Location_Info_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(DriverLocationInfoMapping), "OrderId"), "OrderId"},
            {(typeof(DriverLocationInfoMapping), "Latitude"), "Latitude"},
            {(typeof(DriverLocationInfoMapping), "Longitude"), "Longitude"},
            {(typeof(DriverLocationInfoMapping), "DeliveryStatus"), "DeliveryStatus"},
            {(typeof(DriverLocationInfoMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
