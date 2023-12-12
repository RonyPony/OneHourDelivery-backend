using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    ///<inheritdoc/>
    public class NotificationRequestBuilder : INotificationRequestBuilder
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;
        private readonly IGenericAttributeService _genericAttributeService;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="NotificationCenterService"/>.
        /// </summary>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="logger">An instance of <see cref="ILogger"/>.</param>
        public NotificationRequestBuilder(
            IAddressService addressService,
            IOrderService orderService,
            IProductService productService,
            IVendorService vendorService,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            ICustomerService customerService,
            ILogger logger,
            IGenericAttributeService genericAttributeService)
        {
            _addressService = addressService;
            _orderService = orderService;
            _productService = productService;
            _vendorService = vendorService;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _customerService = customerService;
            _logger = logger;
            _genericAttributeService = genericAttributeService;
        }
        #endregion

        ///<inheritdoc/>
        public NotificationRequest Build(NotificationTemplateType type, int orderId)
        {
            var types = new Dictionary<NotificationTemplateType, Func<Order, NotificationTemplateType, NotificationRequest>>()
            {
                {NotificationTemplateType.ClientClientOrderCreated, ClientClientOrderCreated },
                {NotificationTemplateType.ClientCommerceNewOrder, ClientCommerceNewOrder },
                {NotificationTemplateType.CommerceClientOrderAccepted, CommerceClientOrderAccepted},
                {NotificationTemplateType.CommerceClientOrderCompleted, CommerceClientOrderCompleted},
                {NotificationTemplateType.CommerceDriverOrderCompleted, CommerceDriverOrderCompleted},
                {NotificationTemplateType.DriverClientOrderAccepted, DriverClientOrderAccepted},
                {NotificationTemplateType.DriverCommerceOrderAccepted, DriverCommerceOrderAccepted},
                {NotificationTemplateType.DriverClientOrderRetreived, DriverClientOrderRetreived},
                {NotificationTemplateType.DriverClientOrderDelivered, DriverClientOrderDelivered},
                {NotificationTemplateType.DriverCommerceOrderDelivered, DriverCommerceOrderDelivered },
                {NotificationTemplateType.CommerceClientOrderCancelled, CommerceClientOrderCancelled},
                {NotificationTemplateType.DriverCommerceOrderCancelled, DriverCommerceOrderCancelled},
            };

            try
            {
                Order order = _orderService.GetOrderById(orderId);

                if (order is null)
                    throw new ArgumentException("OrderNotFound");

                return types[type](order, type);

            }
            catch (Exception e)
            {
                _logger.Error(e.ToString(), e);
                return null;
            }
        }

        private NotificationRequest DriverCommerceOrderCancelled(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.CommerceApp,
                CustomerId = vendor.Id,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() },
                    {"commerce", vendor.Name},
                    {"reason", GetOrderCancelMessage(order)}
                }
            };
        }

        private NotificationRequest CommerceClientOrderCancelled(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.ClientApp,
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() },
                    {"commerce", vendor.Name},
                    {"reason", GetOrderCancelNotes(order)}
                }
            };
        }

        private NotificationRequest DriverCommerceOrderDelivered(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.CommerceApp,
                CustomerId = vendorCustomer.Id,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() }
                }
            };
        }

        private NotificationRequest DriverClientOrderDelivered(Order order, NotificationTemplateType type) => new NotificationRequest
        {
            AppPackageName = MobileAppPackageName.ClientApp,
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            TemplateType = type,
            Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() },
                    {"client", GetClientName(order)}
                }
        };

        private NotificationRequest DriverClientOrderRetreived(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.ClientApp,
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString()},
                    {"commerce", vendor.Name},
                    {"driver", GetDriver(order).Item2.FirstName}
                }
            };
        }

        private NotificationRequest DriverCommerceOrderAccepted(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.CommerceApp,
                CustomerId = vendorCustomer.Id,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() }
                }
            };
        }

        private NotificationRequest DriverClientOrderAccepted(Order order, NotificationTemplateType type) => new NotificationRequest
        {
            AppPackageName = MobileAppPackageName.ClientApp,
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            TemplateType = type,
            Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() },
                    {"driver", GetDriver(order).Item2.FirstName}
                }
        };

        private NotificationRequest CommerceDriverOrderCompleted(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);
            (Customer driver, _) = GetDriver(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.DriverApp,
                CustomerId = driver.Id,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString()},
                    {"commerce", vendor.Name}
                }
            };
        }

        private NotificationRequest CommerceClientOrderCompleted(Order order, NotificationTemplateType type) => new NotificationRequest
        {
            AppPackageName = MobileAppPackageName.ClientApp,
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            TemplateType = type,
            Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() }
                }
        };

        private NotificationRequest CommerceClientOrderAccepted(Order order, NotificationTemplateType type) => new NotificationRequest
        {
            AppPackageName = MobileAppPackageName.ClientApp,
            CustomerId = order.CustomerId,
            OrderId = order.Id,
            TemplateType = type,
            Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() }
                }
        };

        #region Private methods
        private NotificationRequest ClientClientOrderCreated(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.ClientApp,
                CustomerId = order.CustomerId,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString()},
                    {"commerce", vendor.Name},
                    {"client", GetClientName(order)}
                }
            };
        }

        private NotificationRequest ClientCommerceNewOrder(Order order, NotificationTemplateType type)
        {
            (Vendor vendor, Customer vendorCustomer) = GetVendor(order);

            return new NotificationRequest
            {
                AppPackageName = MobileAppPackageName.CommerceApp,
                CustomerId = vendorCustomer.Id,
                OrderId = order.Id,
                TemplateType = type,
                Payload = new Dictionary<string, string>()
                {
                    {"orderId", order.Id.ToString() }
                }
            };
        }

        private string GetOrderCancelMessage(Order order)
        {
            OrderDeliveryStatusMapping orderDeliveryStatusMapping = _orderDeliveryStatusMappingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == order.Id);

            if (orderDeliveryStatusMapping is null)
                throw new ArgumentException("OrderDeliveryNotFound");

            return orderDeliveryStatusMapping.MessageToDeclinedOrder;
        }

        private string GetOrderCancelNotes(Order order)
        {
            OrderNote orderNote = _orderService.GetOrderNotesByOrderId(order.Id).OrderByDescending(x => x.CreatedOnUtc)
                                                                        .FirstOrDefault(x => x.Note.Contains("cancelled"));
            if (orderNote is null)
                throw new ArgumentException("OrderNotFound");

            string[] splittedMessage = orderNote.Note.Split("Message:");

            string actualMessage = splittedMessage[1];

            return actualMessage;
        }

        private (Vendor, Customer) GetVendor(Order order)
        {
            IList<OrderItem> orderItems = _orderService.GetOrderItems(order.Id);
            Product product = _productService.GetProductById(orderItems.First().ProductId);

            Vendor vendor = _vendorService.GetVendorById(product.VendorId);
            Customer vendorCustomer = _customerService.GetCustomerByEmail(vendor.Email);

            return (vendor, vendorCustomer);
        }

        private string GetClientName(Order order, bool includeLastname = false)
        {
            Customer foundCustomer = _customerService.GetCustomerById(order.CustomerId);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            var firstName = _genericAttributeService.GetAttribute<string>(foundCustomer, NopCustomerDefaults.FirstNameAttribute);
            var lastName = _genericAttributeService.GetAttribute<string>(foundCustomer, NopCustomerDefaults.LastNameAttribute);

            return firstName + (includeLastname ? $" {lastName}" : "");
        }

        private (Customer, Address) GetDriver(Order order)
        {
            OrderDeliveryStatusMapping orderMapping = _orderDeliveryStatusMappingRepository.Table
                  .FirstOrDefault(statusMapping => statusMapping.OrderId == order.Id);

            if (orderMapping is null)
                throw new ArgumentException("OrderDeliveryNotFound");

            Customer driver = _customerService.GetCustomerById(orderMapping.CustomerId.Value);

            if (driver is null)
                throw new ArgumentException("CustomerNotFound");

            if (driver.ShippingAddressId is null)
                throw new ArgumentException("ShippingAddressNotFound");

            Address driverAddress = _addressService.GetAddressById(driver.ShippingAddressId.Value);

            return (driver, driverAddress);

        }
        #endregion
    }
}
