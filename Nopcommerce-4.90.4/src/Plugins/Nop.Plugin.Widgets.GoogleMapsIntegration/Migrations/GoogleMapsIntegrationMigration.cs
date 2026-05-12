using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/11/23 00:00:00", "Nop.Plugin.Widgets.GoogleMapsIntegration schema")]
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
        }
    }
}
