using FluentValidation;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="VendorWarehouseMappingModel"/>.
    /// </summary>
    public sealed class VendorWarehouseMappingModelValidator : BaseNopValidator<VendorWarehouseMappingModel>
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="VendorWarehouseMappingModelValidator"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public VendorWarehouseMappingModelValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.WarehouseId)
                .GreaterThan(0)
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.VendorWarehouse.Warehouse.Required"));
        }

        #endregion
    }
}
