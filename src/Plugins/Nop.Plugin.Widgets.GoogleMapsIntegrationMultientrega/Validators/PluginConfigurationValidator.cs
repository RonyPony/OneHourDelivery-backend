using FluentValidation;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Validators
{
    /// <summary>
    /// Validator for <see cref="PluginConfiguration"/>.
    /// </summary>
    public sealed class PluginConfigurationValidator : BaseNopValidator<PluginConfiguration>
    {
        /// <summary>
        /// Initialices a new instance of <see cref="PluginConfigurationValidator"/>.
        /// </summary>
        /// <param name="_localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public PluginConfigurationValidator(ILocalizationService _localizationService)
        {
            RuleFor(config => config.ApiKey)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required"));

            RuleFor(config => config.NorthBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Required"));
            RuleFor(config => config.SouthBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Required"));
            RuleFor(config => config.WestBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Required"));
            RuleFor(config => config.EastBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Required"));

            RuleFor(config => config.DefaultLatitude)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.DefaultLatLngEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Required"));
            RuleFor(config => config.DefaultLongitude)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.DefaultLatLngEnabled)
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Required"));

            RuleFor(config => config.Email)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.Email.Required"));
            RuleFor(config => config.Password)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.Password.Required"));
            RuleFor(config => config.BaseUrl)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.BaseUrl.Required"));
            RuleFor(config => config.BranchOffice)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.BranchOffice.Required"));
            RuleFor(config => config.Nit)
                .NotEmpty()
                .WithMessage(_localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Fields.Nit.Required"));
        }
    }
}
