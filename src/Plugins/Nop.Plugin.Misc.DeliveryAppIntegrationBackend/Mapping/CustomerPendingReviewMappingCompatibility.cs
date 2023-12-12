using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="CustomerPendingReviewMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public class CustomerPendingReviewMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(CustomerPendingReviewMapping) , "Customer_Pending_Review_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CustomerPendingReviewMapping), "CustomerId"), "CustomerId"},
            {(typeof(CustomerPendingReviewMapping), "OrderId"), "OrderId"},
            {(typeof(CustomerPendingReviewMapping), "VendorId"), "VendorId"},
            {(typeof(CustomerPendingReviewMapping), "PendingReviewStatus"), "PendingReviewStatus"},
            {(typeof(CustomerPendingReviewMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
