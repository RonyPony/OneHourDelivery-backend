using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Models;

namespace Nop.Plugin.Payments.CyberSource.Migrations
{
    /// <summary>
    /// manages plugin date migration and Database Schema and the self-reversal of this same.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/09/06 14:50:00:6455423", "Payments.CyberSource schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// manages plugin schema migrations to database
        /// </summary>
        /// <param name="migrationManager">Migrates the data of the actions of the plugin such as its general information, transactions, order number, the currency that was used etc.</param>
        public SchemaMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        public override void Up()
        {
            _migrationManager.BuildTable<CyberSourceTransactionLog>(Create);
            _migrationManager.BuildTable<CustomerPaymentTokenMapping>(Create);
        }
    }
}