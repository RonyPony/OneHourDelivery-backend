using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Plugin.Widgets.ProductAvailability.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Shipping;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Controllers
{
    /// <summary>
    /// Represents a controller for product availability verification on checkout.
    /// </summary>
    [HttpsRequirement]
    [AutoValidateAntiforgeryToken]
    public sealed class AvailabilityCheckoutController : CheckoutController
    {
        #region Fields

        private readonly INotificationService _notificationService;
        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AvailabilityCheckoutController"/>.
        /// </summary>
        /// <param name="addressSettings">An instance of <see cref="AddressSettings"/>.</param>
        /// <param name="customerSettings">An instance of <see cref="CustomerSettings"/>.</param>
        /// <param name="addressAttributeParser">An implementation of <see cref="IAddressAttributeParser"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="checkoutModelFactory">An implementation of <see cref="ICheckoutModelFactory"/>.</param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="paymentPluginManager">An implementation of <see cref="IPaymentPluginManager"/>.</param>
        /// <param name="paymentService">An implementation of <see cref="IPaymentService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="productAvailabilityService">An implementation of <see cref="IProductAvailabilityService"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="shoppingCartService">An implementation of <see cref="IShoppingCartService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="orderSettings">An instance of <see cref="OrderSettings"/>.</param>
        /// <param name="paymentSettings">An instance of <see cref="PaymentSettings"/>.</param>
        /// <param name="rewardPointsSettings">An instance of <see cref="RewardPointsSettings"/>.</param>
        /// <param name="shippingSettings">An instance of <see cref="ShippingSettings"/>.</param>
        public AvailabilityCheckoutController(
            AddressSettings addressSettings,
            CustomerSettings customerSettings,
            IAddressAttributeParser addressAttributeParser,
            IAddressService addressService,
            ICheckoutModelFactory checkoutModelFactory,
            ICountryService countryService,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            ILogger logger,
            IOrderProcessingService orderProcessingService,
            IOrderService orderService,
            IPaymentPluginManager paymentPluginManager,
            IPaymentService paymentService,
            IProductService productService,
            IProductAvailabilityService productAvailabilityService,
            IShippingService shippingService,
            IShoppingCartService shoppingCartService,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext,
            OrderSettings orderSettings,
            PaymentSettings paymentSettings,
            RewardPointsSettings rewardPointsSettings,
            ShippingSettings shippingSettings) : base(addressSettings, customerSettings, addressAttributeParser, addressService, checkoutModelFactory, countryService, customerService, genericAttributeService, localizationService, logger, orderProcessingService, orderService, paymentPluginManager, paymentService, productService, shippingService, shoppingCartService, storeContext, webHelper, workContext, orderSettings, paymentSettings, rewardPointsSettings, shippingSettings)

        {
            _notificationService = notificationService;
            _productAvailabilityService = productAvailabilityService;
            _shoppingCartService = shoppingCartService;
            _storeContext = storeContext;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        private CheckoutAvailabilityVerificationResult CheckAvailability(IFormCollection form)
        {
            var result = new CheckoutAvailabilityVerificationResult { Success = true, Errors = new List<string>() };
            string cellarNumber = string.Empty;

            if (base.ParsePickupInStore(form))
                cellarNumber = _productAvailabilityService.GetCellarNumberByPickupPointId(base.ParsePickupOption(form).Id);
            else
                cellarNumber = _productAvailabilityService.GetConfiguredWarehouseCellarNumber();

            IList<ShoppingCartItem> cartItems = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, ShoppingCartType.ShoppingCart, _storeContext.CurrentStore.Id);
            Task<CheckoutAvailabilityVerificationResult> taskResult = _productAvailabilityService.CheckShoppingCartItemsAvailabilityOnSelectedPickupPointBeforeContinuingCheckout(cellarNumber, cartItems);
            taskResult.Wait();

            if (!taskResult.Result.Success)
            {
                foreach (string error in taskResult.Result.Errors)
                {
                    _notificationService.ErrorNotification(error);
                }
            }

            result = taskResult.Result;

            return result;
        }

        #endregion

        #region Methods

        #region Multistep checkout

        /// <summary>
        /// Makes the product availability verification on multistep checkout when shipping method is selected.
        /// </summary>
        /// <param name="shippingoption">The selected shipping option.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost, ActionName("ShippingMethod")]
        [FormValueRequired("nextstep")]
        public override IActionResult SelectShippingMethod(string shippingoption, IFormCollection form)
        {
            try
            {
                CheckoutAvailabilityVerificationResult availabilityCheckResult = CheckAvailability(form);
                if (!availabilityCheckResult.Success) return base.ShippingMethod();

                return base.SelectShippingMethod(shippingoption, form);
            }
            catch (Exception e)
            {
                _notificationService.ErrorNotification(e.Message);
                return base.ShippingMethod();
            }
        }

        #endregion

        #region One Page Checkout

        /// <summary>
        /// Gets and sets the required models for one page checkout.
        /// </summary>
        /// <returns>An implementation of<see cref="IActionResult"/>.</returns>
        public override IActionResult OnePageCheckout()
        {
            return base.OnePageCheckout();
        }

        /// <summary>
        /// Makes the product availability verification on one page checkout when shipping method is selected.
        /// </summary>
        /// <param name="shippingoption">The selected shipping option.</param>
        /// <param name="form">An implementation of <see cref="IFormCollection"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [IgnoreAntiforgeryToken]
        public override IActionResult OpcSaveShippingMethod(string shippingoption, IFormCollection form)
        {
            try
            {
                CheckoutAvailabilityVerificationResult availabilityCheckResult = CheckAvailability(form);
                if (!availabilityCheckResult.Success) return Json(new { error = 1, message = availabilityCheckResult.Errors });

                return base.OpcSaveShippingMethod(shippingoption, form);
            }
            catch (Exception e)
            {
                return Json(new { error = 1, message = e.Message });
            }
        }

        #endregion

        #endregion
    }
}
