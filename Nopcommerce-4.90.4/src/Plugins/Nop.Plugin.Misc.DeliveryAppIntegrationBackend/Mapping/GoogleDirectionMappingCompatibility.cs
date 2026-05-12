using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="GoogleDirectionMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public class GoogleDirectionMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(GoogleDirectionMapping) , "Google_Direction_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(GoogleDirectionMapping), "OrderId"), "OrderId"},
            {(typeof(GoogleDirectionMapping), "GoogleResource"), "GoogleResource"},
            {(typeof(GoogleDirectionMapping), "DestinationType"), "DestinationType"},
            {(typeof(GoogleDirectionMapping), "RequestNumber"), "RequestNumber"},
            {(typeof(GoogleDirectionMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
