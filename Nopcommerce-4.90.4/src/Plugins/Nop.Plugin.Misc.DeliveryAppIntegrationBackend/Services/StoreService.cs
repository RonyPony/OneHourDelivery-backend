using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Media;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents the service implementation to interact with Stores
    /// </summary>
    public class StoreService : IStoreService
    {
        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IPictureService _pictureService;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor

        /// <summary>
        /// An instance of <see cref="StoreService"/>.
        /// </summary>
        /// <param name="productRepository">An instance of <see cref="IRepository<Product>"/></param>
        /// <param name="pictureService">An instance of <see cref="IPictureService"/></param>
        /// <param name="categoryService">An instance of <see cref="ICategoryService"/></param>
        public StoreService(IRepository<Product> productRepository,
            IPictureService pictureService,
            ICategoryService categoryService,
            IRepository<AddressGeoCoordinatesMapping> AddressGeoCoordinateRespository,
            IProductAttributeService productAttributeService)
        {
            _productRepository = productRepository;
            _pictureService = pictureService;
            _categoryService = categoryService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get ProductPicture Url from <see cref="Picture"/>.
        /// </summary>
        /// <param name="productId">An instance of <see cref="Product"/></param>
        /// <returns>An instance of <see cref="GetProductPictureUrl"/></returns>
        private string GetProductPictureUrl(int productId)
        {
            Picture productPicture = _pictureService.GetPicturesByProductId(productId).FirstOrDefault();
            string pictureUrl = string.Empty;

            if (productPicture != null)
            {
                pictureUrl = _pictureService.GetPictureUrl(productPicture.Id);
            }
            else
            {
                pictureUrl = _pictureService.GetDefaultPictureUrl();
            }

            return pictureUrl;
        }

        /// <summary>
        /// Get ProductCategoryId from <see cref="Category"/>.
        /// </summary>
        /// <param name="productId">An instance of <see cref="Product"/></param>
        /// <returns>An instance of <see cref="GetProductCategoryId"/></returns>
        private int GetProductCategoryId(int productId)
        {
            Category category = GetOneCategoryFromProduct(productId);
            if (category is null) return 0;
            return category.Id;
        }

        /// <summary>
        /// Get ProductCategoryName from <see cref="Category"/>.
        /// </summary>
        /// <param name="productId">An instance of <see cref="Product"/></param>
        /// <returns>An instance of <see cref="GetProductCategoryName"/></returns>
        private string GetProductCategoryName(int productId)
        {
            Category category = GetOneCategoryFromProduct(productId);
            if (category is null) return string.Empty;
            return category.Name;
        }

        /// <summary>
        /// Get a ProductCategory from <see cref="ProductCategory"/>
        /// </summary>
        /// <param name="productId">An instance of <see cref="Product"/></param>
        /// <returns>An instance of <see cref="GetOneCategoryFromProduct"/></returns>
        private Category GetOneCategoryFromProduct(int productId)
        {
            ProductCategory productCategory =
                _categoryService.GetProductCategoriesByProductId(productId).FirstOrDefault();
            if (productCategory is null) return null;
            Category category = _categoryService.GetCategoryById(productCategory.Id);
            if (category is null) return null;
            return category;
        }

        #endregion

        /// <summary>
        /// Returns a list of products by vendor.
        /// </summary>
        /// <param name="vendorId">vendor Id</param>
        /// <returns>An instance of <see cref="GetStoreProductsByVendorId"/></returns>
        public IList<ProductModel> GetStoreProductsByVendorId(int vendorId)
        {
            IList<Product> products =
                _productRepository.Table.Where(products => products.VendorId == vendorId && !products.Deleted).ToList();
            return BuildProductModelsFromProducts(products);
        }

        /// <summary>
        /// Maps the values from <see cref="Product"/> to <see cref="ProductModel"/>
        /// </summary>
        /// <param name="products">An instance of <see cref="GetProductPictureUrl"/></param>
        /// <returns>An instance of <see cref="ICategoryService"/></returns>
        public IList<ProductModel> BuildProductModelsFromProducts(IList<Product> products)
        {
            var productModels = new List<ProductModel>();

            foreach (Product product in products)
            {
                productModels.Add(new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    PictureUrl = GetProductPictureUrl(product.Id),
                    CategoryId = GetProductCategoryId(product.Id),
                    CategoryName = GetProductCategoryName(product.Id)
                });
            }

            return productModels;
        }
    }
}