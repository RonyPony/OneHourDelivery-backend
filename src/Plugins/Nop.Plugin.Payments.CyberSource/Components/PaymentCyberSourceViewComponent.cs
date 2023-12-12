using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Web.Framework.Components;


namespace Nop.Plugin.Payments.CyberSource.Components
{
    /// <summary>
    /// A view for paymentinfo page
    /// </summary>
    [ViewComponent(Name = "PaymentCyberSource")]
    public class PaymentCyberSourceViewComponent : NopViewComponent
    {
        /// <summary>
        /// Invokes the view component.
        /// </summary>
        public IViewComponentResult Invoke(CyberSourcePaymentInfoModel model)
        {
            if (model is null)
                model = new CyberSourcePaymentInfoModel();

            return View("~/Plugins/Payments.CyberSource/Views/PaymentInfo.cshtml", model);
        }
    }
}
