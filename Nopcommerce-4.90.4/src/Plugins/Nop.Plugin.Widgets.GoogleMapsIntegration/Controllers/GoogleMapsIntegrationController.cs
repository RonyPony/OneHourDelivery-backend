using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Http.Extensions;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Factories;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Attributes;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Extensions;
using Nop.Web.Factories;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Controllers
{
    /// <summary>
    /// Represents the main controller for Google Maps Integration plugin.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public class GoogleMapsIntegrationController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;
        private readonly ICustomerService _customerService;
        private readonly AddressSettings _addressSettings;
        private readonly IAddressModelFactory _addressModelFactory;
        private readonly IWorkContext _workContext;
        private readonly IAttributeParser<AddressAttribute, AddressAttributeValue> _addressAttributeParser;
        private readonly ICountryService _countryService;
        private readonly IAddressService _addressService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly OrderSettings _orderSettings;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreContext _storeContext;
        private readonly ShippingSettings _shippingSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IShippingService _shippingService;
        private readonly ICheckoutModelCustomFactory _checkoutModelCustomFactory;
        private readonly ILogger _logger;
        private readonly ICheckoutModelFactory _checkoutModelFactory;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IPaymentPluginManager _paymentPluginManager;
        private readonly PaymentSettings _paymentSettings;
        private readonly IPermissionService _permissionService;

        #endregion

        #region Properties

        private Customer CurrentCustomer => _workContext.GetCurrentCustomerAsync().GetAwaiter().GetResult();

        private Store CurrentStore => _storeContext.GetCurrentStoreAsync().GetAwaiter().GetResult();

        private Language WorkingLanguage => _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult();

        private Currency WorkingCurrency => _workContext.GetWorkingCurrencyAsync().GetAwaiter().GetResult();

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="GoogleMapsIntegrationController"/>.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="addressAttributeParser">An implementation of <see cref="IAttributeParser{TAttribute,TAttributeValue}"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="addressModelFactory">An implementation of <see cref="IAddressModelFactory"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="addressSettings">An instance of <see cref="AddressSettings"/>.</param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="orderSettings">An instance of <see cref="OrderSettings"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="shippingSettings">An instance of <see cref="ShippingSettings"/>.</param>
        /// <param name="shoppingCartService">An implementation of <see cref="IShoppingCartService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="checkoutModelCustomFactory">An implementation of <see cref="ICheckoutModelCustomFactory"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="checkoutModelFactory">An implementation of <see cref="ICheckoutModelFactory"/>.</param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/>.</param>
        /// <param name="paymentPluginManager">An implementation of <see cref="IPaymentPluginManager"/>.</param>
        /// <param name="paymentSettings">An instance of <see cref="PaymentSettings"/>.</param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/>.</param>
        public GoogleMapsIntegrationController(
            ILocalizationService localizationService,
            INotificationService notificationService,
            ISettingService settingService,
            ICustomerService customerService,
            AddressSettings addressSettings,
            IAddressModelFactory addressModelFactory,
            IWorkContext workContext,
            IAttributeParser<AddressAttribute, AddressAttributeValue> addressAttributeParser,
            ICountryService countryService,
            IAddressService addressService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            OrderSettings orderSettings,
            IShoppingCartService shoppingCartService,
            IStoreContext storeContext,
            ShippingSettings shippingSettings,
            IGenericAttributeService genericAttributeService,
            IShippingService shippingService,
            ICheckoutModelCustomFactory checkoutModelCustomFactory,
            ILogger logger,
            ICheckoutModelFactory checkoutModelFactory,
            IOrderProcessingService orderProcessingService,
            IPaymentPluginManager paymentPluginManager,
            PaymentSettings paymentSettings,
            IPermissionService permissionService)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _settingService = settingService;
            _customerService = customerService;
            _addressSettings = addressSettings;
            _addressModelFactory = addressModelFactory;
            _workContext = workContext;
            _addressAttributeParser = addressAttributeParser;
            _countryService = countryService;
            _addressService = addressService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _orderSettings = orderSettings;
            _shoppingCartService = shoppingCartService;
            _storeContext = storeContext;
            _shippingSettings = shippingSettings;
            _genericAttributeService = genericAttributeService;
            _shippingService = shippingService;
            _checkoutModelCustomFactory = checkoutModelCustomFactory;
            _logger = logger;
            _checkoutModelFactory = checkoutModelFactory;
            _orderProcessingService = orderProcessingService;
            _paymentPluginManager = paymentPluginManager;
            _paymentSettings = paymentSettings;
            _permissionService = permissionService;
        }

        #endregion

        #region Utilities

        private PluginConfiguration GetModel()
        {
            var pluginConfigSettings = _settingService.LoadSetting<PluginConfigurationSettings>();

            //whether plugin is configured
            if (string.IsNullOrWhiteSpace(pluginConfigSettings.ApiKey))
                return new PluginConfiguration();

            //map common properties
            var model = pluginConfigSettings.ToModel<PluginConfiguration>();

            return model;
        }

        private string RenderPartialViewToString(string viewName, object model)
            => RenderPartialViewToStringAsync(viewName, model).GetAwaiter().GetResult();

        /// <summary>
        /// Parses the value indicating whether the "pickup in store" is allowed
        /// </summary>
        /// <param name="form">The form</param>
        /// <returns>The value indicating whether the "pickup in store" is allowed</returns>
        protected virtual bool ParsePickupInStore(IFormCollection form)
        {
            var pickupInStore = false;

            var pickupInStoreParameter = form["PickupInStore"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(pickupInStoreParameter))
                bool.TryParse(pickupInStoreParameter, out pickupInStore);

            return pickupInStore;
        }

        /// <summary>
        /// Parses the pickup option
        /// </summary>
        /// <param name="cart">Shopping cart.</param>
        /// <param name="form">The form</param>
        /// <returns>The pickup option</returns>
        protected virtual PickupPoint ParsePickupOption(IList<ShoppingCartItem> cart, IFormCollection form)
        {
            var pickupPoint = form["pickup-points-id"].ToString().Split(new[] { "___" }, StringSplitOptions.None);
            var billingAddress = CurrentCustomer.BillingAddressId.HasValue
                ? _addressService.GetAddressById(CurrentCustomer.BillingAddressId.Value)
                : null;
            var pickupPoints = _shippingService.GetPickupPointsAsync(cart, billingAddress,
                CurrentCustomer, pickupPoint[1], CurrentStore.Id).GetAwaiter().GetResult().PickupPoints.ToList();
            var selectedPoint = pickupPoints.FirstOrDefault(x => x.Id.Equals(pickupPoint[0]));
            if (selectedPoint == null)
                throw new Exception("Pickup point is not allowed");

            return selectedPoint;
        }

        /// <summary>
        /// Saves the pickup option
        /// </summary>
        /// <param name="pickupPoint">The pickup option</param>
        protected virtual void SavePickupOption(PickupPoint pickupPoint)
        {
            var pickUpInStoreShippingOption = new ShippingOption
            {
                Name = string.Format(_localizationService.GetResource("Checkout.PickupPoints.Name"), pickupPoint.Name),
                Rate = pickupPoint.PickupFee,
                Description = pickupPoint.Description,
                ShippingRateComputationMethodSystemName = pickupPoint.ProviderSystemName,
                IsPickupInStore = true
            };
            _genericAttributeService.SaveAttribute(CurrentCustomer, NopCustomerDefaults.SelectedShippingOptionAttribute, pickUpInStoreShippingOption, CurrentStore.Id);
            _genericAttributeService.SaveAttribute(CurrentCustomer, NopCustomerDefaults.SelectedPickupPointAttribute, pickupPoint, CurrentStore.Id);
        }

        /// <summary>
        /// Gets and sets the required models for shipping method section on one page checkout.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="JsonResult"/>.</returns>
        protected virtual JsonResult OpcLoadStepAfterShippingAddress(IList<ShoppingCartItem> cart)
        {
            var shippingMethodModel = _checkoutModelFactory.PrepareShippingMethodModel(cart, _customerService.GetCustomerShippingAddress(CurrentCustomer));
            if (_shippingSettings.BypassShippingMethodSelectionIfOnlyOne &&
                shippingMethodModel.ShippingMethods.Count == 1)
            {
                //if we have only one shipping method, then a customer doesn't have to choose a shipping method
                _genericAttributeService.SaveAttribute(CurrentCustomer,
                    NopCustomerDefaults.SelectedShippingOptionAttribute,
                    shippingMethodModel.ShippingMethods.First().ShippingOption,
                    CurrentStore.Id);

                //load next step
                return OpcLoadStepAfterShippingMethod(cart);
            }

            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "shipping-method",
                    html = RenderPartialViewToString($"{Defaults.CheckoutViewsDir}/OpcShippingMethods.cshtml", shippingMethodModel)
                },
                goto_section = "shipping_method"
            });
        }

        /// <summary>
        /// Gets and sets the required models for payment method section on one page checkout.
        /// </summary>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="JsonResult"/>.</returns>
        protected virtual JsonResult OpcLoadStepAfterShippingMethod(IList<ShoppingCartItem> cart)
        {
            //Check whether payment workflow is required
            //we ignore reward points during cart total calculation
            var isPaymentWorkflowRequired = _orderProcessingService.IsPaymentWorkflowRequired(cart, false);
            if (isPaymentWorkflowRequired)
            {
                //filter by country
                var filterByCountryId = 0;
                if (_addressSettings.CountryEnabled)
                {
                    filterByCountryId = _customerService.GetCustomerBillingAddress(CurrentCustomer)?.CountryId ?? 0;
                }

                //payment is required
                var paymentMethodModel = _checkoutModelFactory.PreparePaymentMethodModel(cart, filterByCountryId);

                if (_paymentSettings.BypassPaymentMethodSelectionIfOnlyOne &&
                    paymentMethodModel.PaymentMethods.Count == 1 && !paymentMethodModel.DisplayRewardPoints)
                {
                    //if we have only one payment method and reward points are disabled or the current customer doesn't have any reward points
                    //so customer doesn't have to choose a payment method

                    var selectedPaymentMethodSystemName = paymentMethodModel.PaymentMethods[0].PaymentMethodSystemName;
                    _genericAttributeService.SaveAttribute(CurrentCustomer,
                        NopCustomerDefaults.SelectedPaymentMethodAttribute,
                        selectedPaymentMethodSystemName, CurrentStore.Id);

                    var paymentMethodInst = _paymentPluginManager
                        .LoadPluginBySystemName(selectedPaymentMethodSystemName, CurrentCustomer, CurrentStore.Id);
                    if (!_paymentPluginManager.IsPluginActive(paymentMethodInst))
                        throw new Exception("Selected payment method can't be parsed");

                    return OpcLoadStepAfterPaymentMethod(paymentMethodInst, cart);
                }

                //customer have to choose a payment method
                return Json(new
                {
                    update_section = new UpdateSectionJsonModel
                    {
                        name = "payment-method",
                        html = RenderPartialViewToString($"{Defaults.CheckoutViewsDir}/OpcPaymentMethods.cshtml", paymentMethodModel)
                    },
                    goto_section = "payment_method"
                });
            }

            //payment is not required
            _genericAttributeService.SaveAttribute<string>(CurrentCustomer,
                NopCustomerDefaults.SelectedPaymentMethodAttribute, null, CurrentStore.Id);

            var confirmOrderModel = _checkoutModelFactory.PrepareConfirmOrderModel(cart);
            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "confirm-order",
                    html = RenderPartialViewToString($"{Defaults.CheckoutViewsDir}/OpcConfirmOrder.cshtml", confirmOrderModel)
                },
                goto_section = "confirm_order"
            });
        }

        /// <summary>
        /// Gets and sets the required models for confirm order section on one page checkout.
        /// </summary>
        /// <param name="paymentMethod">An implementation of <see cref="IPaymentMethod"/>.</param>
        /// <param name="cart">An implementation of <see cref="IList{T}"/> where T is <see cref="ShoppingCartItem"/>.</param>
        /// <returns>An instance of <see cref="JsonResult"/>.</returns>
        protected virtual JsonResult OpcLoadStepAfterPaymentMethod(IPaymentMethod paymentMethod, IList<ShoppingCartItem> cart)
        {
            if (paymentMethod.SkipPaymentInfo ||
                (paymentMethod.PaymentMethodType == PaymentMethodType.Redirection && _paymentSettings.SkipPaymentInfoStepForRedirectionPaymentMethods))
            {
                //skip payment info page
                var paymentInfo = new ProcessPaymentRequest();

                //session save
                HttpContext.Session.Set("OrderPaymentInfo", paymentInfo);

                var confirmOrderModel = _checkoutModelFactory.PrepareConfirmOrderModel(cart);
                return Json(new
                {
                    update_section = new UpdateSectionJsonModel
                    {
                        name = "confirm-order",
                        html = RenderPartialViewToString($"{Defaults.CheckoutViewsDir}/OpcConfirmOrder.cshtml", confirmOrderModel)
                    },
                    goto_section = "confirm_order"
                });
            }

            //return payment info page
            var paymenInfoModel = _checkoutModelFactory.PreparePaymentInfoModel(paymentMethod);
            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "payment-info",
                    html = RenderPartialViewToString($"{Defaults.CheckoutViewsDir}/OpcPaymentInfo.cshtml", paymenInfoModel)
                },
                goto_section = "payment_info"
            });
        }

        #endregion

        #region Methods

        #region Configure

        /// <summary>
        /// Prepares a <see cref="PluginConfiguration"/> object to send it to Configure.cshtml page and configure it for the plugin.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.ADMIN)]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            PluginConfiguration model = GetModel();

            return View($"~/{Defaults.PluginOutputDir}/Views/Configure.cshtml", model);
        }

        /// <summary>
        /// Receives and sets the configuration for this plugin.
        /// </summary>
        /// <param name="model">An instance <see cref="PluginConfiguration"/>.</param>
        /// <returns>An <see cref="IActionResult"/> with the configuration's view.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.ADMIN)]
        public IActionResult Configure(PluginConfiguration model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var pluginConfigSettings = model.ToEntity<PluginConfigurationSettings>();

            _settingService.SaveSetting(pluginConfigSettings);
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion

        #region Customer Address

        /// <summary>
        /// Deletes an address.
        /// </summary>
        /// <param name="addressId">The id of the address to delete.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        [HttpsRequirement]
        public virtual IActionResult AddressDelete(int addressId)
        {
            if (!_customerService.IsRegistered(CurrentCustomer))
                return Challenge();

            var customer = CurrentCustomer;

            //find address (ensure that it belongs to the current customer)
            var address = _customerService.GetCustomerAddress(customer.Id, addressId);
            if (address != null)
            {
                _customerService.RemoveCustomerAddress(customer, address);
                _customerService.UpdateCustomer(customer);
                _addressGeoCoordinatesService.RemoveAddressGeoCoordinates(addressId);
                //now delete the address record
                _addressService.DeleteAddress(address);
            }

            //redirect to the address list page
            return Json(new
            {
                redirect = Url.RouteUrl("CustomerAddresses"),
            });
        }

        /// <summary>
        /// Gets and sets the required models for adding new addresses including geo coordinates.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpsRequirement]
        public virtual IActionResult AddressAdd()
        {
            if (!_customerService.IsRegistered(CurrentCustomer))
                return Challenge();

            var model = new AddressGeoCoordinatesEditModel {
                PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m
                },
                RenderGoogleMapsJavaScript = true,
                MapDivId = "map",
                AutocompleteInputId = "autocomplete",
                GeoCoordinatesSearchInputId = "geocoordinatesSearch"
            };

            _addressModelFactory.PrepareAddressModel(model.Address,
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(WorkingLanguage.Id));

            return View(model);
        }

        /// <summary>
        /// Inserts a new address and its geo coordinates.
        /// </summary>
        /// <param name="model">An instance of <see cref="AddressGeoCoordinatesEditModel"/>.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public virtual IActionResult AddressAdd(AddressGeoCoordinatesEditModel model, IFormCollection form)
        {
            if (!_customerService.IsRegistered(CurrentCustomer))
                return Challenge();

            //custom address attributes
            var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                var address = model.Address.ToEntity();
                address.CustomAttributes = customAttributes;
                address.CreatedOnUtc = DateTime.UtcNow;
                //some validation
                if (address.CountryId == 0)
                    address.CountryId = null;
                if (address.StateProvinceId == 0)
                    address.StateProvinceId = null;


                _addressService.InsertAddress(address);
                _customerService.InsertCustomerAddress(CurrentCustomer, address);
                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesMapping, address.Id);

                return RedirectToRoute("CustomerAddresses");
            }

            //If we got this far, something failed, redisplay form
            var pluginConfigSettings = _settingService.LoadSetting<PluginConfigurationSettings>();
            model.PluginConfigurationSettings = pluginConfigSettings;
            _addressModelFactory.PrepareAddressModel(model.Address,
                address: null,
                excludeProperties: true,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(WorkingLanguage.Id),
                overrideAttributesXml: customAttributes);

            return View(model);
        }

        /// <summary>
        /// Gets and sets the required models to edit an address with its geo coordinates.
        /// </summary>
        /// <param name="addressId">The id of the address to edit.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpsRequirement]
        public virtual IActionResult AddressEdit(int addressId)
        {
            if (!_customerService.IsRegistered(CurrentCustomer))
                return Challenge();

            var customer = CurrentCustomer;
            //find address (ensure that it belongs to the current customer)
            var address = _customerService.GetCustomerAddress(customer.Id, addressId);
            if (address == null)
                //address is not found
                return RedirectToRoute("CustomerAddresses");

            var model = new AddressGeoCoordinatesEditModel {
                PluginConfigurationSettings = _settingService.LoadSetting<PluginConfigurationSettings>(),
                AddressGeoCoordinatesMapping = _addressGeoCoordinatesService.GetAddressGeoCoordinates(addressId),
                RenderGoogleMapsJavaScript = true,
                MapDivId = "map",
                AutocompleteInputId = "autocomplete",
                GeoCoordinatesSearchInputId = "geocoordinatesSearch"
            };

            if (model.AddressGeoCoordinatesMapping is null)
            {
                model.AddressGeoCoordinatesMapping = new AddressGeoCoordinatesMapping
                {
                    AddressId = addressId,
                    Latitude = 0.0000000m,
                    Longitude = 0.0000000m
                };
            }

            _addressModelFactory.PrepareAddressModel(model.Address,
                address: address,
                excludeProperties: false,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(WorkingLanguage.Id));

            return View(model);
        }

        /// <summary>
        /// Updates an address.
        /// </summary>
        /// <param name="model">An instance of <see cref="AddressGeoCoordinatesEditModel"/>.</param>
        /// <param name="addressId">The id of the address to update.</param>
        /// <param name="form">An instance of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public virtual IActionResult AddressEdit(AddressGeoCoordinatesEditModel model, int addressId, IFormCollection form)
        {
            if (!_customerService.IsRegistered(CurrentCustomer))
                return Challenge();

            var customer = CurrentCustomer;
            //find address (ensure that it belongs to the current customer)
            var address = _customerService.GetCustomerAddress(customer.Id, addressId);
            if (address == null)
                //address is not found
                return RedirectToRoute("CustomerAddresses");

            //custom address attributes
            var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                address = model.Address.ToEntity();
                address.CustomAttributes = customAttributes;
                _addressService.UpdateAddress(address);
                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesMapping, addressId);

                return RedirectToRoute("CustomerAddresses");
            }

            //If we got this far, something failed, redisplay form
            var pluginConfigSettings = _settingService.LoadSetting<PluginConfigurationSettings>();
            model.PluginConfigurationSettings = pluginConfigSettings;
            _addressModelFactory.PrepareAddressModel(model.Address,
                address: address,
                excludeProperties: true,
                addressSettings: _addressSettings,
                loadCountries: () => _countryService.GetAllCountries(WorkingLanguage.Id),
                overrideAttributesXml: customAttributes);

            return View(model);
        }

        #endregion

        #region Multistep Checkout

        /// <summary>
        /// Gets and sets the required model for multistep checkout billing address including address geo coordinates.
        /// </summary>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual IActionResult BillingAddress(IFormCollection form)
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("CheckoutOnePage");

            if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            //model
            var model = _checkoutModelCustomFactory.PrepareCheckoutBillingGeoCoordinatesAddressModel(cart);

            //check whether "billing address" step is enabled
            if (_orderSettings.DisableBillingAddressCheckoutStep && model.CheckoutBillingAddressModel.ExistingAddresses.Any())
            {
                if (model.CheckoutBillingAddressModel.ExistingAddresses.Any())
                {
                    //choose the first one
                    return RedirectToAction("SelectBillingAddress", "Checkout", new { addressId = model.CheckoutBillingAddressModel.ExistingAddresses.First().Id });
                }

                TryValidateModel(model.CheckoutBillingAddressModel);
                TryValidateModel(model.AddressGeoCoordinatesEditModel.Address);
                return NewBillingAddress(model, form);
            }

            return View(model);
        }

        /// <summary>
        /// Inserts a new address with its geo coordinates and sets it as the billing address.
        /// </summary>
        /// <param name="model">An instance of <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ActionName("BillingAddress")]
        [FormValueRequired("nextstep")]
        public virtual IActionResult NewBillingAddress(CheckoutBillingGeoCoordinatesAddressModel model, IFormCollection form)
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("CheckoutOnePage");

            if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            //custom address attributes
            var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            var newAddress = model.AddressGeoCoordinatesEditModel.Address;

            if (ModelState.IsValid)
            {
                //try to find an address with the same values (don't duplicate records)
                var address = _addressService.FindAddress(_customerService.GetAddressesByCustomerId(CurrentCustomer.Id).ToList(),
                    newAddress.FirstName, newAddress.LastName, newAddress.PhoneNumber,
                    newAddress.Email, newAddress.FaxNumber, newAddress.Company,
                    newAddress.Address1, newAddress.Address2, newAddress.City,
                    newAddress.County, newAddress.StateProvinceId, newAddress.ZipPostalCode,
                    newAddress.CountryId, customAttributes);

                if (address == null)
                {
                    //address is not found. let's create a new one
                    address = newAddress.ToEntity();
                    address.CustomAttributes = customAttributes;
                    address.CreatedOnUtc = DateTime.UtcNow;

                    //some validation
                    if (address.CountryId == 0)
                        address.CountryId = null;
                    if (address.StateProvinceId == 0)
                        address.StateProvinceId = null;

                    _addressService.InsertAddress(address);
                    _customerService.InsertCustomerAddress(CurrentCustomer, address);
                }

                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping, address.Id);

                CurrentCustomer.BillingAddressId = address.Id;

                _customerService.UpdateCustomer(CurrentCustomer);

                //ship to the same address?
                if (_shippingSettings.ShipToSameAddress && model.CheckoutBillingAddressModel.ShipToSameAddress && _shoppingCartService.ShoppingCartRequiresShipping(cart))
                {
                    CurrentCustomer.ShippingAddressId = CurrentCustomer.BillingAddressId;
                    _customerService.UpdateCustomer(CurrentCustomer);

                    //reset selected shipping method (in case if "pick up in store" was selected)
                    _genericAttributeService.SaveAttribute<ShippingOption>(CurrentCustomer, NopCustomerDefaults.SelectedShippingOptionAttribute, null, CurrentStore.Id);
                    _genericAttributeService.SaveAttribute<PickupPoint>(CurrentCustomer, NopCustomerDefaults.SelectedPickupPointAttribute, null, CurrentStore.Id);

                    //limitation - "Ship to the same address" doesn't properly work in "pick up in store only" case (when no shipping plugins are available) 
                    return RedirectToRoute("CheckoutShippingMethod");
                }

                return RedirectToRoute("CheckoutShippingAddress");
            }

            //If we got this far, something failed, redisplay form
            model = _checkoutModelCustomFactory.PrepareCheckoutBillingGeoCoordinatesAddressModel(cart, addressId: newAddress.CountryId,
                attributesXml: customAttributes,
                latitude: model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping.Latitude,
                longitude: model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping.Longitude);

            return View(model);
        }

        /// <summary>
        /// Gets and sets the required model for multistep checkout shipping address including address geo coordinates.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public virtual IActionResult ShippingAddress()
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("CheckoutOnePage");

            if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            if (!_shoppingCartService.ShoppingCartRequiresShipping(cart))
                return RedirectToRoute("CheckoutShippingMethod");

            //model
            var model = _checkoutModelCustomFactory.PrepareCheckoutShippingGeoCoordinatesAddressModel(cart, true);

            return View(model);
        }

        /// <summary>
        /// Inserts a new address with its geo coordinates and sets it as the shipping address.
        /// </summary>
        /// <param name="model">An instance of <see cref="CheckoutShippingGeoCoordinatesAddressModel"/>.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ActionName("ShippingAddress")]
        [FormValueRequired("nextstep")]
        public virtual IActionResult NewShippingAddress(CheckoutShippingGeoCoordinatesAddressModel model, IFormCollection form)
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("CheckoutOnePage");

            if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            if (!_shoppingCartService.ShoppingCartRequiresShipping(cart))
                return RedirectToRoute("CheckoutShippingMethod");

            //pickup point
            if (_shippingSettings.AllowPickupInStore && !_orderSettings.DisplayPickupInStoreOnShippingMethodPage)
            {
                var pickupInStore = ParsePickupInStore(form);
                if (pickupInStore)
                {
                    var pickupOption = ParsePickupOption(cart, form);
                    SavePickupOption(pickupOption);

                    return RedirectToRoute("CheckoutPaymentMethod");
                }

                //set value indicating that "pick up in store" option has not been chosen
                _genericAttributeService.SaveAttribute<PickupPoint>(CurrentCustomer, NopCustomerDefaults.SelectedPickupPointAttribute, null, CurrentStore.Id);
            }

            //custom address attributes
            var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            var newAddress = model.AddressGeoCoordinatesEditModel.Address;

            if (ModelState.IsValid)
            {
                //try to find an address with the same values (don't duplicate records)
                var address = _addressService.FindAddress(_customerService.GetAddressesByCustomerId(CurrentCustomer.Id).ToList(),
                    newAddress.FirstName, newAddress.LastName, newAddress.PhoneNumber,
                    newAddress.Email, newAddress.FaxNumber, newAddress.Company,
                    newAddress.Address1, newAddress.Address2, newAddress.City,
                    newAddress.County, newAddress.StateProvinceId, newAddress.ZipPostalCode,
                    newAddress.CountryId, customAttributes);

                if (address == null)
                {
                    address = newAddress.ToEntity();
                    address.CustomAttributes = customAttributes;
                    address.CreatedOnUtc = DateTime.UtcNow;
                    //some validation
                    if (address.CountryId == 0)
                        address.CountryId = null;
                    if (address.StateProvinceId == 0)
                        address.StateProvinceId = null;

                    _addressService.InsertAddress(address);

                    _customerService.InsertCustomerAddress(CurrentCustomer, address);

                }

                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping, address.Id);

                CurrentCustomer.ShippingAddressId = address.Id;
                _customerService.UpdateCustomer(CurrentCustomer);

                return RedirectToRoute("CheckoutShippingMethod");
            }

            //If we got this far, something failed, redisplay form
            model = _checkoutModelCustomFactory.PrepareCheckoutShippingGeoCoordinatesAddressModel(cart, true, addressId: newAddress.CountryId,
                attributesXml: customAttributes,
                latitude: model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping.Latitude,
                longitude: model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping.Longitude);

            return View(model);
        }

        #endregion

        #region One Page Checkout

        /// <summary>
        /// Gets and sets the required models for one page checkout.
        /// </summary>
        /// <returns>An implementation of<see cref="IActionResult"/>.</returns>
        public virtual IActionResult OnePageCheckout()
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (!_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("Checkout");

            if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            var model = _checkoutModelCustomFactory.PrepareOnePageCheckoutModel(cart);

            return View(model);
        }

        /// <summary>
        /// Manages billing address section of one page checkout.
        /// </summary>
        /// <param name="model">An instance of <see cref="CheckoutBillingGeoCoordinatesAddressModel"/>.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of<see cref="IActionResult"/>.</returns>
        [IgnoreAntiforgeryToken]
        public virtual IActionResult OpcSaveBilling(CheckoutBillingGeoCoordinatesAddressModel model, IFormCollection form)
        {
            try
            {
                //validation
                if (_orderSettings.CheckoutDisabled)
                    throw new Exception(_localizationService.GetResource("Checkout.Disabled"));

                var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

                if (!cart.Any())
                    throw new Exception("Your cart is empty");

                if (!_orderSettings.OnePageCheckoutEnabled)
                    throw new Exception("One page checkout is disabled");

                if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                    throw new Exception("Anonymous checkout is not allowed");

                int.TryParse(form["billing_address_id"], out var billingAddressId);

                if (billingAddressId > 0)
                {
                    //existing address
                    var address = _customerService.GetCustomerAddress(CurrentCustomer.Id, billingAddressId)
                        ?? throw new Exception(_localizationService.GetResource("Checkout.Address.NotFound"));

                    CurrentCustomer.BillingAddressId = address.Id;
                    _customerService.UpdateCustomer(CurrentCustomer);
                }
                else
                {
                    //new address
                    var newAddress = model.AddressGeoCoordinatesEditModel.Address;

                    //custom address attributes
                    var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
                    var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
                    foreach (var error in customAttributeWarnings)
                    {
                        ModelState.AddModelError("", error);
                    }

                    //validate model
                    if (!ModelState.IsValid)
                    {
                        //model is not valid. redisplay the form with errors
                        var checkoutBillingGeoCoordinatesAddressModel = _checkoutModelCustomFactory.PrepareCheckoutBillingGeoCoordinatesAddressModel(cart,
                            countryId: newAddress.CountryId, attributesXml: customAttributes);
                        checkoutBillingGeoCoordinatesAddressModel.CheckoutBillingAddressModel.NewAddressPreselected = true;
                        return Json(new
                        {
                            update_section = new UpdateSectionJsonModel
                            {
                                name = "billing",
                                html = RenderPartialViewToString("OpcBillingAddress", checkoutBillingGeoCoordinatesAddressModel)
                            },
                            wrong_billing_address = true,
                        });
                    }

                    //try to find an address with the same values (don't duplicate records)
                    var address = _addressService.FindAddress(_customerService.GetAddressesByCustomerId(CurrentCustomer.Id).ToList(),
                        newAddress.FirstName, newAddress.LastName, newAddress.PhoneNumber,
                        newAddress.Email, newAddress.FaxNumber, newAddress.Company,
                        newAddress.Address1, newAddress.Address2, newAddress.City,
                        newAddress.County, newAddress.StateProvinceId, newAddress.ZipPostalCode,
                        newAddress.CountryId, customAttributes);

                    if (address == null)
                    {
                        //address is not found. let's create a new one
                        address = newAddress.ToEntity();
                        address.CustomAttributes = customAttributes;
                        address.CreatedOnUtc = DateTime.UtcNow;

                        //some validation
                        if (address.CountryId == 0)
                            address.CountryId = null;

                        if (address.StateProvinceId == 0)
                            address.StateProvinceId = null;

                        _addressService.InsertAddress(address);

                        _customerService.InsertCustomerAddress(CurrentCustomer, address);
                    }

                    _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping, address.Id);

                    CurrentCustomer.BillingAddressId = address.Id;

                    _customerService.UpdateCustomer(CurrentCustomer);
                }

                if (_shoppingCartService.ShoppingCartRequiresShipping(cart))
                {
                    //shipping is required
                    var address = _customerService.GetCustomerBillingAddress(CurrentCustomer);

                    //by default Shipping is available if the country is not specified
                    var shippingAllowed = _addressSettings.CountryEnabled ? _countryService.GetCountryByAddress(address)?.AllowsShipping ?? false : true;
                    if (_shippingSettings.ShipToSameAddress && model.CheckoutBillingAddressModel.ShipToSameAddress && shippingAllowed)
                    {
                        //ship to the same address
                        CurrentCustomer.ShippingAddressId = address.Id;
                        _customerService.UpdateCustomer(CurrentCustomer);
                        //reset selected shipping method (in case if "pick up in store" was selected)
                        _genericAttributeService.SaveAttribute<ShippingOption>(CurrentCustomer, NopCustomerDefaults.SelectedShippingOptionAttribute, null, CurrentStore.Id);
                        _genericAttributeService.SaveAttribute<PickupPoint>(CurrentCustomer, NopCustomerDefaults.SelectedPickupPointAttribute, null, CurrentStore.Id);
                        //limitation - "Ship to the same address" doesn't properly work in "pick up in store only" case (when no shipping plugins are available) 
                        return OpcLoadStepAfterShippingAddress(cart);
                    }

                    //do not ship to the same address
                    var checkoutShippingGeoCoordinatesAddressModel = _checkoutModelCustomFactory.PrepareCheckoutShippingGeoCoordinatesAddressModel(cart, false);

                    return Json(new
                    {
                        update_section = new UpdateSectionJsonModel
                        {
                            name = "shipping",
                            html = RenderPartialViewToString("OpcShippingAddress", checkoutShippingGeoCoordinatesAddressModel)
                        },
                        goto_section = "shipping"
                    });
                }

                //shipping is not required
                _genericAttributeService.SaveAttribute<ShippingOption>(CurrentCustomer, NopCustomerDefaults.SelectedShippingOptionAttribute, null, CurrentStore.Id);

                //load next step
                return OpcLoadStepAfterShippingMethod(cart);
            }
            catch (Exception exc)
            {
                _logger.Warning(exc.Message, exc, CurrentCustomer);
                return Json(new { error = 1, message = exc.Message });
            }
        }

        /// <summary>
        /// Manages shipping address section of one page checkout.
        /// </summary>
        /// <param name="model">An instance of <see cref="CheckoutShippingGeoCoordinatesAddressModel"/>.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of<see cref="IActionResult"/>.</returns>
        [IgnoreAntiforgeryToken]
        public virtual IActionResult OpcSaveShipping(CheckoutShippingGeoCoordinatesAddressModel model, IFormCollection form)
        {
            try
            {
                //validation
                if (_orderSettings.CheckoutDisabled)
                    throw new Exception(_localizationService.GetResource("Checkout.Disabled"));

                var cart = _shoppingCartService.GetShoppingCart(CurrentCustomer, ShoppingCartType.ShoppingCart, CurrentStore.Id);

                if (!cart.Any())
                    throw new Exception("Your cart is empty");

                if (!_orderSettings.OnePageCheckoutEnabled)
                    throw new Exception("One page checkout is disabled");

                if (_customerService.IsGuest(CurrentCustomer) && !_orderSettings.AnonymousCheckoutAllowed)
                    throw new Exception("Anonymous checkout is not allowed");

                if (!_shoppingCartService.ShoppingCartRequiresShipping(cart))
                    throw new Exception("Shipping is not required");

                //pickup point
                if (_shippingSettings.AllowPickupInStore && !_orderSettings.DisplayPickupInStoreOnShippingMethodPage)
                {
                    var pickupInStore = ParsePickupInStore(form);
                    if (pickupInStore)
                    {
                        var pickupOption = ParsePickupOption(cart, form);
                        SavePickupOption(pickupOption);

                        return OpcLoadStepAfterShippingMethod(cart);
                    }

                    //set value indicating that "pick up in store" option has not been chosen
                    _genericAttributeService.SaveAttribute<PickupPoint>(CurrentCustomer, NopCustomerDefaults.SelectedPickupPointAttribute, null, CurrentStore.Id);
                }

                int.TryParse(form["shipping_address_id"], out var shippingAddressId);

                if (shippingAddressId > 0)
                {
                    //existing address
                    var address = _customerService.GetCustomerAddress(CurrentCustomer.Id, shippingAddressId)
                        ?? throw new Exception(_localizationService.GetResource("Checkout.Address.NotFound"));

                    CurrentCustomer.ShippingAddressId = address.Id;
                    _customerService.UpdateCustomer(CurrentCustomer);
                }
                else
                {
                    //new address
                    var newAddress = model.AddressGeoCoordinatesEditModel.Address;

                    //custom address attributes
                    var customAttributes = _addressAttributeParser.ParseCustomAddressAttributes(form);
                    var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
                    foreach (var error in customAttributeWarnings)
                    {
                        ModelState.AddModelError("", error);
                    }

                    //validate model
                    if (!ModelState.IsValid)
                    {
                        //model is not valid. redisplay the form with errors
                        var checkoutShippingGeoCoordinatesAddressModel = _checkoutModelCustomFactory.PrepareCheckoutShippingGeoCoordinatesAddressModel(cart,
                            false, countryId: newAddress.CountryId, attributesXml: customAttributes);
                        checkoutShippingGeoCoordinatesAddressModel.CheckoutShippingAddressModel.NewAddressPreselected = true;
                        return Json(new
                        {
                            update_section = new UpdateSectionJsonModel
                            {
                                name = "shipping",
                                html = RenderPartialViewToString("OpcShippingAddress", checkoutShippingGeoCoordinatesAddressModel)
                            }
                        });
                    }

                    //try to find an address with the same values (don't duplicate records)
                    var address = _addressService.FindAddress(_customerService.GetAddressesByCustomerId(CurrentCustomer.Id).ToList(),
                        newAddress.FirstName, newAddress.LastName, newAddress.PhoneNumber,
                        newAddress.Email, newAddress.FaxNumber, newAddress.Company,
                        newAddress.Address1, newAddress.Address2, newAddress.City,
                        newAddress.County, newAddress.StateProvinceId, newAddress.ZipPostalCode,
                        newAddress.CountryId, customAttributes);

                    if (address == null)
                    {
                        address = newAddress.ToEntity();
                        address.CustomAttributes = customAttributes;
                        address.CreatedOnUtc = DateTime.UtcNow;

                        _addressService.InsertAddress(address);

                        _customerService.InsertCustomerAddress(CurrentCustomer, address);
                    }

                    _addressGeoCoordinatesService.InsertAddressGeoCoordinates(model.AddressGeoCoordinatesEditModel.AddressGeoCoordinatesMapping, address.Id);

                    CurrentCustomer.ShippingAddressId = address.Id;

                    _customerService.UpdateCustomer(CurrentCustomer);
                }

                return OpcLoadStepAfterShippingAddress(cart);
            }
            catch (Exception exc)
            {
                _logger.Warning(exc.Message, exc, CurrentCustomer);
                return Json(new { error = 1, message = exc.Message });
            }
        }

        #endregion

        #endregion
    }
}
