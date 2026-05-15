using Nop.Data.Mapping;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Areas.Admin.Models;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Mapping
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
