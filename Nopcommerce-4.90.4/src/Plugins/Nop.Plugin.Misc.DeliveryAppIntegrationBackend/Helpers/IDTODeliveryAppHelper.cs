using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Api.DTO.Categories;
using Nop.Plugin.Api.DTO.Customers;
using Nop.Plugin.Api.DTO.Images;
using Nop.Plugin.Api.DTO.Languages;
using Nop.Plugin.Api.DTO.Manufacturers;
using Nop.Plugin.Api.DTO.OrderItems;
using Nop.Plugin.Api.DTO.ProductAttributes;
using Nop.Plugin.Api.DTO.Products;
using Nop.Plugin.Api.DTO.ShoppingCarts;
using Nop.Plugin.Api.DTO.SpecificationAttributes;
using Nop.Plugin.Api.DTO.Stores;
using Nop.Plugin.Api.DTO.TaxCategory;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers
{
    /// <summary>
    /// Represents a Dto delivery app helper
    /// </summary>
    public interface IDTODeliveryAppHelper
    {
        /// <summary>
        /// Prepares a customer  dto.
        /// </summary>
        /// <param name="customer">A <see cref="Customer"/></param>
        /// <returns>A <see cref="CustomerDto"/></returns>
        CustomerDto PrepareCustomerDTO(Customer customer);

        /// <summary>
        /// Prepares product  dto.
        /// </summary>
        /// <param name="product">A <see cref="Product"/></param>
        /// <returns>a <see cref="ProductDto"/></returns>
        ProductDto PrepareProductDTO(Product product);

        /// <summary>
        /// Prepares category  dto.
        /// </summary>
        /// <param name="category">A <see cref="Category"/></param>
        /// <returns> a <see cref="CategoryDto"/></returns>
        CategoryDto PrepareCategoryDTO(Category category);

        /// <summary>
        /// Prepares order delivery dto.
        /// </summary>
        /// <param name="order"> A <see cref="Order"/></param>
        /// <returns>a <see cref="OrderDeliveryAppDto"/></returns>
        OrderDeliveryAppDto PrepareOrderDTO(Order order);

        /// <summary>
        /// Prepares shopping cart item dto.
        /// </summary>
        /// <param name="shoppingCartItem"> A <see cref="ShoppingCartItem"/></param>
        /// <returns>a <see cref="ShoppingCartItemDto"/></returns>
        ShoppingCartItemDto PrepareShoppingCartItemDTO(ShoppingCartItem shoppingCartItem);

        /// <summary>
        /// Prepares order item dto.
        /// </summary>
        /// <param name="orderItem">A <see cref="OrderItem"/></param>
        /// <returns> A <see cref="OrderItemDto"/></returns>
        OrderItemDto PrepareOrderItemDTO(OrderItem orderItem);

        /// <summary>
        /// Prepares store dto.
        /// </summary>
        /// <param name="store">A <see cref="Store"/></param>
        /// <returns> A <see cref="StoreDto"/></returns>
        StoreDto PrepareStoreDTO(Store store);

        /// <summary>
        /// Prepares language dto.
        /// </summary>
        /// <param name="language">A <see cref="Language"/></param>
        /// <returns> A <see cref="LanguageDto"/></returns>
        LanguageDto PrepareLanguageDto(Language language);

        /// <summary>
        /// Prepares product attribute dto.
        /// </summary>
        /// <param name="productAttribute">A <see cref="ProductAttribute"/></param>
        /// <returns> A <see cref="ProductAttributeDto"/></returns>
        ProductAttributeDto PrepareProductAttributeDTO(ProductAttribute productAttribute);

        /// <summary>
        /// Prepares product specification attribute dto.
        /// </summary>
        /// <param name="productSpecificationAttribute">A <see cref="ProductSpecificationAttribute"/></param>
        /// <returns> A <see cref="ProductSpecificationAttributeDto"/></returns>
        ProductSpecificationAttributeDto PrepareProductSpecificationAttributeDto(ProductSpecificationAttribute productSpecificationAttribute);

        /// <summary>
        /// Prepares  specification attribute dto.
        /// </summary>
        /// <param name="specificationAttribute">A <see cref="SpecificationAttribute"/></param>
        /// <returns> A <see cref="SpecificationAttributeDto"/></returns>
        SpecificationAttributeDto PrepareSpecificationAttributeDto(SpecificationAttribute specificationAttribute);

        /// <summary>
        /// Prepares manufacturer dto.
        /// </summary>
        /// <param name="manufacturer">A <see cref="Manufacturer"/></param>
        /// <returns> A <see cref="ManufacturerDto"/></returns>
        ManufacturerDto PrepareManufacturerDto(Manufacturer manufacturer);

        /// <summary>
        /// Prepares a tax category dto.
        /// </summary>
        /// <param name="taxCategory">A <see cref="TaxCategory"/></param>
        /// <returns> A <see cref="TaxCategoryDto"/></returns>
        TaxCategoryDto PrepareTaxCategoryDTO(TaxCategory taxCategory);

        /// <summary>
        /// Prepares a product picture dto.
        /// </summary>
        /// <param name="productPicture">A <see cref="ProductPicture"/></param>
        /// <returns> A <see cref="ImageMappingDto"/></returns>
        ImageMappingDto PrepareProductPictureDTO(ProductPicture productPicture);

    }
}
