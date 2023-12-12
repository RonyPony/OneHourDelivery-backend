using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Payments.CyberSource
{
    /// <summary>
    /// CyberSource payment processor
    /// </summary>
    public class CyberSourcePaymentProcessor : BasePlugin, IWidgetPlugin, IPaymentMethod
    {
        #region Fields

        private readonly CyberSourcePaymentSettings _cyberSourcePaymentSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ILanguageService _languageService;
        private readonly WidgetSettings _widgetSettings;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="cyberSourcePaymentSettings">Instance of <see cref="CyberSourcePaymentSettings"/></param>
        /// <param name="localizationService">Implementation of <see cref="ILocalizationService"/></param>
        /// <param name="settingService">Implementation of <see cref="ISettingService"/></param>
        /// <param name="webHelper">Implementation of <see cref="IWebHelper"/></param>
        /// <param name="languageService">Implementation of <see cref="ILanguageService"/></param>
        /// <param name="widgetSettings">Instance of <see cref="WidgetSettings"/></param>
        /// <param name="messageTemplateService">Implementation of <see cref="IMessageTemplateService"/></param>
        /// <param name="httpContextAccessor">An implementation of <see cref="IHttpContextAccessor"/></param>
        public CyberSourcePaymentProcessor(
            CyberSourcePaymentSettings cyberSourcePaymentSettings,
            ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            ILanguageService languageService,
            WidgetSettings widgetSettings,
            IMessageTemplateService messageTemplateService,
            IHttpContextAccessor httpContextAccessor)
        {
            _cyberSourcePaymentSettings = cyberSourcePaymentSettings;
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _languageService = languageService;
            _widgetSettings = widgetSettings;
            _messageTemplateService = messageTemplateService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Utilities

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value,
                    languageLanguageCulture);
            }
        }

        private void RegisterPaymentDeclinedMessageTemplates()
        {
            const int DEFAULT_EMAIL_ACCOUNT_ID = 1;
            foreach (string templateName in CyberSourceDefaults.MessageTemplateNames)
            {
                if (_messageTemplateService.GetMessageTemplatesByName(templateName).Any())
                    continue;

                var newMessageTemplate = new MessageTemplate
                {
                    Name = templateName,
                    BccEmailAddresses = null,
                    Subject = CyberSourceDefaults.OrderNotPaidNotificationTemplateSubject,
                    Body = CyberSourceDefaults.MessageTemplateBodies[templateName],
                    IsActive = true,
                    AttachedDownloadId = 0,
                    EmailAccountId = DEFAULT_EMAIL_ACCOUNT_ID,
                    LimitedToStores = false,
                    DelayBeforeSend = null,
                    DelayPeriod = MessageDelayPeriod.Hours
                };

                _messageTemplateService.InsertMessageTemplate(newMessageTemplate);
            }
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult { NewPaymentStatus = PaymentStatus.Pending };
            return result;
        }

        /// <inheritdoc />
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            if (postProcessPaymentRequest.Order.PaymentStatus == PaymentStatus.Pending)
            {
                _httpContextAccessor.HttpContext.Response.Redirect($"/{CyberSourceDefaults.RetryPaymentBaseUrl}{postProcessPaymentRequest.Order.Id}");
                return;
            }
        }

        /// <inheritdoc />
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            return false;
        }

        /// <inheritdoc />
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            return _cyberSourcePaymentSettings.AdditionalFee;
        }

        /// <inheritdoc />
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var result = new CapturePaymentResult();
            result.AddError("Capture method not supported");
            return result;
        }

        /// <inheritdoc />
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var result = new RefundPaymentResult();
            result.AddError("Refund method not supported");
            return result;
        }

        /// <inheritdoc />
        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var result = new VoidPaymentResult();
            result.AddError("Void method not supported");
            return result;
        }

        /// <inheritdoc />
        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.AddError("Recurring payment not supported");
            return result;
        }

        /// <inheritdoc />
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            var result = new CancelRecurringPaymentResult();
            result.AddError("Recurring payment not supported");
            return result;
        }

        /// <inheritdoc />
        public bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            //CyberSource is the redirection payment method
            //it also validates whether order is also paid (after redirection) so customers will not be able to pay twice

            //payment status should be Pending
            return order.PaymentStatus == PaymentStatus.Pending;
        }

        /// <inheritdoc />
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/PaymentCyberSource/Configure";
        }

        /// <inheritdoc />
        public string GetPublicViewComponentName()
        {
            return "PaymentCyberSource";
        }

        /// <inheritdoc />
        public IList<string> ValidatePaymentForm(IFormCollection form)
        {
            var warnings = new List<string>();
            return warnings;
        }

        /// <inheritdoc />
        public ProcessPaymentRequest GetPaymentInfo(IFormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();
            return paymentInfo;
        }

        /// <inheritdoc />
        public override void Install()
        {
            if (!_widgetSettings.ActiveWidgetSystemNames.Contains(CyberSourceDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Add(CyberSourceDefaults.SystemName);
            }

            var settings = new CyberSourcePaymentSettings
            {
                MarkAsPaid = true,
                OrderStatus = OrderStatus.Complete
            };

            _settingService.SaveSetting(settings);

            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(CyberSourceLocalResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(CyberSourceLocalResources.EnglishResources, "en-US");

            RegisterPaymentDeclinedMessageTemplates();

            base.Install();
        }

        /// <inheritdoc />
        public override void Uninstall()
        {
            if (_widgetSettings.ActiveWidgetSystemNames.Contains(CyberSourceDefaults.SystemName))
            {
                _widgetSettings.ActiveWidgetSystemNames.Remove(CyberSourceDefaults.SystemName);
                _settingService.SaveSetting(_widgetSettings);
            }

            _settingService.DeleteSetting<CyberSourcePaymentSettings>();

            _localizationService.DeletePluginLocaleResources("Plugins.Payments.CyberSource");

            base.Uninstall();
        }

        ///<inheritdoc/>
        public IList<string> GetWidgetZones()
            => new List<string> { PublicWidgetZones.CheckoutCompletedTop };

        ///<inheritdoc/>
        public string GetWidgetViewComponentName(string widgetZone)
            => CyberSourceDefaults.WidgetZonesViewComponentsDictionary[widgetZone];

        #endregion

        #region Properies

        /// <inheritdoc />
        public bool SupportCapture
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool SupportPartiallyRefund
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool SupportRefund
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool SupportVoid
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc />
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.NotSupported;
            }
        }

        /// <inheritdoc />
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Redirection;
            }
        }

        /// <inheritdoc />
        public bool SkipPaymentInfo
        {
            get { return false; }
        }

        /// <inheritdoc />
        public string PaymentMethodDescription
        {
            get { return _localizationService.GetResource("Plugins.Payments.CyberSource.PaymentMethodDescription"); }
        }

        ///<inheritdoc/>
        public bool HideInWidgetList => false;

        #endregion
    }
}
