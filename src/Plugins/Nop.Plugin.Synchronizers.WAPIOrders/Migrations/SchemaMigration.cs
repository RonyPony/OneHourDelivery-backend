using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Synchronizers.WAPIOrders.Domains;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/12/16 00:00:00", "Nop.Plugin.Synchronizers.WAPIOrders schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SchemaMigration"/>.
        /// </summary>
        /// <param name="migrationManager">Represents an implementation of <see cref="IMigrationManager"/>.</param>
        public SchemaMigration(IMigrationManager migrationManager)
            => _migrationManager = migrationManager;

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
            => _migrationManager.BuildTable<OrderTransactionCodeMapping>(Create);
    }
}
