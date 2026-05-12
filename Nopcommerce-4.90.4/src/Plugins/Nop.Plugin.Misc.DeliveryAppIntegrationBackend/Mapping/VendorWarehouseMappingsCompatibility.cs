using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// Represents a name compatibility for <see cref="VendorWarehouseMapping"/>.
    /// </summary>
    public sealed class VendorWarehouseMappingsCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(VendorWarehouseMapping), "Vendor_Warehouse_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(VendorWarehouseMapping), "VendorId"), "VendorId"},
            {(typeof(VendorWarehouseMapping), "WarehouseId"), "WarehouseId"}
        };
    }
}
