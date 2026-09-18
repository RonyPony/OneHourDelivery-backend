using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Mapping.Builders;

public sealed class AlanubeDocumentLogBuilder : NopEntityBuilder<AlanubeDocumentLog>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table.WithColumn(nameof(AlanubeDocumentLog.Operation)).AsString(100).NotNullable();
        table.WithColumn(nameof(AlanubeDocumentLog.Endpoint)).AsString(500).NotNullable();
        table.WithColumn(nameof(AlanubeDocumentLog.HttpMethod)).AsString(10).NotNullable();
        table.WithColumn(nameof(AlanubeDocumentLog.RequestJson)).AsString(int.MaxValue).Nullable();
        table.WithColumn(nameof(AlanubeDocumentLog.ResponseJson)).AsString(int.MaxValue).Nullable();
        table.WithColumn(nameof(AlanubeDocumentLog.ErrorCode)).AsString(100).Nullable();
        table.WithColumn(nameof(AlanubeDocumentLog.ErrorMessage)).AsString(2000).Nullable();
    }
}
