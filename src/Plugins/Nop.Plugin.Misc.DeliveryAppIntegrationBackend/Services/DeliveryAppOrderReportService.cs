using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Responsible of providing order reports data.
    /// </summary>
    public sealed class DeliveryAppOrderReportService: IDeliveryAppOrderReportService
    {
        #region #Fields

        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<OrderNote> _orderNoteRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductWarehouseInventory> _productWarehouseInventoryRepository;
        private readonly IDateTimeHelper _dateTimeHelper;

        #endregion

        #region #Ctor

        /// <summary>
        /// Creates an instance of <see cref="DeliveryAppOrderReportService"/>
        /// </summary>
        /// <param name="addressRepository">An implementation of <see cref="IRepository{Address}"/></param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/></param>
        /// <param name="orderItemRepository">An implementation of <see cref="IRepository{OrderItem}"/></param>
        /// <param name="orderNoteRepository">An implementation of <see cref="IRepository{OrderNote}"/></param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{Product}"/></param>
        /// <param name="productWarehouseInventoryRepository">An implementation of <see cref="IRepository{ProductWarehouseInventory}"/></param>
        /// <param name="dateTimeHelper">An implementation of <see cref="IDateTimeHelper"/></param>
        public DeliveryAppOrderReportService(
                                            IRepository<Address> addressRepository,
                                            IRepository<Order> orderRepository,
                                            IRepository<OrderItem> orderItemRepository,
                                            IRepository<OrderNote> orderNoteRepository,
                                            IRepository<Product> productRepository,
                                            IRepository<ProductWarehouseInventory> productWarehouseInventoryRepository,
                                            IDateTimeHelper dateTimeHelper)
        {
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderNoteRepository = orderNoteRepository;
            _productRepository = productRepository;
            _productWarehouseInventoryRepository = productWarehouseInventoryRepository;
            _dateTimeHelper = dateTimeHelper;
        }

        #endregion

        #region #Methods

        /// <summary>
        /// Get order average report
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <param name="os">Order status</param>
        /// <returns>Result</returns>
        public OrderAverageReportLineSummary OrderAverageReport(int storeId, OrderStatus os, int vendorId)
        {
            var item = new OrderAverageReportLineSummary
            {
                OrderStatus = os
            };
            var orderStatuses = new List<int> { (int)os };

            var nowDt = _dateTimeHelper.ConvertToUserTime(DateTime.Now);
            var timeZone = _dateTimeHelper.CurrentTimeZone;

            //today
            var t1 = new DateTime(nowDt.Year, nowDt.Month, nowDt.Day);
            DateTime? startTime1 = _dateTimeHelper.ConvertToUtcTime(t1, timeZone);
            var todayResult = GetOrderAverageReportLine(storeId,
                osIds: orderStatuses,
                startTimeUtc: startTime1,
                vendorId: vendorId
                );
            item.SumTodayOrders = todayResult.SumOrders;
            item.CountTodayOrders = todayResult.CountOrders;

            //week
            var fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var today = new DateTime(nowDt.Year, nowDt.Month, nowDt.Day);
            var t2 = today.AddDays(-(today.DayOfWeek - fdow));
            DateTime? startTime2 = _dateTimeHelper.ConvertToUtcTime(t2, timeZone);
            var weekResult = GetOrderAverageReportLine(storeId,
                osIds: orderStatuses,
                startTimeUtc: startTime2,
                vendorId: vendorId);
            item.SumThisWeekOrders = weekResult.SumOrders;
            item.CountThisWeekOrders = weekResult.CountOrders;

            //month
            var t3 = new DateTime(nowDt.Year, nowDt.Month, 1);
            DateTime? startTime3 = _dateTimeHelper.ConvertToUtcTime(t3, timeZone);
            var monthResult = GetOrderAverageReportLine(storeId,
                osIds: orderStatuses,
                startTimeUtc: startTime3,
                vendorId: vendorId);
            item.SumThisMonthOrders = monthResult.SumOrders;
            item.CountThisMonthOrders = monthResult.CountOrders;

            //year
            var t4 = new DateTime(nowDt.Year, 1, 1);
            DateTime? startTime4 = _dateTimeHelper.ConvertToUtcTime(t4, timeZone);
            var yearResult = GetOrderAverageReportLine(storeId,
                osIds: orderStatuses,
                startTimeUtc: startTime4,
                vendorId: vendorId);
            item.SumThisYearOrders = yearResult.SumOrders;
            item.CountThisYearOrders = yearResult.CountOrders;

            //all time
            var allTimeResult = GetOrderAverageReportLine(storeId, osIds: orderStatuses, vendorId: vendorId);
            item.SumAllTimeOrders = allTimeResult.SumOrders;
            item.CountAllTimeOrders = allTimeResult.CountOrders;

            return item;
        }


        /// <summary>
        /// Get order average report
        /// </summary>
        /// <param name="storeId">Store identifier; pass 0 to ignore this parameter</param>
        /// <param name="vendorId">Vendor identifier; pass 0 to ignore this parameter</param>
        /// <param name="productId">Product identifier which was purchased in an order; 0 to load all orders</param>
        /// <param name="warehouseId">Warehouse identifier; pass 0 to ignore this parameter</param>
        /// <param name="billingCountryId">Billing country identifier; 0 to load all orders</param>
        /// <param name="orderId">Order identifier; pass 0 to ignore this parameter</param>
        /// <param name="paymentMethodSystemName">Payment method system name; null to load all records</param>
        /// <param name="osIds">Order status identifiers</param>
        /// <param name="psIds">Payment status identifiers</param>
        /// <param name="ssIds">Shipping status identifiers</param>
        /// <param name="startTimeUtc">Start date</param>
        /// <param name="endTimeUtc">End date</param>
        /// <param name="billingPhone">Billing phone. Leave empty to load all records.</param>
        /// <param name="billingEmail">Billing email. Leave empty to load all records.</param>
        /// <param name="billingLastName">Billing last name. Leave empty to load all records.</param>
        /// <param name="orderNotes">Search in order notes. Leave empty to load all records.</param>
        /// <returns>Result</returns>
        private  OrderAverageReportLine GetOrderAverageReportLine(int storeId = 0,
            int vendorId = 0, int productId = 0, int warehouseId = 0, int billingCountryId = 0,
            int orderId = 0, string paymentMethodSystemName = null,
            List<int> osIds = null, List<int> psIds = null, List<int> ssIds = null,
            DateTime? startTimeUtc = null, DateTime? endTimeUtc = null,
            string billingPhone = null, string billingEmail = null, string billingLastName = "", string orderNotes = null)
        {
            var query = _orderRepository.Table;

            query = query.Where(o => !o.Deleted);
            if (storeId > 0)
                query = query.Where(o => o.StoreId == storeId);
            if (orderId > 0)
                query = query.Where(o => o.Id == orderId);

            if (vendorId > 0)
                query = from o in query
                        join oi in _orderItemRepository.Table on o.Id equals oi.OrderId
                        join p in _productRepository.Table on oi.ProductId equals p.Id
                        where p.VendorId == vendorId
                        select o;

            if (productId > 0)
                query = from o in query
                        join oi in _orderItemRepository.Table on o.Id equals oi.OrderId
                        where oi.ProductId == productId
                        select o;

            if (warehouseId > 0)
            {
                var manageStockInventoryMethodId = (int)ManageInventoryMethod.ManageStock;

                query = from o in query
                        join oi in _orderItemRepository.Table on o.Id equals oi.OrderId
                        join p in _productRepository.Table on oi.ProductId equals p.Id
                        join pwi in _productWarehouseInventoryRepository.Table on p.Id equals pwi.ProductId
                        where
                            //"Use multiple warehouses" enabled
                            //we search in each warehouse
                            (p.ManageInventoryMethodId == manageStockInventoryMethodId && p.UseMultipleWarehouses && pwi.WarehouseId == warehouseId) ||
                            //"Use multiple warehouses" disabled
                            //we use standard "warehouse" property
                            ((p.ManageInventoryMethodId != manageStockInventoryMethodId || !p.UseMultipleWarehouses) && p.WarehouseId == warehouseId)
                        select o;
            }

            query = from o in query
                    join oba in _addressRepository.Table on o.BillingAddressId equals oba.Id
                    where
                        (billingCountryId <= 0 || (oba.CountryId == billingCountryId)) &&
                        (string.IsNullOrEmpty(billingPhone) || (!string.IsNullOrEmpty(oba.PhoneNumber) && oba.PhoneNumber.Contains(billingPhone))) &&
                        (string.IsNullOrEmpty(billingEmail) || (!string.IsNullOrEmpty(oba.Email) && oba.Email.Contains(billingEmail))) &&
                        (string.IsNullOrEmpty(billingLastName) || (!string.IsNullOrEmpty(oba.LastName) && oba.LastName.Contains(billingLastName)))
                    select o;

            if (!string.IsNullOrEmpty(paymentMethodSystemName))
                query = query.Where(o => o.PaymentMethodSystemName == paymentMethodSystemName);

            if (osIds != null && osIds.Any())
                query = query.Where(o => osIds.Contains(o.OrderStatusId));

            if (psIds != null && psIds.Any())
                query = query.Where(o => psIds.Contains(o.PaymentStatusId));

            if (ssIds != null && ssIds.Any())
                query = query.Where(o => ssIds.Contains(o.ShippingStatusId));

            if (startTimeUtc.HasValue)
                query = query.Where(o => startTimeUtc.Value <= o.CreatedOnUtc);

            if (endTimeUtc.HasValue)
                query = query.Where(o => endTimeUtc.Value >= o.CreatedOnUtc);

            if (!string.IsNullOrEmpty(orderNotes))
                query = from o in query
                        join n in _orderNoteRepository.Table on o.Id equals n.OrderId
                        where n.Note.Contains(orderNotes)
                        select o;

            var item = (from oq in query
                        group oq by 1
                into result
                        select new
                        {
                            OrderCount = result.Count(),
                            OrderShippingExclTaxSum = result.Sum(o => o.OrderShippingExclTax),
                            OrderPaymentFeeExclTaxSum = result.Sum(o => o.PaymentMethodAdditionalFeeExclTax),
                            OrderTaxSum = result.Sum(o => o.OrderTax),
                            OrderTotalSum = result.Sum(o => o.OrderTotal),
                            OrederRefundedAmountSum = result.Sum(o => o.RefundedAmount),
                        }).Select(r => new OrderAverageReportLine
                        {
                            CountOrders = r.OrderCount,
                            SumShippingExclTax = r.OrderShippingExclTaxSum,
                            OrderPaymentFeeExclTaxSum = r.OrderPaymentFeeExclTaxSum,
                            SumTax = r.OrderTaxSum,
                            SumOrders = r.OrderTotalSum,
                            SumRefundedAmount = r.OrederRefundedAmountSum
                        }).FirstOrDefault();

            item ??= new OrderAverageReportLine
            {
                CountOrders = 0,
                SumShippingExclTax = decimal.Zero,
                OrderPaymentFeeExclTaxSum = decimal.Zero,
                SumTax = decimal.Zero,
                SumOrders = decimal.Zero
            };
            return item;
        }

        #endregion

    }
}
