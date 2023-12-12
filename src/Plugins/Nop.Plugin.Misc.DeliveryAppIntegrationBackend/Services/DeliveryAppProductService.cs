using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Tax.FixedOrByCountryStateZip.Domain;
using Nop.Services.Media;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Responsible of providing product information for delivery app integration.
    /// </summary>
    public class DeliveryAppProductService : IDeliveryAppProductService
    {
        #region Fields
        protected readonly IRepository<Vendor> _vendorRepository;
        protected readonly IRepository<Category> _categoryRepository;
        protected readonly IRepository<Product> _productRepository;
        protected readonly IRepository<ProductCategory> _productCategoryRepository;
        protected readonly IPictureService _pictureService;
        protected readonly IRepository<RelatedProduct> _relatedProductRepository;
        private readonly IRepository<TaxRate> _taxRateRepository;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;

        #endregion

        #region Ctor
        public DeliveryAppProductService(IRepository<Vendor> vendorRepository,
                                         IRepository<Category> categoryRepository,
                                         IRepository<Product> productRepository,
                                         IRepository<ProductCategory> productCategoryRepository,
                                         IPictureService pictureService,
                                         IRepository<RelatedProduct> relatedProductRepository,
                                         IRepository<TaxRate> taxRateRepository, 
                                         IOrderTotalCalculationService orderTotalCalculationService)
        {
            _vendorRepository = vendorRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _pictureService = pictureService;
            _relatedProductRepository = relatedProductRepository;
            _taxRateRepository = taxRateRepository;
            _orderTotalCalculationService = orderTotalCalculationService;
        }

        #endregion
        ///<inheritdoc/>
        public IEnumerable<DeliveryAppRelatedProduct> GetProductRelatedProducts(int productId)
        {

            ICollection<DeliveryAppRelatedProduct> result = (from p in _productRepository.Table
                                                             join rep in _relatedProductRepository.Table on p.Id equals rep.ProductId2
                                                             where rep.ProductId1.Equals(productId) && !p.Deleted
                                                             select new DeliveryAppRelatedProduct { Id = rep.ProductId2, 
                                                                                           Description = p.ShortDescription, 
                                                                                                  Name = p.Name, 
                                                                                                 Price = p.Price }).ToList();

            foreach (var product in result)
            {
                Picture productPicture = _pictureService.GetPicturesByProductId(product.Id).FirstOrDefault();

                product.ImageUrl = productPicture != null ? _pictureService.GetPictureUrl(productPicture.Id) : "";

            }

            return result;
        }

        public decimal GetTaxAmountOfOrderProducts(GetOrderTotalTaxRequest getOrderTotalTaxRequest)
        {
            decimal result = 0;
            if (getOrderTotalTaxRequest.OrderProducts is null)
                throw new ArgumentException("InvalidOrderProductsRequest");

            IList<ShoppingCartItem> shoppingCart = getOrderTotalTaxRequest.OrderProducts
                                                                          .Select(x => new ShoppingCartItem { ProductId = x.ProductId, 
                                                                                                               Quantity = x.Quantity, 
                                                                                                             CustomerId = getOrderTotalTaxRequest.CustomerId,
                                                                                                             CreatedOnUtc = DateTime.UtcNow,
                                                                                                             ShoppingCartType = ShoppingCartType.ShoppingCart,
                                                                                                             ShoppingCartTypeId = (int)ShoppingCartType.ShoppingCart
                                                                          })
                                                                          .ToList();

            result = _orderTotalCalculationService.GetTaxTotal(shoppingCart, usePaymentMethodAdditionalFee: false);
        
           return result;
        }
    }
}
