using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System.Data;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a order entity builder
    /// </summary>
    public partial class OrderPendingToClosePaymentBuilder : NopEntityBuilder<OrderPendingToClosePayment>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(OrderPendingToClosePayment.CustomOrderNumber)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(OrderPendingToClosePayment.BillingAddressId)).AsInt32().ForeignKey<Address>(onDelete: Rule.None)
                .WithColumn(nameof(OrderPendingToClosePayment.VendorId)).AsInt32().ForeignKey<Vendor>(onDelete: Rule.None)
                .WithColumn(nameof(OrderPendingToClosePayment.CustomerId)).AsInt32().ForeignKey<Customer>(onDelete: Rule.None)
                .WithColumn(nameof(OrderPendingToClosePayment.PickupAddressId)).AsInt32().Nullable().ForeignKey<Address>(onDelete: Rule.None)
                .WithColumn(nameof(OrderPendingToClosePayment.ShippingAddressId)).AsInt32().Nullable().ForeignKey<Address>(onDelete: Rule.None);
        }

        #endregion
    }
}
