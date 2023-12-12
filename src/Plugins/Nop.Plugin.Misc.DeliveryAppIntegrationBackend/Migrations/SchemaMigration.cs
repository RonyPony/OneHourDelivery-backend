using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
    [SkipMigrationOnUpdate]
    [NopMigration("2021/08/24 00:00:00", "Nop.Plugin.Misc.DeliveryAppIntegrationBackend schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        private readonly IMigrationManager _migrationManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SchemaMigration"/>.
        /// </summary>
        /// <param name="migrationManager">Represents an implementation of <see cref="IMigrationManager"/>.</param>
        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<VendorReviewMapping>(Create);
            _migrationManager.BuildTable<WarehouseUserCreatorMapping>(Create);
            _migrationManager.BuildTable<VendorWarehouseMapping>(Create);
            _migrationManager.BuildTable<OrderDeliveryStatusMapping>(Create);
            _migrationManager.BuildTable<MessengerDeclinedOrderMapping>(Create);
            _migrationManager.BuildTable<OrderRatingMapping>(Create);
            _migrationManager.BuildTable<CustomerRatingMapping>(Create);
            _migrationManager.BuildTable<OrderPendingToClosePayment>(Create);
            _migrationManager.BuildTable<OrderPendingToClosePaymentItem>(Create);
            _migrationManager.BuildTable<GoogleDirectionMapping>(Create);
            _migrationManager.BuildTable<DriverLocationInfoMapping>(Create);
            _migrationManager.BuildTable<VendorDiscount>(Create);
            _migrationManager.BuildTable<CustomerFavoriteMapping>(Create);
            _migrationManager.BuildTable<DriverRatingMapping>(Create);
            _migrationManager.BuildTable<OrderPaymentCollectionStatus>(Create);
            _migrationManager.BuildTable<CustomerPendingReviewMapping>(Create);
            _migrationManager.BuildTable<CustomerDiscountMapping>(Create);

        }
    }
}
