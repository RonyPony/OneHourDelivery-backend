using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

[NopMigration("2026/09/17 13:00:00", "Nop.Plugin.Misc.Alanube catalog schema", MigrationProcessType.Installation)]
public sealed class AlanubeCatalogMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<AlanubeCatalogEntry>();
        Create.Index("IX_AlanubeCatalogEntry_Type_Code")
            .OnTable(nameof(AlanubeCatalogEntry))
            .OnColumn(nameof(AlanubeCatalogEntry.CatalogType)).Ascending()
            .OnColumn(nameof(AlanubeCatalogEntry.ExternalCode)).Ascending()
            .WithOptions().Unique();
    }
}
