using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="MessengerDeclinedOrderMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public sealed class MessengerDeclinedOrderMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(MessengerDeclinedOrderMapping), "Messenger_DeclinedOrder_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(MessengerDeclinedOrderMapping), "CustomerId"), "CustomerId"},
            {(typeof(MessengerDeclinedOrderMapping), "OrderId"), "OrderId"},
            {(typeof(MessengerDeclinedOrderMapping), "DeclinedDate"), "DeclinedDate"},
            {(typeof(MessengerDeclinedOrderMapping), "DeclinedMessage"), "DeclinedMessage"}
        };
    }
}
