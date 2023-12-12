using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Mapping.Builders
{
    /// <summary>
    /// Represents a customer-SAP customer mapping entity builder
    /// </summary>
    public sealed class CustomerSapCustomerMappingBuilder: NopEntityBuilder<CustomerSapCustomerMapping>
    {
        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerSapCustomerMapping), nameof(CustomerSapCustomerMapping.CustomerId)))
                .AsInt32().ForeignKey<Customer>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerSapCustomerMapping), nameof(CustomerSapCustomerMapping.SapCustomerId)))
                .AsString(50).PrimaryKey();
        }
    }
}
