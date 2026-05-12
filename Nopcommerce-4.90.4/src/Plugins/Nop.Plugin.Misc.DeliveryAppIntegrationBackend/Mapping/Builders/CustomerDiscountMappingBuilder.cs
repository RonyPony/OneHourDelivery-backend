using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Data.Extensions;
using System.Data;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    public class CustomerDiscountMappingBuilder : NopEntityBuilder<CustomerDiscountMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerDiscountMapping), nameof(CustomerDiscountMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerDiscountMapping), nameof(CustomerDiscountMapping.DiscountId)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerDiscountMapping), nameof(CustomerDiscountMapping.CreatedAtUtc)))
                    .AsDateTime().NotNullable();
        }
    }
}
