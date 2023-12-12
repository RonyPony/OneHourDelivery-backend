using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Data.Extensions;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="DriverLocationInfoMapping"/> entity builder.
    /// </summary>
    public class DriverLocationInfoMappingBuilder : NopEntityBuilder<DriverLocationInfoMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.Latitude)))
                    .AsDecimal(10,7).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.Longitude)))
                    .AsDecimal(10,7).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.DeliveryStatus)))
                    .AsInt32().Nullable()
                    .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.DestinationType)))
                    .AsInt32().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverLocationInfoMapping), nameof(DriverLocationInfoMapping.CreatedOnUtc)))
                    .AsDateTime().NotNullable();
        }
        #endregion
    }
}
