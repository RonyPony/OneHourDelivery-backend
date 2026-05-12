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
    /// Represents a <see cref="OrderDeliveryStatusMapping"/> entity builder.
    /// </summary>
    public sealed class OrderDeliveryStatusMappingBuilder : NopEntityBuilder<OrderDeliveryStatusMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.CustomerId)))
                    .AsInt32().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.DeliveryStatusId)))
                   .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.AwaitingForMessengerDate)))
                   .AsDateTime().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.AcceptedByMessengerDate)))
                   .AsDateTime().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.DeliveryInProgressDate)))
                   .AsDateTime().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.DeliveredDate)))
                   .AsDateTime().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.DeclinedByStoreDate)))
                   .AsDateTime().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderDeliveryStatusMapping), nameof(OrderDeliveryStatusMapping.MessageToDeclinedOrder)))
                   .AsString(1000).Nullable();
        }
    }
}
