using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Logging;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Plugin.Payments.CyberSource.Services;
using Nop.Services;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Plugin.Payments.CyberSource.Controllers
{
    /// <summary>
    /// Controller used to configure settings on the admin page and manage CyberSource webhooks after order is successfully paid.
    /// </summary>
    public sealed class PaymentCyberSourceController : BasePaymentController
    {
        #region Fields

        private readonly CyberSourcePaymentSettings _cyberSourcePaymentSettings;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IOrderService _orderService;
        private readonly ICountryService _countryService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly ICyberSourceService _cyberSourceService;
        private readonly IWorkContext _workContext;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IPaymentService _paymentService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDeliveryAppPaymentService _paymentTokenService;

        #endregion

        #region Ctor

        /// <summary>
        /// Constructor method for this class
        /// </summary>
        /// <param name="cyberSourcePaymentSettings">Instance of <see cref="CyberSourcePaymentSettings"/></param>
        /// <param name="orderProcessingService">Implementation of <see cref="IOrderProcessingService"/></param>
        /// <param name="orderService">Implementation of <see cref="IOrderService"/></param>
        /// <param name="permissionService">Implementation of <see cref="IPermissionService"/></param>
        /// <param name="settingService">Implementation of <see cref="ISettingService"/></param>
        /// <param name="storeContext">Implementation of <see cref="IStoreContext"/></param>
        /// <param name="cyberSourceService">Implementation of <see cref="ICyberSourceService"/></param>
        /// <param name="workContext">Implementation of <see cref="IWorkContext"/></param>
        /// <param name="logger">Implementation of <see cref="ILogger"/></param>
        /// <param name="notificationService">Implementation of <see cref="INotificationService"/></param>
        /// <param name="localizationService">Implementation of <see cref="ILocalizationService"/></param>
        /// <param name="paymentService">Implementation of <see cref="IPaymentService"/></param>
        /// <param name="genericAttributeService">Implementation of <see cref="IGenericAttributeService"/></param>
        /// /// <param name="paymentTokenService">Implementation of <see cref="IDeliveryAppPaymentService"/></param>
        public PaymentCyberSourceController(CyberSourcePaymentSettings cyberSourcePaymentSettings,
            IOrderProcessingService orderProcessingService,
            IOrderService orderService,
            ICountryService countryService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext,
            ICyberSourceService cyberSourceService,
            IWorkContext workContext,
            ILogger logger,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IPaymentService paymentService,
            IGenericAttributeService genericAttributeService,
            IDeliveryAppPaymentService paymentTokenService)
        {
            _cyberSourcePaymentSettings = cyberSourcePaymentSettings;
            _orderProcessingService = orderProcessingService;
            _orderService = orderService;
            _countryService = countryService;
            _settingService = settingService;
            _storeContext = storeContext;
            _cyberSourceService = cyberSourceService;
            _workContext = workContext;
            _logger = logger;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _paymentService = paymentService;
            _genericAttributeService = genericAttributeService;
            _permissionService = permissionService;
            _paymentTokenService = paymentTokenService;
        }

        #endregion

        #region Utilities

        private void ConfirmOrder()
        {
            //place order
            var processPaymentRequest = HttpContext.Session.Get<ProcessPaymentRequest>("OrderPaymentInfo") ?? new ProcessPaymentRequest();

            _paymentService.GenerateOrderGuid(processPaymentRequest);
            processPaymentRequest.StoreId = _storeContext.CurrentStore.Id;
            processPaymentRequest.CustomerId = _workContext.CurrentCustomer.Id;

            processPaymentRequest.PaymentMethodSystemName = _genericAttributeService.GetAttribute<string>(
                _workContext.CurrentCustomer,
                NopCustomerDefaults.SelectedPaymentMethodAttribute, _storeContext.CurrentStore.Id);

            HttpContext.Session.Set<ProcessPaymentRequest>("OrderPaymentInfo", processPaymentRequest);

            _orderProcessingService.PlaceOrder(processPaymentRequest);
        }

        private TransactionResponseModel MapToTransactionResponseModel(IFormCollection cyberSourceFormCollection)
        {
            return new TransactionResponseModel
            {
                ReasonCode = cyberSourceFormCollection["reason_code"].ToString().Trim(),
                CardType = cyberSourceFormCollection["card_type_name"].ToString().Trim(),
                Amount = cyberSourceFormCollection["req_amount"].ToString().Trim(),
                TransactionId = cyberSourceFormCollection["transaction_id"].ToString().Trim(),
                Currency = cyberSourceFormCollection["req_currency"].ToString().Trim(),
                Decision = cyberSourceFormCollection["decision"].ToString().Trim(),
                InvalidFields = cyberSourceFormCollection["invalid_fields"].ToString().Trim(),
                Message = cyberSourceFormCollection["message"].ToString().Trim(),
                TransactionUuid = cyberSourceFormCollection["req_transaction_uuid"].ToString().Trim(),
                TransactionType = cyberSourceFormCollection["req_transaction_type"].ToString().Trim(),
                OrderId = cyberSourceFormCollection["req_reference_number"].ToString().Trim()
            };
        }

        private void LogTransaction(TransactionResponseModel transactionResponse, string fullException = null)
        {
            int.TryParse(transactionResponse.OrderId, out int orderId);

            var logEntry = new CyberSourceTransactionLog
            {
                OrderId = orderId,
                CustomerId = _workContext.CurrentCustomer.Id,
                CardType = transactionResponse.CardType,
                Amount = decimal.Parse(transactionResponse.Amount),
                Currency = transactionResponse.Currency,
                TransactionId = transactionResponse.TransactionId,
                TransactionUuid = transactionResponse.TransactionUuid,
                ReasonCode = transactionResponse.ReasonCode,
                Status = transactionResponse.Decision,
                Message = transactionResponse.Message,
                FullException = fullException,
                DateLogged = DateTime.Now
            };

            _cyberSourceService.Log(logEntry);
        }

        private void AddNotificationErrorMessages(string transactionStatus)
        {
            switch (transactionStatus)
            {
                case TransactionResultStatusHelper.Declined:
                    _notificationService.WarningNotification($"{_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionDeclinedMessage")}");
                    break;
                case TransactionResultStatusHelper.Error:
                    _notificationService.WarningNotification($"{_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionErrorMessage")}");
                    break;
                default:
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionCanceledMessage"));
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Action used to GET configuration page.
        /// </summary>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            //Configurable fields in the plugin configuration.
            var model = new ConfigurationModel
            {
                PaymentPageUrl = _cyberSourcePaymentSettings.PaymentPageUrl,
                RedirectUrl = _cyberSourcePaymentSettings.RedirectUrl,
                Access_key = _cyberSourcePaymentSettings.AccessKey,
                MerchantId = _cyberSourcePaymentSettings.MerchantId,
                Secret_key = _cyberSourcePaymentSettings.SecretKey,
                SerialNumber = _cyberSourcePaymentSettings.SerialNumber,
                Transaction_type = _cyberSourcePaymentSettings.TransactionType,
                Currency = _cyberSourcePaymentSettings.Currency,
                Locale = _cyberSourcePaymentSettings.Locale,
                AdditionalFee = _cyberSourcePaymentSettings.AdditionalFee,
                CybersourceEnvironment = _cyberSourcePaymentSettings.CybersourceEnvironment,
                MarkAsPaid = _cyberSourcePaymentSettings.MarkAsPaid,
                CreditCardIsMasked = _cyberSourcePaymentSettings.CreditCardIsMasked,
                OrderStatusId = (int)_cyberSourcePaymentSettings.OrderStatus,
                OrderStatus = _cyberSourcePaymentSettings.OrderStatus
                    .ToSelectList(true)
                    .Select(item => new SelectListItem(item.Text, item.Value)).ToList(),
            };

            return View("~/Plugins/Payments.CyberSource/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Action used to POST plugin settings.
        /// </summary>
        /// <param name="model">Instance of <see cref="ConfigurationModel"/></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePaymentMethods))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            _cyberSourcePaymentSettings.PaymentPageUrl = model.PaymentPageUrl;
            _cyberSourcePaymentSettings.RedirectUrl = model.RedirectUrl;
            _cyberSourcePaymentSettings.AccessKey = model.Access_key;
            _cyberSourcePaymentSettings.MerchantId = model.MerchantId;
            _cyberSourcePaymentSettings.SecretKey = model.Secret_key;
            _cyberSourcePaymentSettings.SerialNumber = model.SerialNumber;
            _cyberSourcePaymentSettings.TransactionType = model.Transaction_type;
            _cyberSourcePaymentSettings.Currency = model.Currency;
            _cyberSourcePaymentSettings.Locale = model.Locale;
            _cyberSourcePaymentSettings.AdditionalFee = model.AdditionalFee;
            _cyberSourcePaymentSettings.CybersourceEnvironment = model.CybersourceEnvironment;
            _cyberSourcePaymentSettings.MarkAsPaid = model.MarkAsPaid;
            _cyberSourcePaymentSettings.CreditCardIsMasked = model.CreditCardIsMasked;
            _cyberSourcePaymentSettings.OrderStatus = (OrderStatus)model.OrderStatusId;

            _settingService.SaveSetting(_cyberSourcePaymentSettings, _storeContext.ActiveStoreScopeConfiguration);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        /// <summary>
        /// Action used to POST payment information needed to confirm and pay the order
        /// </summary>
        /// <param name="model">Instance of <see cref="CyberSourcePaymentInfoModel"/></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ConfirmAndPay(CyberSourcePaymentInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(state => state.Errors))
                {
                    _notificationService.ErrorNotification(error.ErrorMessage);
                }

                return RedirectToRoute("CheckoutPaymentInfo");
            }

            // If OrderId is iqual to 0, means that the order isn't created yet, we must create the order.
            if (model.OrderId == 0)
                ConfirmOrder();

            Order order = model.OrderId != 0 ? _orderService.GetOrderById(model.OrderId) :
                _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1)
                .FirstOrDefault();

            // Send payment (CyberSource will redirect the user to the "Complete" action in this controller)
            model.CardNumber = model.CardNumber.Replace(" ", "");
            _cyberSourceService.ProcessTransaction(order, model);

            return Ok();
        }

        /// <summary>
        /// Process a payment by using stored token
        /// </summary>
        /// <returns></returns>
        [HttpPost("token-payment")]
        public IActionResult ConfirmAndPayWithToken([FromBody] CyberSourceTokenPaymentInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(state => state.Errors))
                {
                    _notificationService.ErrorNotification(error.ErrorMessage);
                }

                return RedirectToRoute("CheckoutPaymentInfo");
            }

            Order order = model.OrderId != 0 ? _orderService.GetOrderById(model.OrderId) :
                _orderService.SearchOrders(_storeContext.CurrentStore.Id, customerId: _workContext.CurrentCustomer.Id, pageSize: 1)
                .FirstOrDefault();

            // Send payment (CyberSource will redirect the user to the "Complete" action in this controller)
            _cyberSourceService.ProcessTransactionWithToken(order, model);

            return Ok();
        }

        /// <summary>
        /// Used to create a token for futures payments
        /// </summary>
        /// <param name="model">Model with all the fields needed to create the token</param>
        /// <returns></returns>
        [HttpPost("register-card")]
        public IActionResult RegisterCard([FromBody] CyberSourceRegisterSingleCard model)
        {
            if (!ModelState.IsValid)
            {
                var errorList = ModelState.Values.SelectMany(state => state.Errors);

                foreach (var error in errorList)
                {
                    _notificationService.ErrorNotification(error.ErrorMessage);
                }

                return RedirectToRoute("CheckoutPaymentInfo");
            }

            int customerId = int.Parse(model.CustomerId);
            string cardLastFour = model.CardNumber[^4..];

            if (model.CardExpirationMonth.Length == 1)
                model.CardExpirationMonth = "0" + model.CardExpirationMonth;

            var exist = _paymentTokenService.ValidateCardAlreadyExist(customerId, cardLastFour, $"{model.CardExpirationMonth}-{model.CardExpirationYear}");

            if (exist)
                return BadRequest("ThisCardIsAlreadyRegistered");

            _cyberSourceService.RegisterNewCard(model);

            return Ok();
        }

        /// <summary>
        /// Get all card information of an specific customer
        /// </summary>
        /// <param name="customerId">id of the customer to find their cards</param>
        /// <returns></returns>
        [HttpGet("{customerId}/registered-cards")]
        public IActionResult GetTokenInformation(int customerId)
        => Ok(_paymentTokenService.GetCustomerRegisteredCards(customerId));

        /// <summary>
        /// Deletes a customer registered card
        /// </summary>
        /// <param name="customerId">Id of the customer</param>
        /// <param name="cardToDelete">Card information</param>
        /// <returns></returns>
        [HttpDelete("{customerId}/customer/card")]
        public IActionResult DeleteCard(int customerId, [FromBody] RegisteredCard cardToDelete)
        {
            _paymentTokenService.DeleteRegisteredCard(customerId, cardToDelete);
            return NoContent();
        }

        /// <summary>
        /// Returns a list of countries
        /// </summary>
        /// <returns></returns>
        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countriesList = _countryService.GetAllCountries().Select(x => new { x.Id, x.Name }).ToList();

            return Ok(countriesList);
        }

        /// <summary>
        /// Controller that should be called after a transaction is done.
        /// </summary>
        /// <param name="form"><see cref="IFormCollection"/> received from CyberSource with transaction details.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Complete(IFormCollection form)
        {
            TransactionResponseModel transactionResponse = MapToTransactionResponseModel(form);

            try
            {
                bool couldConvertOrderId = int.TryParse(transactionResponse.OrderId, out int orderId);
                Order order = _orderService.GetOrderById(orderId);

                if (couldConvertOrderId && !string.IsNullOrWhiteSpace(transactionResponse.ReasonCode) &&
                    transactionResponse.ReasonCode.Trim().Equals(CyberSourceDefaults.SuccessReasonCode))
                {
                    if (order == null)
                    {
                        LogTransaction(transactionResponse, "Order not found when trying to receive the transaction result from CyberSource.");

                        AddNotificationErrorMessages(transactionResponse.Decision);

                        return RedirectToRoute("CheckoutBillingAddress");
                    }

                    int storeScope = _storeContext.ActiveStoreScopeConfiguration;
                    var settings = _settingService.LoadSetting<CyberSourcePaymentSettings>(storeScope);

                    order.PaymentStatus = settings.MarkAsPaid ? PaymentStatus.Paid : PaymentStatus.Pending;
                    order.OrderStatus = settings.OrderStatus;

                    _orderService.InsertOrderNote(new OrderNote
                    {
                        OrderId = order.Id,
                        Note = $"The order has been paid! The CyberSource transaction Id is: {transactionResponse.TransactionId}",
                        DisplayToCustomer = true,
                        CreatedOnUtc = DateTime.UtcNow
                    });

                    LogTransaction(transactionResponse);

                    _orderService.UpdateOrder(order);

                    return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
                }

                // If reached this point, something went wrong, order status and order payment status must stay pending, an email should be sent
                // to the customer indicating the status of the order.
                _cyberSourceService.SendPaymentDeclinedNotificationsAndSaveNotes(order);

                LogTransaction(transactionResponse, _localizationService.GetResource("Plugins.Payments.CyberSource.TransactionErrorMessage"));

                AddNotificationErrorMessages(transactionResponse.Decision);

                return RedirectToRoute("CheckoutCompleted", new { orderId = order.Id });
            }
            catch (Exception e)
            {
                _logger.InsertLog(LogLevel.Error, $"Something went wrong while processing CyberSource payment complete: {e.Message}");

                LogTransaction(transactionResponse, e.Message);

                AddNotificationErrorMessages(transactionResponse.Decision);

                return RedirectToRoute("CheckoutBillingAddress");
            }
        }

        /// <summary>
        /// Set <see cref="CyberSourceDefaults.type"/>value according to the corresponding condition.
        /// </summary>
        /// <returns>An implementation of <see cref="IsCreditCardMask"</returns>
        public object IsCreditCardMask()
        {
            var isCcMaskCheck = _cyberSourcePaymentSettings.CreditCardIsMasked;

            if (isCcMaskCheck == true)
            {
                CyberSourceDefaults.type = "password";
            }
            else
            {
                CyberSourceDefaults.type = "text";
            }

            return CyberSourceDefaults.type;
        }

        public IActionResult RetryPayment(int orderId)
        {
            var model = new CyberSourcePaymentInfoModel
            {
                OrderId = orderId
            };

            return View("~/Plugins/Payments.CyberSource/Views/RetryPayment.cshtml", model);
        }

        #endregion
    }
}
