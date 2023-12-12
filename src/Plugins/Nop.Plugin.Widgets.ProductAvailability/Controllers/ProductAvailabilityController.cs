using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Plugin.Widgets.ProductAvailability.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Controllers
{
    /// <summary>
    /// Represents the main controller for ProductAvailability Plugin.
    /// </summary>
    public sealed class ProductAvailabilityController : BasePluginController
    {
        #region Properties

        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ProductAvailabilityController"/>.
        /// </summary>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="productAvailabilityService">An implementation of <see cref="IProductAvailabilityService"/></param>
        /// <param name="warehouseRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Warehouse"/></param>
        /// <param name="productAttributeRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="ProductAttribute"/></param>
        public ProductAvailabilityController(
            ISettingService settingService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IProductAvailabilityService productAvailabilityService,
            IRepository<Warehouse> warehouseRepository,
            IRepository<ProductAttribute> productAttributeRepository)
        {
            _settingService = settingService;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _productAvailabilityService = productAvailabilityService;
            _warehouseRepository = warehouseRepository;
            _productAttributeRepository = productAttributeRepository;
        }

        #endregion

        #region Utilities

        private ConfigurationModel PrepareModel(ConfigurationModel model, ProductAvailabilitySettings productAvailabilitySettings)
        {
            if (_productAvailabilityService.PluginIsConfigured()) model = productAvailabilitySettings.ToModel<ConfigurationModel>();
            model.Warehouses = GetSelectListItemFromList(_warehouseRepository.Table.ToList());
            model.ProductAttributes = GetSelectListItemFromList(_productAttributeRepository.Table.ToList());

            if (string.IsNullOrWhiteSpace(model.OneSizeProductsIdentifierCode))
                model.OneSizeProductsIdentifierCode = ProductAvailabilityDefault.DefaultOneSizeProductsIdentifierCode;

            return model;
        }

        private List<SelectListItem> GetSelectListItemFromList(IList objects)
        {
            var selectListItem = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Config.SelectList.Default"), Value = "0" }
            };

            if (objects.Count > 0)
            {
                foreach (object obj in objects)
                {
                    if (obj is Warehouse warehouse)
                        selectListItem.Add(new SelectListItem { Text = warehouse.Name, Value = warehouse.Id.ToString() });
                    if (obj is ProductAttribute productAttribute)
                        selectListItem.Add(new SelectListItem { Text = productAttribute.Name, Value = productAttribute.Id.ToString() });
                }
            }

            return selectListItem;
        }

        #endregion

        #region Methods

        #region Configure

        /// <summary>
        /// Gets and prepares the required models and views for Plugin Configuration.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure()
        {
            var productAvailabilitySettings = _settingService.LoadSetting<ProductAvailabilitySettings>();
            var model = new ConfigurationModel();

            return View($"~/{ProductAvailabilityDefault.OutputDir}/Views/Configure.cshtml", PrepareModel(model, productAvailabilitySettings));
        }

        /// <summary>
        /// Validates and inserts to the Configuration the given values.
        /// </summary>
        /// <param name="model">An instance of <see cref="ConfigurationModel"/></param>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        [HttpPost]
        [AuthorizeAdmin]
        [Area(AreaNames.Admin)]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            var productAvailabilitySettings = model.ToEntity<ProductAvailabilitySettings>();
            _settingService.SaveSetting(productAvailabilitySettings);
            _settingService.ClearCache();
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion

        #region Search Inventory

        /// <summary>
        /// Gets and prepares the required model and view for inventory search.
        /// </summary>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        public IActionResult SearchInventory()
            => View($"~/{ProductAvailabilityDefault.OutputDir}/Views/SearchInventory.cshtml", new SearchInventoryModel());

        /// <summary>
        /// Retrieves the result of the inventory search request.
        /// </summary>
        /// <param name="model">An instance of <see cref="SearchInventoryModel"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult" />.</returns>
        [HttpPost]
        public IActionResult SearchInventory(SearchInventoryModel model)
        {
            Task<InventoryRequestResponseModel> requestResponseModelTask = _productAvailabilityService.GetProductInventoryBySku(model.ProductSku);
            requestResponseModelTask.Wait();

            model.RequestResponseModel = requestResponseModelTask.Result;

            return View($"~/{ProductAvailabilityDefault.OutputDir}/Views/SearchInventory.cshtml", model);
        }

        #endregion

        #endregion
    }
}
