using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Services.Customers;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents a factory for delivery App Review Valoration .
    /// </summary>
    public class DeliveryAppDriverReviewValorationFactory : IDeliveryAppDriverReviewValorationFactory
    {
        #region Fields

        private readonly IRepository<DriverRatingMapping> _driverRatingMappingRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Vendor> _vendorRepository;

        #endregion


        #region Ctors

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppDriverReviewValorationFactory"/>.
        /// </summary>
        /// <param name="driverRatingMappingRepository">An implementation of <see cref="IRepository{DriverRatingMapping}"/>.</param>
        /// <param name="customerService">An implementation of <see cref="CustomerService"/>.</param>
        /// <param name="vendorRepository">An implementation of <see cref="IRepository{Vendor}"/>.</param>
        public DeliveryAppDriverReviewValorationFactory(IRepository<DriverRatingMapping> driverRatingMappingRepository ,
            ICustomerService customerService , IRepository<Vendor> vendorRepository)
        {
            _driverRatingMappingRepository = driverRatingMappingRepository;
            _customerService = customerService;
            _vendorRepository = vendorRepository;
        }

        #endregion

        #region Methods
        /// <inheritdoc />
        public DeliveryAppCustomerReviewList GetCustomerReviews()
        {
            const float porcent = 100f;

            IList<DeliveryAppCustomerReview> customerReviews = _driverRatingMappingRepository.Table
                .Where(review => review.RatingType == DriverRatingType.IsClient)
                .GroupBy(group => group.CustomerId)
                .Select(select => new {
                    name = GetCustomerName(select.Key),
                    likesCount = (float)select.Count(c => c.Rating == DriverRating.Like),
                    dislikesCount = (float)select.Count(c => c.Rating == DriverRating.Dislike)
                }).Select(select => new DeliveryAppCustomerReview {
                    CustomerName = select.name,
                    LikeCount = (int)select.likesCount,
                    DislikeCount = (int)select.dislikesCount,
                    ApprovalPorcent = (select.likesCount / (select.likesCount + select.dislikesCount)) * porcent
                }).ToList() ;

            PagedList<DeliveryAppCustomerReview> customerPageList = new PagedList<DeliveryAppCustomerReview>(customerReviews, 0, int.MaxValue);

            DeliveryAppCustomerReviewList reviewList = new DeliveryAppCustomerReviewList()
                .PrepareToGrid(null , customerPageList , () => customerPageList); 

            return reviewList;
        }

        /// <inheritdoc />
        public DeliveryAppVendorReviewList GetVendorsReviews()
        {
            const float porcent = 100f;

            IList< DeliveryAppVendorReviewValoration> vendorReviews = _driverRatingMappingRepository
               .Table.Where(driver => driver.RatingType == DriverRatingType.IsVendor)
               .GroupBy(group => group.CustomerId).Select(select => new
               {
                   name = GetVendorName(select.Key),
                   likesCount = (float) select.Count(c => c.Rating == DriverRating.Like),
                   dislikesCount = (float) select.Count(c => c.Rating == DriverRating.Dislike)
               }).Select(select => new DeliveryAppVendorReviewValoration
               {
                   VendorName = select.name,
                   LikeCount = (int)select.likesCount,
                   DislikeCount = (int)select.dislikesCount,
                   ApprovalPorcent = (select.likesCount / (select.likesCount + select.dislikesCount)) * porcent
               }).ToList();

            PagedList<DeliveryAppVendorReviewValoration> reviewPages = new PagedList<DeliveryAppVendorReviewValoration>(vendorReviews, 0, int.MaxValue);

            DeliveryAppVendorReviewList reviewList = new DeliveryAppVendorReviewList()
                .PrepareToGrid(null , reviewPages , ()=> reviewPages);

            return reviewList;
        }
        #endregion

        #region Utilities
        private string GetVendorName(int customerId)
        {
            Customer foundCustomer = _customerService.GetCustomerById(customerId);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            Vendor foundVendor = _vendorRepository.Table
                .FirstOrDefault(vendor => vendor.Id == foundCustomer.VendorId);

            if (foundVendor is null)
                throw new ArgumentException("VendorNotFound");

            return foundVendor.Name;
        }
    
        private string GetCustomerName(int customerId)
        {
            Customer foundCustomer = _customerService.GetCustomerById(customerId);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            return _customerService.GetCustomerFullName(foundCustomer) ?? "N/A";
        }
        #endregion
    }
}
