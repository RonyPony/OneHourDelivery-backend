using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.Banrural.Models;

namespace Nop.Plugin.Payments.Banrural.Migrations
{
    /// <summary>
    /// Represent the plugin's Schema migration.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/07/03 11:27:00:6455423", "Payments.Banrural schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of SchemaMigration class.
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>
        public SchemaMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up() => _migrationManager.BuildTable<BanruralTransactionLog>(Create);
    }
}