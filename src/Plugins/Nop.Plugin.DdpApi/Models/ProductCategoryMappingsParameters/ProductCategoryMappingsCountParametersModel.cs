using Nop.Plugin.DdpApi.ModelBinders;

namespace Nop.Plugin.DdpApi.Models.ProductCategoryMappingsParameters
{
    using Microsoft.AspNetCore.Mvc;

    [ModelBinder(typeof(ParametersModelBinder<ProductCategoryMappingsCountParametersModel>))]
    public class ProductCategoryMappingsCountParametersModel : BaseCategoryMappingsParametersModel
    {
        // Nothing special here, created just for clarity.
    }
}