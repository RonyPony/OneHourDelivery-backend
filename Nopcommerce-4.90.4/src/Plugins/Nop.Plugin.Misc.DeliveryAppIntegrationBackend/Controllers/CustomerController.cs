using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.ModelBinders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with CustomerRatingMapping entity.
    /// </summary>
    [Route("api/delivery-customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        #region Fields

        private readonly ICustomerRatingService _customerRatingService;
        private readonly IDeliveryAppAddressService _deliveryAppAddressService;
        private readonly ICustomerFavoriteMappingService _customerFavoriteMappingService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor
        /// <summary>
        /// Creates an instance of <see cref="CustomerController"/>
        /// </summary>
        /// <param name="customerRatingService">An implementation of <see cref="ICustomerRatingService"/>.</param>
        /// <param name="deliveryAppAddressService">An implementation of <see cref="IDeliveryAppAddressService"/>.</param>
        /// <param name="customerFavoriteMappingService">An implementation of <see cref="ICustomerFavoriteMappingService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        public CustomerController(
            ICustomerRatingService customerRatingService,
            IDeliveryAppAddressService deliveryAppAddressService,
            ICustomerFavoriteMappingService customerFavoriteMappingService,
            ILogger logger)
        {
            _customerRatingService = customerRatingService;
            _deliveryAppAddressService = deliveryAppAddressService;
            _customerFavoriteMappingService = customerFavoriteMappingService;
            _logger = logger;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new customer rating by request.
        /// </summary>
        /// <param name="customerRating"> customer rating data values.</param>
        /// <param name="customerRating">An instance of <see cref="CustomerRatingMappingRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("rate"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult InsertCustomerRating([FromBody] CustomerRatingMappingRequest customerRating)
        {
            try
            {
                _customerRatingService.InsertCustomerRating(customerRating);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Retrieves a list of addresses for a specific customer.
        /// </summary>
        /// <param name="id">The id of the customer.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpGet("{id}/addresses"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerAddresses(int id)
        {
            try
            {
                IEnumerable<DeliveryAppAddressModel> addresses = _deliveryAppAddressService.GetAddressesByCustomerId(id)
                                                                .GroupBy(x => new { x.Address1, x.CountryId, x.City }).Select(x => x.FirstOrDefault());
                
                return Ok(addresses);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Inserts a new address for a specific customer.
        /// </summary>
        /// <param name="id">The id of the customer.</param>
        /// <param name="addressDelta">The new address to insert.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("{id}/address"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult InsertNewCustomerAddresses(int id, [ModelBinder(typeof(JsonModelBinder<DeliveryAppAddressModel>))] Delta<DeliveryAppAddressModel> addressDelta)
        {
            try
            {
                int newAddressId = _deliveryAppAddressService.InsertNewCustomerAddress(id, addressDelta.Dto);
                return Ok(new { address_id = newAddressId });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }


        /// <summary>
        /// update a address for a specific customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressId"></param>
        /// <param name="addressDelta"></param>
        /// <returns>the id of the address updated</returns>
        [HttpPut("{id}/address/{addressId}"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult UpdateCustomerAddresses(int id, int addressId,
            [ModelBinder(typeof(JsonModelBinder<DeliveryAppAddressModel>))] Delta<DeliveryAppAddressModel> addressDelta)
        {
            try
            {
                addressDelta.Dto.Id = addressId;
                int updateAddressId = _deliveryAppAddressService.UpdateCustomerAddress(id, addressDelta.Dto);
                return Ok(new { address_id = updateAddressId });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Inserts a new customer favorite vendor by request.
        /// </summary>
        /// <param name="id"> customer id.</param>
        /// <param name="vendorId"> vendor id.</param>
        /// <param name="customerFavoriteMappingRequest">An instance of <see cref="CustomerFavoriteMappingService"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("{id}/favorite-vendor"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult MarkVendorAsFavorite(int id, [FromQuery] int vendorId)
        {
            try
            {
                _customerFavoriteMappingService.MarkVendorAsFavoriteByCustomer(id, vendorId);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Get customer favorite.
        /// </summary>
        /// <param name="id"> customer id.</param>
        /// <returns>An implementation of <see cref="IActionResult"/> list of customer's favorite vendors</returns>
        [HttpGet("{id}/favorites-vendors"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerFavoriteVendors(int id)
        {
            try
            {
                IList<StoreResponseModel> favoriteVendors = _customerFavoriteMappingService
                     .GetCustomerFavoriteVendors(id);

                return Ok(favoriteVendors);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Get customer pending review.
        /// </summary>
        /// <param name="id"> customer id.</param>
        /// <returns>An implementation of <see cref="IActionResult"/> list of customer's favorite vendors</returns>
        [HttpGet("{id}/reviews"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerPendingReviews(int id)
        {
            try
            {
                IList<CustomerPendingReviewsModel> customerReviews = _customerRatingService
                    .GetCustomerPendingReviews(id);                    
                return Ok(customerReviews);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets a list of the payment tokens of the specified customer
        /// </summary>
        /// <param name="id">The id of the customer</param>
        /// <returns></returns>
        [HttpGet("{id}/payment-tokens"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerPaymentTokens(int id)
        {
            try
            {
                IList<CustomerPaymentTokenModel> customerPaymentTokens = _customerRatingService
                    .GetCustomerCybersourceTokens(id);
                return Ok(customerPaymentTokens);
                }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }
        
         /// <summary>
        ///  Assign a specific address by the customer as shipping address
        /// </summary>
        /// <param name="id">Customer's id</param>
        /// <param name="shippingAddressId">Customer's shipping Address id</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}/address"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerSelectedAddress(int id, [FromQuery] int shippingAddressId)
        {
            try
            {
                _customerFavoriteMappingService
                   .GetCustomerSelectedShippingAddress(id, shippingAddressId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        #endregion
    }
}
