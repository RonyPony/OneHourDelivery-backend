using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Synchronizers.SAPCustomers.Domains;

namespace Nop.Plugin.Synchronizers.SAPCustomers.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/09/22 14:51:00", "Synchronizers.SAPCustomers base schema")]
    public sealed class SapCustomerSynchronizerMigration: AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of SapCustomerSynchronizerMigration class with the value indicated as parameter.
        /// </summary>
        /// <param name="migrationManager">Represents an implementation of <see cref="IMigrationManager"/>.</param>
        public SapCustomerSynchronizerMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Executes the BuildTable method when running this migration.
        /// </summary>
        public override void Up() => _migrationManager.BuildTable<CustomerSapCustomerMapping>(Create);
    }
}
