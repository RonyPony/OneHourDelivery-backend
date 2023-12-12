using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Payments.CardNet.Domains;

namespace Nop.Plugin.Payments.CardNet.Migrations
{
    [SkipMigrationOnUpdate]
    [NopMigration("2020/08/21 16:47:00", "Payments.CardNet base schema")]
    public class CardNetSchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        public CardNetSchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }
        public override void Up()
        {
            _migrationManager.BuildTable<CardNetTransactionLog>(Create);
        }
    }
}
