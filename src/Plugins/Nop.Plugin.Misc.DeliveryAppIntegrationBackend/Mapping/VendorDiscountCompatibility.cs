using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="INameCompatibility"/> for <see cref="VendorDiscount"/> entity.
    /// </summary>
    public sealed class VendorDiscountCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(VendorDiscount), "VendorDiscount" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(VendorDiscount), "VendorId"), "Vendor_Id"},
            {(typeof(VendorDiscount), "DiscountId"), "Discount_Id"}
        };
    }
}
