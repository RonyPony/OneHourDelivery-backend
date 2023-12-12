using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Zoom.Helpers;
using Nop.Web.Framework.Components;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.Zoom.Components
{
    /// <summary>
    /// Represents a view component for ProductDetailsAfter widget zone.
    /// </summary>
    [ViewComponent(Name = "ProductDetailsAfterPictures")]
    public class ProductDetailsAfterPicturesViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="additionalData">An instance of <see cref="ProductDetailsModel"/>.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(ProductDetailsModel additionalData)
        {
            return View($"/{ZoomDefaults.PluginOutputDir}/Views/_ProductDetailsAfterPictures.cshtml", additionalData);
        }
    }
}
