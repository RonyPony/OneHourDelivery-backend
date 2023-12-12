using FluentValidation;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Validators
{
    /// <summary>
    /// Represents a validator for <see cref="DeliveryAppAddressModel"/>.
    /// </summary>
    public class DeliveryAppAddressModelValidator : BaseNopValidator<DeliveryAppAddressModel>
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppAddressModelValidator"/>.
        /// </summary>
        public DeliveryAppAddressModelValidator()
        {
            RuleFor(model => model.Latitude).NotEmpty().NotEqual(decimal.Zero);
            RuleFor(model => model.Longitude).NotEmpty().NotEqual(decimal.Zero);
        }
        #endregion
    }
}
