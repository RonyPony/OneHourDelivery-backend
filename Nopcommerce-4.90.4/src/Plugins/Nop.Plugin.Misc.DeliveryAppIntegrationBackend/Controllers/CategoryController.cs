using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Logging;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using Nop.Services.Media;


namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with category entity.
    /// </summary>
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger _logger;
        private readonly IPictureService _pictureService;

        /// <summary>
        /// Creates a new instance of <see cref="CategoryController"/>
        /// </summary>
        /// <param name="categoryService">An implementation of <see cref="ICategoryService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        public CategoryController(ICategoryService categoryService,
            ILogger logger,
            IPictureService pictureService)
        {
            _categoryService = categoryService;
            _logger = logger;
            _pictureService = pictureService;
        }

        /// <summary>
        /// Gets a list of categories by store.
        /// </summary>
        /// <returns>An instance of <see cref="OkObjectResult"/></returns>
        [HttpGet("vendor/{vendorId}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the Get StoreCategories functionality", typeof(List<Category>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes the Get StoreCategories functionality", typeof(ErrorMessage))]

        public IActionResult GetStoreCategories(int vendorId)
        {
            try
            {
                IList<Category> categories = _categoryService.GetAllCategories(storeId: vendorId);

                return Ok(GetListOfCategoriesData(categories));
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting categories. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        private List<CategoryData> GetListOfCategoriesData(IList<Category> categories)
        {
            var categoriesData = new List<CategoryData>();

            foreach (Category category in categories)
            {
                CategoryData categoryData = category.ToModel<CategoryData>();
                categoryData.Picture = GetPictureUrl(category.PictureId);
                categoriesData.Add(categoryData);
            }

            return categoriesData;
        }

        private string GetPictureUrl(int pictureId)
        {
            return _pictureService.GetPictureUrl(pictureId);
        }
    }
}
