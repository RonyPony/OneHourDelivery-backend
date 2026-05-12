using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Events;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    internal class CustomOrderService : OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderItem> _orderItemRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductWarehouseInventory> _productWarehouseInventoryRepository;
        private readonly IRepository<OrderNote> _orderNoteRepository;
        private readonly IRepository<Address> _addressRepository;
        public CustomOrderService(CachingSettings cachingSettings, IEventPublisher eventPublisher, IProductService productService, IRepository<Address> addressRepository, IRepository<Customer> customerRepository, IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<OrderNote> orderNoteRepository, IRepository<Product> productRepository, IRepository<ProductWarehouseInventory> productWarehouseInventoryRepository, IRepository<RecurringPayment> recurringPaymentRepository, IRepository<RecurringPaymentHistory> recurringPaymentHistoryRepository, IShipmentService shipmentService) : base(cachingSettings, eventPublisher, productService, addressRepository, customerRepository, orderRepository, orderItemRepository, orderNoteRepository, productRepository, productWarehouseInventoryRepository, recurringPaymentRepository, recurringPaymentHistoryRepository, shipmentService)
        {
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderNoteRepository = orderNoteRepository;
            _productRepository = productRepository;
            _productWarehouseInventoryRepository = productWarehouseInventoryRepository;
        }

        public override IPagedList<Order> SearchOrders(int storeId = 0,
            int vendorId = 0, int customerId = 0,
            int productId = 0, int affiliateId = 0, int warehouseId = 0,
            int billingCountryId = 0, string paymentMethodSystemName = null,
            DateTime? createdFromUtc = null, DateTime? createdToUtc = null,
            List<int> osIds = null, List<int> psIds = null, List<int> ssIds = null,
            string billingPhone = null, string billingEmail = null, string billingLastName = "",
            string orderNotes = null, int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false)
        {
            var query = _orderRepository.Table;

            if (storeId > 0)
                query = query.Where(o => o.StoreId == storeId);

            if (vendorId > 0)
            {

                query = from o in query
                        join oi in _orderItemRepository.Table on o.Id equals oi.OrderId
                        join p in _productRepository.Table on oi.ProductId equals p.Id

                        where p.VendorId == vendorId
                        select o;
                query = query.Distinct();
            }

            if (customerId > 0)
                query = query.Where(o => o.CustomerId == customerId);

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

            if (!string.IsNullOrEmpty(paymentMethodSystemName))
                query = query.Where(o => o.PaymentMethodSystemName == paymentMethodSystemName);

            if (affiliateId > 0)
                query = query.Where(o => o.AffiliateId == affiliateId);

            if (createdFromUtc.HasValue)
                query = query.Where(o => createdFromUtc.Value <= o.CreatedOnUtc);

            if (createdToUtc.HasValue)
                query = query.Where(o => createdToUtc.Value >= o.CreatedOnUtc);

            if (osIds != null && osIds.Any())
                query = query.Where(o => osIds.Contains(o.OrderStatusId));

            if (psIds != null && psIds.Any())
                query = query.Where(o => psIds.Contains(o.PaymentStatusId));

            if (ssIds != null && ssIds.Any())
                query = query.Where(o => ssIds.Contains(o.ShippingStatusId));

            if (!string.IsNullOrEmpty(orderNotes))
                query = query.Where(o => _orderNoteRepository.Table.Any(oNote => oNote.OrderId == o.Id && oNote.Note.Contains(orderNotes)));

            query = from o in query
                    join oba in _addressRepository.Table on o.BillingAddressId equals oba.Id
                    where
                        (billingCountryId <= 0 || (oba.CountryId == billingCountryId)) &&
                        (string.IsNullOrEmpty(billingPhone) || (!string.IsNullOrEmpty(oba.PhoneNumber) && oba.PhoneNumber.Contains(billingPhone))) &&
                        (string.IsNullOrEmpty(billingEmail) || (!string.IsNullOrEmpty(oba.Email) && oba.Email.Contains(billingEmail))) &&
                        (string.IsNullOrEmpty(billingLastName) || (!string.IsNullOrEmpty(oba.LastName) && oba.LastName.Contains(billingLastName)))
                    select o;

            query = query.Where(o => !o.Deleted);
            query = query.OrderByDescending(o => o.CreatedOnUtc);

            //database layer paging

            return new PagedList<Order>(query, pageIndex, pageSize);
        }
    }
}
