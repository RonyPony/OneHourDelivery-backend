using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="DriverRatingMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public class DriverRatingMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(DriverRatingMapping) , "Driver_Rating_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(DriverRatingMapping), "DriverId"), "DriverId"},
            {(typeof(DriverRatingMapping), "CustomerId"), "CustomerId"},
            {(typeof(DriverRatingMapping), "OrderId"), "OrderId"},
            {(typeof(DriverRatingMapping), "Rating"), "Rating"},
            {(typeof(DriverRatingMapping), "RatingType"), "RatingType"},
            {(typeof(DriverRatingMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
