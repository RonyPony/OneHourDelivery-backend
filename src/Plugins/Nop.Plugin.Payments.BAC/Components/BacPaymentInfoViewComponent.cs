using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.BAC.Helpers;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.BAC.Components
{
    /// <summary>
    /// View component to display payment info in public store
    /// </summary>
    [ViewComponent(Name = DefaultsInfo.PaymentInfoViewComponentName)]
    public class BacPaymentInfoViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invokes view component
        /// </summary>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke() => View("~/Plugins/Payments.BAC/Views/PaymentInfo.cshtml");
    }
}
