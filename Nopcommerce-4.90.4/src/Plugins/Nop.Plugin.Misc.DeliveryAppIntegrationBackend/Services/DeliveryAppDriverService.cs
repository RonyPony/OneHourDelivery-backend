using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Media;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IDeliveryAppDriverService"/>.
    /// </summary>
    public sealed class DeliveryAppDriverService : IDeliveryAppDriverService
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IDeliveryAppOrderService _deliveryAppOrderService;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IRepository<OrderPendingToClosePayment> _orderPendingToClosePaymentRepository;
        private readonly IVendorService _vendorService;
        private readonly IRepository<DriverRatingMapping> _driverRatingMappingRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IAddressService _addressService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPictureService _pictureService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly IOrderPaymentCollectionService _orderPaymentCollectionService;
        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppDriverService"/>.
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="deliveryAppOrderService">An implementation of <see cref="IDeliveryAppOrderService"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="orderPendingToClosePaymentRepository">An implementation of <see cref="IRepository{OrderPendingToClosePayment}"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="driverRatingMappingRepository">An implementation of <see cref="IRepository{DriverRatingMapping}"/>.</param>
        /// <param name="customerRepository">An implementation of <see cref="IRepository{Customer}"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/>.</param>
        /// <param name="customerAttributeParser">An implementation of <see cref="ICustomerAttributeParser"/>.</param>
        /// <param name="orderPaymentCollectionService">An implementation of <see cref="IOrderPaymentCollectionService"/>.</param>
        /// <param name="deliveryAppBackendConfigurationSettings"> Delivery app backend plugin settings. </param>
        public DeliveryAppDriverService(
            ICustomerService customerService,
            IDeliveryAppOrderService deliveryAppOrderService,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IRepository<OrderPendingToClosePayment> orderPendingToClosePaymentRepository,
            IVendorService vendorService,
            IRepository<DriverRatingMapping> driverRatingMappingRepository,
            IRepository<Customer> customerRepository,
            IAddressService addressService,
            IGenericAttributeService genericAttributeService,
            IPictureService pictureService,
            ICustomerAttributeParser customerAttributeParser,
            IOrderPaymentCollectionService orderPaymentCollectionService,
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings)
        {
            _customerService = customerService;
            _deliveryAppOrderService = deliveryAppOrderService;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _orderPendingToClosePaymentRepository = orderPendingToClosePaymentRepository;
            _vendorService = vendorService;
            _driverRatingMappingRepository = driverRatingMappingRepository;
            _customerRepository = customerRepository;
            _addressService = addressService;
            _genericAttributeService = genericAttributeService;
            _pictureService = pictureService;
            _customerAttributeParser = customerAttributeParser;
            _orderPaymentCollectionService = orderPaymentCollectionService;
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
        }

        ///<inheritdoc/>
        public DriverInfoModel GetDriverInfo(int orderId)
        {
            if (orderId == 0)
                throw new ArgumentException("InvalidOrderId");

            OrderDeliveryStatusMapping orderMapping = _orderDeliveryStatusMappingRepository
                .Table.FirstOrDefault(mapping => mapping.OrderId == orderId);

            if (orderMapping is null)
                throw new ArgumentException("InvalidOrderMapping");

            if (orderMapping.CustomerId is null)
                throw new ArgumentException("ThereAreNotDriverAssignedToOrder");

            var customerOrderDeliveredCount = _orderDeliveryStatusMappingRepository.Table
                .Where(mapping => mapping.CustomerId == orderMapping.CustomerId &&
                mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered).ToList().Count;

            Customer foundCustomer = _customerService.GetCustomerById(orderMapping.CustomerId.Value);

            if (foundCustomer is null)
                throw new ArgumentException("CustomerNotFound");

            if (foundCustomer.ShippingAddressId is null)
                throw new ArgumentException("ShippingAddressNotFound to: "+ foundCustomer.Email);

            Address foundCustomerAddress = _addressService.GetAddressById(foundCustomer.ShippingAddressId.Value);

            if (foundCustomerAddress is null)
                throw new ArgumentException("AddressNotFound");


            GenericAttribute customCustomerAttibute = _genericAttributeService
                .GetAttributesForEntity(foundCustomer.Id, foundCustomer.GetType().Name).FirstOrDefault(attibute => attibute.Key == "CustomCustomerAttributes");

            if (customCustomerAttibute is null)
                throw new ArgumentException("CustomerAttributeNotFound for Driver "+foundCustomer.Email);

            var customerAttribute = _customerAttributeParser
                .ParseCustomerAttributes(customCustomerAttibute.Value)
                .Select(attribute => new DeliveryAppCustomerAttribute
                {
                    AttributeId = attribute.Id,
                    AttributeName = attribute.Name
                }).ToDictionary(key => key.AttributeName);

            string customerRating = GetAttributeValue(customerAttribute, customCustomerAttibute, Defaults.CustomerRatingAttribute.Name);
            string vehicleBrand = GetAttributeValue(customerAttribute, customCustomerAttibute, Defaults.VehicleBrandAttribute.Name);
            string vehicleModel = GetAttributeValue(customerAttribute, customCustomerAttibute, Defaults.VehicleModelAttribute.Name);
            string vehiclePlate = GetAttributeValue(customerAttribute, customCustomerAttibute, Defaults.VehicleLicensePlateAttribute.Name);
            string vehicleColor = GetAttributeValue(customerAttribute, customCustomerAttibute, Defaults.VehicleColorAttribute.Name);

            return new DriverInfoModel
            {
                CustomerId = foundCustomer.Id,
                DriverName = $"{foundCustomerAddress.FirstName} {foundCustomerAddress.LastName}",
                DriverPicture = _pictureService
                .GetPictureUrl(_genericAttributeService.GetAttribute<int>(foundCustomer, NopCustomerDefaults.AvatarPictureIdAttribute)),
                OrdersDeliveredCount = customerOrderDeliveredCount,
                DriverRating = string.IsNullOrWhiteSpace(customerRating) ? 0 : decimal.Parse(customerRating),
                DriverRegisteredDate = Math.Abs(foundCustomer.CreatedOnUtc.Month - DateTime.Now.Month),
                DriverVehicleInfo = new DriverVehicleInfo
                {
                    VehicleBrand = vehicleBrand ?? "N/A",
                    VehicleModel = vehicleModel ?? "N/A",
                    VehicleColor = vehicleColor ?? "N/A",
                    VehiclePlate = vehiclePlate ?? "N/A"
                }
            };
        }

        private string GetAttributeValue(Dictionary<string, DeliveryAppCustomerAttribute> customerAttribute, GenericAttribute customCustomerAttibute, string attributeName)
        {
            if (!customerAttribute.ContainsKey(attributeName))
            {
                return null;
            }

            return _customerAttributeParser
                    .ParseValues(customCustomerAttibute.Value, customerAttribute.ContainsKey(attributeName) ? customerAttribute[attributeName].AttributeId : 0)
                    .FirstOrDefault();
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public IList<DriverProfitModel> GetOrdersPendingToClosePaymentByDriverId(int driverId)
        {
            Customer driverCustomer = _customerService.GetCustomerById(driverId);

            if (driverCustomer == null) throw new ArgumentException("DriverNotFound");

            IList<OrderDeliveryStatusMapping> ordersDeliveredByDriver = _orderDeliveryStatusMappingRepository.Table
                                                                                                             .Where(mapping 
                                                                                                                        => mapping.CustomerId == driverCustomer.Id && 
                                                                                                                           mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered)
                                                                                                             .ToList();

            IList<OrderPendingToClosePayment> ordersPending = _orderPendingToClosePaymentRepository.Table
                                                                                                   .Where(orderPending 
                                                                                                            => ordersDeliveredByDriver.Select(mapping => mapping.OrderId)
                                                                                                                                      .Contains(orderPending.OrderId))
                                                                                                   .ToList();
            var driverProfits = new List<DriverProfitModel>();

            foreach (OrderPendingToClosePayment orderPending in ordersPending)
            {
                Vendor orderVendor = new Vendor();
                int tmpVendorId = _deliveryAppOrderService.GetVendorIdByOrderId(Convert.ToInt32(orderPending.CustomOrderNumber));
                if (tmpVendorId!=0)
                {
                   orderVendor = _vendorService.GetVendorById(tmpVendorId);
                }
                else
                {
                    Vendor notFoundVendor = new Vendor();
                    notFoundVendor.Id = 0;
                    notFoundVendor.Name = "Sin Suplidor";
                    orderVendor = notFoundVendor;
                }
                

                driverProfits.Add(new DriverProfitModel
                {
                    OrderId = orderPending.Id,
                    CustomOrderNumber = orderPending.CustomOrderNumber,
                    DriverProfitAmount = orderPending.ShippingTaxMessengerProfitAmount,
                    PaymentStatusId = orderPending.DriverPaymentStatusId,
                    PaymentStatusName = orderPending.DriverPaymentStatus.ToString(),
                    ShippingTaxAmount = orderPending.OrderShippingExclTax,
                    DeliveredOnUtc = ordersDeliveredByDriver.FirstOrDefault(mapping => mapping.OrderId == orderPending.OrderId)?.DeliveredDate,
                    StoreName = orderVendor?.Name ?? "N/A"
                });
            }

            return driverProfits.OrderByDescending(x => x.DeliveredOnUtc).ToList();
        }

        ///<inheritdoc/>
        public void RegisterDriverRatingValoration(DriverRatingMappingRequest driverRating)
        {
            int customerId = 0;

            if (driverRating is null)
                throw new ArgumentException("InvalidDriverRequest");

            if (driverRating.RatingType == DriverRatingType.IsVendor)
            {
                Vendor foundVendor = _vendorService.GetVendorById(driverRating.CustomerId);

                if (foundVendor is null)
                    throw new ArgumentException("VendorNotFound");

                Customer foundCustomer = _customerRepository.Table
                    .FirstOrDefault(customer => customer.VendorId == foundVendor.Id);

                if (foundCustomer is null)
                    throw new ArgumentException("CustomerNotFound");

                customerId = foundCustomer.Id;

                if (_driverRatingMappingRepository.Table
                    .Any(rating => rating.CustomerId == customerId &&
                    rating.DriverId == driverRating.DriverId && rating.OrderId == driverRating.OrderId))
                    throw new ArgumentException("TheVendorHasBeenRated");
            }
            else
            {
                customerId = driverRating.CustomerId;

                if (_driverRatingMappingRepository.Table
                    .Any(rating => rating.CustomerId == customerId &&
                    rating.DriverId == driverRating.DriverId && rating.OrderId == driverRating.OrderId))
                    throw new ArgumentException("TheCustomerHasBeenRated");
            }

            _driverRatingMappingRepository.Insert(new DriverRatingMapping
            {
                DriverId = driverRating.DriverId,
                CustomerId = customerId,
                OrderId = driverRating.OrderId,
                Rating = driverRating.Rating,
                RatingType = driverRating.RatingType,
                CreatedOnUtc = DateTime.UtcNow
            });
        }

        ///<inheritdoc/>
        public DriverPaymentCollection GetPaymentCollection(int driverId)
        {
            Customer driverCustomer = _customerService.GetCustomerById(driverId);
            if (driverCustomer == null) throw new ArgumentException("DriverNotFound");

            IList<OrderPaymentCollectionStatus> orderPendingToDeliverMoney = _orderPaymentCollectionService.GetOrdersPendingToCollectByDriverCustomerId(driverId);

            DriverPaymentCollection result = new DriverPaymentCollection
            {
                TotalAmountPendingToDeliver = orderPendingToDeliverMoney == null ? 0 : orderPendingToDeliverMoney.Sum(x => x.OrderTotal),
                TotalLimitAmount = _deliveryAppBackendConfigurationSettings.MaxMoneyAmountDriverCanCarry
            };

            return result;
        }

        #endregion
    }

    class DeliveryAppCustomerAttribute
    {
        /// <summary>
        /// Customer attribute id.
        /// </summary>
        public int AttributeId { get; set; }

        /// <summary>
        /// Customer attribute name.
        /// </summary>
        public string AttributeName { get; set; }
    }
}
