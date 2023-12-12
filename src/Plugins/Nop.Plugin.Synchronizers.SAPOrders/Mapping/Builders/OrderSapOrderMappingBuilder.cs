using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.SAPOrders.Domains;

namespace Nop.Plugin.Synchronizers.SAPOrders.Mapping.Builders
{
    /// <summary>
    /// Represents an order-SAP order mapping entity builder
    /// </summary>
    public sealed class OrderSapOrderMappingBuilder : NopEntityBuilder<OrderSapOrderMapping>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderSapOrderMapping), nameof(OrderSapOrderMapping.OrderId)))
                .AsInt32().ForeignKey<Order>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderSapOrderMapping), nameof(OrderSapOrderMapping.SapOrderId)))
                .AsString(50).PrimaryKey();
        }
    }
}
