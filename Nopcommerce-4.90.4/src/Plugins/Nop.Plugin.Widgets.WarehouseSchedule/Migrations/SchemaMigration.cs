using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [NopMigration("2021/06/16 12:51:00", "Widgets.WarehouseSchedule schema. Added Day and Warehouse_Schedule_Mapping tables", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            Create.TableFor<Day>();
            Create.TableFor<WarehouseScheduleMapping>();
        }
    }
}
