using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.BAC.Domains;

namespace Nop.Plugin.Payments.BAC.Migrations
{
    /// <summary>
    /// Represents the schema migration for working with the entities needed for this plugin.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/10/30 12:00:00", "Payments.BAC schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of the SchemaMigration class.
        /// </summary>
        /// <param name="migrationManager"></param>
        public SchemaMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up() => _migrationManager.BuildTable<BacTransactionLog>(Create);
    }
}
