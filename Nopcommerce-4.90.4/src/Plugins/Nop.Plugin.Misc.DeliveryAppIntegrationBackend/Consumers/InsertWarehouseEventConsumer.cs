using Nop.Core;
using Nop.Core.Domain.Shipping;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an event consumer for <see cref="EntityInsertedEvent{Warehouse}"/>.
    /// </summary>
    public sealed class InsertWarehouseEventConsumer : IConsumer<EntityInsertedEvent<Warehouse>>
    {
        #region Fields

        private readonly IRepository<WarehouseUserCreatorMapping> _warehouseUserCreatorMappingRepository;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="InsertWarehouseEventConsumer"/>.
        /// </summary>
        /// <param name="warehouseUserCreatorMappingRepository">An implementation of <see cref="IRepository{WarehouseUserCreatorMapping}"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public InsertWarehouseEventConsumer(
            IRepository<WarehouseUserCreatorMapping> warehouseUserCreatorMappingRepository,
            IWorkContext workContext)
        {
            _warehouseUserCreatorMappingRepository = warehouseUserCreatorMappingRepository;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the <see cref="EntityInsertedEvent{Warehouse}"/>.
        /// </summary>
        /// <param name="eventMessage">An instance of <see cref="EntityInsertedEvent{Warehouse}"/>.</param>
        public async Task HandleEventAsync(EntityInsertedEvent<Warehouse> eventMessage)
        {
            _warehouseUserCreatorMappingRepository.Insert(new WarehouseUserCreatorMapping
            {
                WarehouseId = eventMessage.Entity.Id,
                CustomerId = _workContext.GetCurrentCustomerAsync().GetAwaiter().GetResult().Id
            });
        }

        #endregion
    }
}
