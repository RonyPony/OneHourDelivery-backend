using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Plugin.Payments.CyberSource.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Web.Framework.Controllers;
using System;
using System.Linq;
using System.Net.Http;

namespace Nop.Plugin.Payments.CyberSource.Controllers
{
    /// <summary>
    /// Responsible of providing functionality to interact with cybersource api.
    /// </summary>
    public sealed class PaymentCybersourceFormController : BasePaymentController
    {

        #region Fields

        private readonly IDeliveryAppPaymentService _deliveryAppPaymentService;
        private readonly IOrderService _orderService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IWorkContext _workContext;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly ILogger _logger;
        private readonly CyberSourceService _cyberSourceService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="PaymentCybersourceFormController"/>
        /// </summary>
        /// <param name="deliveryAppPaymentService">An implementation of <see cref="IDeliveryAppPaymentService"/></param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="cyberSourceService">An instance of <see cref="CyberSourceService"/></param>
        public PaymentCybersourceFormController(
            IDeliveryAppPaymentService deliveryAppPaymentService,
            IOrderService orderService,
            IOrderProcessingService orderProcessingService,
            IWorkContext workContext,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IStoreContext storeContext,
            ISettingService settingService,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            CyberSourceService cyberSourceService
            )
        {
            _logger = logger;
            _settingService = settingService;
            _storeContext = storeContext;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _orderService = orderService;
            _workContext = workContext;
            _orderProcessingService = orderProcessingService;
            _cyberSourceService = cyberSourceService;
            _deliveryAppPaymentService = deliveryAppPaymentService;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        [HttpGet]
        public IActionResult GetByOrderId(int orderId)
        {
            try
            {
                var post = _deliveryAppPaymentService.GetPostForPaymentForm(orderId);
                post.Post();

                return Ok(post);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
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
                OrderId = cyberSourceFormCollection["req_reference_number"].ToString().Trim(),
                Token = cyberSourceFormCollection["request_token"].ToString().Trim(),
                CardExpiryDate = cyberSourceFormCollection["req_card_expiry_date"].ToString().Trim(),
                CardLastDigits = cyberSourceFormCollection["req_card_number"].ToString().Trim(),
                ConsumerId = cyberSourceFormCollection["req_reference_number"].ToString().Trim()
            };
        }

        private void ReCreateOrder(int orderId)
        {
            Order order = _orderService.GetOrderById(orderId);

            _orderProcessingService.DeleteOrder(order);

            _orderProcessingService.ReOrder(order);
        }

        private void MarkOrderAsPaid(Order order, TransactionResponseModel transactionResponse)
        {
            order.PaymentStatus = PaymentStatus.Paid;

            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = order.Id,
                Note = $"The order has been paid! The CyberSource transaction Id is: {transactionResponse.TransactionId}",
                DisplayToCustomer = true,
                CreatedOnUtc = DateTime.UtcNow
            });

            LogTransaction(transactionResponse);

