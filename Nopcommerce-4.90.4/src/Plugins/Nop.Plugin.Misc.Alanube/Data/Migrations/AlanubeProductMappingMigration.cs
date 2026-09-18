using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

[NopMigration("2026/09/17 14:00:00", "Nop.Plugin.Misc.Alanube product mapping schema", MigrationProcessType.Installation)]
public sealed class AlanubeProductMappingMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<AlanubeProductMapping>();
        Create.Index("IX_AlanubeProductMapping_ProductId").OnTable(nameof(AlanubeProductMapping)).OnColumn(nameof(AlanubeProductMapping.ProductId)).Ascending().WithOptions().Unique();
    }
}
