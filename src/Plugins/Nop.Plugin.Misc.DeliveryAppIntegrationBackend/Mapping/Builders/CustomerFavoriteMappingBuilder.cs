using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Data.Extensions;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    public class CustomerFavoriteMappingBuilder : NopEntityBuilder<CustomerFavoriteMapping>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerFavoriteMapping), nameof(CustomerFavoriteMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerFavoriteMapping), nameof(CustomerFavoriteMapping.VendorId)))
                    .AsInt32().ForeignKey<Vendor>()    
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerFavoriteMapping), nameof(CustomerFavoriteMapping.CreatedOnUtc)))
                    .AsDateTime().NotNullable();
        }
    }
}
