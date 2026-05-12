using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Shipping;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="WarehouseUserCreatorMapping"/>.
    /// </summary>
    public sealed class WarehouseUserCreatorMappingBuilder : NopEntityBuilder<WarehouseUserCreatorMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseUserCreatorMapping), nameof(WarehouseUserCreatorMapping.WarehouseId)))
                    .AsInt32().ForeignKey<Warehouse>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseUserCreatorMapping), nameof(WarehouseUserCreatorMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>(onDelete: Rule.None);
        }

        #endregion
    }
}
