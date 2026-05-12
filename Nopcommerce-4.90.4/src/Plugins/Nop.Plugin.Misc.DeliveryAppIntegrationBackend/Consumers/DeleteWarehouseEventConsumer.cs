using Nop.Core.Domain.Shipping;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Events;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an event consumer for <see cref="EntityDeletedEvent{Warehouse}"/>.
    /// </summary>
    public sealed class DeleteWarehouseEventConsumer : IConsumer<EntityDeletedEvent<Warehouse>>
    {
        #region Fields

        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;
        private readonly IRepository<WarehouseUserCreatorMapping> _warehouseUserCreatorMappingRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteWarehouseEventConsumer"/>.
        /// </summary>
        /// <param name="vendorWarehouseMappingRepository">An implementation of <see cref="IRepository{VendorWarehouseMapping}"/>.</param>
        /// <param name="warehouseUserCreatorMappingRepository">An implementation of <see cref="IRepository{WarehouseUserCreatorMapping}"/>.</param>
        public DeleteWarehouseEventConsumer(
            IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository,
            IRepository<WarehouseUserCreatorMapping> warehouseUserCreatorMappingRepository)
        {
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
            _warehouseUserCreatorMappingRepository = warehouseUserCreatorMappingRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the <see cref="EntityDeletedEvent{Warehouse}"/>.
        /// </summary>
        /// <param name="eventMessage">An instance of <see cref="EntityDeletedEvent{Warehouse}"/>.</param>
        public async Task HandleEventAsync(EntityDeletedEvent<Warehouse> eventMessage)
        {
            VendorWarehouseMapping vendorWarehouse = _vendorWarehouseMappingRepository.Table.Where(vw => vw.WarehouseId == eventMessage.Entity.Id)
                .FirstOrDefault();
            WarehouseUserCreatorMapping warehouseUserCreator = _warehouseUserCreatorMappingRepository.Table.Where(wuc => wuc.WarehouseId == eventMessage.Entity.Id)
                .FirstOrDefault();

            if (vendorWarehouse != null) _vendorWarehouseMappingRepository.Delete(vendorWarehouse);
            if (warehouseUserCreator != null) _warehouseUserCreatorMappingRepository.Delete(warehouseUserCreator);
        }

        #endregion
    }
}
