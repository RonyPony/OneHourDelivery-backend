using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Components
{
    /// <summary>
    /// Representsa view component for delivery app discounts.
    /// </summary>
    [ViewComponent(Name = "DeliveryAppDiscount")]
    public sealed class DeliveryAppDiscountComponent : NopViewComponent
    {
        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke()
        {
            return View($"/{Defaults.OutputDir}/Areas/Admin/Views/DeliveryAppDiscount.cshtml");
        }
    }
}
