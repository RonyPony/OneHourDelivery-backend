
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class DeliveryAppApiAdminController : BasePluginController
    {
        #region Fields

        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IOrderPendingToClosePaymentModelFactory _orderPendingToClosePaymentModelFactory;
        private readonly IOrderPendingToClosePaymentService _orderPendingToPayService;
        private readonly IOrderPaymentCollectionService _orderPaymentCollectionService;
        private readonly IWorkContext _workContext;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IProductService _productService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IOrderExportManager _exportManager;
        private readonly IVendorService _vendorService;
        private readonly IOrderService _orderService;
        private readonly IDeliveryAppOrderService _deliveryAppOrderService;
        private readonly ILogger _logger;
        private readonly IOrderModelFactory _orderModelFactory;


        #endregion

        #region Ctor

        public DeliveryAppApiAdminController(
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IOrderPendingToClosePaymentModelFactory orderPendingToClosePaymentModelFactory,
            IOrderPendingToClosePaymentService orderPendingToPayService,
            IOrderPaymentCollectionService orderPaymentCollectionService,
            IWorkContext workContext,
            IDateTimeHelper dateTimeHelper,
            IProductService productService,
            IProductAttributeParser productAttributeParser,
            IOrderExportManager exportManager,
            IVendorService vendorService,
            IOrderService orderService,
            IDeliveryAppOrderService deliveryAppOrderService,
            ILogger logger,
            IOrderModelFactory orderModelFactory)
        {
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _orderPendingToClosePaymentModelFactory = orderPendingToClosePaymentModelFactory;
            _orderPendingToPayService = orderPendingToPayService;
            _orderPaymentCollectionService = orderPaymentCollectionService;
            _workContext = workContext;
            _dateTimeHelper = dateTimeHelper;
            _productService = productService;
            _productAttributeParser = productAttributeParser;
            _exportManager = exportManager;
            _vendorService = vendorService;
            _orderService = orderService;
            _deliveryAppOrderService = deliveryAppOrderService;
            _logger = logger;
            _orderModelFactory = orderModelFactory;

        }

        #endregion

        #region Utilities

        protected virtual bool HasAccessToOrder(OrderPendingToClosePayment order)
        {
            return order != null && HasAccessToOrder(order.Id);
        }

        protected virtual bool HasAccessToTracingOrder(Order order)
        {
            return order != null && HasAccessToTracingOrder(order.Id);
        }

        protected virtual bool HasAccessToOrder(int orderId)
        {
            if (orderId == 0)
                return false;

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;
            var hasVendorProducts = _orderPendingToPayService.GetOrderItems(orderId, vendorId: vendorId).Any();

            return hasVendorProducts;
        }

        protected virtual bool HasAccessToTracingOrder(int orderId)
        {
            if (orderId == 0)
                return false;

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;
            var hasVendorProducts = _orderService.GetOrderItems(orderId, vendorId: vendorId).Any();

            return hasVendorProducts;
        }

        protected virtual bool HasAccessToProduct(OrderPendingToClosePaymentItem orderItem)
        {
            if (orderItem == null || orderItem.ProductId == 0)
                return false;

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;

            return _productService.GetProductById(orderItem.ProductId)?.VendorId == vendorId;
        }

        protected virtual bool HasAccessToShipment(Shipment shipment)
        {
            if (shipment == null)
                throw new ArgumentNullException(nameof(shipment));

            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() == null)
                //not a vendor; has access
                return true;

            return HasAccessToOrder(shipment.OrderId);
        }

        protected virtual void LogEditOrder(int orderId)
        {
            var order = _orderPendingToPayService.GetOrderById(orderId);
            if(order != null)
            {
                _customerActivityService.InsertActivity("EditOrder",
                string.Format(_localizationService.GetResource("ActivityLog.EditOrder"), order.CustomOrderNumber), order);
            }
        }

        protected virtual string AddGiftCards(IFormCollection form, Product product, string attributesXml,
           out string recipientName, out string recipientEmail, out string senderName, out string senderEmail,
           out string giftCardMessage)
        {
            recipientName = string.Empty;
            recipientEmail = string.Empty;
            senderName = string.Empty;
            senderEmail = string.Empty;
            giftCardMessage = string.Empty;

            if (!product.IsGiftCard)
                return attributesXml;

            foreach (var formKey in form.Keys)
            {
                if (formKey.Equals("giftcard.RecipientName", StringComparison.InvariantCultureIgnoreCase))
                {
                    recipientName = form[formKey];
                    continue;
                }

                if (formKey.Equals("giftcard.RecipientEmail", StringComparison.InvariantCultureIgnoreCase))
                {
                    recipientEmail = form[formKey];
                    continue;
                }

                if (formKey.Equals("giftcard.SenderName", StringComparison.InvariantCultureIgnoreCase))
                {
                    senderName = form[formKey];
                    continue;
                }

                if (formKey.Equals("giftcard.SenderEmail", StringComparison.InvariantCultureIgnoreCase))
                {
                    senderEmail = form[formKey];
                    continue;
                }

                if (formKey.Equals("giftcard.Message", StringComparison.InvariantCultureIgnoreCase))
                {
                    giftCardMessage = form[formKey];
                }
            }

            attributesXml = _productAttributeParser.AddGiftCardAttribute(attributesXml,
                recipientName, recipientEmail, senderName, senderEmail, giftCardMessage);

            return attributesXml;
        }

        #endregion

        #region Order list

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public virtual IActionResult List(List<int> orderStatuses = null, List<int> paymentStatuses = null, List<int> shippingStatuses = null)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderSearchModel(new OrderPendingToClosePaymentSearchModel
            {
                OrderStatusIds = orderStatuses,
                VendorPaymentStatusIds = paymentStatuses,
                DriverPaymentStatusIds = shippingStatuses
            });

            return View($"~/Plugins/Misc.DeliveryAppIntegrationBackend/Areas/Admin/Views/DeliveryAppApiAdmin/List.cshtml", model);
        }

        public virtual IActionResult TracingList(List<int> orderStatuses = null, List<int> paymentStatuses = null, List<int> shippingStatuses = null, List<int> deliveryStatuses = null)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareTracingOrderSearchModel(new OrderTracingSearchModel
            {
                OrderStatusIds = orderStatuses,
                PaymentStatusIds = paymentStatuses,
                ShippingStatusIds = shippingStatuses,
                DeliveryStatusIds = deliveryStatuses
            });

            return View($"~/Plugins/Misc.DeliveryAppIntegrationBackend/Areas/Admin/Views/DeliveryAppApiAdmin/OrderTracingList.cshtml", model);
        }

        public virtual IActionResult VendorOrdersEarningList(List<int> orderStatuses = null, List<int> paymentStatuses = null, List<int> shippingStatuses = null)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderSearchModel(new OrderPendingToClosePaymentSearchModel
            {
                OrderStatusIds = orderStatuses,
                VendorPaymentStatusIds = paymentStatuses,
                DriverPaymentStatusIds = shippingStatuses
            });

            return View($"~/Plugins/Misc.DeliveryAppIntegrationBackend/Areas/Admin/Views/DeliveryAppApiAdmin/VendorOrdersEarningList.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult OrderList(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            var customer = _workContext.GetCurrentCustomerAsync().GetAwaiter().GetResult();

            bool isVendor = customer.VendorId != 0;

            if (isVendor)
            {
                var vendor = _vendorService.GetVendorById(customer.VendorId);
                searchModel.VendorId = vendor.Id;
            }

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult OrderTracingList(OrderTracingSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderTracingListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult VendorOrderEarningList(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            var customer = _workContext.GetCurrentCustomerAsync().GetAwaiter().GetResult();

            bool isVendor = customer.VendorId != 0;

            if (isVendor)
            {
                var vendor = _vendorService.GetVendorById(customer.VendorId);
                searchModel.VendorId = vendor.Id;
            }

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareVendorOrderEarningListModel(searchModel);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult ReportAggregates(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderAggregatorModel(searchModel);

            return Json(model);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("go-to-order-by-number")]
        public virtual IActionResult GoToOrderId(OrderSearchModel model)
        {
            var order = _orderPendingToPayService.GetOrderByCustomOrderNumber(model.GoDirectlyToCustomOrderNumber);

            if (order == null)
                return List();

            return RedirectToAction("Edit", "DeliveryAppApiAdmin", new { id = order.Id });
        }

        [HttpPost]
        public virtual IActionResult SaveOrderDriver(OrderDeliveryInfoModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            try
            {
                _deliveryAppOrderService.SaveOrderDriver(model.OrderId, model.DriverId);
                return Json(new { Message = _localizationService.GetResource("Admin.Order.Driver.Updated") });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return Json(new { e.Message });
            }
        }

        [HttpPost]
        public virtual IActionResult OrderNotesSelect(OrderNoteSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            //try to get an order with the specified id
            var orderPending = _orderPendingToPayService.GetOrderById(searchModel.OrderId);
            var order = _orderService.GetOrderById(orderPending.OrderId)
                ?? throw new ArgumentException("No order found with the specified id");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return Content(string.Empty);

            //prepare model
            var model = _orderModelFactory.PrepareOrderNoteListModel(searchModel, order);

            return Json(model);
        }

        [HttpPost]
        public virtual IActionResult OrderNoteDelete(int id, int orderId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            _ = _orderService.GetOrderById(orderId)
                ?? throw new ArgumentException("No order found with the specified id");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return RedirectToAction("Edit", "Order", new { id = orderId });

            //try to get an order note with the specified id
            var orderNote = _orderService.GetOrderNoteById(id)
                ?? throw new ArgumentException("No order note found with the specified id");

            //try to get an order with the specified id
            _orderService.DeleteOrderNote(orderNote);

            return new NullJsonResult();
        }

        [HttpPost]
        public virtual IActionResult ShipmentsByOrder(OrderShipmentSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return Json(new { error = "Access denied" });

            //try to get an order with the specified id
            var orderPending = _orderPendingToPayService.GetOrderById(searchModel.OrderId);
            var order = _orderService.GetOrderById(orderPending.OrderId)
                ?? throw new ArgumentException("No order found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null && !HasAccessToOrder(orderPending))
                return Content(string.Empty);

            //prepare model
            var model = _orderModelFactory.PrepareOrderShipmentListModel(searchModel, order);

            return Json(model);
        }


        #endregion

        #region Export / Import

        [HttpPost, ActionName("ExportVendorOrderEarningXml")]
        [FormValueRequired("exportxmlvendororderearning-all")]
        public virtual IActionResult ExportVendorOrderEarningXmlAll(OrderPendingToClosePaymentSearchModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var startDateValue = model.StartDate == null ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());

            var endDateValue = model.EndDate == null ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);

            //a vendor should have access only to his products
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
            {
                model.VendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;
            }

            var vendorPaymentStatusIds = model.VendorPaymentStatusIds != null && !model.VendorPaymentStatusIds.Contains(0)
                ? model.VendorPaymentStatusIds.ToList()
                : null;
            var driverPaymentStatusIds = model.DriverPaymentStatusIds != null && !model.DriverPaymentStatusIds.Contains(0)
                ? model.DriverPaymentStatusIds.ToList()
                : null;

            //load orders
            var orders = _orderPendingToPayService.SearchOrders(
                driverId: model.DriverId,
                vendorId: model.VendorId,
                paymentMethodSystemName: model.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverPaymentStatusIds);

            //ensure that we at least one order selected
            if (!orders.Any())
            {
                _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Orders.NoOrders"));
                return RedirectToAction("List");
            }

            try
            {
                var xml = _exportManager.ExportVendorOrdersEarningToXml(orders);
                return File(Encoding.UTF8.GetBytes(xml), MimeTypes.ApplicationXml, "vendor orders earning.xml");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual IActionResult ExportXmlVendorOrderEarningSelected(string selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var orders = new List<OrderPendingToClosePayment>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                orders.AddRange(_orderPendingToPayService.GetOrdersByIds(ids).Where(HasAccessToOrder));
            }

            try
            {
                var xml = _exportManager.ExportVendorOrdersEarningToXml(orders);
                return File(Encoding.UTF8.GetBytes(xml), MimeTypes.ApplicationXml, "vendor orders earning.xml");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost, ActionName("ExportExcelVendorOrdersEarning")]
        [FormValueRequired("exportexcelvendorordersearning-all")]
        public virtual IActionResult ExportExcelVendorOrderEarningAll(OrderPendingToClosePaymentSearchModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var startDateValue = model.StartDate == null ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());

            var endDateValue = model.EndDate == null ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);

            //a vendor should have access only to his products
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
            {
                model.VendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;
            }

            var vendorPaymentStatusIds = model.VendorPaymentStatusIds != null && !model.VendorPaymentStatusIds.Contains(0)
                ? model.VendorPaymentStatusIds.ToList()
                : null;
            var driverPaymentStatusIds = model.DriverPaymentStatusIds != null && !model.DriverPaymentStatusIds.Contains(0)
                ? model.DriverPaymentStatusIds.ToList()
                : null;

            //load orders
            var orders = _orderPendingToPayService.SearchOrders(
                driverId: model.DriverId,
                vendorId: model.VendorId,
                paymentMethodSystemName: model.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverPaymentStatusIds);

            //ensure that we at least one order selected
            if (!orders.Any())
            {
                _notificationService.ErrorNotification(_localizationService.GetResource("Admin.Orders.NoOrders"));
                return RedirectToAction("List");
            }

            ////clean up duplicated orders - shoudl be improved

            //List<OrderPendingToClosePayment> distinct = orders.Distinct().ToList();
            orders.Union(orders).ToList();
            var orderIdList = new List<int>();
            var finalOrders = new List<OrderPendingToClosePayment>();
            foreach (var item in orders)
            {
                if (!orderIdList.Contains(item.OrderId)) //we can also use !listWithoutDuplicates.Any(x => x.Equals(item))
                {
                    orderIdList.Add(item.OrderId);
                    finalOrders.Add(item);
                }
            }
            //orders =finalOrders;

            try
            {
                var bytes = _exportManager.ExportVendorOrdersEarningToXlsx(finalOrders);
                return File(bytes, MimeTypes.TextXlsx, "vendor orders earning.xlsx");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual IActionResult ExportExcelVendorOrderEarningSelected(string selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var orders = new List<OrderPendingToClosePayment>();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                orders.AddRange(_orderPendingToPayService.GetOrdersByIds(ids).Where(HasAccessToOrder));
            }

            try
            {
                var bytes = _exportManager.ExportVendorOrdersEarningToXlsx(orders);
                return File(bytes, MimeTypes.TextXlsx, "vendor order earning.xlsx");
            }
            catch (Exception exc)
            {
                _notificationService.ErrorNotification(exc);
                return RedirectToAction("List");
            }
        }

        #endregion

        #region Edit, delete

        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            var order = _orderPendingToPayService.GetOrderById(id);
            if (order == null || order.Deleted)
                return RedirectToAction("List");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null && !HasAccessToOrder(order))
                return RedirectToAction("List");

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

            return View($"~/Plugins/Misc.DeliveryAppIntegrationBackend/Areas/Admin/Views/DeliveryAppApiAdmin/Edit.cshtml", model);
        }

        public virtual IActionResult EditOrderTracing(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            var order = _orderService.GetOrderById(id);
            if (order == null || order.Deleted)
                return RedirectToAction("TracingList");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null && !HasAccessToTracingOrder(order))
                return RedirectToAction("TracingList");

            //prepare model
            var model = _orderPendingToClosePaymentModelFactory.PrepareOrderPendingToClosePaymentModelFromOrder(null, order);

            return View($"~/Plugins/Misc.DeliveryAppIntegrationBackend/Areas/Admin/Views/DeliveryAppApiAdmin/EditOrderTracing.cshtml", model);
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("markorderascollected")]
        public virtual IActionResult MarkOrderAsCollected(int orderId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
                return RedirectToAction("TracingList");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return RedirectToAction("EditOrderTracing", "DeliveryAppApiAdmin", new { Id = orderId });

            var validationResult = _orderPaymentCollectionService.GetValidOrdersForPymentCollectionByOrderIds(new int[] { orderId });

            try
            {
                if (!validationResult.Success) throw new Exception(validationResult.Message);

                _orderPaymentCollectionService.MarkOrderPaymentAsCollected(order);
                _notificationService.SuccessNotification(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.OrderCollected"));

                return RedirectToAction("EditOrderTracing", "DeliveryAppApiAdmin", new { Id = orderId });
            }
            catch (Exception exc)
            {
                _logger.Error(exc.Message, exc);

                foreach (string error in exc.Message.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _notificationService.ErrorNotification(error);
                }

                return RedirectToAction("EditOrderTracing", "DeliveryAppApiAdmin", new { Id = orderId });
            }
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("markorderaspaid")]
        public virtual IActionResult MarkOrderVendorAsPaid(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            var order = _orderPendingToPayService.GetOrderById(id);
            if (order == null)
                return RedirectToAction("List");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return RedirectToAction("Edit", "DeliveryAppApiAdmin", new { id });

            try
            {
                order.VendorPaymentStatusId = (int)PaymentStatus.Paid;
                order.PaidDateUtc = DateTime.UtcNow;
                _orderPendingToPayService.UpdateOrder(order);

                LogEditOrder(order.Id);

                //prepare model
                var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                return View(model);
            }
            catch (Exception exc)
            {
                //prepare model
                var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                _notificationService.ErrorNotification(exc);
                return View(model);
            }
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("markorderdriveraspaid")]
        public virtual IActionResult MarkOrderDriverAsPaid(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            //try to get an order with the specified id
            var order = _orderPendingToPayService.GetOrderById(id);
            if (order == null)
                return RedirectToAction("List");

            //a vendor does not have access to this functionality
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return RedirectToAction("Edit", "DeliveryAppApiAdmin", new { id });

            try
            {
                order.DriverPaymentStatusId = (int)PaymentStatus.Paid;
                order.PaidDateUtc = DateTime.UtcNow;
                _orderPendingToPayService.UpdateOrder(order);
                LogEditOrder(order.Id);

                //prepare model
                var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                return View(model);
            }
            catch (Exception exc)
            {
                //prepare model
                var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                _notificationService.ErrorNotification(exc);
                return View(model);
            }
        }

        [HttpPost]
        public virtual IActionResult MarkGroupOrderVendorAsPaid(string selectedIds)
        {
            try
            {
                if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                    return AccessDeniedView();

                List<int> ids = selectedIds
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Convert.ToInt32(x))
                        .ToList();

                var validationResult = new ValidateOrdersForPaymentCollectionResult();
                validationResult = _orderPaymentCollectionService.GetValidOrdersToPayVendor(ids.ToArray());

                if (!validationResult.Success) throw new Exception(validationResult.Message);

                var stringBuilder = new StringBuilder();

                foreach (var orderId in ids)
                {
                    //try to get an order with the specified id
                    var order = _orderPendingToPayService.GetOrderById(orderId);
                    if (order == null)
                        continue;

                    //a vendor does not have access to this functionality
                    if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                        return RedirectToAction("Edit", "DeliveryAppApiAdmin", new { orderId });

                    order.VendorPaymentStatusId = (int)PaymentStatus.Paid;
                    order.PaidDateUtc = DateTime.UtcNow;
                    _orderPendingToPayService.UpdateOrder(order);
                    LogEditOrder(order.OrderId);

                    //prepare model
                    var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                    stringBuilder.Append($"{order.OrderId},");
                }
                _notificationService.SuccessNotification(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersPaidToVendor"), stringBuilder.ToString()));

                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _logger.Error(exc.Message, exc);
                foreach (string error in exc.Message.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _notificationService.ErrorNotification(error);
                }

                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public virtual IActionResult MarkGroupOrderDriverAsPaid(string selectedIds)
        {
            try
            {
                if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                    return AccessDeniedView();

                List<int> ids = selectedIds
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Convert.ToInt32(x))
                        .ToList();

                var validationResult = new ValidateOrdersForPaymentCollectionResult();
                validationResult = _orderPaymentCollectionService.GetValidOrdersToPayDriver(ids.ToArray());

                if (!validationResult.Success) throw new Exception(validationResult.Message);

                var stringBuilder = new StringBuilder();

                foreach (var orderId in ids)
                {
                    //try to get an order with the specified id
                    var order = _orderPendingToPayService.GetOrderById(orderId);
                    if (order == null)
                        continue;

                    //a vendor does not have access to this functionality
                    if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                        return RedirectToAction("Edit", "DeliveryAppApiAdmin", new { orderId });

                    order.DriverPaymentStatusId = (int)PaymentStatus.Paid;
                    order.PaidDateUtc = DateTime.UtcNow;
                    _orderPendingToPayService.UpdateOrder(order);
                    LogEditOrder(order.OrderId);

                    //prepare model
                    var model = _orderPendingToClosePaymentModelFactory.PrepareOrderModel(null, order);

                    stringBuilder.Append($"{order.OrderId},");
                }
                _notificationService.SuccessNotification(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersPaidToDriver"), stringBuilder.ToString()));

                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                _logger.Error(exc.Message, exc);
                foreach (string error in exc.Message.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _notificationService.ErrorNotification(error);
                }

                return RedirectToAction("List");
            }
        }

        #endregion

        #region Payment Collection

        [HttpPost]
        public virtual IActionResult CollectOrdersPayments(string selectedIds)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var validationResult = new ValidateOrdersForPaymentCollectionResult();
            if (selectedIds != null)
            {
                var ids = selectedIds
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                validationResult = _orderPaymentCollectionService.GetValidOrdersForPymentCollectionByOrderIds(ids);
            }

            try
            {
                if (!validationResult.Success) throw new Exception(validationResult.Message);

                var stringBuilder = new StringBuilder();

                foreach (Order order in validationResult.Orders)
                {
                    _orderPaymentCollectionService.MarkOrderPaymentAsCollected(order);
                    stringBuilder.Append($"{order.Id},");
                }

                _notificationService.SuccessNotification(string.Format(_localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.PaymentCollection.SelectedOrdersCollected"), stringBuilder.ToString()));

                return RedirectToAction("TracingList");
            }
            catch (Exception exc)
            {
                _logger.Error(exc.Message, exc);
                foreach (string error in exc.Message.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _notificationService.ErrorNotification(error);
                }

                return RedirectToAction("TracingList");
            }
        }

        #endregion
    }
}
