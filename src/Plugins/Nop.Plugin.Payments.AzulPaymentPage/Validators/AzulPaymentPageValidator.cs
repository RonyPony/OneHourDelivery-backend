using FluentValidation;
using Nop.Plugin.Payments.AzulPaymentPage.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.AzulPaymentPage.Validators
{
    /// <summary>
    /// Responsible for validating the AZUL configuration form.
    /// </summary>
    public sealed class AzulPaymentPageValidator : BaseNopValidator<AzulConfigurationModel>
    {
        public AzulPaymentPageValidator(ILocalizationService localizationService)
        {
            RuleFor(model => model.Url)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.Url)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"));

            RuleFor(model => model.AlternativeUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.AlternativeUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"));

            RuleFor(model => model.MerchantId)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));

            RuleFor(model => model.MerchantName)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));

            RuleFor(model => model.MerchantType)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));

            RuleFor(model => model.AuthKey)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));

            RuleFor(model => model.CurrencyCode)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.CurrencyCode)
                .MaximumLength(3)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.CurrencyCodeMaxLengthExceeded"));

            RuleFor(model => model.ApprovedUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.ApprovedUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"));

            RuleFor(model => model.DeclinedUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.DeclinedUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"));

            RuleFor(model => model.CancelUrl)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"));
            RuleFor(model => model.CancelUrl)
                .MaximumLength(150)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.UrlMaxLengthExceeded"));

            RuleFor(model => model.CustomField1Label)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"))
                .When(model => model.UseCustomField1);
            RuleFor(model => model.CustomField1Label)
                .MaximumLength(15)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"))
                .When(model => model.UseCustomField1);

            RuleFor(model => model.CustomField1Value)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"))
                .When(model => model.UseCustomField1);
            RuleFor(model => model.CustomField1Value)
                .MaximumLength(15)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"))
                .When(model => model.UseCustomField1);

            RuleFor(model => model.CustomField2Label)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"))
                .When(model => model.UseCustomField2);
            RuleFor(model => model.CustomField2Label)
                .MaximumLength(15)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"))
                .When(model => model.UseCustomField2);

            RuleFor(model => model.CustomField2Value)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.IsRequired"))
                .When(model => model.UseCustomField2);
            RuleFor(model => model.CustomField2Value)
                .MaximumLength(15)
                .WithMessage(localizationService.GetResource("Plugins.Payments.AzulPaymentPage.MaxLengthExceeded"))
                .When(model => model.UseCustomField2);
        }
    }
}
