using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Plugin.Widgets.WarehouseSchedule.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Media;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the delivery app custom favorite services.
    /// </summary>
    public class CustomerFavoriteMappingService : ICustomerFavoriteMappingService
    {
        #region Field
        private readonly IRepository<CustomerFavoriteMapping> _customerFavoriteMappingRepository;    
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;
        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;

        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="CustomerFavoriteMappingService"/>.
        /// </summary>
        /// /// <param name="customerFavoriteMappingRepository">An implementation of <see cref="IRepository{CustomerFavoriteMapping}"/>.</param>
        /// /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/>.</param>      
        /// /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>           
        /// /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>      
        public CustomerFavoriteMappingService(IRepository<CustomerFavoriteMapping> customerFavoriteMappingRepository,
            IVendorDeliveryAppService vendorDeliveryAppService , ICustomerService customerService 
              , IAddressService addressService)
        {
            _customerFavoriteMappingRepository = customerFavoriteMappingRepository;       
            _vendorDeliveryAppService = vendorDeliveryAppService;
            _customerService = customerService;
            _addressService = addressService;
        }
        #endregion

        ///<inheritdoc/>
        public IList<StoreResponseModel> GetCustomerFavoriteVendors(int customerId)
        {
            
            IList<int> favoriteVendors = _customerFavoriteMappingRepository.Table
                .Where(favorites => favorites.CustomerId == customerId)
                .Select(favorites => favorites.VendorId)
                .ToList();

            return  _vendorDeliveryAppService.GetAllStores()
                .Where(mapping => favoriteVendors.Contains(mapping.VendorId))
                .ToList();    
        }

        ///<inheritdoc/>
        public void GetCustomerSelectedShippingAddress(int customerId, int shippingAddressId)
        {
            if (customerId == 0)
                throw new ArgumentException("InvalidCustomerId");

            if (shippingAddressId == 0)
                throw new ArgumentException("InvalidShippingAddressId");

            Customer foundCustomer = _customerService.GetCustomerById(customerId);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            Address foundAddress = _addressService.GetAddressById(shippingAddressId);

            if (foundAddress is null)
                throw new ArgumentException("AddressNotFound");

            foundCustomer.ShippingAddressId = foundAddress.Id;
            foundCustomer.BillingAddressId = foundAddress.Id;
            _customerService.UpdateCustomer(foundCustomer);

        }

        ///<inheritdoc/>
        public void MarkVendorAsFavoriteByCustomer(int customerId , int vendorId)
        {          
            CustomerFavoriteMapping customerFavoriteResult = _customerFavoriteMappingRepository.Table
                .FirstOrDefault(favorite => favorite.CustomerId == customerId
                && favorite.VendorId == vendorId);

            if (customerFavoriteResult != null)
            {
                _customerFavoriteMappingRepository.Delete(customerFavoriteResult);
            }
            else
            {
                _customerFavoriteMappingRepository.Insert(new CustomerFavoriteMapping
                {
                    CustomerId = customerId,
                    VendorId = vendorId,
                    CreatedOnUtc = DateTime.UtcNow
                });
            }         
        }

    }
}
