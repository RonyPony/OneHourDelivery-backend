using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="CustomerRatingMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public sealed class CustomerRatingMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string> 
        {
            { typeof(CustomerRatingMapping) , "Customer_Rating_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CustomerRatingMapping), "CreatorCustomerId"), "CreatorCustomerId"},
            {(typeof(CustomerRatingMapping), "RatedCustomerId"), "RatedCustomerId"},
            {(typeof(CustomerRatingMapping), "Rate"), "Rate"},
            {(typeof(CustomerRatingMapping), "Comment"), "Comment"},
            {(typeof(CustomerRatingMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
