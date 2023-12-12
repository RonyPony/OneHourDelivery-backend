using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.CardNet.Domains;
using Nop.Plugin.Payments.CardNet.Extensions;
using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Plugin.Payments.CardNet.Models;
using Nop.Plugin.Payments.CardNet.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Payments.CardNet
{
    /// <summary>
    /// Main file for this plug-in
    /// </summary>
    public class CardNetPlugin : BasePlugin, IPaymentMethod, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly ILanguageService _languageService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IMemoryCache _memoryCache;
        private readonly CardNetService _cardNetService;
        private readonly IOrderService _orderService;
        private readonly ICurrencyService _currencyService;

        #endregion

        #region Ctor

        public CardNetPlugin(ILocalizationService localizationService, IWebHelper webHelper,
            ILanguageService languageService, WidgetSettings widgetSettings, ISettingService settingService,
            IMemoryCache memoryCache, CardNetService cardNetService, IOrderService orderService,
            ICurrencyService currencyService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _languageService = languageService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _memoryCache = memoryCache;
            _cardNetService = cardNetService;
            _orderService = orderService;
            _currencyService = currencyService;
        }

        #endregion

        #region Properties

        public bool SupportCapture => false;
        public bool SupportPartiallyRefund => false;
        public bool SupportRefund => false;
        public bool SupportVoid => false;
        public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.NotSupported;
        public PaymentMethodType PaymentMethodType => PaymentMethodType.Standard;
        public bool SkipPaymentInfo => false;
        public string PaymentMethodDescription => _localizationService.GetResource("Plugins.Payments.CardNet.PaymentMethodDescription");
        public bool HideInWidgetList => true;

        #endregion

        #region Methods

        /// <summary>
        /// Gets Configure.cshtml view route
        /// </summary>
        /// <returns></returns>
        public override string GetConfigurationPageUrl() => $"{_webHelper.GetStoreLocation()}Admin/CardNet/Configure";

        /// <summary>
        /// Installs the plug-in
        /// </summary>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(CardNetDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(CardNetDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            // Add language resources
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(CardNetLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(CardNetLocaleResources.EnglishResources, "en-US");

            base.Install();
        }

        /// <summary>
        /// Adds locale language resources to database
        /// </summary>
        /// <param name="resources">Resources to add</param>
        /// <param name="languageLanguageCulture">Language culture of the resources to add</param>
        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value,
                    languageLanguageCulture);
            }
        }

        /// <summary>
        /// Uninstalls the plug-in
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(CardNetDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(CardNetDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<CardNetSettings>();

            _localizationService.DeletePluginLocaleResources("plugins.payments.cardnet");

            base.Uninstall();
        }

        /// <summary>
        /// Processes the payment after the order is placed
        /// </summary>
        /// <param name="processPaymentRequest">Process payment result</param>
        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            // Get One Time Token saved in cache
            string orderToken = _memoryCache.Get<string>(CardNetDefaults.CacheTokenName);

            decimal orderTotal = processPaymentRequest.OrderTotal;
            string shortenedOrderNumber = processPaymentRequest.OrderGuid.ToString().Split("-").First();

            var purchase = new CardNetPurchaseModel
            { 
                TrxToken = orderToken,
                Order = shortenedOrderNumber,
                Amount = orderTotal.ToDefaultCardNetCurrency(_currencyService).FormatAsCardNetAmount(),
                Currency = CardNetDefaults.DefaultCurrencyCode,
                Capture = true,
                DataDo = new CardNetDataDoModel
                {
                    Invoice = shortenedOrderNumber,
                    Tax = 0
                }
            };

            // Process payment on CardNet API
            var (purchaseResult, error) = _cardNetService.ProcessPayment(purchase);

            if (!string.IsNullOrEmpty(error))
                return new ProcessPaymentResult {Errors = new[] {error}};

            if (purchaseResult.Errors.Any())
                return new ProcessPaymentResult {Errors = purchaseResult.Errors.Select(err => err.Message).ToList()};

            if (purchaseResult.Response.Transaction.Status == CardNetStatus.Rejected)
                return new ProcessPaymentResult { Errors = new[] { _localizationService.GetResource("Plugins.Payments.CardNet.RejectedTransactionError") } };

            _memoryCache.Remove(CardNetDefaults.CacheTokenName);

            return new ProcessPaymentResult
            {
                CaptureTransactionId = purchaseResult.Response.Transaction.ApprovalCode,
                CaptureTransactionResult = purchaseResult.Response.Transaction.Status
            };
        }

        /// <summary>
        /// Post process payment (used to insert order notes)
        /// </summary>
        /// <param name="postProcessPaymentRequest">Order info required to insert order note</param>
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = postProcessPaymentRequest.Order.Id,
                Note = $"{_localizationService.GetResource("Plugins.Payments.CardNet.OrderPaidMessage")} {postProcessPaymentRequest.Order.CaptureTransactionId}",
                DisplayToCustomer = true,
                CreatedOnUtc = DateTime.UtcNow
            });

            postProcessPaymentRequest.Order.PaymentStatus = PaymentStatus.Paid;

            var logEntry = new CardNetTransactionLog
            {
                OrderId = postProcessPaymentRequest.Order.Id,
                Amount = postProcessPaymentRequest.Order.OrderTotal.ToDefaultCardNetCurrency(_currencyService),
                Currency = postProcessPaymentRequest.Order.CustomerCurrencyCode,
                ApprovalCode = postProcessPaymentRequest.Order.CaptureTransactionId,
                ResultType = CardNetStatus.Approved,
                DateLogged = DateTime.Now
            };

            _cardNetService.Log(logEntry);

            _orderService.UpdateOrder(postProcessPaymentRequest.Order);
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                PublicWidgetZones.CheckoutPaymentInfoTop,
                PublicWidgetZones.OpcContentBefore
            };
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone == null)
                throw new ArgumentNullException(nameof(widgetZone));

            if (widgetZone.Equals(PublicWidgetZones.CheckoutPaymentInfoTop) || widgetZone.Equals(PublicWidgetZones.OpcContentBefore))
                return CardNetDefaults.ScriptViewComponentName;

            return string.Empty;
        }

        public bool HidePaymentMethod(IList<ShoppingCartItem> cart) => false;

        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart) => decimal.Zero;

        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest) => new CapturePaymentResult
            {Errors = new[] {"Capture payment not supported"}};

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest) => new RefundPaymentResult
            {Errors = new[] {"Refund not supported"}};

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest) =>
            new VoidPaymentResult {Errors = new[] {"Void not supported"}};

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest) =>
            new ProcessPaymentResult {Errors = new[] {"Recurring payment not supported"}};

        public CancelRecurringPaymentResult
            CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest) =>
            new CancelRecurringPaymentResult {Errors = new[] {"Recurring payment not supported"}};

        public bool CanRePostProcessPayment(Order order) => false;

        public IList<string> ValidatePaymentForm(IFormCollection form) => new List<string>();

        public ProcessPaymentRequest GetPaymentInfo(IFormCollection form) => new ProcessPaymentRequest();

        public string GetPublicViewComponentName() => CardNetDefaults.PaymentInfoViewComponentName;

        #endregion
    }
}