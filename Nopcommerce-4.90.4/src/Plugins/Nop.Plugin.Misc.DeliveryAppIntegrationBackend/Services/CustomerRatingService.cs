using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Payments.CyberSource.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the customer rating services.
    /// </summary>
    public class CustomerRatingService : ICustomerRatingService
    {
        #region Fields
        private readonly IRepository<CustomerRatingMapping> _customerRatingMappingRepository;
        private readonly IRepository<CustomerPendingReviewMapping> _customerPendingReviewMappingRepository;
        private readonly IRepository<CustomerPaymentTokenMapping> _customerPaymentTokenMapping;
        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="CustomerRatingService"/>.
        /// </summary>
        /// /// <param name="customerRatingMappingRepository">An implementation of <see cref="IRepository{CustomerRatingMapping}"/>.</param>
        /// /// <param name="customerPendingReviewMappingRepository">An implementation of <see cref="IRepository{CustomerPendingReviewMapping}"/>.</param>
        public CustomerRatingService(IRepository<CustomerRatingMapping> customerRatingMappingRepository,
            IRepository<CustomerPendingReviewMapping> customerPendingReviewMappingRepository,
            IRepository<CustomerPaymentTokenMapping> customerPaymentTokenMapping) {
            _customerRatingMappingRepository = customerRatingMappingRepository;
            _customerPendingReviewMappingRepository = customerPendingReviewMappingRepository;
            _customerPaymentTokenMapping = customerPaymentTokenMapping;
        }

        /// <inheritdoc/>
        public void ChangeStatusToCustomerPendingReview(CustomerPendingReviewsModel pendingReview)
        {
            if (pendingReview is null)
                throw new ArgumentException("InvalidRequest");
         
            CustomerPendingReviewMapping customerReview = _customerPendingReviewMappingRepository.Table
                .FirstOrDefault(review => review.CustomerId == pendingReview.CustomerId 
                && review.VendorId == pendingReview.VendorId && review.OrderId == pendingReview.OrderId);

            if (customerReview is null)
                throw new ArgumentException("CustomerReviewNotFound");

            customerReview.PendingReviewStatus = pendingReview.PendingReviewStatus;
            customerReview.CreatedOnUtc = DateTime.UtcNow;

            _customerPendingReviewMappingRepository.Update(customerReview);
        }

        /// <inheritdoc/>
        public IList<CustomerPaymentTokenModel> GetCustomerCybersourceTokens(int customerId)
        {
            if (customerId == 0)
                throw new ArgumentException("CustomerNotFound");

            return _customerPaymentTokenMapping.Table
                .Where(cybersource => cybersource.CustomerId == customerId)
                .Select(select => new CustomerPaymentTokenModel { 
                   CustomerId = select.CustomerId,
                   Token = select.Token,
                   CardType = select.CardType,
                   CardLastFourDigits = select.CardLastFourDigits,
                   CardExpirationDate = select.CardExpirationDate,
                   IsDefaultPaymentMethod = select.IsDefaultPaymentMethod
                }).ToList();
        }

        /// <inheritdoc/>
        public IList<CustomerPendingReviewsModel> GetCustomerPendingReviews(int customerId)
        {
            if (customerId == 0)
                throw new ArgumentException("InvalidCustomerId");

            return _customerPendingReviewMappingRepository.Table
                .Where(review => review.CustomerId == customerId)
                .OrderByDescending(review => review.CreatedOnUtc)
                .Select(select => new CustomerPendingReviewsModel { 
                     CustomerId = select.CustomerId,
                     OrderId = select.OrderId,
                     VendorId = select.VendorId,
                     PendingReviewStatus = select.PendingReviewStatus
                  })
                .ToList();
        }
        #endregion

        /// <inheritdoc/>
        public void InsertCustomerRating(CustomerRatingMappingRequest customerRating)
        {
            if (customerRating is null) throw new ArgumentException("customerRatingCantBeNull");

            if (customerRating.CreatorCustomerId == 0) throw new ArgumentException("InvalidCreatorCustomerId");

            if (customerRating.RatedCustomerId == 0) throw new ArgumentException("InvalidRatedCustomerId");


            var setDeliveryRatingMapping = new CustomerRatingMapping
            {
                CreatorCustomerId = customerRating.CreatorCustomerId,
                RatedCustomerId = customerRating.RatedCustomerId,
                Rate = customerRating.Rate,
                Comment = customerRating.Comment,
                CreateOnUtc = DateTime.UtcNow
            };

            _customerRatingMappingRepository.Insert(setDeliveryRatingMapping);
        }
    }
}
