using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.BAC.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Linq;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.BAC.Domains;
using Nop.Plugin.Payments.BAC.Enums;
using Nop.Plugin.Payments.BAC.Services;

namespace Nop.Plugin.Payments.BAC.Controllers
{
    /// <summary>
    ///  Represent the main controller. It's used for configuring the BAC payment plugin.
    /// </summary>
    public sealed class BacPaymentController : BasePaymentController
    {
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly BacServiceManager _bacServiceManager;

        /// <summary>
        /// Initializes a new instance of BacPaymentController class.
        /// </summary>
        /// <param name="permissionService">Represents an implementation of <see cref="IPermissionService"/>.</param>
        /// <param name="storeContext">Represents an implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="notificationService">Represents an implementation of <see cref="INotificationService"/>.</param>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="orderService">Represents an implementation of <see cref="IOrderService"/>.</param>
        /// <param name="workContext">Represents an implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="logger">Represents an implementation of <see cref="ILogger"/>.</param>
        /// <param name="orderProcessingService">Represents an implementation of <see cref="IOrderProcessingService"/>.</param>
        /// <param name="bacServiceManager">Represents an instance of <see cref="BacServiceManager"/>.</param>
        public BacPaymentController(IPermissionService permissionService,
            IStoreContext storeContext,
            ISettingService settingService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IOrderService orderService,
            IWorkContext workContext,
            ILogger logger,
            IOrderProcessingService orderProcessingService,
            BacServiceManager bacServiceManager)
        {
            _permissionService = permissionService;
            _storeContext = storeContext;
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _orderService = orderService;
            _workContext = workContext;
            _logger = logger;
            _orderProcessingService = orderProcessingService;
            _bacServiceManager = bacServiceManager;
        }

        /// <summary>
        /// Prepare and retrieves the configure view.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the configure view for this plugin.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<BacSettings>(storeScope);

            var model = new BacConfigurationModel
            {
                MerchantId = settings.MerchantId,
                MerchantPassword = settings.MerchantPassword,
                CurrencyExponent = settings.CurrencyExponent,
                AcquirerId = settings.AcquirerId,
                GatewayUrl = settings.GatewayUrl,
                CardHolderResponseUrl = settings.CardHolderResponseUrl,
                Currency = settings.Currency,
                MarkAsPaid = settings.MarkAsPaid,
                SignatureMethod = settings.SignatureMethod,
                HostedPageUrl = settings.HostedPageUrl,
                OrderStatus = OrderStatus.Pending.ToSelectList(true)
                    .Select(item => new SelectListItem(item.Text, item.Value))
                    .ToList(),
                TokenRequestUrl = settings.TokenRequestUrl
            };

            return View("~/Plugins/Payments.BAC/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Saves the configuration for this plugin.
        /// </summary>
        /// <returns>an <see cref="IActionResult"/> with the saved configuration.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(BacConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            if (!ModelState.IsValid)
            {
                return Configure();
            }

            int storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<BacSettings>(storeScope);

            settings.MerchantId = model.MerchantId;
            settings.MerchantPassword = model.MerchantPassword;
            settings.CurrencyExponent = model.CurrencyExponent;
            settings.AcquirerId = model.AcquirerId;
            settings.GatewayUrl = model.GatewayUrl;
            settings.CardHolderResponseUrl = model.CardHolderResponseUrl;
            settings.Currency = model.Currency;
            settings.MarkAsPaid = model.MarkAsPaid;
            settings.SignatureMethod = model.SignatureMethod;
            settings.HostedPageUrl = model.HostedPageUrl;
            settings.OrderStatus = (OrderStatus) model.OrderStatusId;
            settings.TokenRequestUrl = model.TokenRequestUrl;

            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MerchantId, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MerchantPassword, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CurrencyExponent, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.AcquirerId, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.GatewayUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CardHolderResponseUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Currency, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MarkAsPaid, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.SignatureMethod, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.HostedPageUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.OrderStatus, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.TokenRequestUrl, true, storeScope, false);

            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        /// <summary>
        /// Represents the endpoint used for capturing the response of the BAC after finishing a transaction.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> with the corresponding view according to the transaction result.</returns>
        public IActionResult TransactionDetails(BacTransactionResult bacTransactionResult)
        {
            Order order = _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1).FirstOrDefault();

            if (order == null)
            {
                _logger.Error("BAC Payment Page. Order is not found when trying to receive the transaction result form BAC", new NopException());

                return RedirectToRoute("Error");
            }

            var logEntry = new BacTransactionLog
            {
                DateLogged = DateTime.UtcNow,
                OrderId = order.Id,
                ResponseCode = bacTransactionResult.RespCode,
                ReasonCode = bacTransactionResult.ReasonCode
            };
            
            if (bacTransactionResult.RespCode == (int)TransactionResultCode.Approved)
            {
                int storeScope = _storeContext.ActiveStoreScopeConfiguration;
                var settings = _settingService.LoadSetting<BacSettings>(storeScope);
                order.PaymentStatus = settings.MarkAsPaid ? PaymentStatus.Paid : PaymentStatus.Pending;
                order.OrderStatus = settings.OrderStatus;

                _orderService.InsertOrderNote(new OrderNote
                {
                    OrderId = order.Id,
                    Note = $"The order has been paid! The BAC order id is: {bacTransactionResult.ID}",
                    DisplayToCustomer = true,
                    CreatedOnUtc = DateTime.UtcNow
                });

                _bacServiceManager.Log(logEntry);

                _orderService.UpdateOrder(order);

                return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
            }

            _orderProcessingService.DeleteOrder(order);

            _orderProcessingService.ReOrder(order);

            switch (bacTransactionResult.RespCode)
            {
                case (int)TransactionResultCode.Declined:
                    _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Payments.BAC.TransactionDeclinedMessage"));
                    break;
                case (int)TransactionResultCode.Error:
                    _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Payments.BAC.TransactionErrorMessage"));
                    break;
                default:
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Payments.BAC.TransactionCanceledMessage"));
                    break;
            }

            _bacServiceManager.Log(logEntry);

            return RedirectToRoute("CheckoutBillingAddress");
        }
    }
}