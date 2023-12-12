using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Orders;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.Banrural.Helpers;
using Nop.Plugin.Payments.Banrural.Models;
using Nop.Plugin.Payments.Banrural.Services;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Payments;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.Banrural.Components
{
    /// <summary>
    /// Represents the view component to display payment info in public store
    /// </summary>
    [ViewComponent(Name = DefaultsInfo.PAYMENT_INFO_VIEW_COMPONENT_NAME)]
    public sealed class BanruralViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IPaymentService _paymentService;
        private readonly BanruralServiceManager _serviceManager;
        private readonly BanruralSettings _settings;
        private readonly ILocalizationService _localizationService;
        private readonly OrderSettings _orderSettings;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of Banrural Viewcomponent.
        /// </summary>
        /// <param name="paymentService">An implementation of <see cref="IPaymentService"/></param>
        /// <param name="serviceManager">An implementation of <see cref="BanruralServiceManager"/></param>
        /// <param name="settings">An implementation of <see cref="BanruralSettings"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="orderSettings">An implementation of <see cref="OrderSettings"/></param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
        public BanruralViewComponent(
            IPaymentService paymentService,
            BanruralServiceManager serviceManager,
            BanruralSettings settings,
            ILocalizationService localizationService,
            OrderSettings orderSettings,
            INotificationService notificationService)
        {
            _paymentService = paymentService;
            _serviceManager = serviceManager;
            _settings = settings;
            _localizationService = localizationService;
            _orderSettings = orderSettings;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke view component
        /// </summary>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke() 
        {
            BanruralInfoModel model = new BanruralInfoModel();

            //prepare order GUID
            ProcessPaymentRequest paymentRequest = new ProcessPaymentRequest();
            _paymentService.GenerateOrderGuid(paymentRequest);

            HttpContext.Session.Set(DefaultsInfo.PaymentRequestSessionKey, paymentRequest);

            return View("~/Plugins/Payments.Banrural/Views/PaymentInfo.cshtml", model);
        }

        #endregion
    }
}