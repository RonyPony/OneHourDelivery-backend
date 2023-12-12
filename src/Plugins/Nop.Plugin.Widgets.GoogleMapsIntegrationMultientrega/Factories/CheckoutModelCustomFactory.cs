using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
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
using System;
using System.Collections.Generic;
using System.Linq;
using CheckoutBillingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models.CheckoutBillingAddressModel;
using CheckoutShippingAddressModel = Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models.CheckoutShippingAddressModel;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Factories
{
    /// <summary>
    /// Represents a model factory for <see cref="CheckoutBillingAddressModel"/>, <see cref="CheckoutShippingAddressModel"/>, <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>, <see cref="CheckoutShippingGeoCoordinatesAddressModel"/> and <see cref="CustomOnePageCheckoutModel"/>.
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
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly IMultientregaAddressService _multientregaAddressService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="CheckoutModelCustomFactory"/>.
        /// </summary>
        /// <param name="addressSettings">An instance of <see cref="AddressSettings"/>.</param>
        /// <param name="addressModelFactory">An implementation of <see cref="IAddressModelFactory"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/>.</param>
        /// <param name="currencyService">An implementation of <see cref="ICurrencyService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="orderTotalCalculationService">An implementation of <see cref="IOrderTotalCalculationService"/>.</param>
        /// <param name="pickupPluginManager">An implementation of <see cref="IPickupPluginManager"/>.</param>
        /// <param name="priceFormatter">An implementation of <see cref="IPriceFormatter"/>.</param>
        /// <param name="shippingPluginManager">An implementation of <see cref="IShippingPluginManager"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="shoppingCartService">An implementation of <see cref="IShoppingCartService"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="storeMappingService">An implementation of <see cref="IStoreMappingService"/>.</param>
        /// <param name="taxService">An implementation of <see cref="ITaxService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="orderSettings">An instance of <see cref="OrderSettings"/>.</param>
        /// <param name="shippingSettings">An instance of <see cref="ShippingSettings"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="multientregaAddressService">An implementation of <see cref="IMultientregaAddressService"/>.</param>
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
            ISettingService settingService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            IMultientregaAddressService multientregaAddressService)
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
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _multientregaAddressService = multientregaAddressService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepares the checkout pickup points model
        /// </summary>
        /// <param name="cart">Cart</param>
        /// <returns>The checkout pickup points model</returns>
        protected virtual CheckoutPickupPointsModel PrepareCheckoutPickupPointsModel(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutPickupPointsModel()
            {
                AllowPickupInStore = _shippingSettings.AllowPickupInStore
            };
            if (model.AllowPickupInStore)
            {
                model.DisplayPickupPointsOnMap = _shippingSettings.DisplayPickupPointsOnMap;
                model.GoogleMapsApiKey = _shippingSettings.GoogleMapsApiKey;
                var pickupPointProviders = _pickupPluginManager.LoadActivePlugins(_workContext.CurrentCustomer, _storeContext.CurrentStore.Id);
                if (pickupPointProviders.Any())
                {
                    var languageId = _workContext.WorkingLanguage.Id;
                    var pickupPointsResponse = _shippingService.GetPickupPoints(_workContext.CurrentCustomer.BillingAddressId ?? 0,
                        _workContext.CurrentCustomer, storeId: _storeContext.CurrentStore.Id);
                    if (pickupPointsResponse.Success)
                        model.PickupPoints = pickupPointsResponse.PickupPoints.Select(point =>
                        {
                            var country = _countryService.GetCountryByTwoLetterIsoCode(point.CountryCode);
                            var state = _stateProvinceService.GetStateProvinceByAbbreviation(point.StateAbbreviation, country?.Id);

                            var pickupPointModel = new CheckoutPickupPointModel
                            {
                                Id = point.Id,
                                Name = point.Name,
                                Description = point.Description,
                                ProviderSystemName = point.ProviderSystemName,
                                Address = point.Address,
                                City = point.City,
                                County = point.County,
                                StateName = state != null ? _localizationService.GetLocalized(state, x => x.Name, languageId) : string.Empty,
                                CountryName = country != null ? _localizationService.GetLocalized(country, x => x.Name, languageId) : string.Empty,
                                ZipPostalCode = point.ZipPostalCode,
                                Latitude = point.Latitude,
                                Longitude = point.Longitude,
                                OpeningHours = point.OpeningHours
                            };

                            var cart = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, ShoppingCartType.ShoppingCart, _storeContext.CurrentStore.Id);
                            var amount = _orderTotalCalculationService.IsFreeShipping(cart) ? 0 : point.PickupFee;

                            if (amount > 0)
                            {
                                amount = _taxService.GetShippingPrice(amount, _workContext.CurrentCustomer);
                                amount = _currencyService.ConvertFromPrimaryStoreCurrency(amount, _workContext.WorkingCurrency);
                                pickupPointModel.PickupFee = _priceFormatter.FormatShippingPrice(amount, true);
                            }

                            //adjust rate
                            var shippingTotal = _orderTotalCalculationService.AdjustShippingRate(point.PickupFee, cart, out var _, true);
                            var rateBase = _taxService.GetShippingPrice(shippingTotal, _workContext.CurrentCustomer);
                            var rate = _currencyService.ConvertFromPrimaryStoreCurrency(rateBase, _workContext.WorkingCurrency);
                            pickupPointModel.PickupFee = _priceFormatter.FormatShippingPrice(rate, true);

                            return pickupPointModel;
                        }).ToList();
                    else
                        foreach (var error in pickupPointsResponse.Errors)
                            model.Warnings.Add(error);
                }

                //only available pickup points
                var shippingProviders = _shippingPluginManager.LoadActivePlugins(_workContext.CurrentCustomer, _storeContext.CurrentStore.Id);
                if (!shippingProviders.Any())
                {
                    if (!pickupPointProviders.Any())
                    {
                        model.Warnings.Add(_localizationService.GetResource("Checkout.ShippingIsNotAllowed"));
                        model.Warnings.Add(_localizationService.GetResource("Checkout.PickupPoints.NotAvailable"));
                    }
                    model.PickupInStoreOnly = true;
                    model.PickupInStore = true;
                    return model;
                }
            }

            return model;
        }

        private IList<Address> GetExistingAddresses()
            => _customerService.GetAddressesByCustomerId(_workContext.CurrentCustomer.Id)
                   .Where(a => _countryService.GetCountryByAddress(a) is Country country &&
                       (//published
                       country.Published &&
                       //allow billing
                       country.AllowsBilling &&
                       //enabled for the current store
                       _storeMappingService.Authorize(country)))
                   .ToList();

        private IList<AddressModel> GetAddressModelByValidState(IList<Address> addresses, bool validAddressState)
        {
            var addressesModels = new List<AddressModel>();

            foreach (var address in addresses)
            {
                var addressModel = new AddressModel();
                _addressModelFactory.PrepareAddressModel(addressModel,
                    address: address,
                    excludeProperties: false,
                    addressSettings: _addressSettings);

                if (validAddressState && _addressService.IsAddressValid(address))
                {
                    addressesModels.Add(addressModel);
                }
                
                if (!validAddressState && !_addressService.IsAddressValid(address))
                {
                    addressesModels.Add(addressModel);
                }
            }

            return addressesModels;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares a <see cref="CheckoutBillingAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="CheckoutBillingAddressModel"/>.</returns>
        public virtual CheckoutBillingAddressModel PrepareBillingAddressModel(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutBillingAddressModel
            {
                ShipToSameAddressAllowed = _shippingSettings.ShipToSameAddress && _shoppingCartService.ShoppingCartRequiresShipping(cart),
                //allow customers to enter (choose) a shipping address if "Disable Billing address step" setting is enabled
                ShipToSameAddress = !_orderSettings.DisableBillingAddressCheckoutStep
            };

            //existing addresses
            var addresses = GetExistingAddresses();
            model.ExistingAddresses = GetAddressModelByValidState(addresses, true);
            model.InvalidExistingAddresses = GetAddressModelByValidState(addresses, false);

            return model;
        }

        /// <summary>
        /// Prepares a <see cref="CheckoutShippingAddressModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="CheckoutShippingAddressModel"/>.</returns>
        public virtual CheckoutShippingAddressModel PrepareShippingAddressModel(IList<ShoppingCartItem> cart)
        {
            var model = new CheckoutShippingAddressModel()
            {
                DisplayPickupInStore = !_orderSettings.DisplayPickupInStoreOnShippingMethodPage
            };

            if (!_orderSettings.DisplayPickupInStoreOnShippingMethodPage)
                model.PickupPointsModel = PrepareCheckoutPickupPointsModel(cart);

            //existing addresses
            var addresses = GetExistingAddresses();
            model.ExistingAddresses = GetAddressModelByValidState(addresses, true);
            model.InvalidExistingAddresses = GetAddressModelByValidState(addresses, false);

            return model;
        }

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
        public virtual CheckoutBillingGeoCoordinatesAddressModel PrepareCheckoutBillingGeoCoordinatesAddressModel(IList<ShoppingCartItem> cart,
            int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
        {
            var mapping = new AddressGeoCoordinatesMapping();
            if (addressId.HasValue) mapping =  _addressGeoCoordinatesService.GetAddressGeoCoordinates(addressId.Value);

            var model = new CheckoutBillingGeoCoordinatesAddressModel
            {
                AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel
                {
                    PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                    AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                    {
                        AddressId = mapping?.AddressId ?? 0,
                        Latitude = mapping?.Latitude ?? decimal.Zero,
                        Longitude = mapping?.Longitude ?? decimal.Zero,
                        ProvinceId = mapping?.ProvinceId ?? "0",
                        DistrictId = mapping?.DistrictId ?? "0",
                        TownshipId = mapping?.TownshipId ?? "0",
                        NeighborhoodId = mapping?.NeighborhoodId ?? "0",
                    },
                    RenderGoogleMapsJavaScript = true,
                    MapDivId = "billing-map",
                    AutocompleteInputId = "billing-autocomplete",
                    GeoCoordinatesSearchInputId = "billing-geocoordinatesSearch",
                    AvailableProvinces = mapping != null ? _multientregaAddressService.GetProvincesSelectListItems("0", false) :
                        countryId.HasValue ? _multientregaAddressService.GetProvincesSelectListItems(countryId.Value.ToString()) :
                        _multientregaAddressService.GetProvincesSelectListItems("0"),
                    AvailableDistricts = mapping?.ProvinceId != null ? _multientregaAddressService.GetDistrictsSelectListItems(mapping.ProvinceId) :
                        _multientregaAddressService.GetDistrictsSelectListItems("0"),
                    AvailableTownships = mapping?.DistrictId != null ? _multientregaAddressService.GetTownshipsSelectListItems(mapping.DistrictId) :
                        _multientregaAddressService.GetTownshipsSelectListItems("0"),
                    AvailableNeighborhoods = mapping?.TownshipId != null ? _multientregaAddressService.GetNeigborhoodsSelectListItems(mapping.TownshipId) :
                        _multientregaAddressService.GetNeigborhoodsSelectListItems("0")
                },
                CheckoutBillingAddressModel = PrepareBillingAddressModel(cart)
            };

            model.AddressGeoCoordinatesEditModel.Address.Id = addressId ?? 0;
            model.AddressGeoCoordinatesEditModel.Address.CountryId = countryId ?? 0;
            _addressModelFactory.PrepareAddressModel(model.AddressGeoCoordinatesEditModel.Address,
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(_workContext.WorkingLanguage.Id).Where(country => country.TwoLetterIsoCode.Equals("PA")).ToList(),
                prePopulateWithCustomerFields: true,
                customer: _workContext.CurrentCustomer,
                overrideAttributesXml: attributesXml);

            return model;
        }

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
        public virtual CheckoutShippingGeoCoordinatesAddressModel PrepareCheckoutShippingGeoCoordinatesAddressModel(IList<ShoppingCartItem> cart,
            bool renderMapsJS, int? addressId = null, int? countryId = null, string attributesXml = "", decimal? latitude = null, decimal? longitude = null)
        {
            var mapping = new AddressGeoCoordinatesMapping();
            if (addressId.HasValue) mapping = _addressGeoCoordinatesService.GetAddressGeoCoordinates(addressId.Value);

            var model = new CheckoutShippingGeoCoordinatesAddressModel
            {
                AddressGeoCoordinatesEditModel = new AddressGeoCoordinatesEditModel
                {
                    PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                    AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                    {
                        AddressId = addressId ?? 0,
                        Latitude = latitude ?? decimal.Zero,
                        Longitude = longitude ?? decimal.Zero,
                        ProvinceId = mapping?.ProvinceId ?? "0",
                        DistrictId = mapping?.DistrictId ?? "0",
                        TownshipId = mapping?.TownshipId ?? "0",
                        NeighborhoodId = mapping?.NeighborhoodId ?? "0",
                    },
                    RenderGoogleMapsJavaScript = renderMapsJS,
                    MapDivId = "shipping-map",
                    AutocompleteInputId = "shipping-autocomplete",
                    GeoCoordinatesSearchInputId = "shipping-geocoordinatesSearch",
                    AvailableProvinces = mapping != null ? _multientregaAddressService.GetProvincesSelectListItems("0", false) :
                        countryId.HasValue ? _multientregaAddressService.GetProvincesSelectListItems(countryId.Value.ToString()) :
                        _multientregaAddressService.GetProvincesSelectListItems("0"),
                    AvailableDistricts = mapping?.ProvinceId != null ? _multientregaAddressService.GetDistrictsSelectListItems(mapping.ProvinceId) :
                        _multientregaAddressService.GetDistrictsSelectListItems("0"),
                    AvailableTownships = mapping?.DistrictId != null ? _multientregaAddressService.GetTownshipsSelectListItems(mapping.DistrictId) :
                        _multientregaAddressService.GetTownshipsSelectListItems("0"),
                    AvailableNeighborhoods = mapping?.TownshipId != null ? _multientregaAddressService.GetNeigborhoodsSelectListItems(mapping.TownshipId) :
                        _multientregaAddressService.GetNeigborhoodsSelectListItems("0")
                },
                CheckoutShippingAddressModel = PrepareShippingAddressModel(cart)
            };

            model.AddressGeoCoordinatesEditModel.Address.Id = addressId ?? 0;
            model.AddressGeoCoordinatesEditModel.Address.CountryId = countryId ?? 0;
            _addressModelFactory.PrepareAddressModel(model.AddressGeoCoordinatesEditModel.Address,
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(_workContext.WorkingLanguage.Id).Where(country => country.TwoLetterIsoCode.Equals("PA")).ToList(),
                prePopulateWithCustomerFields: true,
                customer: _workContext.CurrentCustomer,
                overrideAttributesXml: attributesXml);

            return model;
        }

        /// <summary>
        /// Prepares a <see cref="CustomOnePageCheckoutModel"/>.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is of <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="OnePageCheckoutModel"/>.</returns>
        public virtual CustomOnePageCheckoutModel PrepareOnePageCheckoutModel(IList<ShoppingCartItem> cart)
        {
            if (cart == null)
                throw new ArgumentNullException(nameof(cart));

            var model = new CustomOnePageCheckoutModel
            {
                ShippingRequired = _shoppingCartService.ShoppingCartRequiresShipping(cart),
                DisableBillingAddressCheckoutStep = _orderSettings.DisableBillingAddressCheckoutStep && _customerService.GetAddressesByCustomerId(_workContext.CurrentCustomer.Id).Any(),
                CheckoutBillingGeoCoordinatesAddressModel = PrepareCheckoutBillingGeoCoordinatesAddressModel(cart)
            };

            return model;
        }

        #endregion
    }
}
