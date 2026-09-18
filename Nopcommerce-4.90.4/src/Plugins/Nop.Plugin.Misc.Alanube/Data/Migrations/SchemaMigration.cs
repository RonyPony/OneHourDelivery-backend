using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

[NopMigration("2026/09/17 12:00:00", "Nop.Plugin.Misc.Alanube base schema", MigrationProcessType.Installation)]
public sealed class SchemaMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<AlanubeDocument>();
        Create.Index("IX_AlanubeDocument_Order_DocumentType_Version")
            .OnTable(nameof(AlanubeDocument))
            .OnColumn(nameof(AlanubeDocument.OrderId)).Ascending()
            .OnColumn(nameof(AlanubeDocument.DocumentType)).Ascending()
            .OnColumn(nameof(AlanubeDocument.Version)).Ascending()
            .WithOptions().Unique();
    }
}
