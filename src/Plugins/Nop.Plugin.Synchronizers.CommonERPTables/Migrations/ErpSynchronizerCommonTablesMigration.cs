using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Synchronizers.CommonERPTables.Domains;

namespace Nop.Plugin.Synchronizers.CommonERPTables.Migrations
{
    /// <summary>
    /// Represents the schema migration for working with the entities needed for this plugin.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/01/14 10:05:00", "Nop.Plugin.Synchronizers.CommonERPTables schema")]
    public class ErpSynchronizerCommonTablesMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of the ErpSynchronizerCommonTablesMigration class.
        /// </summary>
        /// <param name="migrationManager"></param>
        public ErpSynchronizerCommonTablesMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<ErpCustomersNopCommerceCustomersMapping>(Create);
            _migrationManager.BuildTable<ErpOrdersNopCommerceOrdersMapping>(Create);
            _migrationManager.BuildTable<ErpProductsNopCommerceProductsMapping>(Create);
        }
    }
}
