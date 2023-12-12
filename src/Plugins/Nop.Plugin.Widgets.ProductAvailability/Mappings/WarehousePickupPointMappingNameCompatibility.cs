using Nop.Data.Mapping;
using Nop.Plugin.Widgets.ProductAvailability.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.ProductAvailability.Mappings
{
    /// <summary>
    /// Represents the backward compatibility of table naming for <see cref="WarehousePickupPointMapping"/>.
    /// </summary>
    public sealed class WarehousePickupPointMappingNameCompatibility : INameCompatibility
    {
        /// <summary>
        /// Gets the table name for mapping with the type.
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(WarehousePickupPointMapping), "Warehouse_PickupPoint_Mapping" }
        };

        /// <summary>
        ///  Gets the column names for mapping with the entity's property and type.
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(WarehousePickupPointMapping), "WarehouseId"), "WarehouseId"},
            {(typeof(WarehousePickupPointMapping), "PickupPointId"), "PickupPointId"},
            {(typeof(WarehousePickupPointMapping), "NumBodega"), "NumBodega"},
            {(typeof(WarehousePickupPointMapping), "NumTienda"), "NumTienda"},
        };
    }
}
