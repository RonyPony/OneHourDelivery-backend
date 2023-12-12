using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Mapping.Builders
{
    /// <summary>
    /// Represents a ERP-nopCommerce customer mapping entity builder
    /// </summary>
    public class ErpCustomersNopCommerceCustomersBuilder : NopEntityBuilder<ErpCustomersNopCommerceCustomersMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(ErpCustomersNopCommerceCustomersMapping.Id)).AsInt32().Identity()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpCustomersNopCommerceCustomersMapping),
                    nameof(ErpCustomersNopCommerceCustomersMapping.CustomerId)))
                .AsInt32().ForeignKey<Customer>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpCustomersNopCommerceCustomersMapping),
                    nameof(ErpCustomersNopCommerceCustomersMapping.ErpCustomerId)))
                .AsString(150).PrimaryKey();
        }

        #endregion
    }
}