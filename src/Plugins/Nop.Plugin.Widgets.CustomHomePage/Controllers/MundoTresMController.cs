using Microsoft.AspNetCore.Mvc;
using Nop.Web.Controllers;
using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Widgets.CustomHomePage.Controllers
{
    /// <summary>
    /// MundoTresM controller
    /// </summary>
    public partial class MundoTresMController : BasePublicController
    {

        #region Fields

        private readonly IThemeContext _themeContext;

        #endregion

        #region Ctor
        /// <summary>
        /// MundoTresM controller Constructor
        /// </summary>
        /// <param name="themeContext">Represents a theme context <see cref="IThemeContext"/></param>
        public MundoTresMController(
            IThemeContext themeContext)
        {
            _themeContext = themeContext;
        }
        #endregion


        /// <summary>
        /// Default Action when call /MundoTresM
        /// </summary>
        /// <returns>MundoTresM View by theme</returns>
        public virtual IActionResult index()
        {
            return View("~/Themes/" + _themeContext.WorkingThemeName + "/Views/MundoTresM/Index.cshtml");
        }
    }
}