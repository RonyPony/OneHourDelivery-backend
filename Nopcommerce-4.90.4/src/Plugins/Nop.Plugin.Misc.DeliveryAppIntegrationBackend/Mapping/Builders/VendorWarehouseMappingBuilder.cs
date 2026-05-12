using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="VendorWarehouseMapping"/>.
    /// </summary>
    public sealed class VendorWarehouseMappingBuilder : NopEntityBuilder<VendorWarehouseMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorWarehouseMapping), nameof(VendorWarehouseMapping.VendorId)))
                    .AsInt32().ForeignKey<Vendor>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorWarehouseMapping), nameof(VendorWarehouseMapping.WarehouseId)))
                    .AsInt32().ForeignKey<Warehouse>(onDelete: Rule.None);
        }

        #endregion
    }
}
