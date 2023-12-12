using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    public class CustomerDiscountMappingCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(CustomerDiscountMapping) , "Customer_Discount_Mapping" }
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CustomerDiscountMapping), "CustomerId"), "CustomerId"},
            {(typeof(CustomerDiscountMapping), "DiscountId"), "DiscountId"},
            {(typeof(CustomerDiscountMapping), "CreatedAtUtc"), "CreatedAtUtc"}
        };
    }
}
