using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="OrderDeliveryStatusMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public sealed class OrderDeliveryStatusMappingCompatibility : INameCompatibility
    {
        /// <inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(OrderDeliveryStatusMapping), "Order_DeliveryStatus_Mapping" }
        };

        /// <inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(OrderDeliveryStatusMapping), "CustomerId"), "CustomerId"},
            {(typeof(OrderDeliveryStatusMapping), "OrderId"), "OrderId"},
            {(typeof(OrderDeliveryStatusMapping), "DeliveryStatusId"), "DeliveryStatusId"},
            {(typeof(OrderDeliveryStatusMapping), "AwaitingForMessengerDate"), "AwaitingForMessengerDate"},
            {(typeof(OrderDeliveryStatusMapping), "AcceptedByMessengerDate"), "AcceptedByMessengerDate"},
            {(typeof(OrderDeliveryStatusMapping), "DeliveryInProgressDate"), "DeliveryInProgressDate"},
            {(typeof(OrderDeliveryStatusMapping), "DeliveredDate"), "DeliveredDate"},
            {(typeof(OrderDeliveryStatusMapping), "DeclinedByStoreDate"), "DeclinedByStoreDate"},
            {(typeof(OrderDeliveryStatusMapping), "MessageToDeclinedOrder"), "MessageToDeclinedOrder"}
        };
    }
}
