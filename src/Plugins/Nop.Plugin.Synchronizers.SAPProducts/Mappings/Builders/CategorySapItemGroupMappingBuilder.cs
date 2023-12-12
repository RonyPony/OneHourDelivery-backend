using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings.Builders
{
    /// <summary>
    /// Represents an entity builder for categories and SAP item's groups mappings.
    /// </summary>
    public sealed class CategorySapItemGroupMappingBuilder : NopEntityBuilder<CategorySapItemGroupMapping>
    {
        /// <summary>
        /// Applies entity configuration for <see cref="CategorySapItemGroupMapping"/>.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CategorySapItemGroupMapping), nameof(CategorySapItemGroupMapping.CategoryId)))
                .AsInt32().ForeignKey<Category>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CategorySapItemGroupMapping), nameof(CategorySapItemGroupMapping.ItemGroupNumber)))
                .AsInt32().PrimaryKey();
        }
    }
}
