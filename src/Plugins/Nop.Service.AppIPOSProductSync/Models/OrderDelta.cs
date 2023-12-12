using Nop.Plugin.Api.DTO.Orders;

namespace Nop.Service.AppIPOSSync.Models
{
    /// <summary>
    /// Class used to make requests to Nop.Api order controller
    /// </summary>
    public class OrderDelta
    {
        /// <summary>
        /// Order "Delta" object
        /// </summary>
        public OrderDto Order { get; set; }
    }
}