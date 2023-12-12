using Nop.Plugin.DdpApi.ModelBinders;

namespace Nop.Plugin.DdpApi.Models.ProductsParameters
{
    using Microsoft.AspNetCore.Mvc;

    [ModelBinder(typeof(ParametersModelBinder<ProductsCountParametersModel>))]
    public class ProductsCountParametersModel : BaseProductsParametersModel
    {
        // Nothing special here, created just for clarity.
    }
}