using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="CustomerPendingReviewMapping"/> entity builder.
    /// </summary>
    public class CustomerPendingReviewMappingBuilder : NopEntityBuilder<CustomerPendingReviewMapping>
    {
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerPendingReviewMapping), nameof(CustomerPendingReviewMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerPendingReviewMapping), nameof(CustomerPendingReviewMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerPendingReviewMapping), nameof(CustomerPendingReviewMapping.VendorId)))
                    .AsInt32().ForeignKey<Vendor>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerPendingReviewMapping), nameof(CustomerPendingReviewMapping.PendingReviewStatus)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerPendingReviewMapping), nameof(CustomerPendingReviewMapping.CreatedOnUtc)))
                    .AsDateTime();
        }
    }
}
