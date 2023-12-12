using Nop.Plugin.DdpApi.DTO;
using System.Collections.Generic;

namespace Nop.Plugin.DdpApi.Services
{
    public interface IProductAttributeConverter
    {
        List<ProductItemAttributeDto> Parse(string attributesXml);
        string ConvertToXml(List<ProductItemAttributeDto> attributeDtos, int productId);
    }
}
