using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Mapping.Builders;

public sealed class AlanubeDocumentBuilder : NopEntityBuilder<AlanubeDocument>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(AlanubeDocument.AlanubeId)).AsString(26).Nullable();
        table.WithColumn(nameof(AlanubeDocument.DocumentType)).AsInt32().NotNullable();
        table.WithColumn(nameof(AlanubeDocument.CompanyId)).AsString(26).Nullable();
        table.WithColumn(nameof(AlanubeDocument.OfficeId)).AsString(26).Nullable();
        table.WithColumn(nameof(AlanubeDocument.CurrencyCode)).AsString(3).Nullable();
        table.WithColumn(nameof(AlanubeDocument.Subtotal)).AsDecimal(18, 4).NotNullable();
        table.WithColumn(nameof(AlanubeDocument.Discount)).AsDecimal(18, 4).NotNullable();
        table.WithColumn(nameof(AlanubeDocument.Tax)).AsDecimal(18, 4).NotNullable();
        table.WithColumn(nameof(AlanubeDocument.Total)).AsDecimal(18, 4).NotNullable();
    }
}
