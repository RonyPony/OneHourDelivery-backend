using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents an implementation for <see cref="IOrderPendingToPayReportServices"/>.
    /// </summary>
    public sealed class OrderPendingToPayReportServices : IOrderPendingToPayReportServices
    {
        #region Fields

        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IRepository<OrderPendingToClosePayment> _orderPendingToClosePaymentRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Product> _productRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderPendingToPayReportServices"/>.
        /// </summary>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="orderPendingToClosePaymentRepository">An implementation of <see cref="IRepository{OrderPendingToClosePayment}"/>.</param>
        /// <param name="orderItemRepository">An implementation of <see cref="IRepository{OrderItem}"/>.</param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{Product}"/>.</param>
        public OrderPendingToPayReportServices(
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IRepository<OrderPendingToClosePayment> orderPendingToClosePaymentRepository,
            IRepository<OrderItem> orderItemRepository,
            IRepository<Product> productRepository)
        {
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _orderPendingToClosePaymentRepository = orderPendingToClosePaymentRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public OrderAverageReportLine GetOrderAverageReportLine(int vendorId = 0, int driverId = 0, string paymentMethodSystemName = null,
            List<int> psIdsVendor = null, List<int> psIdsDriver = null, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null)
        {
            var query = _orderPendingToClosePaymentRepository.Table;

            query = query.Where(o => !o.Deleted);

            if (driverId > 0)
                query = from o in query
                        join od in _orderDeliveryStatusMappingRepository.Table on o.OrderId equals od.OrderId
                        where od.CustomerId == driverId && od.DeliveryStatusId == (int)DeliveryStatus.Delivered
                        select o;

            if (vendorId > 0)
                query = from o in query
                        join oi in _orderItemRepository.Table on o.OrderId equals oi.OrderId
                        join p in _productRepository.Table on oi.ProductId equals p.Id
                        where p.VendorId == vendorId
                        select o;

            if (!string.IsNullOrEmpty(paymentMethodSystemName))
                query = query.Where(o => o.PaymentMethodSystemName == paymentMethodSystemName);

            if (psIdsVendor != null && psIdsVendor.Any())
                query = query.Where(o => psIdsVendor.Contains(o.VendorPaymentStatusId));

            if (psIdsDriver != null && psIdsDriver.Any())
                query = query.Where(o => psIdsDriver.Contains(o.DriverPaymentStatusId));

            if (startTimeUtc.HasValue)
                query = query.Where(o => startTimeUtc.Value <= o.CreatedOnUtc);

            if (endTimeUtc.HasValue)
                query = query.Where(o => endTimeUtc.Value >= o.CreatedOnUtc);

            var item = (from oq in query
                        group oq by 1
                into result
                        select new
                        {
                            OrderCount = result.Count(),
                            OrderShippingExclTaxSum = result.Sum(o => o.OrderShippingInclTax),
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

        ///<inheritdoc/>
        public decimal ProfitReport(int vendorId = 0, int driverId = 0, string paymentMethodSystemName = null,
            List<int> psIdsVendor = null, List<int> psIdsDriver = null, DateTime? startTimeUtc = null, DateTime? endTimeUtc = null)
        {
            var dontSearchPaymentMethods = string.IsNullOrEmpty(paymentMethodSystemName);

            var orders = _orderPendingToClosePaymentRepository.Table;

            if (psIdsVendor != null && psIdsVendor.Any())
                orders = orders.Where(o => psIdsVendor.Contains(o.VendorPaymentStatusId));

            if (psIdsDriver != null && psIdsDriver.Any())
                orders = orders.Where(o => psIdsDriver.Contains(o.DriverPaymentStatusId));

            var query = from orderItem in _orderItemRepository.Table
                        join o in orders on orderItem.OrderId equals o.OrderId
                        join od in _orderDeliveryStatusMappingRepository.Table on o.OrderId equals od.OrderId
                        join p in _productRepository.Table on orderItem.ProductId equals p.Id
                        where (dontSearchPaymentMethods || paymentMethodSystemName == o.PaymentMethodSystemName) &&
                              (!startTimeUtc.HasValue || startTimeUtc.Value <= o.CreatedOnUtc) &&
                              (!endTimeUtc.HasValue || endTimeUtc.Value >= o.CreatedOnUtc) &&
                              !o.Deleted &&
                              (vendorId == 0 || p.VendorId == vendorId) &&
                              (driverId == 0 || od.CustomerId == driverId)
                        select orderItem;

            var productCost = Convert.ToDecimal(query.Sum(orderItem => (decimal?)orderItem.OriginalProductCost * orderItem.Quantity));

            var reportSummary = GetOrderAverageReportLine(
                vendorId,
                driverId,
                paymentMethodSystemName,
                psIdsVendor,
                psIdsDriver,
                startTimeUtc,
                endTimeUtc);

            var profit = reportSummary.SumOrders
                         - reportSummary.SumShippingExclTax
                         - reportSummary.OrderPaymentFeeExclTaxSum
                         - reportSummary.SumTax
                         - reportSummary.SumRefundedAmount
                         - productCost;

            return profit;
        }

        #endregion
    }
}
