using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Mapping.Builders
{
    /// <summary>
    /// Represents a ERP-nopCommerce order mapping entity builder
    /// </summary>
    public class ErpOrdersNopCommerceOrdersBuilder: NopEntityBuilder<ErpOrdersNopCommerceOrdersMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(ErpOrdersNopCommerceOrdersMapping.Id)).AsInt32().Identity()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpOrdersNopCommerceOrdersMapping),
                    nameof(ErpOrdersNopCommerceOrdersMapping.OrderId)))
                .AsInt32().ForeignKey<Order>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpOrdersNopCommerceOrdersMapping),
                    nameof(ErpOrdersNopCommerceOrdersMapping.ErpOrderId)))
                .AsString(150).PrimaryKey();
        }

        #endregion
    }
}
