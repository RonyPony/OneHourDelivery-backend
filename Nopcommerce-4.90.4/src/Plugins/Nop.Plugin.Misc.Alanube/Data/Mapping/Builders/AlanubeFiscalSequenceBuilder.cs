using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Mapping.Builders;

public sealed class AlanubeFiscalSequenceBuilder : NopEntityBuilder<AlanubeFiscalSequence>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(AlanubeFiscalSequence.CompanyId)).AsString(26).NotNullable();
        table.WithColumn(nameof(AlanubeFiscalSequence.OfficeId)).AsString(26).NotNullable();
        table.WithColumn(nameof(AlanubeFiscalSequence.BillingPoint)).AsString(3).NotNullable();
        table.WithColumn(nameof(AlanubeFiscalSequence.DocumentType)).AsString(2).NotNullable();
        table.WithColumn(nameof(AlanubeFiscalSequence.LastNumber)).AsInt64().NotNullable();
    }
}
