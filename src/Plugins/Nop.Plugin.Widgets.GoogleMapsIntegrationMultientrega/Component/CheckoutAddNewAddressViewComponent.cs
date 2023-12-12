using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Component
{
    /// <summary>
    /// Represents a view component for add new address button on checkout.
    /// </summary>
    [ViewComponent(Name = "CheckoutAddNewAddress")]
    public sealed class CheckoutAddNewAddressViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke()
        {
            return View($"~/{Defaults.PluginOutputDir}/Views/CheckoutAddNewAddress.cshtml");
        }
    }
}
