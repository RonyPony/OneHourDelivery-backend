using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
using Nop.Plugin.DdpApi.DTO.Images;
using Nop.Plugin.DdpApi.DTO.Languages;
using Nop.Plugin.DdpApi.DTO.ProductAttributes;
using Nop.Plugin.DdpApi.DTO.Products;

namespace Nop.Plugin.DdpApi.Helpers
{
    public interface IDTOHelper
    {
        ProductDto PrepareProductDTO(Product product);
        LanguageDto PrepareLanguageDto(Language language);
        ProductAttributeDto PrepareProductAttributeDTO(ProductAttribute productAttribute);
        ImageMappingDto PrepareProductPictureDTO(ProductPicture productPicture);
    }
}

