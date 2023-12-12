using Nop.Plugin.Api.DTO.OrderItems;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api orderItem controller
    /// </summary>
    public class OrderItemDelta
    {
        /// <summary>
        /// OrderItem "Delta" object
        /// </summary>
        public OrderItemDto OrderItem { get; set; }
    }
}