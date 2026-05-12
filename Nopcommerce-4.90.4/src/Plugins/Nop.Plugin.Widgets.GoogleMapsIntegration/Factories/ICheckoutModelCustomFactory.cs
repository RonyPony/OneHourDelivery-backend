using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Web.Models.Checkout;
using System.Collections.Generic;
using CheckoutBillingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutBillingAddressModel;
using CheckoutShippingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutShippingAddressModel;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Factories
{
    /// <summary>
    /// Represents a contract for checkout model custom factory.
    /// </summary>
    public partial interface ICheckoutModelCustomFactory
    {
        /// <summary>
        /// Prepares a <see cref="CheckoutBillingAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="CheckoutBillingAddressModel"/>.</returns>
        CheckoutBillingAddressModel PrepareBillingAddressModel(IList<ShoppingCartItem> cart);

        /// <summary>
        /// Prepares a <see cref="CheckoutShippingAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="CheckoutShippingAddressModel"/>.</returns>
        CheckoutShippingAddressModel PrepareShippingAddressModel(IList<ShoppingCartItem> cart);

        /// <summary>
        /// Prepares a <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <param name="addressId">An address id.</param>
        /// <param name="countryId">A country id.</param>
        /// <param name="attributesXml">Custom attributes for address.</param>
        /// <param name="latitude">A latitude.</param>
        /// <param name="longitude">A longitude.</param>
        /// <returns>An instance of <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>.</returns>
        CheckoutBillingGeoCoordinatesAddressModel PrepareCheckoutBillingGeoCoordinatesAddressModel(IList<ShoppingCartItem> cart,
            int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null);

        /// <summary>
        /// Prepares a <see cref="CheckoutShippingGeoCoordinatesAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <param name="renderMapsJS">A boolean that indicates if Google Maps JavaScript API renderization is required.</param>
        /// <param name="addressId">An address id.</param>
        /// <param name="countryId">A country id.</param>
        /// <param name="attributesXml">Custom attributes for address.</param>
        /// <param name="latitude">A latitude.</param>
        /// <param name="longitude">A longitude.</param>
        /// <returns>An instance of <see cref="CheckoutShippingGeoCoordinatesAddressModel"/>.</returns>
        CheckoutShippingGeoCoordinatesAddressModel PrepareCheckoutShippingGeoCoordinatesAddressModel(IList<ShoppingCartItem> cart,
            bool renderMapsJS, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null);

        /// <summary>
        /// Prepares a <see cref="OnePageCheckoutModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="OnePageCheckoutModel"/>.</returns>
        CustomOnePageCheckoutModel PrepareOnePageCheckoutModel(IList<ShoppingCartItem> cart);
    }
}
