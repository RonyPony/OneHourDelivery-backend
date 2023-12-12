using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widget.AutoAdvanceSearch.Controllers
{
    /// <summary>
    /// Represents a main controller for this plugin.
    /// </summary>
    public sealed class AutoAdvanceSearchController : BasePublicController
    {
        #region Fields

        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AutoAdvanceSearchController"/>.
        /// </summary>
        /// <param name="catalogModelFactory">An implementation of <see cref="ICatalogModelFactory"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public AutoAdvanceSearchController(
            ICatalogModelFactory catalogModelFactory,
            IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IWebHelper webHelper,
            IWorkContext workContext)
        {
            _catalogModelFactory = catalogModelFactory;
            _genericAttributeService = genericAttributeService;
            _storeContext = storeContext;
            _webHelper = webHelper;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        public IActionResult Search(SearchModel model, CatalogPagingFilteringModel command)
        {
            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                NopCustomerDefaults.LastContinueShoppingPageAttribute,
                _webHelper.GetThisPageUrl(true),
                _storeContext.CurrentStore.Id);

            if (model == null)
                model = new SearchModel();

            // Activate advance search with all options
            model.sid = true;
            model.adv = true;
            model.asv = true;
            model.isc = true;

            model = _catalogModelFactory.PrepareSearchModel(model, command);
            return View("~/Views/Catalog/Search.cshtml", model);
        }

        #endregion
    }
}
