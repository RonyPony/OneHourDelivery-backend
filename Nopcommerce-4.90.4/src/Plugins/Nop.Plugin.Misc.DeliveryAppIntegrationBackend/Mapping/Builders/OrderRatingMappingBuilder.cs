using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="OrderRatingMapping"/> entity builder.
    /// </summary>
    public sealed class OrderRatingMappingBuilder : NopEntityBuilder<OrderRatingMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderRatingMapping), nameof(OrderRatingMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderRatingMapping), nameof(OrderRatingMapping.Rate)))
                   .AsDecimal(3,2).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderRatingMapping), nameof(OrderRatingMapping.Comment)))
                    .AsString(1500).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderRatingMapping), nameof(OrderRatingMapping.CreatedOnUtc)))
                    .AsDateTime().NotNullable();
        }
    }
}
