using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a model for <see cref="VendorWarehouseMapping"/>.
    /// </summary>
    public sealed class VendorWarehouseMappingModel : BaseNopEntityModel
    {
        /// <summary>
        /// Indicates the vendor id.
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Indicates the warehouse id.
        /// </summary>
        [NopResourceDisplayName("Plugin.Misc.DeliveryAppIntegrationBackend.VendorWarehouse.Warehouse")]
        public int WarehouseId { get; set; }

        /// <summary>
        /// Represents a list of warehouses created by the vendor.
        /// </summary>
        public List<SelectListItem> Warehouses { get; set; }
    }
}
