using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.Banrural.Domains;
using Nop.Plugin.Payments.Banrural.Helpers;
using Nop.Plugin.Payments.Banrural.Models;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nop.Core.Domain.Localization;

namespace Nop.Plugin.Payments.Banrural
{
    /// <summary>
    /// Represents the main file for this plug-in.
    /// </summary>
    public class BanruralPlugin : BasePlugin, IPaymentMethod, IWidgetPlugin
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICurrencyService _currencyService;
        private readonly IWorkContext _workContext;
        private readonly IGenericAttributeService _genericAttributeService;
        decimal _orderTotal;
        decimal _orderTax;
        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of BanruralPlugin class.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/></param>
        /// <param name="widgetSettings">An implementation of <see cref="WidgetSettings"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/></param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/></param>
        /// <param name="currencyService">An implementation of <see cref="ICurrencyService"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/></param>
        public BanruralPlugin(ILocalizationService localizationService,
           ILanguageService languageService,
           WidgetSettings widgetSettings,
           ISettingService settingService,
           IUrlHelperFactory urlHelperFactory,
           IActionContextAccessor actionContextAccessor,
           ICurrencyService currencyService,
           IWorkContext workContext,
           IGenericAttributeService genericAttributeService)
        {
            _localizationService = localizationService;
            _languageService = languageService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _currencyService = currencyService;
            _workContext = workContext;
            _genericAttributeService = genericAttributeService;
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public bool SupportCapture => false;

        ///<inheritdoc/>
        public bool SupportPartiallyRefund => false;

        ///<inheritdoc/>
        public bool SupportRefund => false;

        ///<inheritdoc/>
        public bool SupportVoid => false;

        ///<inheritdoc/>
        public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.NotSupported;

        ///<inheritdoc/>
        public PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

        ///<inheritdoc/>
        public bool SkipPaymentInfo => false;

        ///<inheritdoc/>
        public string PaymentMethodDescription => _localizationService.GetResource("Plugins.Payments.Banrural.PaymentMethodDescription");

        /// <summary>
        /// Gets a value indicating whether to hide this plug-in on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => true;

        #endregion

        #region Methods

        /// <summary>
        /// Installs the plug-in
        /// </summary>
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            Language language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(BanruralLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(BanruralLocaleResources.EnglishResources, "en-US");

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
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        /// <summary>
        /// Uninstalls the plug-in
        /// </summary>
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<BanruralSettings>();

            _localizationService.DeletePluginLocaleResources("plugins.payments.banrural");
            base.Uninstall();
        }

        /// <summary>
        /// Gets the configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(DefaultsInfo.ConfigurationRouteName);

        ///<inheritdoc/>
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest) 
            => new CancelRecurringPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        ///<inheritdoc/>
        public bool CanRePostProcessPayment(Order order) => false;

