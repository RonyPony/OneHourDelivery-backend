using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Web.Models.Checkout;
using CheckoutBillingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutBillingAddressModel;
using CheckoutShippingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutShippingAddressModel;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Factories
{
    /// <summary>
    /// Represents a contract for checkout model custom factory.
    /// </summary>
    public partial interface ICheckoutModelCustomFactory
    {
        Task<CheckoutBillingAddressModel> PrepareBillingAddressModelAsync(IList<ShoppingCartItem> cart);

        Task<CheckoutShippingAddressModel> PrepareShippingAddressModelAsync(IList<ShoppingCartItem> cart);

        Task<CheckoutBillingGeoCoordinatesAddressModel> PrepareCheckoutBillingGeoCoordinatesAddressModelAsync(IList<ShoppingCartItem> cart,
            int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null);

        Task<CheckoutShippingGeoCoordinatesAddressModel> PrepareCheckoutShippingGeoCoordinatesAddressModelAsync(IList<ShoppingCartItem> cart,
            bool renderMapsJS, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null);

        Task<CustomOnePageCheckoutModel> PrepareOnePageCheckoutModelAsync(IList<ShoppingCartItem> cart);
    }
}
