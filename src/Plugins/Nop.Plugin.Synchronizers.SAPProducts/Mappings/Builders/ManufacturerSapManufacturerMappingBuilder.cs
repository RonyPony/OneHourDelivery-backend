using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;

namespace Nop.Plugin.Synchronizers.SAPProducts.Mappings.Builders
{
    /// <summary>
    /// Represents an entity builder for manufacturers and SAP manufacturers mappings.
    /// </summary>
    public sealed class ManufacturerSapManufacturerMappingBuilder : NopEntityBuilder<ManufacturerSapManufacturerMapping>
    {
        /// <summary>
        /// Applies entity configuration for <see cref="ManufacturerSapManufacturerMapping"/>.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ManufacturerSapManufacturerMapping), nameof(ManufacturerSapManufacturerMapping.ManufacturerId)))
                .AsInt32().ForeignKey<Manufacturer>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(ManufacturerSapManufacturerMapping), nameof(ManufacturerSapManufacturerMapping.SapManufacturerCode)))
                .AsInt32().PrimaryKey();
        }
    }
}
