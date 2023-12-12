using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services;
using Nop.Services.Logging;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Controllers
{
    /// <summary>
    /// Represents a controller for Multientrega services interaction.
    /// </summary>
    [AutoValidateAntiforgeryToken]
    public sealed class MultientregaController : BasePluginController
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IMultientregaAddressService _multientregaAddressService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="MultientregaController"/>.
        /// </summary>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="multientregaAddressService">An implementation of <see cref="IMultientregaAddressService"/>.</param>
        public MultientregaController(
            ILogger logger,
            IMultientregaAddressService multientregaAddressService)
        {
            _logger = logger;
            _multientregaAddressService = multientregaAddressService;
        }

        #endregion

        #region Methods

        //available even when navigation is not allowed
        [CheckAccessPublicStore(true)]
        public IActionResult GetProvincesByCountryId(string countryId)
        {
            try
            {
                var model = _multientregaAddressService.GetProvincesSelectListItems(countryId);
                return Json(new { Success = true, Data = model });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return Json(new { Success = false, e.Message });
            }
        }

        //available even when navigation is not allowed
        [CheckAccessPublicStore(true)]
        public IActionResult GetDistrictsByProvinceId(string provinceId)
        {
            try
            {
                var model = _multientregaAddressService.GetDistrictsSelectListItems(provinceId);
                return Json(new { Success = true, Data = model });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return Json(new { Success = false, e.Message});
            }
        }

        //available even when navigation is not allowed
        [CheckAccessPublicStore(true)]
        public IActionResult GetTownshipsByDistrictId(string districtId)
        {
            try
            {
                var model = _multientregaAddressService.GetTownshipsSelectListItems(districtId);
                return Json(new { Success = true, Data = model });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return Json(new { Success = false, e.Message });
            }
        }

        //available even when navigation is not allowed
        [CheckAccessPublicStore(true)]
        public IActionResult GetNeighborhoodsByTownshipId(string townshipId)
        {
            try
            {
                var model = _multientregaAddressService.GetNeigborhoodsSelectListItems(townshipId);
                return Json(new { Success = true, Data = model });
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return Json(new { Success = false, e.Message });
            }
        }

        [HttpPost]
        public IActionResult ProvinceList(MultientregaSearchModel searchModel)
        {
            var model = _multientregaAddressService.PrepareProvinceListModel(searchModel);
            return Json(model);
        }

        [HttpPost]
        public IActionResult DistrictList(MultientregaSearchModel searchModel)
        {
            var model = _multientregaAddressService.PrepareDistrictListModel(searchModel);
            return Json(model);
        }

        [HttpPost]
        public IActionResult TownshipList(MultientregaSearchModel searchModel)
        {
            var model = _multientregaAddressService.PrepareTownshipListModel(searchModel);
            return Json(model);
        }

        [HttpPost]
        public IActionResult NeighborhoodList(MultientregaSearchModel searchModel)
        {
            var model = _multientregaAddressService.PrepareNeighborhoodListModel(searchModel);
            return Json(model);
        }

        #endregion
    }
}
