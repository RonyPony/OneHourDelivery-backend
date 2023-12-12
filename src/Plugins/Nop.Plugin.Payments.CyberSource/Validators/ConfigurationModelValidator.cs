using FluentValidation;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Payments.CyberSource.Validators
{
    /// <summary>
    /// Represents a Fluent Validator for <see cref="ConfigurationModel"/>.
    /// </summary>
    public sealed class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
    {
        /// <summary>
        /// Initilizes a new instance of <see cref="ConfigurationModelValidator"/>.
        /// </summary>
        public ConfigurationModelValidator()
        {
            RuleFor(model => model.CybersourceEnvironment)
                .Equal("test").When(model => !model.CybersourceEnvironment.Equals("live"))
                .WithMessage("Cybersource environment needs to be 'test' or 'live'")
                .Equal("live").When(model => !model.CybersourceEnvironment.Equals("test"))
                .WithMessage("Cybersource environment needs to be 'test' or 'live'");
        }
    }
}
