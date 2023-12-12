using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.Calendar.Helpers;
using Nop.Services.Catalog;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Widgets.Calendar.Controllers
{

    /// <summary>
    /// Calendar controller for display calendar events in public store
    /// </summary>
    public partial class CatalogController : BasePublicController
    {
        #region Fields

        private readonly IProductModelFactory _productModelFactory;
        private readonly IProductService _productService;
        private readonly IThemeContext _themeContext;
        private readonly ICommonModelFactory _commonModelFactory;
        #endregion

        #region Ctor

        public CatalogController(
            IProductModelFactory productModelFactory,
            IProductService productService,
            IThemeContext themeContext,
            ICommonModelFactory commonModelFactory)
        {
            _productModelFactory = productModelFactory;
            _productService = productService;
            _themeContext = themeContext;
            _commonModelFactory = commonModelFactory;
        }

        #endregion


        private class CalendarEvents
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string date { get; set; }
            public string type { get; set; }
            public bool everyYear { get; set; }

        }

        /// <summary>
        /// Get All product for display in calendar
        /// </summary>
        [HttpGet]
        public virtual JsonResult ProductAll()
        {
            List<CalendarEvents> listResult = new List<CalendarEvents>();

            var model = _productService.GetAllProductsDisplayedOnHomepage().Where(x => x.AvailableEndDateTimeUtc != null);
            foreach (var item in model)
            {
                CalendarEvents calendarEvent = new CalendarEvents()
                {
                    id = item.Id,
                    name = "",
                    description = item.Name,
                    date = ((DateTime)item.AvailableEndDateTimeUtc).ToString("MMMM/dd/yyy", new CultureInfo("en-US")).Replace("-", "/"),
                    everyYear = true,
                    type = "event"
                };
                listResult.Add(calendarEvent);
            }
            return Json(listResult);
        }

        /// <summary>
        /// return partialview product by id
        /// </summary>
        [HttpGet]
        public virtual PartialViewResult Product(int Id)
        {
            if (Id == 0)
            {
                return PartialView("~/Plugins/WidgetsCalendar/Views/_ProductMiniDetail.cshtml", new Product());
            }
            else
            {
                var product = _productService.GetProductById(Id);

                var model = _productModelFactory.PrepareProductDetailsModel(product, null, false);
                TempData["Instructor"] = model.ProductManufacturers;
                TempData["SeName"] = model.SeName;
                TempData["Logo"] = _commonModelFactory.PrepareLogoModel();


                return _themeContext.WorkingThemeName == DefaultsInfo.VentureThemeName
               ? PartialView("~/Plugins/WidgetsCalendar/Themes/" + _themeContext.WorkingThemeName + "/Views/_ProductMiniDetail.cshtml", product)
               : PartialView("~/Plugins/WidgetsCalendar/Views/_ProductMiniDetail.cshtml", product);

            }

        }


    }
}