            _orderService.UpdateOrder(order);
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
                    _notificationService.ErrorNotification($"{_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionDeclinedMessage")}");
                    break;
                case TransactionResultStatusHelper.Error:
                    _notificationService.ErrorNotification($"{_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionErrorMessage")}");
                    break;
                default:
                    _notificationService.WarningNotification(_localizationService.GetResource("Plugins.Payments.CyberSource.TransactionCanceledMessage"));
                    break;
            }
        }
        public IActionResult CompleteTransactionWithToken(IFormCollection form)
        {
            TransactionResponseModel transactionResponse = MapToTransactionResponseModel(form);

            if (transactionResponse.TransactionType.Equals("create_payment_token"))
            {
                if (transactionResponse.ReasonCode.Trim() != CyberSourceDefaults.SuccessReasonCode)
                    return Json(transactionResponse);

                _deliveryAppPaymentService.InsertTokenPayment(new CustomerPaymentTokenMapping
                {
                    CustomerId = int.Parse(transactionResponse.ConsumerId),
                    Token = transactionResponse.TransactionId,
                    CardType = transactionResponse.CardType,
                    CardLastFourDigits = transactionResponse.CardLastDigits,
                    CardExpirationDate = transactionResponse.CardExpiryDate,
                    IsDefaultPaymentMethod = false
                });

                return Json(transactionResponse);
            }

            //if the transaction type is not create_payment_token then it is a sale transaction
            int orderId = int.Parse(transactionResponse.OrderId);
            Order order = _orderService.GetOrderById(orderId);
            if (transactionResponse.ReasonCode.Trim() != CyberSourceDefaults.SuccessReasonCode)
            {
                if (_orderProcessingService.CanCancelOrder(order))
                    _orderProcessingService.CancelOrder(order, false);

                return Json(transactionResponse);
            }

            //if it gets here, the sale was successful
            MarkOrderAsPaid(order, transactionResponse);

            //Send emails and push notifications for the order
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = _httpContextAccessor.HttpContext.Request.Scheme,
                Host = _httpContextAccessor.HttpContext.Request.Host.Host,
                Port = _httpContextAccessor.HttpContext.Request.Host.Port ?? -1,
                Path = "api/delivery-orders/notify-paid-order",
                Query = $"orderId={order.Id}"
            };

            using HttpClient _httpClient = new HttpClient();
            var request = _httpClient.GetAsync(uriBuilder.Uri);
            request.Wait();

            return Json(transactionResponse);
        }

        public IActionResult CompleteTransaction(IFormCollection form)
        {
            TransactionResponseModel transactionResponse = MapToTransactionResponseModel(form);

            try
            {
                bool couldConvertOrderId = int.TryParse(transactionResponse.OrderId, out int orderId);

                if (transactionResponse.ReasonCode.Trim().Equals(CyberSourceDefaults.generalDeclined))
                {
                    LogTransaction(transactionResponse, "This order has been declined by the processor - Possible action: request a different card or other form of payment.");
                    return View("~/Plugins/Payments.CyberSource/Views/PaymentFailed.cshtml");
                }

                if (couldConvertOrderId && !string.IsNullOrWhiteSpace(transactionResponse.ReasonCode) &&
                    transactionResponse.ReasonCode.Trim().Equals(CyberSourceDefaults.SuccessReasonCode))
                {
                    Order order = _orderService.GetOrderById(orderId);

                    if (order == null)
                    {
                        ReCreateOrder(orderId);

                        LogTransaction(transactionResponse, "Order not found when trying to receive the transaction result from CyberSource.");

                        AddNotificationErrorMessages(transactionResponse.Decision);

                        return View("~/Plugins/Payments.CyberSource/Views/PaymentFailed.cshtml");
                    }

                    int storeScope = _storeContext.ActiveStoreScopeConfiguration;
                    var settings = _settingService.LoadSetting<CyberSourcePaymentSettings>(storeScope);

                    order.PaymentStatus = PaymentStatus.Paid;

                    _orderService.InsertOrderNote(new OrderNote
                    {
                        OrderId = order.Id,
                        Note = $"The order has been paid! The CyberSource transaction Id is: {transactionResponse.TransactionId}",
                        DisplayToCustomer = true,
                        CreatedOnUtc = DateTime.UtcNow
                    });

                    if (order.AllowStoringCreditCardNumber == true && _deliveryAppPaymentService.CallAlreadyCustomerRegisteredCard(order.CustomerId,
                        transactionResponse.CardLastDigits, transactionResponse.CardExpiryDate) == null)
                    {
                        _deliveryAppPaymentService.InsertTokenPayment(new CustomerPaymentTokenMapping
                        {
                            CustomerId = order.CustomerId,
                            Token = transactionResponse.TransactionId,
                            CardType = transactionResponse.CardType,
                            CardLastFourDigits = transactionResponse.CardLastDigits,
                            CardExpirationDate = transactionResponse.CardExpiryDate,
                            IsDefaultPaymentMethod = false
                        });
                    }

                    LogTransaction(transactionResponse);

                    _orderService.UpdateOrder(order);
                    return View("~/Plugins/Payments.CyberSource/Views/ReciptPage.cshtml", transactionResponse);
                }

                // If reached this point, something went wrong and order will be re-created
                ReCreateOrder(orderId);

                LogTransaction(transactionResponse, _localizationService.GetResource("Plugins.Payments.CyberSource.TransactionErrorMessage"));

                AddNotificationErrorMessages(transactionResponse.Decision);

                //return RedirectToRoute("CheckoutBillingAddress");

                return View("~/Plugins/Payments.CyberSource/Views/PaymentFailed.cshtml");
            }
            catch (Exception e)
            {
                _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, $"Something went wrong while processing CyberSource payment complete: {e.Message}");

                LogTransaction(transactionResponse, e.Message);

                AddNotificationErrorMessages(transactionResponse.Decision);

                //return RedirectToRoute("CheckoutBillingAddress");

                return View("~/Plugins/Payments.CyberSource/Views/PaymentFailed.cshtml");
            }
        }
    }
}
