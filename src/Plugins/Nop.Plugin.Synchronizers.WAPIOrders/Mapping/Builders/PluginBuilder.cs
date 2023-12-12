using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.WAPIOrders.Domains;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="OrderTransactionCodeMapping"/> entity builder.
    /// </summary>
    public class PluginBuilder : NopEntityBuilder<OrderTransactionCodeMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderTransactionCodeMapping), nameof(OrderTransactionCodeMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(OrderTransactionCodeMapping), nameof(OrderTransactionCodeMapping.TransactionCode)))
                    .AsString(50).PrimaryKey();
        }

        #endregion
    }
}