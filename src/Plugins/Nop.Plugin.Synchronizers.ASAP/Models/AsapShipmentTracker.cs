using Nop.Plugin.Synchronizers.ASAP.Managers;
using Nop.Services.Logging;
using Nop.Services.Shipping.Tracking;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Synchronizers.ASAP.Models
{
    /// <summary>
    /// Provides the businness logic for interaction that tracking orders.
    /// </summary>
    public sealed class AsapShipmentTracker : IShipmentTracker
    {
        private readonly ILogger _logger;
        private readonly ShippingManager _shippingManager;

        /// <summary>
        /// Creates an instance of <see cref="AsapShipmentTracker"/>
        /// </summary>
        /// <param name="logger">Implement a instance of <see cref="ILogger"/></param>
        /// <param name="shippingManager">Implement a instance of <see cref="ShippingManager"/></param>
        public AsapShipmentTracker(ILogger logger, ShippingManager shippingManager)
        {
            _logger = logger;
            _shippingManager = shippingManager;
        }

        /// <inheritdoc/>
        IList<ShipmentStatusEvent> IShipmentTracker.GetShipmentEvents(string trackingNumber)
        {
            return new List<ShipmentStatusEvent> {};
        }

        /// <inheritdoc/>
        string IShipmentTracker.GetUrl(string trackingNumber)
        {
            try
            {
                return _shippingManager.GetDeliveryTrackingLink(trackingNumber).Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return string.Empty;
            }
        }

        /// <inheritdoc/>
        bool IShipmentTracker.IsMatch(string trackingNumber)
        {
            return true;
        }
    }
}
