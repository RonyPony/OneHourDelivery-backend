using Nop.Data.Mapping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping
{
    /// <summary>
    /// <see cref="VendorReviewMapping"/> instance of backward compatibility of table naming.
    /// </summary>
    public partial class VendorReviewMappingCompatibility : INameCompatibility
    {
        /// <summary>
        /// Gets table name for mapping with the type.
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            { typeof(VendorReviewMapping), "Vendor_Review_Mappings" }
        };

        /// <summary>
        /// Gets column name for mapping with the entity's property and type.
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            {(typeof(VendorReviewMapping), "VendorId"), "VendorId"},
            {(typeof(VendorReviewMapping), "CustomerId"), "CustomerId"},
            {(typeof(VendorReviewMapping), "Rating"), "Rating"},
            {(typeof(VendorReviewMapping), "Comment"), "Comment"},
        };
    }
}
