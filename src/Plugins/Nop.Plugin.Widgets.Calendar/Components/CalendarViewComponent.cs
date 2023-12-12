using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.Calendar.Helpers;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Widgets.Calendar.Components
{

    /// <summary>
    /// View component to display calendar in public store
    /// </summary>
    [ViewComponent]
    public partial class CalendarViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IThemeContext _themeContext;

        #endregion

        #region Ctor

        public CalendarViewComponent(
            IThemeContext themeContext)
        {
            _themeContext = themeContext;
        }

        #endregion
        public IViewComponentResult Invoke()
        {
            return _themeContext.WorkingThemeName == DefaultsInfo.VentureThemeName
                ? View("~/Plugins/WidgetsCalendar/Themes/"+ _themeContext.WorkingThemeName + "/Views/Calendar.cshtml")
                : View("~/Plugins/WidgetsCalendar/Views/Calendar.cshtml");
        }
    }
}
