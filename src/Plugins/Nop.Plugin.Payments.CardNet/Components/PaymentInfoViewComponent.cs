using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.CardNet.Extensions;
using Nop.Plugin.Payments.CardNet.Helpers;
using Nop.Plugin.Payments.CardNet.Models;
using Nop.Services.Directory;
using Nop.Services.Orders;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.CardNet.Components
{
    /// <summary>
    /// View component to display payment info in public store
    /// </summary>
    [ViewComponent(Name = CardNetDefaults.PaymentInfoViewComponentName)]
    public class PaymentInfoViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ICurrencyService _currencyService;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;

        #endregion

        #region Ctor

        public PaymentInfoViewComponent(IShoppingCartService shoppingCartService, IWorkContext workContext, IStoreContext storeContext,
            ICurrencyService currencyService, IOrderTotalCalculationService orderTotalCalculationService)
        {
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
            _storeContext = storeContext;
            _currencyService = currencyService;
            _orderTotalCalculationService = orderTotalCalculationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes view component
        /// </summary>
        /// <returns>View component result</returns>
        public IViewComponentResult Invoke()
        {
            IList<ShoppingCartItem> cart = _shoppingCartService.GetShoppingCart(_workContext.CurrentCustomer, ShoppingCartType.ShoppingCart, _storeContext.CurrentStore.Id);

            var total = _orderTotalCalculationService.GetShoppingCartTotal(cart).GetValueOrDefault()
                .ToDefaultCardNetCurrency(_currencyService);

            var model = new CardNetPaymentInfoModel
            {
                OrderTotal = $"{total:0.00}"
            };

            return View("~/Plugins/Payments.CardNet/Views/PaymentInfo.cshtml", model);
        }

        #endregion
    }
}