using Nop.Core;
using Nop.Core.Domain.Discounts;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an <see cref="EntityInsertedEvent{T}"/> consumer where T is <see cref="Discount"/> entity.
    /// </summary>
    public sealed class InsertDiscountEventConsumer : IConsumer<EntityInsertedEvent<Discount>>
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IRepository<VendorDiscount> _vendorDiscountRepository;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="InsertDiscountEventConsumer"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="vendorDiscountRepository">An implementation of <see cref="IRepository{VendorDiscount}"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public InsertDiscountEventConsumer(
            ILogger logger,
            INotificationService notificationService,
            IRepository<VendorDiscount> vendorDiscountRepository,
            IWorkContext workContext)
        {
            _logger = logger;
            _notificationService = notificationService;
            _vendorDiscountRepository = vendorDiscountRepository;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public async Task HandleEventAsync(EntityInsertedEvent<Discount> eventMessage)
        {
            try
            {
                if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                {
                    _vendorDiscountRepository.Insert(new VendorDiscount
                    {
                        VendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id,
                        DiscountId = eventMessage.Entity.Id
                    });
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                _notificationService.WarningNotification($"An error occurred trying to bind the discount to the vendor. Error message: {e.Message}");
            }
        }

        #endregion
    }
}
