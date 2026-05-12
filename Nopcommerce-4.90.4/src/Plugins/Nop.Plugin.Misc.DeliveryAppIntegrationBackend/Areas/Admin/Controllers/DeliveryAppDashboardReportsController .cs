using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Models.Home;
using Nop.Web.Areas.Admin.Models.Orders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Controllers
{
    /// <summary>
    /// Represents a controller for the delivery app reports management.
    /// </summary>
    public partial class DeliveryAppDashboardReportsController : BaseAdminController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ICommonModelFactory _commonModelFactory;
        private readonly IHomeModelFactory _homeModelFactory;

        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IOrderModelFactory _orderModelFactory;
        private readonly IOrderService _orderService;
        private readonly IOrderPendingToClosePaymentModelFactory _orderPendingToClosePaymentModelFactory;
        private readonly IRepository<DriverRatingMapping> _driverRatingMappingRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IDeliveryAppDriverReviewValorationFactory _deliveryAppDriverReviewValorationFactory;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppDashboardReportsController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="commonModelFactory">An implementation of <see cref="ICommonModelFactory"/>.</param>
        /// <param name="homeModelFactory">An implementation of <see cref="IHomeModelFactory"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="dateTimeHelper">An implementation of <see cref="IDateTimeHelper"/>.</param>
        /// <param name="orderModelFactory">An implementation of <see cref="IOrderModelFactory"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="orderPendingToClosePaymentModelFactory">An implementation of <see cref="IOrderPendingToClosePaymentModelFactory"/>.</param>
        /// <param name="driverRatingMappingRepository">An implementation of <see cref="IRepository{DriverRatingMapping}"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="vendorRepository">An implementation of <see cref="IRepository{Vendor}"/>.</param>
        /// <param name="deliveryAppDriverReviewValorationFactory">An implementation of <see cref="IDeliveryAppDriverReviewValorationFactory"/>.</param>
        public DeliveryAppDashboardReportsController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ICommonModelFactory commonModelFactory,
            IHomeModelFactory homeModelFactory,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper,
            IOrderModelFactory orderModelFactory,
            IOrderService orderService,
            IOrderPendingToClosePaymentModelFactory orderPendingToClosePaymentModelFactory ,
            IRepository<DriverRatingMapping> driverRatingMappingRepository ,
            ICustomerService customerService,
            IRepository<Vendor> vendorRepository,
            IDeliveryAppDriverReviewValorationFactory deliveryAppDriverReviewValorationFactory)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _commonModelFactory = commonModelFactory; 
            _homeModelFactory = homeModelFactory;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
            _orderModelFactory = orderModelFactory;
            _orderService = orderService;
            _orderPendingToClosePaymentModelFactory = orderPendingToClosePaymentModelFactory;
            _driverRatingMappingRepository = driverRatingMappingRepository;
            _customerService = customerService;
            _vendorRepository = vendorRepository;
            _deliveryAppDriverReviewValorationFactory = deliveryAppDriverReviewValorationFactory;
        }

        #endregion

        #region Methods

        public virtual IActionResult Index()
        {
            //display a warning to a store owner if there are some error
            if (_permissionService.Authorize(StandardPermissionProvider.ManageMaintenance))
            {
                var warnings = _commonModelFactory.PrepareSystemWarningModels();
                if (warnings.Any(warning => warning.Level == SystemWarningLevel.Fail ||
                                            warning.Level == SystemWarningLevel.Warning))
                    _notificationService.WarningNotification(
                        string.Format(_localizationService.GetResource("Admin.System.Warnings.Errors"),
                        Url.Action("Warnings", "Common")),
                        //do not encode URLs
                        false);
            }

            //prepare model
            var model = _homeModelFactory.PrepareDashboardModel(new DashboardModel());

            return View(model);
        }

        #region Reports

        [HttpPost]
        public virtual IActionResult OrderAverageReportList(OrderAverageReportSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
            {
                //prepare model
                var vendorModel = _orderPendingToClosePaymentModelFactory.PrepareOrderAverageReportListModel(searchModel, _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id);
                return Json(vendorModel);
            }

            //prepare model
            var model = _orderModelFactory.PrepareOrderAverageReportListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult OrderIncompleteReportList(OrderIncompleteReportSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
            {
                var vendorModel = _orderPendingToClosePaymentModelFactory.PrepareOrderIncompleteReportListModel(searchModel, _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id);
                return Json(vendorModel);
            }

            var model = _orderModelFactory.PrepareOrderIncompleteReportListModel(searchModel);

            return Json(model);
        }

        public virtual IActionResult LoadOrderStatistics(string period)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Content(string.Empty);

            int vendorId = 0;

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                vendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;

            var result = new List<object>();

            var nowDt = _dateTimeHelper.ConvertToUserTime(DateTime.Now);
            var timeZone = _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult();

            var culture = new CultureInfo(_workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().LanguageCulture);

            switch (period)
            {
                case "year":
                    //year statistics
                    var yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
                    var searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);
                    for (var i = 0; i <= 12; i++)
                    {
                        result.Add(new
                        {
                            date = searchYearDateUser.Date.ToString("Y", culture),
                            value = _orderService.SearchOrders(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser.AddMonths(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true,
                                vendorId: vendorId
                                ).TotalCount.ToString(),
                        });

                        searchYearDateUser = searchYearDateUser.AddMonths(1);
                    }

                    break;
                case "month":
                    //month statistics
                    var monthAgoDt = nowDt.AddDays(-30);
                    var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                    for (var i = 0; i <= 30; i++)
                    {
                        result.Add(new
                        {
                            date = searchMonthDateUser.Date.ToString("M", culture),
                            value = _orderService.SearchOrders(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser.AddDays(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true,
                                vendorId: vendorId
                                ).TotalCount.ToString()
                        });

                        searchMonthDateUser = searchMonthDateUser.AddDays(1);
                    }

                    break;
                case "week":
                default:
                    //week statistics
                    var weekAgoDt = nowDt.AddDays(-7);
                    var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                    for (var i = 0; i <= 7; i++)
                    {
                        result.Add(new
                        {
                            date = searchWeekDateUser.Date.ToString("d dddd", culture),
                            value = _orderService.SearchOrders(
                                createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser, timeZone),
                                createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser.AddDays(1), timeZone),
                                pageIndex: 0,
                                pageSize: 1, getOnlyTotalCount: true,
                                vendorId: vendorId
                                ).TotalCount.ToString()
                        });

                        searchWeekDateUser = searchWeekDateUser.AddDays(1);
                    }

                    break;
            }

            return Json(result);
        }

        /// <summary>
        /// request of vendors review valoration
        /// </summary>
        [HttpPost]
        public virtual IActionResult GetVendorsReviewValoration()
        {

            DeliveryAppVendorReviewList vendorsReviewsModel = 
                _deliveryAppDriverReviewValorationFactory.GetVendorsReviews();

            return Json(vendorsReviewsModel);
        }

        /// <summary>
        /// request of customer review valorations
        /// </summary>
        [HttpPost]
        public virtual IActionResult GetCustomersReviewValoration()
        {
            var customerReviewsValoration = _deliveryAppDriverReviewValorationFactory
                .GetCustomerReviews();

            return Json(customerReviewsValoration);
        }

        #endregion

        #endregion
    }

}
