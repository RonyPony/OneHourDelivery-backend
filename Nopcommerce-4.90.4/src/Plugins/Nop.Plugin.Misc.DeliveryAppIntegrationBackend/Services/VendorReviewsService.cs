using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Events;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents the services implementation to interact with vendor reviews.
    /// </summary>
    public sealed class VendorReviewsService : IVendorReviewsService
    {
        #region Fields

        private readonly IRepository<VendorReviewMapping> _vendorReviewMappingRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerRatingService _customerRatingService;

        #endregion

        #region Ctor

        /// <summary>
        ///  Initializes a new instance of <see cref="VendorReviewsService"/>.
        /// </summary>
        /// <param name="vendorReviewMappingRepository">An implementation of <see cref="IRepository{VendorReviewMapping}"/></param>
        /// <param name="eventPublisher">An implementation of <see cref="IEventPublisher"/></param>
        /// <param name="customerRatingService">An implementation of <see cref="ICustomerRatingService"/></param>
        public VendorReviewsService(IRepository<VendorReviewMapping> vendorReviewMappingRepository,
                                    IEventPublisher eventPublisher ,
                                    ICustomerRatingService customerRatingService)
        {
            _vendorReviewMappingRepository = vendorReviewMappingRepository;
            _eventPublisher = eventPublisher;
            _customerRatingService = customerRatingService;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        void IVendorReviewsService.InsertReview(VendorReviewModel vendorReview)
        {
            VendorReviewMapping review = new VendorReviewMapping
            {
                VendorId = vendorReview.VendorId,
                CustomerId = vendorReview.CustomerId,
                Comment = vendorReview.Comment,
                Rating = vendorReview.Rating
            };
     
            if(vendorReview.Rating != 0)
            {
                ValidateReview(review);

                _vendorReviewMappingRepository.Insert(review);

                _eventPublisher.EntityInserted(review);
            }
;

            _customerRatingService
                .ChangeStatusToCustomerPendingReview(new CustomerPendingReviewsModel { 
                   CustomerId = vendorReview.CustomerId,
                   VendorId = vendorReview.VendorId,
                   OrderId = vendorReview.OrderId,
                   PendingReviewStatus = vendorReview.ReviewStatus
                });
        }

        private static void ValidateReview(VendorReviewMapping vendorReview)
        {
            if (vendorReview == null)
            {
                throw new ArgumentException("ReviewCantBeNull");
            }

            if (vendorReview.CustomerId == 0)
            {
                throw new ArgumentException("CustomerIdCantBeZero");
            }

            if (vendorReview.VendorId == 0)
            {
                throw new ArgumentException("VendorIdCantBeZero");
            }

            if (vendorReview.Rating == 0)
            {
                throw new ArgumentException("RatingCantBeZero");
            }
        }

        ///<inheritdoc/>
        IEnumerable<VendorReview> IVendorReviewsService.GetReviewsByVendor(int vendorId)
        {
            IEnumerable<VendorReviewMapping> result =  _vendorReviewMappingRepository.Table.Where(vendorReview => vendorReview.VendorId == vendorId);

            return GetListOfReviewsData(result);
        }

        private IEnumerable<VendorReview> GetListOfReviewsData(IEnumerable<VendorReviewMapping> vendorReviews)
        {
            var reviews = new List<VendorReview>();

            foreach (VendorReviewMapping review in vendorReviews)
            {
                VendorReview categoryData = review.ToModel<VendorReview>();
                reviews.Add(categoryData);
            }

            return reviews;
        }

        ///<inheritdoc/>
        void IVendorReviewsService.UpdateReview(VendorReviewMapping vendorReview)
        {
            VendorReviewMapping foundVendorReview = _vendorReviewMappingRepository.Table.FirstOrDefault(review => review.VendorId == vendorReview.VendorId && review.CustomerId == vendorReview.CustomerId);

            if (foundVendorReview == null)
            {
                throw new ArgumentException("ReviewToUpdateDoesNotExist");
            }

            foundVendorReview.Rating = vendorReview.Rating;
            foundVendorReview.Comment = vendorReview.Comment;

            _vendorReviewMappingRepository.Update(foundVendorReview);

            _eventPublisher.EntityUpdated(foundVendorReview);
        }

        #endregion
    }
}
