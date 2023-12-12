using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Plugin.Payments.CardNet.Services;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Payments.CardNet.Components
{
    /// <summary>
    /// View component to add script to pages
    /// </summary>
    [ViewComponent(Name = CardNetDefaults.ScriptViewComponentName)]
    public class ScriptViewComponent : NopViewComponent
    {
        #region Fields

        private readonly CardNetService _cardNetService;

        #endregion

        #region Ctor

        public ScriptViewComponent(CardNetService cardNetService)
        {
            _cardNetService = cardNetService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke view component
        /// </summary>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            if (!widgetZone.Equals(PublicWidgetZones.CheckoutPaymentInfoTop) && !widgetZone.Equals(PublicWidgetZones.OpcContentBefore))
            {
                return Content(string.Empty);
            }

            string script = _cardNetService.GetScript();

            return new HtmlContentViewComponentResult(new HtmlString(script ?? string.Empty));
        }

        #endregion
    }
}
