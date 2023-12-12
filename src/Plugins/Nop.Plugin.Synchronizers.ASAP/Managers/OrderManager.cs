using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Synchronizers.ASAP.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.ASAP.Managers
{
    /// <summary>
    /// Provides the businness logic for interaction that interacts with orders.
    /// </summary>
    public sealed class OrderManager
    {
        #region Fields

        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IAsapDeliveryService _asapDeliveryService;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="OrderManager"/>
        /// </summary>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/></param>
        /// <param name="addressRepository">An implementation of <see cref="IRepository{Address}"/></param>
        /// <param name="asapDeliveryService">An implementation of <see cref="IAsapDeliveryService"/></param>
        public OrderManager(IRepository<Order> orderRepository, IRepository<Address> addressRepository, IAsapDeliveryService asapDeliveryService)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
            _asapDeliveryService = asapDeliveryService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all the orders that should be updated
        /// </summary>
        /// <returns>Returns <see cref="IEnumerable{Order}"/> For this operation </returns>
        public IEnumerable<Order> GetOrdersToUpdate() => _orderRepository.Table.Where(order => order.ShippingRateComputationMethodSystemName.Equals(AsapDeliveryDefaults.SystemName)
           && order.ShippingStatusId != (int)ShippingStatus.Delivered && order.PaymentStatusId == (int)PaymentStatus.Paid).ToList();

        /// <summary>
        /// Creates Order in ASAP.
        /// </summary>
        /// <param name="shippingAddressId">Identification number for a shipping address</param>
        /// <returns>Return a tracking number by order </returns>
        public async Task<string> CreateOrder(int? shippingAddressId)
        {
            Address shippingAddress = _addressRepository.GetById(shippingAddressId);

            return await _asapDeliveryService.CreateOrder(shippingAddress);
        }

        #endregion
    }
}
