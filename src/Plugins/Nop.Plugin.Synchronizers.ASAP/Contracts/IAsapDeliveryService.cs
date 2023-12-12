using Nop.Core.Domain.Common;
using Nop.Plugin.Synchronizers.ASAP.Models;
using System.Threading.Tasks;

namespace Nop.Plugin.Synchronizers.ASAP.Contracts
{
    /// <summary>
    /// Provides Asap Delivery Interaction.
    /// </summary>
    public interface IAsapDeliveryService
    {
        /// <summary>
        /// Gets Delivery tracking link
        /// </summary>
        /// <param name="deliveryId"> delivery id </param>
        /// <returns> Returns <see cref=Task{string}"/></returns>
        Task<string> GetDeliveryTrackingLink(string deliveryId);

        /// <summary>
        /// Gets Delivery status
        /// </summary>
        /// <param name="deliveryId"> delivery id </param>
        /// <returns> Returns <see cref=Task{AsapDeliveryStatus}"/></returns>
        Task<AsapDeliveryStatus> GetDeliveryStatus(string deliveryId);

        /// <summary>
        /// Creates Order
        /// </summary>
        /// <param name="shippingAddress"> <see cref="Address"/> </param>
        /// <returns> Returns <see cref=Task{string}"/></returns>
        Task<string> CreateOrder(Address shippingAddress);
    }
}
