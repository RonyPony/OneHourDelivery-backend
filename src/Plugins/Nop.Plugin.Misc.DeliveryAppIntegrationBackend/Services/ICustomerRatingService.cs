using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Payments.CyberSource.Domains;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the services to interact with Customer rating.
    /// </summary>
    public interface ICustomerRatingService
    {
        /// <summary>
        /// Inserts a new custom rating
        /// </summary>
        /// <param name="customerRating">a request to CustomerRatingMapping to set the data.</param>
        void InsertCustomerRating(CustomerRatingMappingRequest customerRating);

        /// <summary>
        ///  Retrieves the customer pending reviews
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>a list of customer pending review of specific customer</returns>
        IList<CustomerPendingReviewsModel> GetCustomerPendingReviews(int customerId);

        /// <summary>
        /// Change the review status for a specific customer.
        /// </summary>
        /// <param name="pendingReview">Customer's pending review</param>
        void ChangeStatusToCustomerPendingReview(CustomerPendingReviewsModel pendingReview);

        /// <summary>
        /// Retrieves a list of payment token of the specific customer.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>a list of customer payment token</returns>
        IList<CustomerPaymentTokenModel> GetCustomerCybersourceTokens(int customerId);
    }
}
