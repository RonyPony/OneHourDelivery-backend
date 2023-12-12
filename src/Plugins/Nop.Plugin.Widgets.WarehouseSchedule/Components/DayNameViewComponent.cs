using Microsoft.AspNetCore.Mvc;
using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Web.Framework.Components;
using System.Linq;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Components
{
    /// <summary>
    /// View component for _DayName.cshtml partial view.
    /// </summary>
    [ViewComponent(Name = "DayName")]
    public sealed class DayNameViewComponent : NopViewComponent
    {
        #region Fields

        private readonly IRepository<Day> _dayRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DayNameViewComponent" />.
        /// </summary>
        /// <param name="dayRepository">An implementation of <see cref="IRepository{T}" /> where T is <see cref="typeof(Day)"/>.</param>
        public DayNameViewComponent(IRepository<Day> dayRepository)
        {
            _dayRepository = dayRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares and renders the _DayName.cshtml partial view.
        /// </summary>
        /// <param name="dayId">Day Id</param>
        /// <returns>An implementation of <see cref="IViewComponentResult"/>.</returns>
        public IViewComponentResult Invoke(int dayId)
        {
            Day day = _dayRepository.Table.FirstOrDefault(day => day.Id == dayId);
            if (day != null) return View($"/{WarehouseScheduleDefaults.OutputDir}/Views/_DayName.cshtml", day);

            return Content(string.Empty);
        }

        #endregion
    }
}
