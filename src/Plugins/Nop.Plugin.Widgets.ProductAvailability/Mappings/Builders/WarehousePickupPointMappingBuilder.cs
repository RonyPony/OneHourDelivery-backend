using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Shipping;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Pickup.PickupInStore.Domain;
using Nop.Plugin.Widgets.ProductAvailability.Domains;

namespace Nop.Plugin.Widgets.ProductAvailability.Mappings.Builders
{
    /// <summary>
    /// Represents a base entity builder for <see cref="WarehousePickupPointMapping"/>.
    /// </summary>
    public sealed class WarehousePickupPointMappingBuilder : NopEntityBuilder<WarehousePickupPointMapping>
    {
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehousePickupPointMapping), nameof(WarehousePickupPointMapping.WarehouseId)))
                    .AsInt32().ForeignKey<Warehouse>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehousePickupPointMapping), nameof(WarehousePickupPointMapping.PickupPointId)))
                    .AsInt32().ForeignKey<StorePickupPoint>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehousePickupPointMapping), nameof(WarehousePickupPointMapping.NumBodega)))
                    .AsString(4).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehousePickupPointMapping), nameof(WarehousePickupPointMapping.NumTienda)))
                    .AsString(4).NotNullable();
        }
    }
}
