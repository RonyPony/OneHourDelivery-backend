using Nop.Core.Domain.Catalog;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Catalog;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an <see cref="EntityInsertedEvent{T}"/> consumer where T is <see cref="ProductSpecificationAttribute"/> entity.
    /// </summary>
    public class InsertProductSpecificationEventConsumer : IConsumer<EntityInsertedEvent<ProductSpecificationAttribute>>
    {
        #region Fields

        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="InsertDiscountEventConsumer"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="specificationAttributeService">An implementation of <see cref="ISpecificationAttributeService"/>.</param>
        /// <param name="productSpecificationAttributeRepository">An implementation of <see cref="IRepository{ProductSpecificationAttribute}"/>.</param>
        /// <param name="specificationAttributeRepository">An implementation of <see cref="IRepository{SpecificationAttribute}"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// </summary>
        public InsertProductSpecificationEventConsumer(ISpecificationAttributeService specificationAttributeService,
         IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
         IRepository<SpecificationAttribute> specificationAttributeRepository, ILogger logger,
         INotificationService notificationService)
        {
            _specificationAttributeService = specificationAttributeService;
            _productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            _specificationAttributeRepository = specificationAttributeRepository;
            _logger = logger;
            _notificationService = notificationService;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void HandleEvent(EntityInsertedEvent<ProductSpecificationAttribute> eventMessage)
        {
            try
            {
                SpecificationAttribute specificationAttribute = _specificationAttributeRepository
                .Table.FirstOrDefault(attribute => attribute.Name.Equals(Defaults.VendorDefaultProduct));

                IList<SpecificationAttributeOption> specificationAttributeOptions = _specificationAttributeService
                     .GetSpecificationAttributeOptionsBySpecificationAttribute(specificationAttribute.Id);

                foreach (SpecificationAttributeOption specificationOption in specificationAttributeOptions)
                {
                    if (specificationOption.Id != eventMessage.Entity.SpecificationAttributeOptionId)
                    {
                        var productSpecificationAttribute = _productSpecificationAttributeRepository
                            .Table.FirstOrDefault(attribute => attribute.SpecificationAttributeOptionId == specificationOption.Id
                                && attribute.ProductId == eventMessage.Entity.ProductId);

                        if (productSpecificationAttribute != null)
                        {
                            _specificationAttributeService.DeleteProductSpecificationAttribute(productSpecificationAttribute);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _notificationService
                   .ErrorNotification($"There was an error trying to add product  attribute. {e.Message}");

                _logger.Error($"There was an error trying to add product specifications attribute. {e.Message}", e);
            }
        }

        #endregion
    }
}
