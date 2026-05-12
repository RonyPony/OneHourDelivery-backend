using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// Represents a name compatibility for <see cref="WarehouseUserCreatorMapping"/>.
    /// </summary>
    public sealed class WarehouseUserCreatorMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(WarehouseUserCreatorMapping), "Warehouse_UserCreator_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(WarehouseUserCreatorMapping), "WarehouseId"), "WarehouseId"},
            {(typeof(WarehouseUserCreatorMapping), "CustomerId"), "CustomerId"}
        };
    }
}
