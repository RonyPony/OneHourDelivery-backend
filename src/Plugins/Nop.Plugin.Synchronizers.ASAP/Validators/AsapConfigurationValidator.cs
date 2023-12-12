using FluentValidation;
using Nop.Plugin.Synchronizers.ASAP.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Synchronizers.ASAP.Validators
{
    /// <summary>
    /// Provides The Delivery Asap Configuration Model Validations
    /// </summary>
    public sealed class AsapConfigurationValidator : BaseNopValidator<DeliveryAsapConfigurationModel>
    {
        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="AsapConfigurationValidator"/>
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        public AsapConfigurationValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.ApiKey)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.ApiKey.ErrorMessage"));

            RuleFor(model => model.ServiceUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.ServiceUrl.ErrorMessage"));

            RuleFor(model => model.SharedSecret)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.SharedSecret.ErrorMessage"));

            RuleFor(model => model.UserToken)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.UserToken.ErrorMessage"));

            RuleFor(model => model.Email)
               .NotEmpty()
               .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.Email.ErrorMessage"));

            RuleFor(model => model.Rate)
               .GreaterThanOrEqualTo(decimal.Zero)
               .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.Rate.ErrorMessage"));

            RuleFor(model => model.DefaultWarehouseId)
               .GreaterThanOrEqualTo(0)
               .WithMessage(localizationService.GetResource("Plugins.Synchronizers.ASAP.Fields.DefaultWarehoseId.ErrorMessage"));
        }

        #endregion
    }
}
