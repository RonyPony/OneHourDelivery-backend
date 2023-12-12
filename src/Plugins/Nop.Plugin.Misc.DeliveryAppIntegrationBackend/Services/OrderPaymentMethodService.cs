using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents an implementation for <see cref="IOrderPaymentMethodService"/>.
    /// </summary>
    public class OrderPaymentMethodService : IOrderPaymentMethodService
    {
        #region Fields

        private readonly ICheckoutAttributeParser _checkoutAttributeParser;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderPaymentMethodService"/>.
        /// </summary>
        /// <param name="checkoutAttributeParser">An implementation of <see cref="ICheckoutAttributeParser"/>.</param>
        public OrderPaymentMethodService(
            ICheckoutAttributeParser checkoutAttributeParser)
        {
            _checkoutAttributeParser = checkoutAttributeParser;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public string GetOrderPaymentMethodName(Order order)
        {
            if (order is null)
                throw new ArgumentException("OrderNotFound");

            IList<CheckoutAttributeValue> paymentMethodAttributeValues = _checkoutAttributeParser.ParseCheckoutAttributeValues(order.CheckoutAttributesXml)
               .Where(attr => attr.attribute.Name.Equals(Defaults.PaymentMethodCheckoutAttribute.Name))
               .Select(attr => attr.values.ToList()).FirstOrDefault();
            return paymentMethodAttributeValues != null && paymentMethodAttributeValues.Any() ? paymentMethodAttributeValues.First().Name : string.Empty;
        }

        #endregion
    }
}
