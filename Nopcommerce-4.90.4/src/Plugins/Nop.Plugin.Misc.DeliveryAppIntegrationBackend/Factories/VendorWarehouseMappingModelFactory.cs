using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Localization;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Factories
{
    /// <summary>
    /// Represents the implementation of <see cref="VendorWarehouseMappingModel"/> factory services.
    /// </summary>
    public sealed class VendorWarehouseMappingModelFactory : IVendorWarehouseMappingModelFactory
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IVendorDeliveryAppService _vendorDeliveryAppService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="VendorWarehouseMappingModelFactory"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="vendorDeliveryAppService">An implementation of <see cref="IVendorDeliveryAppService"/>.</param>
        public VendorWarehouseMappingModelFactory(
            ILocalizationService localizationService,
            IVendorDeliveryAppService vendorDeliveryAppService)
        {
            _localizationService = localizationService;
            _vendorDeliveryAppService = vendorDeliveryAppService;
        }

        #endregion

        #region Utilities

        private List<SelectListItem> GetVendorWarehousesAsSelectListItems(int vendorId)
        {
            IList<Warehouse> vendorWarehosues = _vendorDeliveryAppService.GetAllWarehousesFromVendor(vendorId);
            int? vendorWarehouseId = _vendorDeliveryAppService.GetVendorWarehouseIdByVendorId(vendorId);
            var selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.SelectWarehouse"), Selected = vendorWarehouseId is null || vendorWarehouseId == 0, Disabled = true }
            };

            foreach (Warehouse vendorWarehouse in vendorWarehosues)
            {
                selectList.Add(new SelectListItem { Value = $"{vendorWarehouse.Id}", Text = vendorWarehouse.Name, Selected = vendorWarehouse.Id == vendorWarehouseId });
            }

            return selectList;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public VendorWarehouseMappingModel PrepareVendorWarehouseMappingModel(int vendorId)
        {
            return new VendorWarehouseMappingModel
            {
                VendorId = vendorId,
                WarehouseId = _vendorDeliveryAppService.GetVendorWarehouseIdByVendorId(vendorId) ?? 0,
                Warehouses = GetVendorWarehousesAsSelectListItems(vendorId)
            };
        }

        #endregion
    }
}
