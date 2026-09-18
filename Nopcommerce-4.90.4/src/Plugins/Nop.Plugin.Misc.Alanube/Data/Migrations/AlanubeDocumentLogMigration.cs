using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

[NopMigration("2026/09/17 12:30:00", "Nop.Plugin.Misc.Alanube document log schema", MigrationProcessType.Installation)]
public sealed class AlanubeDocumentLogMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<AlanubeDocumentLog>();
    }
}
