using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Plugin.Widgets.WarehouseSchedule.Factories;
using Nop.Web.Areas.Admin.Models.Shipping;
using Nop.Web.Framework.Components;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Components
{
    /// <summary>
    /// Represents a view component for warehouses schedule.
    /// </summary>
    [ViewComponent(Name = "WarehouseSchedule")]
    public sealed class WarehouseScheduleViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IWarehouseScheduleMappingFactory _warehouseScheduleMappingFactory;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="WarehouseScheduleViewComponent"/>.
        /// </summary>
        /// <param name="warehouseScheduleMappingFactory">An implementation of <see cref="IWarehouseScheduleMappingFactory"/>.</param>
        public WarehouseScheduleViewComponent(
            IWarehouseScheduleMappingFactory warehouseScheduleMappingFactory)
        {
            _warehouseScheduleMappingFactory = warehouseScheduleMappingFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares and renders the views for warehouse schedule.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone invoking the view component.</param>
        /// <param name="additionalData">Data send through the widget zone.</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            if (widgetZone.Equals(AdminWidgetZones.WarehouseDetailsBottom) && additionalData is WarehouseModel warehouseModel)
            {
                IList<WarehouseScheduleMapping> model = await _warehouseScheduleMappingFactory.PrepareWarehouseScheduleMappingsAsync(warehouseModel.Id);
                return View($"/{WarehouseScheduleDefaults.OutputDir}/Views/WarehouseSchedule.cshtml", model);
            }

            return Content(string.Empty);
        }

        #endregion
    }
}
