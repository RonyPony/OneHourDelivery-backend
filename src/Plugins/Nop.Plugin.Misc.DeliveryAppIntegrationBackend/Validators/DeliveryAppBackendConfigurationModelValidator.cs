using FluentValidation;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="DeliveryAppBackendConfigurationModel"/>.
    /// </summary>
    public sealed class DeliveryAppBackendConfigurationModelValidator : BaseNopValidator<DeliveryAppBackendConfigurationModel>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppBackendConfigurationModelValidator"/>.
        /// </summary>
        public DeliveryAppBackendConfigurationModelValidator(
            ILocalizationService localizationService)
        {
            RuleFor(model => model.AdministrativeProfit)
                .LessThanOrEqualTo(100M)
                .GreaterThanOrEqualTo(0M)
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.AdministrativeProfit.RangeInvalid"));

            RuleFor(model => model.MessengerProfit)
                .LessThanOrEqualTo(100M)
                .GreaterThanOrEqualTo(0M)
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.MessengerProfit.RangeInvalid"));

            RuleFor(model => model)
                .Must(model => model.AdministrativeProfit + model.MessengerProfit <= 100M && model.AdministrativeProfit + model.MessengerProfit >= 0M)
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.Validation.ProfitMismatch"));

            RuleFor(model => model.NotificationCenterUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.NoficationCenterUrl.Required"));
            
            RuleFor(model => model.NotificationDriverTrackingUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.NotificationDriverTrackingUrl.Required"));

            RuleFor(model => model.MaxMoneyAmountDriverCanCarry)
                .GreaterThan(0)
                .WithMessage(localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.Generics.MaxMoneyAmountDriverCanCarry.Required"));
        }
    }
}
