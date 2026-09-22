using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

[NopMigration("2026/09/22 12:00:00", "Nop.Plugin.Misc.Alanube fiscal sequence schema", MigrationProcessType.Installation)]
public sealed class AlanubeFiscalSequenceMigration : AutoReversingMigration
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
