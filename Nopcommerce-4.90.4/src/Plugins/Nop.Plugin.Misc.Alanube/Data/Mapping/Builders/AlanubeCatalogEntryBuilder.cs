using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Mapping.Builders;

public sealed class AlanubeCatalogEntryBuilder : NopEntityBuilder<AlanubeCatalogEntry>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(AlanubeCatalogEntry.CatalogType)).AsInt32().NotNullable();
        table.WithColumn(nameof(AlanubeCatalogEntry.ExternalCode)).AsString(100).NotNullable();
        table.WithColumn(nameof(AlanubeCatalogEntry.Description)).AsString(1000).NotNullable();
        table.WithColumn(nameof(AlanubeCatalogEntry.ParentCode)).AsString(100).Nullable();
    }
}
