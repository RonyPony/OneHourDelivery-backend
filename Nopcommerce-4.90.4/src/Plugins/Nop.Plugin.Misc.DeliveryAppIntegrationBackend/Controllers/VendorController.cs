
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with Vendor entity.
    /// </summary>
    [Route("api/vendors")]
    [ApiController]
    public sealed class VendorController : Controller
    {
        #region Fields

        private readonly IDeliveryAppOrderService _deliveryAppOrderService;
        private readonly IVendorReviewsService _vendorReviewsService;
        private readonly ILogger _logger;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;
        private readonly IVendorService _vendorService;
        private readonly IPictureService _pictureService;
        private readonly IDownloadService _downloadService;
        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of <see cref="VendorController"/>
        /// </summary>
        /// <param name="deliveryAppOrderService">An implementation of <see cref="IDeliveryAppOrderService"/></param>
        /// <param name="vendorReviewsService">An implementation of <see cref="IVendorReviewsService"/></param>
        /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        public VendorController(
            IDeliveryAppOrderService deliveryAppOrderService,
            IVendorReviewsService vendorReviewsService,
            ILogger logger,
            IVendorDeliveryAppService vendorDeliveryAppService,
            IVendorService vendorService,
            IPictureService pictureService,
            IDownloadService downloadService,
            CustomerSettings customerSettings,
            IWorkContext workContext)
        {
            _deliveryAppOrderService = deliveryAppOrderService;
            _vendorReviewsService = vendorReviewsService;
            _logger = logger;
            _vendorDeliveryAppService = vendorDeliveryAppService;
            _vendorService = vendorService;
            _pictureService = pictureService;
            _downloadService = downloadService;
            _customerSettings = customerSettings;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the logo/avatar of the vendor owned by the authenticated commerce account.
        /// </summary>
        /// <param name="id">Vendor identifier.</param>
        /// <param name="uploadedFile">New vendor image.</param>
        [HttpPatch("{id}/avatar"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public async Task<IActionResult> UpdateAvatar(int id, IFormFile uploadedFile)
        {
            try
            {
                var currentCustomer = await _workContext.GetCurrentCustomerAsync();
                if (currentCustomer?.VendorId != id)
                    return Forbid(JwtBearerDefaults.AuthenticationScheme);

                var vendor = await _vendorService.GetVendorByIdAsync(id);
                if (vendor == null || vendor.Deleted)
                    return NotFound(new { message = "VendorNotFound" });

                if (uploadedFile == null)
                    return BadRequest(new { message = "WasNotFoundUploadedFileFormValue" });
                if (string.IsNullOrWhiteSpace(uploadedFile.FileName))
                    return BadRequest(new { message = "WasNotFoundUploadedFileName" });
                if (!uploadedFile.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                    return BadRequest(new { message = "UploadedFileMustBeAnImage" });
                if (uploadedFile.Length > _customerSettings.AvatarMaximumSizeBytes)
                    return BadRequest(new
                    {
                        message = $"MaximumUploadedFileSizeExceded({uploadedFile.Length}>{_customerSettings.AvatarMaximumSizeBytes})"
                    });

                var pictureBinary = _downloadService.GetDownloadBits(uploadedFile);
                Picture vendorPicture;
                if (vendor.PictureId > 0)
                {
                    vendorPicture = _pictureService.UpdatePicture(vendor.PictureId, pictureBinary,
                        uploadedFile.ContentType, null);
                }
                else
                {
                    vendorPicture = _pictureService.InsertPicture(pictureBinary,
                        uploadedFile.ContentType, null);
                }

                if (vendorPicture == null)
                    throw new InvalidOperationException("VendorPictureCouldNotBeSaved");

                vendor.PictureId = vendorPicture.Id;
                await _vendorService.UpdateVendorAsync(vendor);

                return Ok(new
                {
                    vendorId = vendor.Id,
                    pictureUrl = _pictureService.GetPictureUrl(vendor.PictureId)
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"There was an error updating vendor avatar. {ex.Message}", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all vendor's reviews.
        /// </summary>
        /// <param name="vendorId">Indicates vendor id.</param>
        /// <returns>A result of <see cref="IActionResult"/> type.</returns>
        [HttpGet("{vendorId}/reviews"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get VendorReviews functionality", typeof(List<VendorReview>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get VendorReviews functionality", typeof(ErrorMessage))]
        public IActionResult GetVendorReviews(int vendorId)
        {
            try
            {
                IEnumerable<VendorReview> result = _vendorReviewsService.GetReviewsByVendor(vendorId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was an error getting vendor reviews. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Inserts a review to a given vendor.
        /// </summary>
        /// <param name="vendorReview">Indicates the information needed to add a review.</param>
        /// <returns>A result of <see cref="IActionResult"/> type.</returns>
        [HttpPost("{vendorId}/reviews"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get VendorReviews functionality", typeof(ErrorMessage))]
        public IActionResult AddReview([FromBody] VendorReviewModel vendorReview)
        {
            try
            {
                _vendorReviewsService.InsertReview(vendorReview);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"There was an error adding vendor review. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates a vendor review.
        /// </summary>
        /// <param name="vendorReview">Indicates the information needed to update a review.</param>
        /// <returns>A result of <see cref="IActionResult"/> type.</returns>
        [HttpPatch("{vendorId}/reviews"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get VendorReviews functionality", typeof(ErrorMessage))]
        public IActionResult UpdateReview([FromBody] VendorReviewMapping vendorReview)
        {
            try
            {
                _vendorReviewsService.UpdateReview(vendorReview);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"There was an error updating vendor review. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/pending-orders"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetStorePendingOrders(int id)
        {
            try
            {
                var ordersRootObject = new OrdersRootObject()
                {
                    Orders = _deliveryAppOrderService.GetPendingOrdersByVendorId(id)
                };

                return Ok(ordersRootObject);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/processing-orders"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrdersProcessingByStore(int id)
        {
            try
            {
                var ordersRootObject = new OrdersRootObject()
                {
                    Orders = _deliveryAppOrderService.GetOrdersInProgressByVendorId(id)
                };

                return Ok(ordersRootObject);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/historic-order"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrderHistoricByStore(int id)
        {
            try
            {
                var orders = _deliveryAppOrderService.GetHistoricOrdersByVendorId(id);

                return Ok(orders);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/vendor-profit"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetVendorProfit(int id)
        {
            try
            {
                ProfitVendorModel profit = _vendorDeliveryAppService.GetVendorProfit(id);

                return Ok(profit);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("search"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SearchStoresBySearchText([FromQuery] string searchText,[FromQuery] decimal latitude, [FromQuery] decimal longitude)
        {
            try
            {
                IEnumerable<StoreResponseModel> vendors = _vendorDeliveryAppService.GetVendorsBySearchText(searchText,latitude,longitude);

                return Ok(vendors);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetVendorInfo(int id)
        {
            try
            {
                VendorInfo vendorInfo = _vendorDeliveryAppService.GetVendorInfo(vendorId: id);

                return Ok(vendorInfo);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }




        //[HttpGet("categories/{categoryName}"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[GetRequestsErrorInterceptorActionFilter]
        //public IActionResult GetStoresByCategory(string categoryName, [FromQuery] decimal latitude, [FromQuery] decimal longitude)
        //{
        //    try
        //    {
        //        IList<StoreResponseModel> stores = _vendorDeliveryAppService.GetAllStoresByCategory(categoryName, latitude, longitude);

        //        return Ok(stores);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error($"There was error getting stored by category. {ex.Message},  Full exception: {ex}, ", ex);
        //        return BadRequest(new { message = ex.Message });
        //    }

        //}
        #endregion
    }
}
