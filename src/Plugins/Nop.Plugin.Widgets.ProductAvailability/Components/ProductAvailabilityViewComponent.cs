using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Plugin.Widgets.ProductAvailability.Services;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Components
{
    /// <summary>
    /// Represents a view component handler for product availability per store grid.
    /// </summary>
    [ViewComponent(Name = "ProductAvailability")]
    public sealed class ProductAvailabilityViewComponent : NopViewComponent
    {
        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly ProductAvailabilitySettings _productAvailabilitySettings;

        /// <summary>
        /// Initilizes a new instance of <see cref="ProductAvailabilityViewComponent"/>.
        /// </summary>
        /// <param name="productAvailabilityService">An implementation of <see cref="IProductAvailabilityService"/>.</param>
        /// <param name="productAvailabilitySettings">An instance of <see cref="ProductAvailabilitySettings"/>.</param>
        public ProductAvailabilityViewComponent(
            IProductAvailabilityService productAvailabilityService,
            ProductAvailabilitySettings productAvailabilitySettings)
        {
            _productAvailabilityService = productAvailabilityService;
            _productAvailabilitySettings = productAvailabilitySettings;
        }

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="additionalData">An instance of <see cref="ProductDetailsModel"/>.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(ProductDetailsModel additionalData)
        {
            if (!_productAvailabilitySettings.DisplayStoreAvailabilityOnProductDetailsPage) return Content(string.Empty);
            Task<InventoryRequestResponseModel> modelTask = _productAvailabilityService.GetProductInventoryBySku(additionalData.Sku);
            modelTask.Wait();
            if (modelTask.Result is null) return Content(string.Empty);
            return View($"~/{ProductAvailabilityDefault.OutputDir}/Views/_InventorySearchResult.cshtml", modelTask.Result);
        }
    }
}
