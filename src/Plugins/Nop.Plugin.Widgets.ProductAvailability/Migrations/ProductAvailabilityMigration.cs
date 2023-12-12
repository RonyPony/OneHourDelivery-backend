using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.ProductAvailability.Domains;

namespace Nop.Plugin.Widgets.ProductAvailability.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/02/04 10:47:00", "Widgets.ProductAvailability base schema. Warehouse_PickupPoint_Mapping")]
    public sealed class ProductAvailabilityMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of <see cref="ProductAvailabilityMigration"/>.
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>.</param>
        public ProductAvailabilityMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Executes the BuildTable method when running this migration.
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<WarehousePickupPointMapping>(Create);
        }
    }
}
