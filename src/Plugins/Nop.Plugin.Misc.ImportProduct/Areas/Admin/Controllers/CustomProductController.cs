using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.ImportProduct.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Framework.Controllers;
using System;

namespace Nop.Plugin.Misc.ImportProduct.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents a controller for Import Product and Images.
    /// </summary>
    public class CustomProductController : BaseController
    {
        #region Fields
        private readonly IImportManager _importManager;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;
        private readonly VendorSettings _vendorSettings;
        private readonly ExcelImportService _excelImportService;

        #endregion
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="CustomProductController"/>.
        /// </summary>
        /// <param name="importManager">An implementation of <see cref="IImportManager"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="excelImportService">An implementation of <see cref="ExcelImportService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="vendorSettings">An implementation of <see cref="VendorSettings"/>.</param>
        public CustomProductController(
            IImportManager importManager,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ExcelImportService excelImportService,
            IWorkContext workContext,
            VendorSettings vendorSettings)
        {
            _importManager = importManager;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _workContext = workContext;
            _vendorSettings = vendorSettings;
            _excelImportService = excelImportService;
        }

        #endregion
        /// <summary>
        /// Import the excel that contains the Products and Images.
        /// </summary>
        /// <param name="importexcelfile">An implementation of <see cref="IFormFile"/>.</param>
        [HttpPost]
        public IActionResult ImportExcel(IFormFile importexcelfile)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            if (_workContext.CurrentVendor != null && !_vendorSettings.AllowVendorsToImportProducts)
                //a vendor can not import products
                return AccessDeniedView();

            try
            {
                if (importexcelfile != null && importexcelfile.Length > 0)
                {
                    _excelImportService.ImportProductsFromXlsx(importexcelfile.OpenReadStream());
                }
                else
                {
                    _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Common.UploadFile"));
                    return RedirectToAction("List","Product", new { area = "Admin" });
                }

                _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Imported"));
                return RedirectToAction("List", "Product", new { area = "Admin" });
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("List", "Product", new { area = "Admin" });
            }
        }
    }
}
