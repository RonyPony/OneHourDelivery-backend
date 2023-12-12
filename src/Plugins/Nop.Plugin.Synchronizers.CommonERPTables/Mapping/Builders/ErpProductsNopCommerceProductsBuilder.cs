using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Mapping.Builders
{
    /// <summary>
    /// Represents a ERP-nopCommerce products mapping entity builder
    /// </summary>
    public class ErpProductsNopCommerceProductsBuilder: NopEntityBuilder<ErpProductsNopCommerceProductsMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(ErpProductsNopCommerceProductsMapping.Id)).AsInt32().Identity()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpProductsNopCommerceProductsMapping),
                    nameof(ErpProductsNopCommerceProductsMapping.ProductId)))
                .AsInt32().ForeignKey<Product>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ErpProductsNopCommerceProductsMapping),
                    nameof(ErpProductsNopCommerceProductsMapping.ErpProductId)))
                .AsString(100).PrimaryKey();
        }

        #endregion
    }
}
