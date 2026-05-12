using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO
{
    /// <summary>
    /// Represents an Delivery App order response
    /// </summary>
    public class DeliveryAppOrderRootObject : ISerializableObject
    {
        /// <summary>
        /// Creates an instance of <see cref="DeliveryAppOrderRootObject"/>
        /// </summary>
        public DeliveryAppOrderRootObject()
        {
            Orders = new List<OrderDeliveryAppDto>();
        }

        /// <summary>
        /// Represents orders
        /// </summary>
        [JsonProperty("orders")]
        public IList<OrderDeliveryAppDto> Orders { get; set; }

        /// <summary>
        /// Indicates primary property name.
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryPropertyName()
        => "orders";
        
        /// <summary>
        /// Indicates primary property type
        /// </summary>
        /// <returns></returns>
        public Type GetPrimaryPropertyType()
        => typeof(OrderDeliveryAppDto);
        
    }
}
