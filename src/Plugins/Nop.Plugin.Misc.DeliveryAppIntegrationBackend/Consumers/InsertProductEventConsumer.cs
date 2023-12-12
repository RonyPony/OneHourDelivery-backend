using Nop.Core.Domain.Catalog;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Catalog;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Messages;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Consumers
{
    /// <summary>
    /// Represents an event consumer for <see cref="EntityInsertedEvent{Product}"/>.
    /// </summary>
    public class InsertProductEventConsumer : IConsumer<EntityInsertedEvent<Product>>
    {
        #region Fields
        private readonly IProductAttributeService _productAttributeService;
        private readonly ILogger _logger;
        private readonly INotificationService _notificationService;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IRepository<SpecificationAttributeOption> _specificationAttributteOptionRepository;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="InsertProductEventConsumer"/>.
        /// </summary>
        /// <param name="productAttributeService">An implementation of <see cref="IProductAttributeService"/> </param>
        /// <param name="logger">An implementation of <see cref="ILogger"/> </param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/> </param>
        /// <param name="productSpecificationAttributeRepository">An implementation of <see cref="IRepository{ProductSpecificationAttribute}"/> </param>
        /// <param name="specificationAttributeService">An implementation of <see cref="ISpecificationAttributeService"/> </param>
        /// <param name="specificationAttributteOptionRepository">An implementation of <see cref="IRepository{SpecificationAttributeOption}"/> </param>
        /// <param name="specificationAttributeRepository">An implementation of <see cref="IRepository{SpecificationAttribute}"/> </param>
        public InsertProductEventConsumer(IProductAttributeService productAttributeService, 
            ILogger logger , INotificationService notificationService ,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            ISpecificationAttributeService specificationAttributeService,
            IRepository<SpecificationAttributeOption> specificationAttributteOptionRepository,
            IRepository<SpecificationAttribute> specificationAttributeRepository)
        {
            _productAttributeService = productAttributeService;
            _logger = logger;
            _notificationService = notificationService;
            _productAttributeService = productAttributeService;
            _productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            _specificationAttributeService = specificationAttributeService;
            _specificationAttributteOptionRepository = specificationAttributteOptionRepository;
            _specificationAttributeRepository = specificationAttributeRepository;
        }
        #endregion

        public void HandleEvent(EntityInsertedEvent<Product> eventMessage)
        {
            try
            {
                AddProductSpecialSpecificationsAttribute(eventMessage.Entity);
                AddProductSpecificationAttribute(eventMessage.Entity);
            }
            catch (Exception e)
            {
                _notificationService
                    .ErrorNotification($"There was an error trying to add product  attribute. {e.Message}");

                _logger.Error($"There was an error trying to add product  specifications attribute. {e.Message}", e);
            }
        }

        private void AddProductSpecialSpecificationsAttribute(Product product)
        {
            var productAttibute = _productAttributeService.GetAllProductAttributes().
                FirstOrDefault(attribute => attribute.Name.Equals(Defaults.ProductSpecialSpecificationAttribute.Name));

            if (productAttibute is null)
                throw new ArgumentException($"The product attribute {Defaults.ProductSpecialSpecificationAttribute.Name} does not exists.");

            _productAttributeService.InsertProductAttributeMapping(new ProductAttributeMapping
            {
                ProductId = product.Id,
                ProductAttributeId = productAttibute.Id,
                AttributeControlType = AttributeControlType.TextBox,
                IsRequired = false,
                DisplayOrder = 10
            });
        }

        private void AddProductSpecificationAttribute(Product product)
        {
            SpecificationAttribute specificationAttribute = _specificationAttributeRepository.Table
                .FirstOrDefault(attribute => attribute.Name.Equals(Defaults.VendorDefaultProduct));

            if (specificationAttribute is null)
                throw new  ArgumentException($"The specification attribute {Defaults.ProductSpecialSpecificationAttribute} does not exists.");

            SpecificationAttributeOption specificationOption = _specificationAttributteOptionRepository.Table
                .FirstOrDefault(attributeOption => attributeOption.SpecificationAttributeId == specificationAttribute.Id
                && attributeOption.Name.Equals(Defaults.VendorDefaultProductNegation));

            if (specificationOption is null)
                throw new ArgumentException($"The specification attribute option {Defaults.VendorDefaultProductNegation} does not exists.");

            ProductSpecificationAttribute productAttribute = _productSpecificationAttributeRepository.Table
                .FirstOrDefault(attribute => attribute.ProductId == product.Id
                && attribute.SpecificationAttributeOptionId == specificationOption.Id);

            if(productAttribute is null)
            {
                _specificationAttributeService.InsertProductSpecificationAttribute(new ProductSpecificationAttribute
                {
                    ProductId = product.Id,
                    SpecificationAttributeOptionId = specificationOption.Id,
                    DisplayOrder = 1,
                    AttributeTypeId = 0,
                    AllowFiltering = false,
                });
            }

        }
    }
}
