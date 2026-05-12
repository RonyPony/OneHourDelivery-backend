using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="OrderRatingMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public sealed class OrderRatingMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(OrderRatingMapping), "Order_Rating_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(OrderRatingMapping), "OrderId"), "OrderId"},
            {(typeof(OrderRatingMapping), "Rate"), "Rate"},
            {(typeof(OrderRatingMapping), "Comment"), "Comment"},
            {(typeof(OrderRatingMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
