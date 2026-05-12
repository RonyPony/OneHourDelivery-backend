using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with Store entity.
    /// </summary>
    [Route("api/vendor-stores")]
    [ApiController]
    public class StoreController : Controller
    {
        #region Fields
        private readonly IStoreService _storeService;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of <see cref="StoreController"/>
        /// </summary>
        /// <param name="storeService">An instance of <see cref="IStoreService"/></param>
        /// <param name="vendorDeliveryAppService">An instance of <see cref="IVendorDeliveryAppService"/></param>
        /// <param name="logger">An instance of <see cref="ILogger"/></param>
        /// <param name="cache">An instance of <see cref="IMemoryCache"/></param>
        public StoreController(IStoreService storeService,
                               IVendorDeliveryAppService vendorDeliveryAppService,
                               ILogger logger, 
                               IMemoryCache cache)
        {
            _storeService = storeService;
            _vendorDeliveryAppService = vendorDeliveryAppService;
            _logger = logger;
            _cache = cache;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Store products by vendor.
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <returns>An instance of <see cref="GetStoreProductsByVendorId"/></returns>
        [HttpGet("{vendorId}/products"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreProductsByVendorId functionality", typeof(List<ProductModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreProductsByVendorId functionality", typeof(ErrorMessage))]
        public IActionResult GetStoreProductsByVendorId(int vendorId)
        {
            return Ok(_storeService.GetStoreProductsByVendorId(vendorId));
        }

        /// <summary>
        /// Get the closest stores
        /// </summary>
        /// <param name="latitude">Client address latitude. </param>
        /// <param name="longitude">Client address longitude.</param>
        /// <returns>An implementation of <see cref="IActionResult"/></returns>
        [HttpGet]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetStoreByLocation([FromQuery] decimal latitude, [FromQuery] decimal longitude)
        {
            try
            {
                IEnumerable<StoreResponseModel> stores = _vendorDeliveryAppService.GetClosestStores(latitude, longitude).OrderBy(x=> x.IsOpen);

                return Ok(stores);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting stored by location. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get Store products by vendor.
        /// </summary>
        /// <param name="vendorId">vendor id</param>
        /// <returns>An instance of <see cref="StoreByVendorResponse"/></returns>
        [HttpGet("{vendorId}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<ProductModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetStoreByVendor(int vendorId , [FromQuery] int customerId)
        {
            try
            {
                return Ok(_vendorDeliveryAppService.GetVendorProductsGroupByCategories(vendorId , customerId));
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting stored by vendorId. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Get Store products by category.
        /// </summary>
        /// <param name="categoryName">Category Name</param>
        /// <param name="longitude">longitude </param>
        /// <param name="latitude">latitude</param>        
        /// <returns>An instance of <see cref="StoreByVendorResponse"/></returns>
        [HttpGet("categories/{categoryName}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<StoreResponseModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetStoresByCategory(string categoryName, [FromQuery] decimal latitude, [FromQuery] decimal longitude)
        {
            try
            {
                IList<StoreResponseModel> stores = _vendorDeliveryAppService.GetAllStoresByCategory(categoryName, latitude, longitude);

                return Ok(stores);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting stored by category. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        /// <summary>
        /// Get store groupings by distance, popularity, when was created and stores with promotions.
        /// </summary>
        /// <param name="longitude">longitude </param>
        /// <param name="latitude">latitude</param>        
        /// <returns>An instance of <see cref="StoreGroupings"/></returns>
        [HttpGet("new"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<StoreResponseModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetNewStores([FromQuery] long customerId, [FromQuery] decimal longitude, [FromQuery] decimal latitude)
        {
            try
            {
                List<StoreResponseModel> result = _vendorDeliveryAppService.GetAllNewStore(latitude, longitude);
                 return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting new stores. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }

        }




        /// <summary>
        /// Get popular stores by distance.
        /// </summary>
        /// <param name="longitude">longitude </param>
        /// <param name="latitude">latitude</param>        
        /// <returns>An instance of <see cref="StoreGroupings"/></returns>
        [HttpGet("populars"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<StoreResponseModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetPopularStores([FromQuery] long customerId, [FromQuery] decimal longitude, [FromQuery] decimal latitude)
        {
            try
            {
                List<StoreResponseModel> result = _vendorDeliveryAppService.GetAllPopularStore(latitude, longitude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting the popular stores. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }

        }

        #endregion

        /// <summary>
        /// Get near stores.
        /// </summary>
        /// <param name="longitude">longitude </param>
        /// <param name="latitude">latitude</param>        
        /// <returns>An instance of <see cref="StoreGroupings"/></returns>
        [HttpGet("near"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<StoreResponseModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetNearStores([FromQuery] long customerId, [FromQuery] decimal longitude, [FromQuery] decimal latitude)
        {
            try
            {
                List<StoreResponseModel> result = _vendorDeliveryAppService.GetAllNearStore(latitude, longitude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting the near stores. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// Get near stores.
        /// </summary>
        /// <param name="longitude">longitude </param>
        /// <param name="latitude">latitude</param>        
        /// <returns>An instance of <see cref="StoreGroupings"/></returns>
        [HttpGet("promotions-store"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get GetStoreByVendor functionality", typeof(List<StoreResponseModel>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get GetStoreByVendor functionality", typeof(ErrorMessage))]
        public IActionResult GetStoresWithPromotions([FromQuery] long customerId, [FromQuery] decimal longitude, [FromQuery] decimal latitude)
        {
            try
            {
                List<StoreResponseModel> result = _vendorDeliveryAppService.GetAllStoresWithPromotions(latitude, longitude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting the promotional stores. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }

        }

    }


}