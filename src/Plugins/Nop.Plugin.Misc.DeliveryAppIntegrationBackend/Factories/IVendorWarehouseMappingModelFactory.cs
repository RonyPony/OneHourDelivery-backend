using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Factories
{
    /// <summary>
    /// Represents a contract for <see cref="VendorWarehouseMappingModel"/> factory services.
    /// </summary>
    public interface IVendorWarehouseMappingModelFactory
    {
        /// <summary>
        /// Prepares the <see cref="VendorWarehouseMappingModel"/> model.
        /// </summary>
        /// <param name="vendorId">Indicates the vendor id.</param>
        /// <returns>An instance of <see cref="VendorWarehouseMappingModel"/>.</returns>
        VendorWarehouseMappingModel PrepareVendorWarehouseMappingModel(int vendorId);
    }
}
