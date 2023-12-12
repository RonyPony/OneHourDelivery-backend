using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.AzulPaymentPage.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.AzulPaymentPage.Components
{
    /// <summary>
    /// Represents the view component to display payment info in public store
    /// </summary>
    [ViewComponent(Name = DefaultsInfo.PaymentInfoViewComponentName)]
    public sealed class AzulPaymentInfoViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invoke view component
        /// </summary>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke() => View("~/Plugins/Payments.AzulPaymentPage/Views/PaymentInfo.cshtml");
    }
}
