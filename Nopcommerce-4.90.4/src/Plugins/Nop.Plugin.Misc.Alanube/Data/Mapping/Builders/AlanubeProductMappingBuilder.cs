using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Mapping.Builders;

public sealed class AlanubeProductMappingBuilder : NopEntityBuilder<AlanubeProductMapping>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(AlanubeProductMapping.GoodsServiceCode)).AsString(100).NotNullable();
        table.WithColumn(nameof(AlanubeProductMapping.MeasurementUnitCode)).AsString(100).NotNullable();
        table.WithColumn(nameof(AlanubeProductMapping.TaxCode)).AsString(100).Nullable();
        table.WithColumn(nameof(AlanubeProductMapping.DescriptionOverride)).AsString(1000).Nullable();
    }
}
