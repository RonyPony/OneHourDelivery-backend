using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.AzulPaymentPage.Domains;

namespace Nop.Plugin.Payments.AzulPaymentPage.Migrations
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/11/19 11:27:00", "Payments.AzulPaymentPage base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up() => _migrationManager.BuildTable<AzulPaymentTransactionLog>(Create);
    }
}
