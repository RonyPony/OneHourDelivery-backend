using FluentValidation;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Validators
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
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.ApiKey.Required").GetAwaiter().GetResult());

            RuleFor(config => config.NorthBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.NorthBound.Required").GetAwaiter().GetResult());
            RuleFor(config => config.SouthBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.SouthBound.Required").GetAwaiter().GetResult());
            RuleFor(config => config.WestBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.WestBound.Required").GetAwaiter().GetResult());
            RuleFor(config => config.EastBound)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.MapBoundariesEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.EastBound.Required").GetAwaiter().GetResult());

            RuleFor(config => config.DefaultLatitude)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.DefaultLatLngEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.DefaultLatitude.Required").GetAwaiter().GetResult());
            RuleFor(config => config.DefaultLongitude)
                .NotEmpty()
                .NotEqual(decimal.Zero)
                .When(config => config.DefaultLatLngEnabled)
                .WithMessage(_localizationService.GetResourceAsync($"{Defaults.ResourcesNamePrefix}.Fields.DefaultLongitude.Required").GetAwaiter().GetResult());
        }
    }
}
