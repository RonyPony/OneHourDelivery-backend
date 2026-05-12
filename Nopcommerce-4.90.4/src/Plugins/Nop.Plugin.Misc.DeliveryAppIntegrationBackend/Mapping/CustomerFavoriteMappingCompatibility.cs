using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="CustomerFavoriteMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public class CustomerFavoriteMappingCompatibility : INameCompatibility
    {
        ///<inheritdoc/>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(CustomerFavoriteMapping) , "Customer_Favorite_Mapping" }
        };

        ///<inheritdoc/>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(CustomerFavoriteMapping), "CustomerId"), "CustomerId"},
            {(typeof(CustomerFavoriteMapping), "VendorId"), "VendorId"},
            {(typeof(CustomerFavoriteMapping), "CreatedOnUtc"), "CreatedOnUtc"}
        };
    }
}
