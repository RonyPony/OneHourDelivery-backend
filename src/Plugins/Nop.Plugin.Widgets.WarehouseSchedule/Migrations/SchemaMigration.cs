using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/06/16 12:51:00", "Widgets.WarehouseSchedule schema. Added Day and Warehouse_Schedule_Mapping tables")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;
        
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaMigration"/>.
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>.</param>
        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="SchemaMigration"/>.
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>.</param>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<Day>(Create);
            _migrationManager.BuildTable<WarehouseScheduleMapping>(Create);
        }
    }
}
