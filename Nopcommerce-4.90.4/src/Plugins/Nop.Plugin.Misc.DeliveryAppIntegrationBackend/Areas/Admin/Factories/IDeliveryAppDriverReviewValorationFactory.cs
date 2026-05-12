using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents a contract for Delivery app driver review valoration factory.
    /// </summary>
    public interface IDeliveryAppDriverReviewValorationFactory
    {
        /// <summary>
        /// Retrives a list of vendor reviews valoration by the driver
        /// </summary>
        /// <returns>a list of deliveryAppVendorReviewList</returns>
        DeliveryAppVendorReviewList GetVendorsReviews();

        /// <summary>
        /// Retrives a list of customer reviews valoration by the driver
        /// </summary>
        /// <returns>a list of deliveryAppCustomerReviewList</returns>
        DeliveryAppCustomerReviewList GetCustomerReviews();
    }
}
