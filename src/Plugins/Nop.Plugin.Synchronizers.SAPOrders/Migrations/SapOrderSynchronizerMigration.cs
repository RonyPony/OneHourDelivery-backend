using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Synchronizers.SAPOrders.Domains;

namespace Nop.Plugin.Synchronizers.SAPOrders.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/09/30 11:44:00", "Synchronizers.SAPOrders base schema")]
    public sealed class SapOrderSynchronizerMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of SapOrderSynchronizerMigration class with the value indicated as parameter.
        /// </summary>
        /// <param name="migrationManager">Represents an implementation of <see cref="IMigrationManager"/>.</param>
        public SapOrderSynchronizerMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Executes the BuildTable method when running this migration.
        /// </summary>
        public override void Up() => _migrationManager.BuildTable<OrderSapOrderMapping>(Create);
    }
}
