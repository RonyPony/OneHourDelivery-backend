using FluentValidation;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="OrderDeliveryInfoModel"/>.
    /// </summary>
    public sealed class OrderDeliveryInfoModelValidator : BaseNopValidator<OrderDeliveryInfoModel>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OrderDeliveryInfoModelValidator"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public OrderDeliveryInfoModelValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.DriverId)
                .GreaterThanOrEqualTo(0)
                .WithMessage(localizationService.GetResource("Must select a driver"));
        }
    }
}
