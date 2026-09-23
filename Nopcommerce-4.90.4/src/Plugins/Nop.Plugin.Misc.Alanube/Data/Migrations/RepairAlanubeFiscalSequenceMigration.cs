using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

/// <summary>Repairs the fiscal sequence schema when a previous installation was partial.</summary>
[NopMigration("2026/09/23 12:00:00", "Nop.Plugin.Misc.Alanube repair fiscal sequence schema", MigrationProcessType.Update)]
public sealed class RepairAlanubeFiscalSequenceMigration : Migration
{
    public override void Up()
    {
        if (!Schema.Table(nameof(AlanubeFiscalSequence)).Exists())
            Create.TableFor<AlanubeFiscalSequence>();

        if (!Schema.Table(nameof(AlanubeFiscalSequence)).Index("IX_AlanubeFiscalSequence_Series").Exists())
            Create.Index("IX_AlanubeFiscalSequence_Series")
                .OnTable(nameof(AlanubeFiscalSequence))
                .OnColumn(nameof(AlanubeFiscalSequence.CompanyId)).Ascending()
                .OnColumn(nameof(AlanubeFiscalSequence.OfficeId)).Ascending()
                .OnColumn(nameof(AlanubeFiscalSequence.BillingPoint)).Ascending()
                .OnColumn(nameof(AlanubeFiscalSequence.DocumentType)).Ascending()
                .WithOptions().Unique();
    }

    public override void Down()
    {
        if (Schema.Table(nameof(AlanubeFiscalSequence)).Exists())
            Delete.Table(nameof(AlanubeFiscalSequence));
    }
}
