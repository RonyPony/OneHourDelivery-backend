using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Plugin.Widgets.WarehouseSchedule.Factories;
using Nop.Plugin.Widgets.WarehouseSchedule.Services;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Controllers
{
    /// <summary>
    /// Represents the main controller for Warehouse Schedule Plugin.
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.ADMIN)]
    public class WarehouseScheduleController : BaseAdminController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWarehouseScheduleMappingFactory _warehouseScheduleMappingFactory;
        private readonly IWarehouseScheduleService _warehouseScheduleService;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="WarehouseScheduleController" />.
        /// </summary>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService" /></param>
        /// <param name="warehouseScheduleMappingFactory">An implementation of <see cref="IWarehouseScheduleMappingFactory" /></param>
        /// <param name="warehouseScheduleService">An implementation of <see cref="IWarehouseScheduleService" /></param>
        /// <param name="logger">An implementation of <see cref="ILogger" /></param>
        public WarehouseScheduleController(
            ILocalizationService localizationService,
            IWarehouseScheduleMappingFactory warehouseScheduleMappingFactory,
            IWarehouseScheduleService warehouseScheduleService,
            ILogger logger)
        {
            _localizationService = localizationService;
            _warehouseScheduleMappingFactory = warehouseScheduleMappingFactory;
            _warehouseScheduleService = warehouseScheduleService;
            _logger = logger;
        }

        #endregion

        #region Utilities

        private bool ValidateBeginHourIsEarlierThanEndHour(WarehouseScheduleMapping mapping)
            => mapping.IsActive ? TimeSpan.Compare(mapping.BeginTime, mapping.EndTime) == 1 : false;

        #endregion

        #region Methods

        /// <summary>
        /// Updates a scheduled day for a warehouse. In case the scheduled day isn't registered, then proceeds to register it.
        /// </summary>
        /// <param name="warehouseScheduleMapping">The warehouses scheduled day.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateWarehouseScheduledDay(WarehouseScheduleMapping warehouseScheduleMapping)
        {
            try
            {
                if (ValidateBeginHourIsEarlierThanEndHour(warehouseScheduleMapping)) throw new Exception(await _localizationService.GetResourceAsync($"{WarehouseScheduleDefaults.LocaleResourcesPrefix}.Error.BeginEndHourValidation"));
                await _warehouseScheduleService.CreateOrUpdateWarehouseScheduledDayAsync(warehouseScheduleMapping);

                return Json(new
                {
                    Success = true,
                    Message = await _localizationService.GetResourceAsync("Admin.Configuration.Shipping.Warehouses.Updated")
                });
            }
            catch (Exception e)
            {
                await _logger.ErrorAsync($"An error occurred while updating warehouse schedule. {e.Message}", e);
                return Json(new { Success = false, e.Message });
            }
        }

        /// <summary>
        /// Retrieves the schedule of a warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse id.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        public async Task<IActionResult> GetWarehouseScheduleByWarehouseId([FromQuery]int warehouseId)
        {
            IList<WarehouseScheduleMapping> model = await _warehouseScheduleMappingFactory.PrepareWarehouseScheduleMappingsAsync(warehouseId);

            return View($"/{WarehouseScheduleDefaults.OutputDir}/Views/WarehouseSchedule.cshtml", model);
        }

        #endregion
    }
}
