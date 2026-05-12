using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Areas.Admin.Models.Reports;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the order model factory
    /// </summary>
    public partial interface IOrderPendingToClosePaymentModelFactory
    {
        /// <summary>
        /// Prepare order search model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order search model</returns>
        OrderPendingToClosePaymentSearchModel PrepareOrderSearchModel(OrderPendingToClosePaymentSearchModel searchModel);

        /// <summary>
        /// Prepare order tracing search model
        /// </summary>
        /// <param name="searchModel">Order tracing search model</param>
        /// <returns>Order search model</returns>
        OrderTracingSearchModel PrepareTracingOrderSearchModel(OrderTracingSearchModel searchModel);

        /// <summary>
        /// Prepare paged order list model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order list model</returns>
        OrderPendingToClosePaymentListModel PrepareOrderListModel(OrderPendingToClosePaymentSearchModel searchModel);

        /// <summary>
        /// Prepare paged order  tracing list model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order list model</returns>
        OrderTracingListModel PrepareOrderTracingListModel(OrderTracingSearchModel searchModel);

        /// <summary>
        /// Prepare paged order list model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order list model</returns>
        OrderPendingToClosePaymentListModel PrepareVendorOrderEarningListModel(OrderPendingToClosePaymentSearchModel searchModel);

        /// <summary>
        /// Prepare order aggregator model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order aggregator model</returns>
        OrderAggreratorModel PrepareOrderAggregatorModel(OrderPendingToClosePaymentSearchModel searchModel);

        /// <summary>
        /// Prepare order model
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Order model</returns>
        OrderPendingToClosePaymentModel PrepareOrderModel(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order, bool excludeProperties = false);

        /// <summary>
        /// Prepare order pending to close payment model from order
        /// </summary>
        /// <param name="model">Order pending to close payment model</param>
        /// <param name="order">Order</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Order pending to close payment model</returns>
        OrderPendingToClosePaymentModel PrepareOrderPendingToClosePaymentModelFromOrder(OrderPendingToClosePaymentModel model, Order order, bool excludeProperties = false);

        /// <summary>
        /// Prepare product search model to add to the order
        /// </summary>
        /// <param name="searchModel">Product search model to add to the order</param>
        /// <param name="order">Order</param>
        /// <returns>Product search model to add to the order</returns>
        AddProductToOrderSearchModel PrepareAddProductToOrderSearchModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order);

        /// <summary>
        /// Prepare paged product list model to add to the order
        /// </summary>
        /// <param name="searchModel">Product search model to add to the order</param>
        /// <param name="order">Order</param>
        /// <returns>Product search model to add to the order</returns>
        AddProductToOrderListModel PrepareAddProductToOrderListModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order);

        /// <summary>
        /// Prepare product model to add to the order
        /// </summary>
        /// <param name="model">Product model to add to the order</param>
        /// <param name="order">Order</param>
        /// <param name="product">Product</param>
        /// <returns>Product model to add to the order</returns>
        AddProductToOrderModel PrepareAddProductToOrderModel(AddProductToOrderModel model, OrderPendingToClosePayment order, Product product);

        /// <summary>
        /// Prepare order address model
        /// </summary>
        /// <param name="model">Order address model</param>
        /// <param name="order">Order</param>
        /// <param name="address">Address</param>
        /// <returns>Order address model</returns>
        OrderAddressModel PrepareOrderAddressModel(OrderAddressModel model, OrderPendingToClosePayment order, Address address);

        /// <summary>
        /// Prepare paged order note list model
        /// </summary>
        /// <param name="searchModel">Order note search model</param>
        /// <param name="order">Order</param>
        /// <returns>Order note list model</returns>
        OrderNoteListModel PrepareOrderNoteListModel(OrderNoteSearchModel searchModel, OrderPendingToClosePayment order);

        /// <summary>
        /// Prepare bestseller brief search model
        /// </summary>
        /// <param name="searchModel">Bestseller brief search model</param>
        /// <returns>Bestseller brief search model</returns>
        BestsellerBriefSearchModel PrepareBestsellerBriefSearchModel(BestsellerBriefSearchModel searchModel);

        /// <summary>
        /// Prepare paged bestseller brief list model
        /// </summary>
        /// <param name="searchModel">Bestseller brief search model</param>
        /// <returns>Bestseller brief list model</returns>
        BestsellerBriefListModel PrepareBestsellerBriefListModel(BestsellerBriefSearchModel searchModel);

        /// <summary>
        /// Prepare order average line summary report list model
        /// </summary>
        /// <param name="searchModel">Order average line summary report search model</param>
        /// <param name="vendorId"> Vendor id of the vendor that sells the product</param>
        /// <returns>Order average line summary report list model</returns>
        OrderAverageReportListModel PrepareOrderAverageReportListModel(OrderAverageReportSearchModel searchModel, int vendorId);

        /// <summary>
        /// Prepare incomplete order report list model
        /// </summary>
        /// <param name="searchModel">Incomplete order report search model</param>
        /// <returns>Incomplete order report list model</returns>
        OrderIncompleteReportListModel PrepareOrderIncompleteReportListModel(OrderIncompleteReportSearchModel searchModel, int vendorId);
    }
}
