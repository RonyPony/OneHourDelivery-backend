using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Orders;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.AzulPaymentPage.Domains;
using Nop.Plugin.Payments.AzulPaymentPage.Helpers;
using Nop.Plugin.Payments.AzulPaymentPage.Models;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Nop.Plugin.Payments.AzulPaymentPage.Services;
using Nop.Services.Directory;
using Nop.Services.Logging;

namespace Nop.Plugin.Payments.AzulPaymentPage
{
    /// <summary>
    /// Represents the main file for this plug-in.
    /// </summary>
    public sealed class AzulPaymentPagePlugin : BasePlugin, IPaymentMethod, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly WidgetSettings _widgetSettings;
        private readonly IWorkContext _workContext;
        private readonly ICurrencyService _currencyService;
        private readonly ILogger _logger;
        private readonly AzulServiceManager _azulServiceManager;

        /// <inheritdoc />
        public bool SupportCapture => false;

        /// <inheritdoc />
        public bool SupportPartiallyRefund => false;

        /// <inheritdoc />
        public bool SupportRefund => false;

        /// <inheritdoc />
        public bool SupportVoid => false;

        /// <inheritdoc />
        public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.NotSupported;

        /// <inheritdoc />
        public PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

        /// <inheritdoc />
        public bool SkipPaymentInfo => false;

        /// <inheritdoc />
        public string PaymentMethodDescription => _localizationService.GetResource("Plugins.Payments.AzulPaymentPage.PaymentMethodDescription");

        /// <inheritdoc />
        public bool HideInWidgetList => true;

        private string _formattedOrderTotal;
        private string _formattedOrderTax;

