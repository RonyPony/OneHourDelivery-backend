using Nop.Plugin.DdpApi.ModelBinders;

namespace Nop.Plugin.DdpApi.Models.ProductManufacturerMappingsParameters
{
    using Microsoft.AspNetCore.Mvc;

    [ModelBinder(typeof(ParametersModelBinder<ProductManufacturerMappingsCountParametersModel>))]
    public class ProductManufacturerMappingsCountParametersModel : BaseManufacturerMappingsParametersModel
    {
        // Nothing special here, created just for clarity.
    }
}