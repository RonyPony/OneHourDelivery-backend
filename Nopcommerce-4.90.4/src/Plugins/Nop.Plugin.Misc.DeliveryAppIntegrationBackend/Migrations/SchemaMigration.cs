using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Migrations;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Migrations
{
    /// <summary>
    /// Represents the migration class for connecting this plugin with the database environment.
    /// </summary>
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
            Create.TableFor<VendorReviewMapping>();
            Create.TableFor<WarehouseUserCreatorMapping>();
            Create.TableFor<VendorWarehouseMapping>();
            Create.TableFor<OrderDeliveryStatusMapping>();
            Create.TableFor<MessengerDeclinedOrderMapping>();
            Create.TableFor<OrderRatingMapping>();
            Create.TableFor<CustomerRatingMapping>();
            Create.TableFor<OrderPendingToClosePayment>();
            Create.TableFor<OrderPendingToClosePaymentItem>();
            Create.TableFor<GoogleDirectionMapping>();
            Create.TableFor<DriverLocationInfoMapping>();
            Create.TableFor<VendorDiscount>();
            Create.TableFor<CustomerFavoriteMapping>();
            Create.TableFor<DriverRatingMapping>();
            Create.TableFor<OrderPaymentCollectionStatus>();
            Create.TableFor<CustomerPendingReviewMapping>();
            Create.TableFor<CustomerDiscountMapping>();

        }
    }
}
