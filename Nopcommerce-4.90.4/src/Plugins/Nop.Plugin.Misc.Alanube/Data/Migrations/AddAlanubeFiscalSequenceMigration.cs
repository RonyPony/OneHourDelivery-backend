using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

/// <summary>Adds the fiscal series table for installations created before this feature.</summary>
[NopMigration("2026/09/22 12:00:01", "Nop.Plugin.Misc.Alanube add fiscal sequence schema", MigrationProcessType.Update)]
public sealed class AddAlanubeFiscalSequenceMigration : AutoReversingMigration
{
    public override void Up()
    {
        Create.TableFor<AlanubeFiscalSequence>();
        Create.Index("IX_AlanubeFiscalSequence_Series")
            .OnTable(nameof(AlanubeFiscalSequence))
            .OnColumn(nameof(AlanubeFiscalSequence.CompanyId)).Ascending()
            .OnColumn(nameof(AlanubeFiscalSequence.OfficeId)).Ascending()
            .OnColumn(nameof(AlanubeFiscalSequence.BillingPoint)).Ascending()
            .OnColumn(nameof(AlanubeFiscalSequence.DocumentType)).Ascending()
            .WithOptions().Unique();
    }
}
