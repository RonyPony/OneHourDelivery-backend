using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [NopMigration("2020/11/23 00:00:00", "Nop.Plugin.Widgets.GoogleMapsIntegration schema", MigrationProcessType.Installation)]
    public sealed class GoogleMapsIntegrationMigration : AutoReversingMigration
    {
        /// <summary>
        /// Collects the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            Create.TableFor<AddressGeoCoordinatesMapping>();
        }
    }
}
