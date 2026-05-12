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
    /// Represents a <see cref="OrderPaymentCollectionStatus"/> entity builder.
    /// </summary>
    public sealed class OrderPaymentCollectionStatusBuilder : NopEntityBuilder<OrderPaymentCollectionStatus>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.OrderId)))
                    .AsInt32().ForeignKey<Order>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.CustomerId)))
                    .AsInt32().ForeignKey<Customer>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.OrderTotal)))
                   .AsDecimal(18, 4).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.PaymentCollectionStatusId)))
                    .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.CreatedOnUtc)))
                   .AsDateTime2().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.CollectedByCustomerId)))
                    .AsInt32().Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderPaymentCollectionStatus), nameof(OrderPaymentCollectionStatus.CollectedOnUtc)))
                   .AsDateTime2().Nullable();
        }
    }
}
