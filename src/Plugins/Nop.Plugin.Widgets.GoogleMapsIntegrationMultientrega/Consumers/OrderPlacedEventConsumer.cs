using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Consumers
{
    /// <summary>
    /// Represents an event consumer for <see cref="OrderPlacedEvent"/>.
    /// </summary>
    public sealed class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
    {
        #region Properties

        private readonly ICustomerService _customerService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderPlacedEventConsumer"/>.
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        public OrderPlacedEventConsumer(
            ICustomerService customerService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ILogger logger,
            INotificationService notificationService)
        {
            _customerService = customerService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _logger = logger;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event consumer that will be fired every time an order is placed.
        /// </summary>
        /// <param name="eventMessage">An instance of <see cref="OrderPlacedEvent"/>.</param>
        public void HandleEvent(OrderPlacedEvent eventMessage)
        {
            if (eventMessage.Order.ShippingAddressId.HasValue)
            {
                try
                {
                    Customer customer = _customerService.GetCustomerById(eventMessage.Order.CustomerId);

                    if (customer.ShippingAddressId.HasValue && customer.ShippingAddressId.Value != eventMessage.Order.ShippingAddressId.Value)
                    {
                        AddressGeoCoordinatesMapping customerShippingAddressGeoCoordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(customer.ShippingAddressId.Value);
                        AddressGeoCoordinatesMapping orderShippingAddressGeoCoordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(eventMessage.Order.ShippingAddressId.Value);

                        if (customerShippingAddressGeoCoordinates != null && orderShippingAddressGeoCoordinates is null)
                        {
                            orderShippingAddressGeoCoordinates = new AddressGeoCoordinatesMapping
                            {
                                AddressId = eventMessage.Order.ShippingAddressId.Value,
                                Latitude = customerShippingAddressGeoCoordinates.Latitude,
                                Longitude = customerShippingAddressGeoCoordinates.Longitude,
                                ProvinceId = customerShippingAddressGeoCoordinates.ProvinceId,
                                DistrictId = customerShippingAddressGeoCoordinates.DistrictId,
                                TownshipId = customerShippingAddressGeoCoordinates.TownshipId,
                                NeighborhoodId = customerShippingAddressGeoCoordinates.NeighborhoodId
                            };

                            _addressGeoCoordinatesService.InsertAddressGeoCoordinates(orderShippingAddressGeoCoordinates, eventMessage.Order.ShippingAddressId.Value);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.Error($"Error duplicating shipping address geo coordinates for order #{eventMessage.Order.Id}. {e.Message}", e);
                    _notificationService.WarningNotification("Order placed successfully but some errors may have occurred while processing shipment information, please contact the administrators.");
                }
            }
        }

        #endregion
    }
}