        ///<inheritdoc/>
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest) 
            => new CapturePaymentResult { Errors = new[] { "Capture payment not supported" }};

        ///<inheritdoc/>
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart) => decimal.Zero;

        ///<inheritdoc/>
        public ProcessPaymentRequest GetPaymentInfo(IFormCollection form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            return _actionContextAccessor.ActionContext.HttpContext.Session.Get<ProcessPaymentRequest>(DefaultsInfo.PaymentRequestSessionKey);
        }

        ///<inheritdoc/>
        public string GetPublicViewComponentName() => DefaultsInfo.PAYMENT_INFO_VIEW_COMPONENT_NAME;
        
        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone == null)
            {
                throw new ArgumentNullException(nameof(widgetZone));
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the plugin's widget zones.
        /// </summary>
        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {};
        }

        ///<inheritdoc/>
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart) => false;

        /// <summary>
        /// Post process payment (used to insert order notes)
        /// </summary>
        /// <param name="postProcessPaymentRequest">Order info required to insert order note</param>
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            var banruralSettings = _settingService.LoadSetting(typeof(BanruralSettings)) as BanruralSettings;

            if (banruralSettings == null)
            {
                throw new NullReferenceException("We can not find the Banrural settings");
            }

            _orderTotal = postProcessPaymentRequest.Order.OrderTotal;
            _orderTax = postProcessPaymentRequest.Order.OrderTax;

            RemotePost paymentPageForm = BuildBanruralForm(postProcessPaymentRequest, banruralSettings);

            paymentPageForm.Post();
        }

        private string FormatDecimal(decimal target) => $"{target:0.00}";
                
        private RemotePost BuildBanruralForm(PostProcessPaymentRequest postProcessPaymentRequest, BanruralSettings banruralSettings)
        {            
            string _formattedOrderTotal;
            string _formattedOrderTax;
            
            _orderTotal = _currencyService.ConvertFromPrimaryStoreCurrency(postProcessPaymentRequest.Order.OrderTotal, _currencyService.GetCurrencyByCode(DefaultsInfo.DefaultUSDCurrencyCode));
            _orderTax = _currencyService.ConvertFromPrimaryStoreCurrency(postProcessPaymentRequest.Order.OrderTax, _currencyService.GetCurrencyByCode(DefaultsInfo.DefaultUSDCurrencyCode));

            _formattedOrderTax = FormatDecimal(_orderTax);
            _formattedOrderTotal = FormatDecimal(_orderTotal);

            var paymentPageForm = new RemotePost
            {
                FormName = "paymentForm",
                Url = banruralSettings.Url,
                Method = "post"
            };

            paymentPageForm.Add("_key", banruralSettings.KeyID);
            paymentPageForm.Add("_cancel", banruralSettings.CancelUrl);
            paymentPageForm.Add("_complete", banruralSettings.CompleteUrl);
            paymentPageForm.Add("_callback", banruralSettings.CallbackUrl);
            paymentPageForm.Add("_order_id", postProcessPaymentRequest.Order.CustomOrderNumber);
            paymentPageForm.Add("_order_date", postProcessPaymentRequest.Order.CreatedOnUtc.ToString(CultureInfo.InvariantCulture));
            paymentPageForm.Add("_currency", DefaultsInfo.DefaultUSDCurrencyCode);
            paymentPageForm.Add("_tax_amount", _formattedOrderTax);
            paymentPageForm.Add("_amount", _formattedOrderTotal);
            paymentPageForm.Add("_first_name", _genericAttributeService.GetAttribute<string>(_workContext.CurrentCustomer, NopCustomerDefaults.FirstNameAttribute));
            paymentPageForm.Add("_last_name", _genericAttributeService.GetAttribute<string>(_workContext.CurrentCustomer, NopCustomerDefaults.LastNameAttribute));
            paymentPageForm.Add("_email", _workContext.CurrentCustomer.Email);

            return paymentPageForm;
        }

        /// <summary>
        /// Processes the payment after the order is placed
        /// </summary>
        /// <param name="processPaymentRequest">Order info.</param>
        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest) => new ProcessPaymentResult();

        /// <summary>
        /// Processes the recurring payment after the order is placed
        /// </summary>
        /// <param name="processPaymentRequest">Order info.</param>
        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest) => new ProcessPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        /// <summary>
        /// Processes the payment refund
        /// </summary>
        /// <param name="refundPaymentRequest">Represent the refund payment result.</param>
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest) => new RefundPaymentResult { Errors = new[] { "Refund not supported" }};

        /// <summary>
        /// Validate de payment form.
        /// </summary>
        /// <param name="form">Represent the parse form values sent with the HBttpRequest.</param>
        public IList<string> ValidatePaymentForm(IFormCollection form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            var errors = new List<string>();

            if (form.TryGetValue(nameof(BanruralInfoModel.Errors), out var errorValue) && !StringValues.IsNullOrEmpty(errorValue))
            {
                errors.Add(errorValue.ToString());
            }

            return errors;
        }

        /// <summary>
        /// Represent the a void payment result.
        /// </summary>
        /// <param name="voidPaymentRequest">Represent the a void payment request.</param>
        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest) => new VoidPaymentResult { Errors = new[] { "Void not supported" } };

        #endregion
    }
}