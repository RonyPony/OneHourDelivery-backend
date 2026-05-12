using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="OrderPaymentCollectionStatus"/> instance of backward compatibility of table naming.
    /// </summary>
    public sealed class OrderPaymentCollectionStatusCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(OrderPaymentCollectionStatus) , "Order_PaymentCollectionStatus_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(OrderPaymentCollectionStatus), "OrderId"), "OrderId"},
            {(typeof(OrderPaymentCollectionStatus), "CustomerId"), "CustomerId"},
            {(typeof(OrderPaymentCollectionStatus), "OrderTotal"), "OrderTotal"},
            {(typeof(OrderPaymentCollectionStatus), "PaymentCollectionStatusId"), "PaymentCollectionStatusId"},
            {(typeof(OrderPaymentCollectionStatus), "CreatedOnUtc"), "CreatedOnUtc"},
            {(typeof(OrderPaymentCollectionStatus), "CollectedByCustomerId"), "CollectedByCustomerId"},
            {(typeof(OrderPaymentCollectionStatus), "CollectedOnUtc"), "CollectedOnUtc"}
        };
    }
}
