using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings.Builders
{
    /// <summary>
    /// Represents an entity builder for products and SAP items mappings.
    /// </summary>
    public sealed class ProductSapItemMappingBuilder : NopEntityBuilder<ProductSapItemMapping>
    {
        /// <summary>
        /// Applies entity configuration for <see cref="ProductSapItemMapping"/>.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ProductSapItemMapping), nameof(ProductSapItemMapping.ProductId)))
                .AsInt32().ForeignKey<Product>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ProductSapItemMapping), nameof(ProductSapItemMapping.SapItemCode)))
                .AsString(100).PrimaryKey();
        }
    }
}
