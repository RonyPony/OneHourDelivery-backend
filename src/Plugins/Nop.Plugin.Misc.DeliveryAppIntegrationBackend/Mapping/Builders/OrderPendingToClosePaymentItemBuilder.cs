using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a order Pending To Close Payment item entity builder
    /// </summary>
    public partial class OrderPendingToClosePaymentItemBuilder : NopEntityBuilder<OrderPendingToClosePaymentItem>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(OrderPendingToClosePaymentItem.OrderId)).AsInt32().ForeignKey<OrderPendingToClosePayment>()
                .WithColumn(nameof(OrderPendingToClosePaymentItem.ProductId)).AsInt32().ForeignKey<Product>();
        }

        #endregion
    }
}
