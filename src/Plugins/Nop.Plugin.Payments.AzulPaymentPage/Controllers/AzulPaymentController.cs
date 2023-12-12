using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.AzulPaymentPage.Enums;
using Nop.Plugin.Payments.AzulPaymentPage.Helpers;
using Nop.Plugin.Payments.AzulPaymentPage.Models;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;
using Nop.Plugin.Payments.AzulPaymentPage.Domains;
using Nop.Plugin.Payments.AzulPaymentPage.Services;

namespace Nop.Plugin.Payments.AzulPaymentPage.Controllers
{
    /// <summary>
    /// Represent the main controller. It's used for configuring the AZUL payment plug-in.
    /// </summary>
    [HttpsRequirement]
    [AutoValidateAntiforgeryToken]
    [ValidateIpAddress]
    public sealed class AzulPaymentController : BasePaymentController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly ILogger _logger;
        private readonly AzulServiceManager _azulServiceManager;
        
        /// <summary>
        /// Initializes a new instance of AzulPaymentController class.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="shoppingCartSettings">An instance of <see cref="ShoppingCartSettings"/></param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="azulServiceManager">An instance of <see cref="AzulServiceManager"/></param>
        public AzulPaymentController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            ShoppingCartSettings shoppingCartSettings,
            IOrderService orderService,
            IWorkContext workContext,
            IOrderProcessingService orderProcessingService,
            ILogger logger,
            AzulServiceManager azulServiceManager)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
            _shoppingCartSettings = shoppingCartSettings;
            _orderService = orderService;
            _workContext = workContext;
            _orderProcessingService = orderProcessingService;
            _logger = logger;
            _azulServiceManager = azulServiceManager;
        }

        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<AzulPaymentPageSettings>(storeScope);

            var model = new AzulConfigurationModel
            {
                Url = settings.Url,
                AlternativeUrl = settings.AlternativeUrl,
                MerchantId = settings.MerchantId,
                MerchantName = settings.MerchantName,
                MerchantType = settings.MerchantType,
                AuthKey = settings.AuthKey,
                CurrencyCode = settings.CurrencyCode,
                ApprovedUrl = settings.ApprovedUrl,
                CancelUrl = settings.CancelUrl,
                DeclinedUrl = settings.DeclinedUrl,
                UseCustomField1 = settings.UseCustomField1,
                CustomField1Label = settings.CustomField1Label,
                CustomField1Value = settings.CustomField1Value,
                UseCustomField2 = settings.UseCustomField2,
                CustomField2Label = settings.CustomField2Label,
                CustomField2Value = settings.CustomField2Value,
                ShowTransactionResult = settings.ShowTransactionResult,
                Locale = Locale.Es.ToSelectList(false).Select(item => new SelectListItem(item.Text, item.Value)).ToList(),
                OrderStatus = OrderStatus.Pending.ToSelectList(false).Select(item => new SelectListItem(item.Text, item.Value)).ToList(),
                MarkAsPaid = settings.MarkAsPaid
            };

            if (!_shoppingCartSettings.RoundPricesDuringCalculation)
            {
                var url = Url.Action("AllSettings", "Setting", new { settingName = nameof(ShoppingCartSettings.RoundPricesDuringCalculation) });
                var warning = string.Format(_localizationService.GetResource("Plugins.Payments.AzulPaymentPage.RoundingWarning"), url);
                _notificationService.WarningNotification(warning, false);
            }

            return View("~/Plugins/Payments.AzulPaymentPage/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(AzulConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
            {
                return AccessDeniedView();
            }

            if (!ModelState.IsValid)
            {
                return Configure();
            }

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<AzulPaymentPageSettings>(storeScope);

            settings.Url = model.Url;
            settings.AlternativeUrl = model.AlternativeUrl;
            settings.MerchantId = model.MerchantId;
            settings.MerchantName = model.MerchantName;
            settings.MerchantType = model.MerchantType;
            settings.AuthKey = model.AuthKey;
            settings.CurrencyCode = model.CurrencyCode;
            settings.ApprovedUrl = model.ApprovedUrl;
            settings.CancelUrl = model.CancelUrl;
            settings.DeclinedUrl = model.DeclinedUrl;
            settings.UseCustomField1 = model.UseCustomField1;
            settings.CustomField1Label = model.CustomField1Label;
            settings.CustomField1Value = model.CustomField1Value;
            settings.UseCustomField2 = model.UseCustomField2;
            settings.CustomField2Label = model.CustomField2Label;
            settings.CustomField2Value = model.CustomField2Value;
            settings.ShowTransactionResult = model.ShowTransactionResult;
            settings.Locale = (Locale)model.LocaleId;
            settings.OrderStatus = (OrderStatus)model.OrderStatusId;
            settings.MarkAsPaid = model.MarkAsPaid;

            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Url, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.AlternativeUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MerchantId, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MerchantName, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MerchantType, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.AuthKey, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CurrencyCode, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.ApprovedUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CancelUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.DeclinedUrl, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.UseCustomField1, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CustomField1Label, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CustomField1Value, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.UseCustomField2, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CustomField2Label, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.CustomField2Value, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.ShowTransactionResult, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.Locale, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.OrderStatus, true, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(settings, setting => setting.MarkAsPaid, true, storeScope, false);

            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        public IActionResult TransactionDetails(TransactionResult transactionResult)
        {
            var order = _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1).FirstOrDefault();

            if (order == null)
            {
                _logger.Error("AZUL Payment Page. Order is not found when trying to receive the transaction result form AZUL", new NopException());

                return RedirectToRoute("Error");
            }

            AzulPaymentTransactionLog azulLogEntry = new AzulPaymentTransactionLog
            {
                Amount = order.OrderTotal,
                AuthorizationCode = transactionResult.AzulOrderId,
                DateLogged = DateTime.Now,
                IsoCode = transactionResult.IsoCode,
                Itbis = order.OrderTax,
                OrderNumber = transactionResult.OrderNumber,
                ResponseCode = transactionResult.ResponseCode,
                ResponseMessage = transactionResult.ResponseMessage,
                Rrn = transactionResult.Rrn,
                TransactionDate = transactionResult.DateTime
            };

            if (transactionResult.ResponseMessage == TransactionResultStatusHelper.Approved)
            {
                var storeScope = _storeContext.ActiveStoreScopeConfiguration;
                var settings = _settingService.LoadSetting<AzulPaymentPageSettings>(storeScope);

                order.PaymentStatus = settings.MarkAsPaid ? PaymentStatus.Paid : PaymentStatus.Pending;
                order.OrderStatus = settings.OrderStatus;

                _orderService.InsertOrderNote(new OrderNote
                {
                    OrderId = order.Id,
                    Note = $"The order has been paid! The AZUL order id is: {transactionResult.AzulOrderId} and the reference number is: {transactionResult.Rrn}",
                    DisplayToCustomer = true,
                    CreatedOnUtc = DateTime.UtcNow
                });

                _orderService.UpdateOrder(order);

                _azulServiceManager.Log(azulLogEntry);

                return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
            }

            _orderProcessingService.DeleteOrder(order);

            _orderProcessingService.ReOrder(order);

            switch (transactionResult.ResponseMessage)
            {
                case TransactionResultStatusHelper.Declined:
                    _notificationService.ErrorNotification(_localizationService.GetResource("Plugins.Payments.AzulPaymentPage.TransactionDeclinedMessage"));
                    break;
                case TransactionResultStatusHelper.Error:
                    _notificationService.ErrorNotification($"{_localizationService.GetResource("Plugins.Payments.AzulPaymentPage.TransactionErrorMessage")} {transactionResult.ErrorDescription}");
                    break;
                default:
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Payments.AzulPaymentPage.TransactionCanceledMessage"));
                    break;
            }

            _azulServiceManager.Log(azulLogEntry);

            return RedirectToRoute("CheckoutBillingAddress");
        }

    }
}
