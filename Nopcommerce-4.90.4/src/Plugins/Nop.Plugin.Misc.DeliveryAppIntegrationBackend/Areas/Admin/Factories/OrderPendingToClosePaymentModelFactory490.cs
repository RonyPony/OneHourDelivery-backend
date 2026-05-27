using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Areas.Admin.Models.Reports;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    public partial class OrderPendingToClosePaymentModelFactory : IOrderPendingToClosePaymentModelFactory
    {
        private readonly IAddressService _addressService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDeliveryAppBaseAdminModelFactory _deliveryAppBaseAdminModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderPendingToClosePaymentService _orderPendingService;
        private readonly IOrderService _orderService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly Nop.Services.Stores.IStoreService _storeService;
        private readonly IVendorService _vendorService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IRepository<OrderPaymentCollectionStatus> _orderPaymentCollectionStatusRepository;

        public OrderPendingToClosePaymentModelFactory(
            IAddressService addressService,
            IBaseAdminModelFactory baseAdminModelFactory,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IDeliveryAppBaseAdminModelFactory deliveryAppBaseAdminModelFactory,
            ILocalizationService localizationService,
            IOrderPendingToClosePaymentService orderPendingService,
            IOrderService orderService,
            IPriceFormatter priceFormatter,
            Nop.Services.Stores.IStoreService storeService,
            IVendorService vendorService,
            IRepository<Order> orderRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Product> productRepository,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IRepository<OrderPaymentCollectionStatus> orderPaymentCollectionStatusRepository)
        {
            _addressService = addressService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
            _deliveryAppBaseAdminModelFactory = deliveryAppBaseAdminModelFactory;
            _localizationService = localizationService;
            _orderPendingService = orderPendingService;
            _orderService = orderService;
            _priceFormatter = priceFormatter;
            _storeService = storeService;
            _vendorService = vendorService;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _orderPaymentCollectionStatusRepository = orderPaymentCollectionStatusRepository;
        }

        public virtual OrderPendingToClosePaymentSearchModel PrepareOrderSearchModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            searchModel ??= new OrderPendingToClosePaymentSearchModel();
            searchModel.SetGridPageSize();
            PreparePaymentStatuses(searchModel.AvailableVendorPaymentStatuses);
            PreparePaymentStatuses(searchModel.AvailableDriverPaymentStatuses);
            return searchModel;
        }

        public virtual OrderTracingSearchModel PrepareTracingOrderSearchModel(OrderTracingSearchModel searchModel)
        {
            searchModel ??= new OrderTracingSearchModel();
            searchModel.SetGridPageSize();
            PrepareOrderStatuses(searchModel.AvailableOrderStatuses);
            PreparePaymentStatuses(searchModel.AvailablePaymentStatuses);
            PrepareShippingStatuses(searchModel.AvailableShippingStatuses);
            PrepareDeliveryStatuses(searchModel.AvailableDeliveryStatuses);
            PreparePaymentCollectionStatuses(searchModel.AvailablePaymentCollectionStatuses);
            _deliveryAppBaseAdminModelFactory.PrepareDrivers(searchModel.AvailableDrivers);
            _deliveryAppBaseAdminModelFactory.PreparePaymentMethods(searchModel.AvailablePaymentMethods);
            _baseAdminModelFactory.PrepareVendorsAsync(searchModel.AvailableVendors).GetAwaiter().GetResult();
            return searchModel;
        }

        public virtual OrderPendingToClosePaymentListModel PrepareOrderListModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            var orders = SearchPendingOrders(searchModel);
            return new OrderPendingToClosePaymentListModel().PrepareToGrid(searchModel, orders,
                () => orders.Select(order => PrepareOrderModel(null, order)));
        }

        public virtual OrderTracingListModel PrepareOrderTracingListModel(OrderTracingSearchModel searchModel)
        {
            var orders = SearchTracingOrders(searchModel);
            return new OrderTracingListModel().PrepareToGrid(searchModel, orders,
                () => orders.Select(PrepareTracingModel));
        }

        public virtual OrderPendingToClosePaymentListModel PrepareVendorOrderEarningListModel(OrderPendingToClosePaymentSearchModel searchModel)
            => PrepareOrderListModel(searchModel);

        public virtual OrderAggreratorModel PrepareOrderAggregatorModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            var orders = SearchPendingOrders(searchModel, int.MaxValue);
            return new OrderAggreratorModel
            {
                AggregatorProfit = FormatPrice(orders.Sum(o => o.OrderTotalAdministrativeProfitAmount + o.ShippingTaxAdministrativeProfitAmount)),
                AggregatorShipping = FormatPrice(orders.Sum(o => o.OrderShippingInclTax)),
                AggregatorTax = FormatPrice(orders.Sum(o => o.OrderTax)),
                AggregatorTotal = FormatPrice(orders.Sum(o => o.OrderTotal))
            };
        }

        public virtual OrderPendingToClosePaymentModel PrepareOrderModel(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order, bool excludeProperties = false)
        {
            ArgumentNullException.ThrowIfNull(order);

            model ??= new OrderPendingToClosePaymentModel();
            model.Id = order.Id;
            model.OrderGuid = order.OrderGuid;
            model.CustomOrderNumber = order.CustomOrderNumber;
            model.CustomerId = order.CustomerId;
            model.CustomerEmail = GetCustomerEmail(order.CustomerId);
            model.CustomerInfo = model.CustomerEmail;
            model.VendorName = GetVendorName(order.VendorId);
            model.StoreName = GetStoreName(order.StoreId);
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);
            model.OrderStatusId = order.OrderStatusId;
            model.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
            model.VendorPaymentStatusId = order.VendorPaymentStatusId;
            model.VendorPaymentStatus = _localizationService.GetLocalizedEnum(order.VendorPaymentStatus);
            model.DriverPaymentStatusId = order.DriverPaymentStatusId;
            model.DriverPaymentStatus = _localizationService.GetLocalizedEnum(order.DriverPaymentStatus);
            model.PaymentMethod = order.PaymentMethodSystemName;
            model.OrderTotal = FormatPrice(order.OrderTotal);
            model.OrderShippingInclTax = FormatPrice(order.OrderShippingInclTax);
            model.OrderShippingExclTax = FormatPrice(order.OrderShippingExclTax);
            model.Tax = FormatPrice(order.OrderTax);
            model.Profit = FormatPrice(order.OrderTotalAdministrativeProfitAmount + order.ShippingTaxAdministrativeProfitAmount);
            model.ShippingTaxAdministrativePercentage = order.ShippingTaxAdministrativePercentage;
            model.ShippingTaxAdministrativeProfitAmount = order.ShippingTaxAdministrativeProfitAmount;
            model.ShippingTaxMessengerPercentage = order.ShippingTaxMessengerPercentage;
            model.ShippingTaxMessengerProfitAmount = order.ShippingTaxMessengerProfitAmount;
            model.OrderTotalAdministrativePercentage = order.OrderTotalAdministrativePercentage;
            model.OrderTotalAdministrativeProfitAmount = order.OrderTotalAdministrativeProfitAmount;
            model.OrderTotalVendorPercentage = order.OrderTotalVendorPercentage;
            model.OrderTotalVendorProfitAmount = order.OrderTotalVendorProfitAmount;
            model.CheckoutAttributeInfo = order.CheckoutAttributeDescription;
            model.OrderNoteSearchModel.OrderId = order.Id;
            model.OrderShipmentSearchModel.OrderId = order.Id;

            if (!excludeProperties)
            {
                model.BillingAddress = PrepareAddressModel(order.BillingAddressId);
                model.ShippingAddress = PrepareAddressModel(order.ShippingAddressId ?? 0);
                model.PickupAddress = PrepareAddressModel(order.PickupAddressId ?? 0);
            }

            return model;
        }

        public virtual OrderPendingToClosePaymentModel PrepareOrderPendingToClosePaymentModelFromOrder(OrderPendingToClosePaymentModel model, Order order, bool excludeProperties = false)
        {
            ArgumentNullException.ThrowIfNull(order);

            model ??= new OrderPendingToClosePaymentModel();
            model.Id = order.Id;
            model.OrderGuid = order.OrderGuid;
            model.CustomOrderNumber = order.CustomOrderNumber;
            model.CustomerId = order.CustomerId;
            model.CustomerEmail = GetCustomerEmail(order.CustomerId);
            model.CustomerInfo = model.CustomerEmail;
            model.StoreName = GetStoreName(order.StoreId);
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);
            model.OrderStatusId = order.OrderStatusId;
            model.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
            model.PaymentMethod = order.PaymentMethodSystemName;
            model.OrderTotal = FormatPrice(order.OrderTotal);
            model.Tax = FormatPrice(order.OrderTax);
            model.CheckoutAttributeInfo = order.CheckoutAttributeDescription;
            return model;
        }

        public virtual AddProductToOrderSearchModel PrepareAddProductToOrderSearchModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order)
        {
            searchModel ??= new AddProductToOrderSearchModel();
            searchModel.OrderId = order?.Id ?? 0;
            searchModel.SetPopupGridPageSize();
            return searchModel;
        }

        public virtual AddProductToOrderListModel PrepareAddProductToOrderListModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order)
            => new AddProductToOrderListModel
            {
                Data = Array.Empty<ProductModel>(),
                Draw = searchModel?.Draw,
                RecordsFiltered = 0,
                RecordsTotal = 0
            };

        public virtual AddProductToOrderModel PrepareAddProductToOrderModel(AddProductToOrderModel model, OrderPendingToClosePayment order, Product product)
        {
            model ??= new AddProductToOrderModel();
            model.OrderId = order?.Id ?? 0;
            model.ProductId = product?.Id ?? 0;
            model.Name = product?.Name;
            model.ProductType = product?.ProductType ?? ProductType.SimpleProduct;
            return model;
        }

        public virtual OrderAddressModel PrepareOrderAddressModel(OrderAddressModel model, OrderPendingToClosePayment order, Address address)
        {
            model ??= new OrderAddressModel();
            model.OrderId = order?.Id ?? 0;
            model.Address = PrepareAddressModel(address);
            return model;
        }

        public virtual OrderNoteListModel PrepareOrderNoteListModel(OrderNoteSearchModel searchModel, OrderPendingToClosePayment order)
        {
            var notes = order is null
                ? new PagedList<OrderNote>(new List<OrderNote>(), 0, 1, 0)
                : new PagedList<OrderNote>(_orderPendingService.GetOrderNotesByOrderId(order.Id).ToList(), searchModel.Page - 1, searchModel.PageSize);

            return new OrderNoteListModel().PrepareToGrid(searchModel, notes,
                () => notes.Select(note => new OrderNoteModel
                {
                    Id = note.Id,
                    OrderId = note.OrderId,
                    Note = _orderPendingService.FormatOrderNoteText(note),
                    DisplayToCustomer = note.DisplayToCustomer,
                    CreatedOn = _dateTimeHelper.ConvertToUserTime(note.CreatedOnUtc, DateTimeKind.Utc)
                }));
        }

        public virtual BestsellerBriefSearchModel PrepareBestsellerBriefSearchModel(BestsellerBriefSearchModel searchModel)
        {
            searchModel ??= new BestsellerBriefSearchModel();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public virtual BestsellerBriefListModel PrepareBestsellerBriefListModel(BestsellerBriefSearchModel searchModel)
            => EmptyList<BestsellerBriefListModel, BestsellerModel>(searchModel);

        public virtual OrderAverageReportListModel PrepareOrderAverageReportListModel(OrderAverageReportSearchModel searchModel, int vendorId)
            => EmptyList<OrderAverageReportListModel, OrderAverageReportModel>(searchModel);

        public virtual OrderIncompleteReportListModel PrepareOrderIncompleteReportListModel(OrderIncompleteReportSearchModel searchModel, int vendorId)
            => EmptyList<OrderIncompleteReportListModel, OrderIncompleteReportModel>(searchModel);

        private IPagedList<OrderPendingToClosePayment> SearchPendingOrders(OrderPendingToClosePaymentSearchModel searchModel, int? pageSizeOverride = null)
        {
            searchModel ??= new OrderPendingToClosePaymentSearchModel();
            var createdFromUtc = searchModel.StartDate;
            var createdToUtc = searchModel.EndDate?.Date.AddDays(1).AddTicks(-1);

            return _orderPendingService.SearchOrders(searchModel.VendorId, searchModel.DriverId, searchModel.PaymentMethodSystemName,
                searchModel.VendorPaymentStatusIds?.ToList(), searchModel.DriverPaymentStatusIds?.ToList(), createdFromUtc, createdToUtc,
                searchModel.Page - 1, pageSizeOverride ?? searchModel.PageSize);
        }

        private IPagedList<Order> SearchTracingOrders(OrderTracingSearchModel searchModel)
        {
            searchModel ??= new OrderTracingSearchModel();
            var createdFromUtc = searchModel.StartDate;
            var createdToUtc = searchModel.EndDate?.Date.AddDays(1).AddTicks(-1);
            var query = _orderRepository.Table.Where(order => !order.Deleted);

            if (createdFromUtc.HasValue)
                query = query.Where(order => order.CreatedOnUtc >= createdFromUtc.Value);
            if (createdToUtc.HasValue)
                query = query.Where(order => order.CreatedOnUtc <= createdToUtc.Value);
            if (searchModel.OrderStatusIds?.Any() == true && !searchModel.OrderStatusIds.Contains(0))
                query = query.Where(order => searchModel.OrderStatusIds.Contains(order.OrderStatusId));
            if (searchModel.PaymentStatusIds?.Any() == true && !searchModel.PaymentStatusIds.Contains(0))
                query = query.Where(order => searchModel.PaymentStatusIds.Contains(order.PaymentStatusId));
            if (searchModel.ShippingStatusIds?.Any() == true && !searchModel.ShippingStatusIds.Contains(0))
                query = query.Where(order => searchModel.ShippingStatusIds.Contains(order.ShippingStatusId));
            if (!string.IsNullOrWhiteSpace(searchModel.PaymentMethodName))
                query = query.Where(order => order.CheckoutAttributeDescription.Contains(searchModel.PaymentMethodName));

            if (searchModel.VendorId > 0)
            {
                query = query.Where(order => _orderItemRepository.Table.Any(item =>
                    item.OrderId == order.Id &&
                    _productRepository.Table.Any(product => product.Id == item.ProductId && product.VendorId == searchModel.VendorId)));
            }

            if (searchModel.DriverId > 0)
                query = query.Where(order => _orderDeliveryStatusMappingRepository.Table.Any(mapping =>
                    mapping.OrderId == order.Id && mapping.CustomerId == searchModel.DriverId));

            if (searchModel.DeliveryStatusIds?.Any() == true && !searchModel.DeliveryStatusIds.Contains(0))
                query = query.Where(order => _orderDeliveryStatusMappingRepository.Table.Any(mapping =>
                    mapping.OrderId == order.Id && searchModel.DeliveryStatusIds.Contains(mapping.DeliveryStatusId)));

            if (searchModel.PaymentCollectionStatusId > 0)
                query = query.Where(order => _orderPaymentCollectionStatusRepository.Table.Any(mapping =>
                    mapping.OrderId == order.Id && mapping.PaymentCollectionStatusId == searchModel.PaymentCollectionStatusId));

            query = query.OrderByDescending(order => order.CreatedOnUtc);
            var totalCount = query.Count();
            var pageIndex = searchModel.Page - 1;
            var data = query.Skip(pageIndex * searchModel.PageSize).Take(searchModel.PageSize).ToList();
            return new PagedList<Order>(data, pageIndex, searchModel.PageSize, totalCount);
        }

        private OrderTracingModel PrepareTracingModel(Order order)
        {
            var billingAddress = PrepareAddressModel(order.BillingAddressId);
            var orderDelivery = _orderDeliveryStatusMappingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == order.Id);
            var paymentCollection = _orderPaymentCollectionStatusRepository.Table.FirstOrDefault(mapping => mapping.OrderId == order.Id);
            var paymentCollectionStatus = paymentCollection?.PaymentCollectionStatus ?? PaymentCollectionStatus.DoesNotApply;
            var model = new OrderTracingModel
            {
                Id = order.Id,
                OrderGuid = order.OrderGuid,
                CustomOrderNumber = order.CustomOrderNumber,
                CustomerId = order.CustomerId,
                CustomerEmail = billingAddress.Email ?? GetCustomerEmail(order.CustomerId),
                CustomerFullName = $"{billingAddress.FirstName} {billingAddress.LastName}".Trim(),
                CustomerInfo = billingAddress.Email ?? GetCustomerEmail(order.CustomerId),
                VendorName = GetOrderVendorName(order.Id),
                StoreName = GetStoreName(order.StoreId),
                CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc),
                OrderStatusId = order.OrderStatusId,
                OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus),
                PaymentStatusId = order.PaymentStatusId,
                PaymentStatus = _localizationService.GetLocalizedEnum(order.PaymentStatus),
                ShippingStatusId = order.ShippingStatusId,
                ShippingStatus = _localizationService.GetLocalizedEnum(order.ShippingStatus),
                DeliveryStatusId = orderDelivery?.DeliveryStatusId ?? 0,
                DeliveryStatus = orderDelivery == null ? string.Empty : _localizationService.GetLocalizedEnum((DeliveryStatus)orderDelivery.DeliveryStatusId),
                DriverName = GetDriverName(orderDelivery?.CustomerId),
                PaymentCollectionStatusId = (int)paymentCollectionStatus,
                PaymentCollectionStatus = _localizationService.GetLocalizedEnum(paymentCollectionStatus),
                PaymentMethod = order.PaymentMethodSystemName,
                OrderTotal = FormatPrice(order.OrderTotal),
                ShippingMethod = order.ShippingMethod,
                CheckoutAttributeInfo = order.CheckoutAttributeDescription
            };

            return model;
        }

        private string GetDriverName(int? driverId)
        {
            var driver = driverId.HasValue ? _customerService.GetCustomerById(driverId.Value) : null;
            return driver is null ? string.Empty : driver.Email;
        }

        private string GetOrderVendorName(int orderId)
        {
            var vendorId = (from item in _orderItemRepository.Table
                            join product in _productRepository.Table on item.ProductId equals product.Id
                            where item.OrderId == orderId
                            select product.VendorId).FirstOrDefault();

            return vendorId == 0 ? string.Empty : GetVendorName(vendorId);
        }

        private AddressModel PrepareAddressModel(int addressId)
            => PrepareAddressModel(addressId > 0 ? _addressService.GetAddressByIdAsync(addressId).GetAwaiter().GetResult() : null);

        private static AddressModel PrepareAddressModel(Address address)
        {
            var model = new AddressModel();
            if (address is null)
                return model;

            model.Id = address.Id;
            model.FirstName = address.FirstName;
            model.LastName = address.LastName;
            model.Email = address.Email;
            model.Company = address.Company;
            model.CountryId = address.CountryId;
            model.StateProvinceId = address.StateProvinceId;
            model.City = address.City;
            model.County = address.County;
            model.Address1 = address.Address1;
            model.Address2 = address.Address2;
            model.ZipPostalCode = address.ZipPostalCode;
            model.PhoneNumber = address.PhoneNumber;
            model.FaxNumber = address.FaxNumber;
            return model;
        }

        private static TList EmptyList<TList, TModel>(Nop.Web.Framework.Models.BaseSearchModel searchModel)
            where TList : Nop.Web.Framework.Models.BasePagedListModel<TModel>, new()
            where TModel : Nop.Web.Framework.Models.BaseNopModel
            => new()
            {
                Data = Array.Empty<TModel>(),
                Draw = searchModel?.Draw,
                RecordsFiltered = 0,
                RecordsTotal = 0
            };

        private string FormatPrice(decimal value) => _priceFormatter.FormatPrice(value);

        private string GetCustomerEmail(int customerId) => _customerService.GetCustomerById(customerId)?.Email;

        private string GetStoreName(int storeId) => _storeService.GetStoreById(storeId)?.Name;

        private string GetVendorName(int vendorId) => _vendorService.GetVendorById(vendorId)?.Name;

        private static void PrepareOrderStatuses(IList<SelectListItem> items)
        {
            foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
                items.Add(new SelectListItem { Text = status.ToString(), Value = ((int)status).ToString() });
        }

        private static void PreparePaymentStatuses(IList<SelectListItem> items)
        {
            foreach (PaymentStatus status in Enum.GetValues(typeof(PaymentStatus)))
                items.Add(new SelectListItem { Text = status.ToString(), Value = ((int)status).ToString() });
        }

        private static void PrepareShippingStatuses(IList<SelectListItem> items)
        {
            foreach (ShippingStatus status in Enum.GetValues(typeof(ShippingStatus)))
                items.Add(new SelectListItem { Text = status.ToString(), Value = ((int)status).ToString() });
        }

        private static void PrepareDeliveryStatuses(IList<SelectListItem> items)
        {
            foreach (DeliveryStatus status in Enum.GetValues(typeof(DeliveryStatus)))
                items.Add(new SelectListItem { Text = status.ToString(), Value = ((int)status).ToString() });
        }

        private static void PreparePaymentCollectionStatuses(IList<SelectListItem> items)
        {
            foreach (PaymentCollectionStatus status in Enum.GetValues(typeof(PaymentCollectionStatus)))
                items.Add(new SelectListItem { Text = status.ToString(), Value = ((int)status).ToString() });
        }
    }
}
