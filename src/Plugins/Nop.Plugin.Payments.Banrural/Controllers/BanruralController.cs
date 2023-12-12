using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.Banrural.Enums;
using Nop.Plugin.Payments.Banrural.Helpers;
using Nop.Plugin.Payments.Banrural.Models;
using Nop.Plugin.Payments.Banrural.Services;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.Banrural.Controllers
{
    /// <summary>
    /// Represent the main controller. It's used for configuring the Banrural payment plug-in.
    /// </summary>
    [HttpsRequirement]
    [AutoValidateAntiforgeryToken]
    [ValidateIpAddress]
    public sealed class BanruralController : BasePaymentController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly ILogger _logger;
        private readonly ICheckoutModelFactory _checkoutModelFactory;
        private readonly BanruralServiceManager _banruralService;

        private readonly ShoppingCartSettings _shoppingCartSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of BanruralController class.
        /// </summary>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="shoppingCartSettings">An instance of <see cref="ShoppingCartSettings"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>       
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="checkoutModelFactory">An implementation of <see cref="ICheckoutModelFactory"/></param>
        /// <param name="banruralService">An implementation of <see cref="BanruralServiceManager"/></param>
        public BanruralController(
            IPermissionService permissionService,
            IStoreContext storeContext,
            ISettingService settingService,
            ShoppingCartSettings shoppingCartSettings,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IOrderService orderService,
            IWorkContext workContext,
            IOrderProcessingService orderProcessingService,
            ILogger logger,
            ICheckoutModelFactory checkoutModelFactory,
            BanruralServiceManager banruralService)
        {
            _permissionService = permissionService;
            _storeContext = storeContext;
            _settingService = settingService;
            _shoppingCartSettings = shoppingCartSettings;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _orderService = orderService;
            _workContext = workContext;
            _orderProcessingService = orderProcessingService;
            _logger = logger;
            _checkoutModelFactory = checkoutModelFactory;
            _banruralService = banruralService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares <see cref="BanruralConfigurationModel"/> to send it to Configure.cshtml page
        /// </summary>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            int storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<BanruralSettings>(storeScope);

            BanruralConfigurationModel model = new BanruralConfigurationModel
            {
                KeyID = settings.KeyID,
                Url = settings.Url,
                CancelUrl = settings.CancelUrl,
                CompleteUrl = settings.CompleteUrl,
                CallbackUrl = settings.CallbackUrl,
                Locale = Locale.Es.ToSelectList(true)
                .Select(item => new SelectListItem(item.Text, item.Value))
                    .ToList(),
                OrderStatus = settings.OrderStatus
                    .ToSelectList(true)
                    .Select(item => new SelectListItem(item.Text, item.Value)).ToList(),
                MarkAsPaid = settings.MarkAsPaid
            };

            if (!_shoppingCartSettings.RoundPricesDuringCalculation)
            {
                var url = Url.Action("AllSettings", "Setting", new { settingName = nameof(ShoppingCartSettings.RoundPricesDuringCalculation) });
                var warning = string.Format(_localizationService.GetResource("Plugins.Payments.Banrural.RoundingWarning"), url);
                _notificationService.WarningNotification(warning, false);
            }

            return View("~/Plugins/Payments.Banrural/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets Banrural configuration
        /// </summary>
        /// <param name="model">Banrural configuration model</param>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(BanruralConfigurationModel model)
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
            var settings = _settingService.LoadSetting<BanruralSettings>(storeScope);

            settings.KeyID = model.KeyID;
            settings.Url = model.Url;
            settings.CancelUrl = model.CancelUrl;
            settings.CompleteUrl = model.CompleteUrl;
            settings.CallbackUrl = model.CallbackUrl;
            settings.Locale = (Locale)model.LocaleId;
            settings.OrderStatus = (OrderStatus)model.OrderStatusId;
            settings.MarkAsPaid = model.MarkAsPaid;

            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.KeyID, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Url, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CancelUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CompleteUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CallbackUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Locale, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.OrderStatus, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MarkAsPaid, true, storeScope, false);
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        /// <summary>
        /// Represent the order's transaction details. It's use to redirect to the cancelation page.
        /// </summary>
        /// <param name="transactionResult">Banrural transaction result model</param>
        public IActionResult TransactionDetails(TransactionResult transactionResult)
        {
            Order order = _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1).FirstOrDefault();

            if (order == null)
            {
                _logger.Error("Banrural. Order is not found when trying to receive the transaction result form Banrural.", new NopException());

                return RedirectToRoute("Error");
            }

            _orderProcessingService.DeleteOrder(order);

            _orderProcessingService.ReOrder(order);

            switch (transactionResult.ResponseMessage)
            {
                case TransactionResultStatusHelper.Declined:
                    _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Payments.Banrural.TransactionDeclinedMessage"));
                    break;
                case TransactionResultStatusHelper.Error:
                    _notificationService.ErrorNotification($"{_localizationService.GetResource("Plugins.Payments.Banrural.TransactionErrorMessage")} {transactionResult.ErrorDescription}");
                    break;
                default:
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Payments.Banrural.TransactionCanceledMessage"));
                    break;
            }

            return RedirectToRoute("CheckoutBillingAddress");
        }

        /// <summary>
        /// WebHook executed after a Banrural order is successfully completed.
        /// </summary>
        /// <param name="model">Response model received from Banrural callback webhook.</param>
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Completed([FromBody] BanruralCallbackResponseModel model)
        {
            BanruralTransactionLog banruralTransactionLog;

            try
            {
                Order order;

                if (int.TryParse(model.Order, out int orderId))
                {
                    order = _orderService.GetOrderById(orderId);
                }
                else
                {
                    order = _orderService.SearchOrders(_storeContext.CurrentStore.Id,
                        customerId: _workContext.CurrentCustomer.Id, pageSize: 1).FirstOrDefault();
                }
                
                if (order == null)
                {
                    banruralTransactionLog = new BanruralTransactionLog
                    {
                        CustomerEmail = _workContext.CurrentCustomer.Email,
                        Currency = _workContext.WorkingCurrency.CurrencyCode,
                        Order = model.Order,
                        IsSuccess = false,
                        FullException = "Order not found when trying to receive the transaction result from Banrural."
                    };

                    _banruralService.Log(banruralTransactionLog);

                    return BadRequest(banruralTransactionLog.FullException);
                }

                int storeScope = _storeContext.ActiveStoreScopeConfiguration;
                var settings = _settingService.LoadSetting<BanruralSettings>(storeScope);

                order.PaymentStatus = settings.MarkAsPaid ? PaymentStatus.Paid : PaymentStatus.Pending;
                order.OrderStatus = settings.OrderStatus;

                _orderService.InsertOrderNote(new OrderNote
                {
                    OrderId = order.Id,
                    Note = $"The order has been paid! The Banrural transaction Id is: {model.transaction_id}",
                    DisplayToCustomer = true,
                    CreatedOnUtc = DateTime.UtcNow
                });

                banruralTransactionLog = new BanruralTransactionLog
                {
                    ReferenceNumber = model.Ref,
                    Uuid = model.Uuid,
                    Order = model.Order,
                    Amount = model.Amount,
                    TaxAmount = model.tax_amount,
                    IsSuccess = true,
                    Currency = model.Currency,
                    CustomerEmail = model.customer_email,
                    Description = model.Description,
                    TransactionId = model.transaction_id
                };

                _banruralService.Log(banruralTransactionLog);

                _orderService.UpdateOrder(order);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.InsertLog(LogLevel.Error, $"Hubo un error en el callback de banrural: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");

                banruralTransactionLog = new BanruralTransactionLog
                {
                    CustomerEmail = _workContext.CurrentCustomer.Email,
                    Currency = model.Currency,
                    Order = model.Order,
                    IsSuccess = false,
                    FullException = $"Exception message: {ex.Message}. Inner exception: {ex.InnerException?.Message}"
                };

                _banruralService.Log(banruralTransactionLog);

                return BadRequest(banruralTransactionLog.FullException);
            }
        }

        #endregion
    }
}
