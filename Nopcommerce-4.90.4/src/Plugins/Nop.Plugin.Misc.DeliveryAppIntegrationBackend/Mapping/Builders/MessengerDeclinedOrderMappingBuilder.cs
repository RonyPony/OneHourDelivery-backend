using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="MessengerDeclinedOrderMapping"/> entity builder.
    /// </summary>
    public sealed class MessengerDeclinedOrderMappingBuilder : NopEntityBuilder<MessengerDeclinedOrderMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MessengerDeclinedOrderMapping), nameof(MessengerDeclinedOrderMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MessengerDeclinedOrderMapping), nameof(MessengerDeclinedOrderMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MessengerDeclinedOrderMapping), nameof(MessengerDeclinedOrderMapping.DeclinedDate)))
                   .AsDateTime().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MessengerDeclinedOrderMapping), nameof(MessengerDeclinedOrderMapping.DeclinedMessage)))
                    .AsString(500).NotNullable();
        }
    }
}
