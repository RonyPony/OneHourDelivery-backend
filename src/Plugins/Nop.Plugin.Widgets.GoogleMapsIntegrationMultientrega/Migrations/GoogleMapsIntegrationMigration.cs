using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/04/26 00:00:00", "Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega schema")]
    public sealed class GoogleMapsIntegrationMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsIntegrationMigration"/>.
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/>.</param>
        public GoogleMapsIntegrationMigration(IMigrationManager migrationManager) => _migrationManager = migrationManager;

        /// <summary>
        /// Collects the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<AddressGeoCoordinatesMapping>(Create);
            _migrationManager.BuildTable<MultientregaProvinceMapping>(Create);
            _migrationManager.BuildTable<MultientregaDistrictMapping>(Create);
            _migrationManager.BuildTable<MultientregaTownshipMapping>(Create);
            _migrationManager.BuildTable<MultientregaNeighborhoodMapping>(Create);
            _migrationManager.BuildTable<MultientregaProvinceStateProvinceMapping>(Create);
        }
    }
}