        /// <summary>
        /// Initializes a new instance of AzulPaymentPagePlugin class.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="widgetSettings">An instance of <see cref="WidgetSettings"/></param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/></param>
        /// <param name="urlHelperFactory">An implementation of <see cref="IUrlHelperFactory"/></param>
        /// <param name="actionContextAccessor">An implementation of <see cref="IActionContextAccessor"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="currencyService">An implementation of <see cref="ICurrencyService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="azulServiceManager">An instance of <see cref="AzulServiceManager"/></param>
        public AzulPaymentPagePlugin(ILocalizationService localizationService,
            ISettingService settingService,
            WidgetSettings widgetSettings,
            ILanguageService languageService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            IWorkContext workContext,
            ICurrencyService currencyService,
            ILogger logger,
            AzulServiceManager azulServiceManager)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _widgetSettings = widgetSettings;
            _languageService = languageService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _workContext = workContext;
            _currencyService = currencyService;
            _logger = logger;
            _azulServiceManager = azulServiceManager;
        }

        /// <inheritdoc />
        public override void Install()
        {
            // Add widget to active widgets
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(AzulLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(AzulLocaleResources.EnglishResources, "en-US");

            base.Install();
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        /// <inheritdoc />
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(DefaultsInfo.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(DefaultsInfo.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<AzulPaymentPageSettings>();

            _localizationService.DeletePluginLocaleResources("plugins.payments.azulpaymentpage");
            base.Uninstall();
        }

        /// <summary>
        /// Gets the configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
            => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(DefaultsInfo.ConfigurationRouteName);

        /// <inheritdoc />
        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                PublicWidgetZones.CheckoutPaymentInfoTop
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
            {
                throw new ArgumentNullException(nameof(widgetZone));
            }

            return string.Empty;
        }

        /// <inheritdoc />
        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest) => new ProcessPaymentResult();

        /// <inheritdoc />
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            AzulPaymentPageSettings azulPaymentPageSettings = _settingService.LoadSetting(typeof(AzulPaymentPageSettings)) as AzulPaymentPageSettings;

            if (azulPaymentPageSettings == null)
            {
                throw new NullReferenceException("We can not find the AZUL Payment Page settings");
            }

            if (!_azulServiceManager.IsConfigured(azulPaymentPageSettings))
            {
                throw new NopException("AZUL Payment Page plugin not configured");
            }

            string joinedValues = ConcatAzulPaymentSettings(postProcessPaymentRequest, azulPaymentPageSettings);

            byte[] joinedBytes = Encoding.UTF8.GetBytes(joinedValues);
            byte[] keyBytes = Encoding.UTF8.GetBytes(azulPaymentPageSettings.AuthKey);

            var authHash = HashString(joinedBytes, keyBytes);

            RemotePost paymentPageForm = BuildAzulPaymentPageForm(postProcessPaymentRequest, azulPaymentPageSettings, authHash);

            paymentPageForm.Post();
        }

        private string ConcatAzulPaymentSettings(PostProcessPaymentRequest postProcessPaymentRequest, AzulPaymentPageSettings azulPaymentPageSettings)
        {
            decimal orderTotal = postProcessPaymentRequest.Order.OrderTotal, orderTax = postProcessPaymentRequest.Order.OrderTax;

            try
            {
                const string dopCurrencyCode = "$";
                if (azulPaymentPageSettings.CurrencyCode == dopCurrencyCode)
                {
                    orderTotal = _currencyService.ConvertFromPrimaryStoreCurrency(postProcessPaymentRequest.Order.OrderTotal, _currencyService.GetCurrencyByCode(DefaultsInfo.DefaultDominicanPesoCurrencyCode));
                    orderTax = _currencyService.ConvertFromPrimaryStoreCurrency(postProcessPaymentRequest.Order.OrderTax, _currencyService.GetCurrencyByCode(DefaultsInfo.DefaultDominicanPesoCurrencyCode));
                }
            }
            catch (Exception e)
            {
                _logger.Error($"AZUL Payment Page. There was an error trying to convert from {_workContext.WorkingCurrency} to DOP.", e, _workContext.CurrentCustomer);
            }

            _formattedOrderTax = FormatDecimal(orderTax);
            _formattedOrderTotal = FormatDecimal(orderTotal);

            StringBuilder joinedValues = new StringBuilder();

            joinedValues.Append(azulPaymentPageSettings.MerchantId);
            joinedValues.Append(azulPaymentPageSettings.MerchantName);
            joinedValues.Append(azulPaymentPageSettings.MerchantType);
            joinedValues.Append(azulPaymentPageSettings.CurrencyCode);
            joinedValues.Append(postProcessPaymentRequest.Order.CustomOrderNumber);
            joinedValues.Append(_formattedOrderTotal.Replace(".", string.Empty));
            joinedValues.Append(_formattedOrderTax.Replace(".", string.Empty));
            joinedValues.Append(azulPaymentPageSettings.ApprovedUrl);
            joinedValues.Append(azulPaymentPageSettings.DeclinedUrl);
            joinedValues.Append(azulPaymentPageSettings.CancelUrl);
            joinedValues.Append(azulPaymentPageSettings.UseCustomField1 ? 1 : 0);
            joinedValues.Append(azulPaymentPageSettings.CustomField1Label);
            joinedValues.Append(azulPaymentPageSettings.CustomField1Value);
            joinedValues.Append(azulPaymentPageSettings.UseCustomField2 ? 1 : 0);
            joinedValues.Append(azulPaymentPageSettings.CustomField2Label);
            joinedValues.Append(azulPaymentPageSettings.CustomField2Value);
            joinedValues.Append(azulPaymentPageSettings.AuthKey);

            return joinedValues.ToString();
        }

        private string FormatDecimal(decimal target) => $"{target:0.00}";

        private string HashString(byte[] joinedBytes, byte[] keyBytes)
        {
            string authHash = string.Empty;

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(joinedBytes);

                for (int i = 0; i < hashValue.Length; i++)
                {
                    authHash += string.Format("{0:x2}", hashValue[i]);
                }
            }

            return authHash;
        }

        private RemotePost BuildAzulPaymentPageForm(PostProcessPaymentRequest postProcessPaymentRequest, AzulPaymentPageSettings azulPaymentPageSettings, string authHash)
        {
            var paymentPageForm = new RemotePost
            {
                FormName = "paymentForm",
                Url = azulPaymentPageSettings.Url,
                Method = "post"
            };

            paymentPageForm.Add("MerchantId", azulPaymentPageSettings.MerchantId);
            paymentPageForm.Add("MerchantName", azulPaymentPageSettings.MerchantName);
            paymentPageForm.Add("MerchantType", azulPaymentPageSettings.MerchantType);
            paymentPageForm.Add("CurrencyCode", azulPaymentPageSettings.CurrencyCode);
            paymentPageForm.Add("OrderNumber", postProcessPaymentRequest.Order.CustomOrderNumber);
            paymentPageForm.Add("Amount", _formattedOrderTotal.Replace(".", string.Empty));
            paymentPageForm.Add("ITBIS", _formattedOrderTax.Replace(".", string.Empty));
            paymentPageForm.Add("ApprovedUrl", azulPaymentPageSettings.ApprovedUrl);
            paymentPageForm.Add("DeclinedUrl", azulPaymentPageSettings.DeclinedUrl);
            paymentPageForm.Add("CancelUrl", azulPaymentPageSettings.CancelUrl);
            paymentPageForm.Add("UseCustomField1", azulPaymentPageSettings.UseCustomField1 ? "1" : "0");
            paymentPageForm.Add("CustomField1Label", azulPaymentPageSettings.CustomField1Label);
            paymentPageForm.Add("CustomField1Value", azulPaymentPageSettings.CustomField1Value);
            paymentPageForm.Add("UseCustomField2", azulPaymentPageSettings.UseCustomField2 ? "1" : "0");
            paymentPageForm.Add("CustomField2Label", azulPaymentPageSettings.CustomField2Label);
            paymentPageForm.Add("CustomField2Value", azulPaymentPageSettings.CustomField2Value);
            paymentPageForm.Add("ShowTransactionResult", azulPaymentPageSettings.ShowTransactionResult ? "1" : "0");
            paymentPageForm.Add("AuthHash", authHash);

            return paymentPageForm;
        }

        /// <inheritdoc />
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart) => false;

        /// <inheritdoc />
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart) => decimal.Zero;

        /// <inheritdoc />
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest) => new CapturePaymentResult { Errors = new[] { "Capture payment not supported" } };

        /// <inheritdoc />
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest) => new RefundPaymentResult { Errors = new[] { "Refund not supported" } };

        /// <inheritdoc />
        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest) => new VoidPaymentResult { Errors = new[] { "Void not supported" } };

        /// <inheritdoc />
        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
            => new ProcessPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        /// <inheritdoc />
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
            => new CancelRecurringPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        /// <inheritdoc />
        public bool CanRePostProcessPayment(Order order) => false;

        /// <inheritdoc />
        public IList<string> ValidatePaymentForm(IFormCollection form) => new List<string>();

        /// <inheritdoc />
        public ProcessPaymentRequest GetPaymentInfo(IFormCollection form) => new ProcessPaymentRequest();

        /// <inheritdoc />
        public string GetPublicViewComponentName() => DefaultsInfo.PaymentInfoViewComponentName;
    }
}
