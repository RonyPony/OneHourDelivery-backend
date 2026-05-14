using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Pickup;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Factories;
using Nop.Web.Models.Checkout;
using Nop.Web.Models.Common;
using CheckoutBillingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutBillingAddressModel;
using CheckoutShippingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegration.Models.CheckoutShippingAddressModel;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Factories
{
    /// <summary>
    /// Represents a model factory for checkout models extended with geo coordinates.
    /// </summary>
    public partial class CheckoutModelCustomFactory : ICheckoutModelCustomFactory
    {
        #region Fields

        private readonly AddressSettings _addressSettings;
        private readonly IAddressModelFactory _addressModelFactory;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IPickupPluginManager _pickupPluginManager;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IShippingPluginManager _shippingPluginManager;
        private readonly IShippingService _shippingService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ITaxService _taxService;
        private readonly IWorkContext _workContext;
        private readonly OrderSettings _orderSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly ISettingService _settingService;

        #endregion

        #region Ctor

        public CheckoutModelCustomFactory(AddressSettings addressSettings,
            IAddressModelFactory addressModelFactory,
            IAddressService addressService,
            ICountryService countryService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IOrderTotalCalculationService orderTotalCalculationService,
            IPickupPluginManager pickupPluginManager,
            IPriceFormatter priceFormatter,
            IShippingPluginManager shippingPluginManager,
            IShippingService shippingService,
            IShoppingCartService shoppingCartService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            ITaxService taxService,
            IWorkContext workContext,
            OrderSettings orderSettings,
            ShippingSettings shippingSettings,
            ISettingService settingService)
        {
            _addressSettings = addressSettings;
            _addressModelFactory = addressModelFactory;
            _addressService = addressService;
            _countryService = countryService;
            _currencyService = currencyService;
            _customerService = customerService;
            _localizationService = localizationService;
            _orderTotalCalculationService = orderTotalCalculationService;
            _pickupPluginManager = pickupPluginManager;
            _priceFormatter = priceFormatter;
            _shippingPluginManager = shippingPluginManager;
            _shippingService = shippingService;
            _shoppingCartService = shoppingCartService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
            _storeMappingService = storeMappingService;
            _taxService = taxService;
            _workContext = workContext;
            _orderSettings = orderSettings;
            _shippingSettings = shippingSettings;
            _settingService = settingService;
        }

        #endregion

        #region Utilities

        protected virtual async Task<CheckoutPickupPointsModel> PrepareCheckoutPickupPointsModelAsync(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutPickupPointsModel
            {
                AllowPickupInStore = _shippingSettings.AllowPickupInStore
            };

            if (!model.AllowPickupInStore)
                return model;

            model.DisplayPickupPointsOnMap = _shippingSettings.DisplayPickupPointsOnMap;
            model.GoogleMapsApiKey = _shippingSettings.GoogleMapsApiKey;

            var customer = await _workContext.GetCurrentCustomerAsync();
            var store = await _storeContext.GetCurrentStoreAsync();
            var pickupPointProviders = await _pickupPluginManager.LoadActivePluginsAsync(customer, store.Id);

            if (pickupPointProviders.Any())
            {
                var languageId = (await _workContext.GetWorkingLanguageAsync()).Id;
                var billingAddress = customer.BillingAddressId.HasValue
                    ? await _addressService.GetAddressByIdAsync(customer.BillingAddressId.Value)
                    : null;
                var pickupPointsResponse = await _shippingService.GetPickupPointsAsync(cart, billingAddress, customer, storeId: store.Id);

                if (pickupPointsResponse.Success)
                {
                    foreach (var point in pickupPointsResponse.PickupPoints)
                    {
                        var country = await _countryService.GetCountryByTwoLetterIsoCodeAsync(point.CountryCode);
                        var state = await _stateProvinceService.GetStateProvinceByAbbreviationAsync(point.StateAbbreviation, country?.Id);

                        var pickupPointModel = new CheckoutPickupPointModel
                        {
                            Id = point.Id,
                            Name = point.Name,
                            Description = point.Description,
                            ProviderSystemName = point.ProviderSystemName,
                            Address = point.Address,
                            City = point.City,
                            County = point.County,
                            StateName = state != null ? await _localizationService.GetLocalizedAsync(state, x => x.Name, languageId) : string.Empty,
                            CountryName = country != null ? await _localizationService.GetLocalizedAsync(country, x => x.Name, languageId) : string.Empty,
                            ZipPostalCode = point.ZipPostalCode,
                            Latitude = point.Latitude,
                            Longitude = point.Longitude,
                            OpeningHours = point.OpeningHours
                        };

                        var (shippingTotal, _) = await _orderTotalCalculationService.AdjustShippingRateAsync(point.PickupFee, cart, true);
                        var (rateBase, _) = await _taxService.GetShippingPriceAsync(shippingTotal, customer);
                        var rate = await _currencyService.ConvertFromPrimaryStoreCurrencyAsync(rateBase, await _workContext.GetWorkingCurrencyAsync());
                        pickupPointModel.PickupFee = await _priceFormatter.FormatShippingPriceAsync(rate, true);

                        model.PickupPoints.Add(pickupPointModel);
                    }
                }
                else
                {
                    foreach (var error in pickupPointsResponse.Errors)
                        model.Warnings.Add(error);
                }
            }

            var shippingProviders = await _shippingPluginManager.LoadActivePluginsAsync(customer, store.Id);
            if (!shippingProviders.Any())
            {
                if (!pickupPointProviders.Any())
                {
                    model.Warnings.Add(await _localizationService.GetResourceAsync("Checkout.ShippingIsNotAllowed"));
                    model.Warnings.Add(await _localizationService.GetResourceAsync("Checkout.PickupPoints.NotAvailable"));
                }

                model.PickupInStoreOnly = true;
                model.PickupInStore = true;
            }

            return model;
        }

        private async Task<IList<Address>> GetExistingAddressesAsync(bool validForShipping)
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var addresses = await _customerService.GetAddressesByCustomerIdAsync(customer.Id);
            var result = new List<Address>();

            foreach (var address in addresses)
            {
                var country = await _countryService.GetCountryByAddressAsync(address);
                if (country == null || !country.Published)
                    continue;

                if (validForShipping && !country.AllowsShipping)
                    continue;

                if (!validForShipping && !country.AllowsBilling)
                    continue;

                if (!await _storeMappingService.AuthorizeAsync(country))
                    continue;

                result.Add(address);
            }

            return result;
        }

        private async Task<IList<AddressModel>> GetAddressModelByValidStateAsync(IList<Address> addresses, bool validAddressState)
        {
            var addressesModels = new List<AddressModel>();

            foreach (var address in addresses)
            {
                var addressModel = new AddressModel();
                await _addressModelFactory.PrepareAddressModelAsync(addressModel,
                    address: address,
                    excludeProperties: false,
                    addressSettings: _addressSettings);

                var isAddressValid = await _addressService.IsAddressValidAsync(address);
                if (validAddressState == isAddressValid)
                    addressesModels.Add(addressModel);
            }

            return addressesModels;
        }

        #endregion

        #region Methods

        public virtual async Task<CheckoutBillingAddressModel> PrepareBillingAddressModelAsync(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutBillingAddressModel
            {
                ShipToSameAddressAllowed = _shippingSettings.ShipToSameAddress && await _shoppingCartService.ShoppingCartRequiresShippingAsync(cart),
                ShipToSameAddress = !_orderSettings.DisableBillingAddressCheckoutStep
            };

            var addresses = await GetExistingAddressesAsync(false);
            model.ExistingAddresses = await GetAddressModelByValidStateAsync(addresses, true);
            model.InvalidExistingAddresses = await GetAddressModelByValidStateAsync(addresses, false);

            return model;
        }

        public virtual async Task<CheckoutShippingAddressModel> PrepareShippingAddressModelAsync(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutShippingAddressModel
            {
                DisplayPickupInStore = !_orderSettings.DisplayPickupInStoreOnShippingMethodPage
            };

            if (!_orderSettings.DisplayPickupInStoreOnShippingMethodPage)
                model.PickupPointsModel = await PrepareCheckoutPickupPointsModelAsync(cart);

            var addresses = await GetExistingAddressesAsync(true);
            model.ExistingAddresses = await GetAddressModelByValidStateAsync(addresses, true);
            model.InvalidExistingAddresses = await GetAddressModelByValidStateAsync(addresses, false);

            return model;
        }

        public virtual async Task<CheckoutBillingGeoCoordinatesAddressModel> PrepareCheckoutBillingGeoCoordinatesAddressModelAsync(IList<ShoppingCartItem> cart,
            int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var languageId = (await _workContext.GetWorkingLanguageAsync()).Id;

            var model = new CheckoutBillingGeoCoordinatesAddressModel
            {
                AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel
                {
                    PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>(),
                    AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                    {
                        AddressId = addressId ?? 0,
                        Latitude = latitude ?? decimal.Zero,
                        Longitude = longitude ?? decimal.Zero
                    },
                    RenderGoogleMapsJavaScript = true,
                    MapDivId = "billing-map",
                    AutocompleteInputId = "billing-autocomplete",
                    GeoCoordinatesSearchInputId = "billing-geocoordinatesSearch"
                },
                CheckoutBillingAddressModel = await PrepareBillingAddressModelAsync(cart)
            };

            model.AddressGeoCoordinatesEditModel.Address.Id = addressId ?? 0;
            model.AddressGeoCoordinatesEditModel.Address.CountryId = countryId ?? 0;
            await _addressModelFactory.PrepareAddressModelAsync(model.AddressGeoCoordinatesEditModel.Address,
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: async () => await _countryService.GetAllCountriesForBillingAsync(languageId),
                prePopulateWithCustomerFields: true,
                customer: customer,
                overrideAttributesXml: attributesXml);

            return model;
        }

        public virtual async Task<CheckoutShippingGeoCoordinatesAddressModel> PrepareCheckoutShippingGeoCoordinatesAddressModelAsync(IList<ShoppingCartItem> cart,
            bool renderMapsJS, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var languageId = (await _workContext.GetWorkingLanguageAsync()).Id;

            var model = new CheckoutShippingGeoCoordinatesAddressModel
            {
                AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel
                {
                    PluginConfigurationSettings = await _settingService.LoadSettingAsync<PluginConfigurationSettings>(),
                    AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                    {
                        AddressId = addressId ?? 0,
                        Latitude = latitude ?? decimal.Zero,
                        Longitude = longitude ?? decimal.Zero
                    },
                    RenderGoogleMapsJavaScript = renderMapsJS,
                    MapDivId = "shipping-map",
                    AutocompleteInputId = "shipping-autocomplete",
                    GeoCoordinatesSearchInputId = "shipping-geocoordinatesSearch"
                },
                CheckoutShippingAddressModel = await PrepareShippingAddressModelAsync(cart)
            };

            model.AddressGeoCoordinatesEditModel.Address.Id = addressId ?? 0;
            model.AddressGeoCoordinatesEditModel.Address.CountryId = countryId ?? 0;
            await _addressModelFactory.PrepareAddressModelAsync(model.AddressGeoCoordinatesEditModel.Address,
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: async () => await _countryService.GetAllCountriesForShippingAsync(languageId),
                prePopulateWithCustomerFields: true,
                customer: customer,
                overrideAttributesXml: attributesXml);

            return model;
        }

        public virtual async Task<CustomOnePageCheckoutModel> PrepareOnePageCheckoutModelAsync(IList<ShoppingCartItem> cart)
        {
            ArgumentNullException.ThrowIfNull(cart);

            var customer = await _workContext.GetCurrentCustomerAsync();

            return new CustomOnePageCheckoutModel
            {
                ShippingRequired = await _shoppingCartService.ShoppingCartRequiresShippingAsync(cart),
                DisableBillingAddressCheckoutStep = _orderSettings.DisableBillingAddressCheckoutStep && (await _customerService.GetAddressesByCustomerIdAsync(customer.Id)).Any(),
                CheckoutBillingGeoCoordinatesAddressModel = await PrepareCheckoutBillingGeoCoordinatesAddressModelAsync(cart)
            };
        }

        #endregion
    }
}
