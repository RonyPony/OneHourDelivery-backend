using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Models;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Migrations
{
    ///<inheritdoc/>
    [SkipMigrationOnUpdate]
    [NopMigration("2020/08/21 16:47:00", "Widgets.RegionsOnRegisterPage base schema")]
    public class RegionsOnRegisterPageSchemaMigration : AutoReversingMigration
    {

        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of RegionsOnRegisterPageSchemaMigration class
        /// </summary>
        /// <param name="migrationManager">An implementation of <see cref="IMigrationManager"/></param>
        public RegionsOnRegisterPageSchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }
                
        /// <inheritdoc/>
        public override void Up()
        {
            _migrationManager.BuildTable<Region>(Create);
            _migrationManager.BuildTable<CustomerRegion>(Create);
        }
    }
}