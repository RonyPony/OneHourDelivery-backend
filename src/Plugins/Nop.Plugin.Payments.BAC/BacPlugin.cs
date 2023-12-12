using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Core.Domain.Cms;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.BAC.Domains;
using Nop.Plugin.Payments.BAC.Enums;
using Nop.Plugin.Payments.BAC.Helpers;
using Nop.Plugin.Payments.BAC.Models;
using Nop.Plugin.Payments.BAC.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nop.Plugin.Payments.BAC
{
    /// <summary>
    /// Represents the main class for this plugin.
    /// </summary>
    public sealed class BacPlugin : BasePlugin, IPaymentMethod
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly WidgetSettings _widgetSettings;
        private readonly ISettingService _settingService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly BacServiceManager _bacServiceManager;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IOrderProcessingService _orderProcessingService;

        bool IPaymentMethod.SupportCapture => false;
        bool IPaymentMethod.SupportPartiallyRefund => false;
        bool IPaymentMethod.SupportRefund => false;
        bool IPaymentMethod.SupportVoid => false;
        bool IPaymentMethod.SkipPaymentInfo => false;
        RecurringPaymentType IPaymentMethod.RecurringPaymentType => RecurringPaymentType.NotSupported;
        PaymentMethodType IPaymentMethod.PaymentMethodType => PaymentMethodType.Redirection;
        string IPaymentMethod.PaymentMethodDescription => _localizationService.GetResource("Plugins.Payments.BAC.PaymentMethodDescription");

        /// <summary>
        /// Initializes a new instances of the BacPlugin class with the values pass as parameter.
        /// </summary>
        /// <param name="localizationService">Represents an implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="languageService">Represents an implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="widgetSettings">Represents an instance of <see cref="WidgetSettings"/>.</param>
        /// <param name="settingService">Represents an implementation of <see cref="ISettingService"/>.</param>
        /// <param name="urlHelperFactory">Represents an implementation of <see cref="IUrlHelperFactory"/>.</param>
        /// <param name="actionContextAccessor">Represents an implementation of <see cref="IActionContextAccessor"/>.</param>
        /// <param name="bacServiceManager">Represents an instance of <see cref="BacServiceManager"/>.</param>
        /// <param name="orderService">epresents an implementation of <see cref="IOrderService"/>.</param>
        /// <param name="storeContext">epresents an implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="workContext">epresents an implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="orderProcessingService">epresents an implementation of <see cref="IOrderProcessingService"/>.</param>
        public BacPlugin(ILocalizationService localizationService,
            ILanguageService languageService,
            WidgetSettings widgetSettings,
            ISettingService settingService,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor,
            BacServiceManager bacServiceManager,
            IOrderService orderService,
            IStoreContext storeContext,
            IWorkContext workContext,
            IOrderProcessingService orderProcessingService)
        {
            _localizationService = localizationService;
            _languageService = languageService;
            _widgetSettings = widgetSettings;
            _settingService = settingService;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
            _bacServiceManager = bacServiceManager;
            _orderService = orderService;
            _workContext = workContext;
            _orderProcessingService = orderProcessingService;
            _storeContext = storeContext;
        }

        /// <inheritdoc />
        public override void Install()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(BacLocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(BacLocaleResources.EnglishResources, "en-US");

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

            _settingService.DeleteSetting<BacSettings>();

            _localizationService.DeletePluginLocaleResources("plugins.payments.bac");

            base.Uninstall();
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl() => _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext).RouteUrl(DefaultsInfo.ConfigurationRouteName);

        /// <inheritdoc />
        CancelRecurringPaymentResult IPaymentMethod.CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
            => new CancelRecurringPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        /// <inheritdoc />
        bool IPaymentMethod.CanRePostProcessPayment(Order order) => false;

        /// <inheritdoc />
        CapturePaymentResult IPaymentMethod.Capture(CapturePaymentRequest capturePaymentRequest) => new CapturePaymentResult { Errors = new[] { "Capture payment not supported" } };

        /// <inheritdoc />
        decimal IPaymentMethod.GetAdditionalHandlingFee(IList<ShoppingCartItem> cart) => decimal.Zero;

        /// <inheritdoc />
        ProcessPaymentRequest IPaymentMethod.GetPaymentInfo(IFormCollection form) => new ProcessPaymentRequest();

        /// <inheritdoc />
        string IPaymentMethod.GetPublicViewComponentName() => DefaultsInfo.PaymentInfoViewComponentName;

        /// <inheritdoc />
        bool IPaymentMethod.HidePaymentMethod(IList<ShoppingCartItem> cart) => false;

        /// <inheritdoc />
        void IPaymentMethod.PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            var bacSettings = _settingService.LoadSetting(typeof(BacSettings)) as BacSettings;

            if (bacSettings == null)
            {
                throw new NullReferenceException("We can not find the BAC settings");
            }

            if (!_bacServiceManager.IsConfigured(bacSettings))
            {
                throw new NopException("The BAC Payment Page plugin is not configured.");
            }

            HostedPagePreprocessRequest hostedPageAuthorizationRequest = BuildHostedPagePreprocessRequest(postProcessPaymentRequest, bacSettings);

            HttpClient client = PrepareTokenRequestClient(bacSettings, hostedPageAuthorizationRequest, out var xml);

            var httpRequestMessage = PrepareContentRequestMessage(bacSettings, xml);

            HttpResponseMessage response = client.SendAsync(httpRequestMessage).Result;

            if (!response.IsSuccessStatusCode)
            {
                var logEntry = new BacTransactionLog
                {
                    DateLogged = DateTime.UtcNow,
                    FullException = $"There was an error while trying to get the BAC Token for working with this transaction. \nReason phrase: {response.ReasonPhrase}\nThe error was: {response.RequestMessage}"
                };
                
                _bacServiceManager.Log(logEntry);

                RevertOrder();

                throw new NopException("There was an error while trying to get the BAC Token for working with this transaction.");
            }

            string result = response.Content.ReadAsStringAsync().Result;

            HostedPagePreprocessResponse hostedPagePreprocessResponse = BuildHostedPagePreprocessResponse(result, bacSettings.GatewayUrl);

            var paymentPageForm = new RemotePost
            {
                FormName = "paymentForm",
                Url = $"{bacSettings.HostedPageUrl}/{hostedPagePreprocessResponse.SecurityToken}",
                Method = "get"
            };

            paymentPageForm.Post();
        }

        private static HttpRequestMessage PrepareContentRequestMessage(BacSettings bacSettings, string xml)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, bacSettings.TokenRequestUrl)
            {
                Content = new StringContent(xml, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            int postData = Encoding.UTF8.GetBytes(xml).Length;
            httpRequestMessage.Content.Headers.ContentLength = postData;
            return httpRequestMessage;
        }

        private HttpClient PrepareTokenRequestClient(BacSettings bacSettings, HostedPagePreprocessRequest hostedPageAuthorizationRequest, out string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(HostedPagePreprocessRequest), bacSettings.GatewayUrl);

            using (var memoryStream = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(memoryStream))
            {
                xmlSerializer.Serialize(xmlWriter, hostedPageAuthorizationRequest);
                var utf8 = memoryStream.ToArray();
                xml = Encoding.Default.GetString(utf8);
            }

            var client = new HttpClient
            {
                DefaultRequestHeaders = {Accept = {new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")}}
            };

            return client;
        }

        private HostedPagePreprocessRequest BuildHostedPagePreprocessRequest(PostProcessPaymentRequest postProcessPaymentRequest, BacSettings bacSettings)
        {
            decimal orderTotal = postProcessPaymentRequest.Order.OrderTotal;
            var hostedPageAuthorizationRequest = new HostedPagePreprocessRequest
            {
                CardHolderResponseURL = bacSettings.CardHolderResponseUrl,
                TransactionDetails = new TransactionDetails
                {
                    AcquirerId = bacSettings.AcquirerId,
                    Amount = BacUtilities.FormatAmount(orderTotal),
                    Currency = bacSettings.Currency,
                    CurrencyExponent = bacSettings.CurrencyExponent,
                    MerchantId = bacSettings.MerchantId,
                    OrderNumber = postProcessPaymentRequest.Order.CustomOrderNumber,
                    Signature = BacUtilities.GenerateSignature(bacSettings, postProcessPaymentRequest.Order.CustomOrderNumber, orderTotal),
                    SignatureMethod = bacSettings.SignatureMethod,
                    TransactionCode = (int)TransactionCode.SinglePassTransaction + (int)TransactionCode.HostedPageAuthAnd3DS,
                    CustomerReference = string.Empty
                }
            };

            return hostedPageAuthorizationRequest;
        }

        private void RevertOrder()
        {
            var order = _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1).FirstOrDefault();

            if (order == null)
            {
                var logEntry = new BacTransactionLog
                {
                    DateLogged = DateTime.UtcNow,
                    FullException = "There was an error trying to revert the order. Order not found."
                };

                _bacServiceManager.Log(logEntry);
            }

            _orderProcessingService.DeleteOrder(order);

            _orderProcessingService.ReOrder(order);
        }

        private HostedPagePreprocessResponse BuildHostedPagePreprocessResponse(string hostedPagePreproccesResponseResult, string gatewayUrl)
        {
            var xmlDeserializer = new XmlSerializer(typeof(HostedPagePreprocessResponse), gatewayUrl);
            HostedPagePreprocessResponse hostedPagePreprocessResponse;

            using (var reader = new StringReader(hostedPagePreproccesResponseResult))
            {
                hostedPagePreprocessResponse = (HostedPagePreprocessResponse)xmlDeserializer.Deserialize(reader);
            }

            return hostedPagePreprocessResponse;
        }

        /// <inheritdoc />
        ProcessPaymentResult IPaymentMethod.ProcessPayment(ProcessPaymentRequest processPaymentRequest) => new ProcessPaymentResult();

        /// <inheritdoc />
        ProcessPaymentResult IPaymentMethod.ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest) => new ProcessPaymentResult { Errors = new[] { "Recurring payment not supported" } };

        /// <inheritdoc />
        RefundPaymentResult IPaymentMethod.Refund(RefundPaymentRequest refundPaymentRequest) => new RefundPaymentResult { Errors = new[] { "Refund not supported" } };

        /// <inheritdoc />
        IList<string> IPaymentMethod.ValidatePaymentForm(IFormCollection form) => new List<string>();

        /// <inheritdoc />
        VoidPaymentResult IPaymentMethod.Void(VoidPaymentRequest voidPaymentRequest) => new VoidPaymentResult { Errors = new[] { "Void not supported" } };
    }
}
