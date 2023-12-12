using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Synchronizers.SAPProducts.Domains;

namespace Nop.Plugin.Synchronizers.SAPProducts.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/09/24 16:31:00", "Synchronizers.SAPProducts base schema. Product_SapItem_Mappings, Category_SapItemGroup_Mapping and Manufacturer_SapManufacturer_Mapping")]
    public sealed class SapProductSynchronizerMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SapProductSynchronizerMigration"/>.
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>.</param>
        public SapProductSynchronizerMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Executes the BuildTable method when running this migration.
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<ProductSapItemMapping>(Create);
            _migrationManager.BuildTable<CategorySapItemGroupMapping>(Create);
            _migrationManager.BuildTable<ManufacturerSapManufacturerMapping>(Create);
        }
    }
}