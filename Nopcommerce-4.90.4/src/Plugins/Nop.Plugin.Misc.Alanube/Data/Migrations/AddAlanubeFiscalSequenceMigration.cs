using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Data.Migrations;

/// <summary>Adds the fiscal series table for installations created before this feature.</summary>
[NopMigration("2026/09/22 12:00:01", "Nop.Plugin.Misc.Alanube add fiscal sequence schema", MigrationProcessType.Update)]
public sealed class AddAlanubeFiscalSequenceMigration : Migration
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
        Execute.Sql("""
            IF OBJECT_ID(N'[dbo].[AlanubeFiscalSequence]', N'U') IS NOT NULL
               AND EXISTS
               (
                   SELECT 1
                   FROM sys.indexes
                   WHERE name = N'IX_AlanubeFiscalSequence_Series'
                     AND object_id = OBJECT_ID(N'[dbo].[AlanubeFiscalSequence]')
               )
            BEGIN
                DROP INDEX [IX_AlanubeFiscalSequence_Series] ON [dbo].[AlanubeFiscalSequence];
            END
            """);
    }
}
