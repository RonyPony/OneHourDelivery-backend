using Nop.Core;
using Nop.Core.Domain.Discounts;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an <see cref="EntityDeletedEvent{T}"/> consumer where T is <see cref="Discount"/> entity.
    /// </summary>
    public sealed class DeleteDiscountEventConsumer : IConsumer<EntityDeletedEvent<Discount>>
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IRepository<VendorDiscount> _vendorDiscountRepository;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteDiscountEventConsumer"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="vendorDiscountRepository">An implementation of <see cref="IRepository{VendorDiscount}"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public DeleteDiscountEventConsumer(
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
        public async Task HandleEventAsync(EntityDeletedEvent<Discount> eventMessage)
        {
            try
            {
                if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                {
                    VendorDiscount vendorDiscount = _vendorDiscountRepository.Table
                        .FirstOrDefault(mapping => mapping.VendorId == _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id && mapping.DiscountId == eventMessage.Entity.Id);

                    _vendorDiscountRepository.Delete(vendorDiscount);
                }
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
