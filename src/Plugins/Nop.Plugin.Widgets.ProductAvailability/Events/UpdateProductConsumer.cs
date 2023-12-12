using Nop.Core.Domain.Catalog;
using Nop.Core.Events;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Plugin.Widgets.ProductAvailability.Services;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Events
{
    /// <summary>
    /// Represents a consumer for <see cref="EntityUpdatedEvent{Product}"/>.
    /// </summary>
    public class UpdateProductConsumer : IConsumer<EntityUpdatedEvent<Product>>
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly ProductAvailabilitySettings _productAvailabilitySettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateProductConsumer" />.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="productAvailabilityService">An implementation of <see cref="IProductAvailabilityService"/>.</param>
        /// <param name="productAvailabilitySettings">An instance of <see cref="ProductAvailabilitySettings"/>.</param>
        public UpdateProductConsumer(
            ILogger logger,
            INotificationService notificationService,
            IProductAvailabilityService productAvailabilityService,
            ProductAvailabilitySettings productAvailabilitySettings)
        {
            _logger = logger;
            _notificationService = notificationService;
            _productAvailabilityService = productAvailabilityService;
            _productAvailabilitySettings = productAvailabilitySettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes when the <see cref="EntityUpdatedEvent{Product}"/> is fired.
        /// <param name="eventMessage">An instance of <see cref="EntityUpdatedEvent{Product}"/>.</param>
        public void HandleEvent(EntityUpdatedEvent<Product> eventMessage)
        {
            if (!eventMessage.Entity.Published || eventMessage.Entity.Deleted) return;

            try
            {
                if (eventMessage.Entity.Sku is null)
                    throw new Exception($"Cannot update inventory for product with ID {eventMessage.Entity.Id} because does not have SKU.");

                if (!_productAvailabilityService.SizeProductAttributeExists())
                    throw new Exception($"Cannot update inventory for product because Size product attribute does not exists.");

                Task<InventoryRequestResponseModel> inventoryRequestResponseTask = _productAvailabilityService.GetProductInventoryBySku(eventMessage.Entity.Sku, filterEmptySizesFromInventory: false);
                inventoryRequestResponseTask.Wait();

                if (inventoryRequestResponseTask.Result is null)
                    throw new Exception($"Cannot update inventory for product with SKU {eventMessage.Entity.Sku} because the inventory web service failed.");

                if (inventoryRequestResponseTask.Result.Tallas.Any(size => size.Trim().ToUpper().Equals(_productAvailabilitySettings.OneSizeProductsIdentifierCode)))
                    _productAvailabilityService.UpdateOneSizeProductInventory(eventMessage.Entity, inventoryRequestResponseTask.Result);
                else
                    _productAvailabilityService.InsertOrUpdateProductAttributeCombination(eventMessage.Entity, inventoryRequestResponseTask.Result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                _notificationService.WarningNotification(e.Message);
            }
        }

        #endregion
    }
}
