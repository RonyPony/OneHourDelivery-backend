using Nop.Core.Domain.Discounts;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IDeliveryAppDiscountService"/>.
    /// </summary>
    public sealed class DeliveryAppDiscountService : IDeliveryAppDiscountService
    {
        #region Fields

        private readonly IRepository<Discount> _discountRepository;
        private readonly IRepository<VendorDiscount> _vendorDiscountRepository;
        private readonly IRepository<CustomerDiscountMapping> _customerDiscountMappingRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppDiscountService"/>.
        /// </summary>
        /// <param name="discountRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Discount"/>.</param>
        /// <param name="vendorDiscountRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="VendorDiscount"/>.</param>
        /// <param name="customerDiscountMappingRepository">An implementation of <see cref="IRepository{CustomerDiscountMapping}"/> </param>
        /// <param name="eventPublisher">An implementation of <see cref="IEventPublisher"/> </param>
        public DeliveryAppDiscountService(
            IRepository<Discount> discountRepository,
            IRepository<VendorDiscount> vendorDiscountRepository,
            IRepository<CustomerDiscountMapping> customerDiscountMappingRepository,
            IEventPublisher eventPublisher)
        {

            _discountRepository = discountRepository;
            _vendorDiscountRepository = vendorDiscountRepository;
            _customerDiscountMappingRepository = customerDiscountMappingRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public IList<Discount> GetAllDiscounts(DiscountType? discountType = null, string couponCode = null,
            string discountName = null, bool showHidden = false, DateTime? startDateUtc = null, DateTime? endDateUtc = null,
            int vendorId = 0)
        {
            //we load all discounts, and filter them using "discountType" parameter later (in memory)
            //we do it because we know that this method is invoked several times per HTTP request with distinct "discountType" parameter
            //that's why let's access the database only once

            var query = _discountRepository.Table;

            if (!showHidden)
            {
                query = query.Where(discount =>
                    (!discount.StartDateUtc.HasValue || discount.StartDateUtc <= DateTime.UtcNow) &&
                    (!discount.EndDateUtc.HasValue || discount.EndDateUtc >= DateTime.UtcNow));
            }

            if (vendorId != 0)
            {
                query = from d in query
                        join vd in _vendorDiscountRepository.Table on d.Id equals vd.DiscountId
                        where vd.VendorId == vendorId
                        select d;
            }

            //filter by coupon code
            if (!string.IsNullOrEmpty(couponCode))
                query = query.Where(discount => discount.CouponCode == couponCode);

            //filter by name
            if (!string.IsNullOrEmpty(discountName))
                query = query.Where(discount => discount.Name.Contains(discountName));

            query = query.OrderBy(discount => discount.Name).ThenBy(discount => discount.Id);

            //we know that this method is usually invoked multiple times
            //that's why we filter discounts by type and dates on the application layer
            if (discountType.HasValue)
                query = query.Where(discount => discount.DiscountType == discountType.Value);

            //filter by dates
            if (startDateUtc.HasValue)
                query = query.Where(discount =>
                    !discount.StartDateUtc.HasValue || discount.StartDateUtc >= startDateUtc.Value);
            if (endDateUtc.HasValue)
                query = query.Where(discount =>
                    !discount.EndDateUtc.HasValue || discount.EndDateUtc <= endDateUtc.Value);

            return query.ToList();
        }

        ///<inheritdoc/>
        public void AttachCouponDiscountToCustomer(CustomerDiscountMapping customerDiscountMapping)
        {

            ValidateCustomerDiscount(customerDiscountMapping);

            _customerDiscountMappingRepository.Insert(customerDiscountMapping);

            _eventPublisher.EntityInserted(customerDiscountMapping);
        }

        private void ValidateCustomerDiscount(CustomerDiscountMapping customerDiscountMapping)
        {
            if (customerDiscountMapping == null)
            {
                throw new ArgumentException("Customer discount mapping can't be null");
            }

            if (customerDiscountMapping.CustomerId == 0)
            {
                throw new ArgumentException("CustomerId can't be zero");
            }

            if (customerDiscountMapping.DiscountId == 0)
            {
                throw new ArgumentException("CustomerId can't be zero");
            }


        }

        ///<inheritdoc/>
        public CustomerDiscountMapping GetDiscountAppliedToCustomer(int customerId, int discountId)
        {
            return _customerDiscountMappingRepository.Table.FirstOrDefault(dcm => dcm.CustomerId == customerId && dcm.DiscountId == discountId);
        }

        public void DeleteDiscountCustomerMapping(CustomerDiscountMapping discountCustomerMapping)
        {
            if (discountCustomerMapping is null)
                throw new ArgumentNullException(nameof(discountCustomerMapping));

            _customerDiscountMappingRepository.Delete(discountCustomerMapping);

            _eventPublisher.EntityDeleted(discountCustomerMapping);
        }

        #endregion
    }
}
