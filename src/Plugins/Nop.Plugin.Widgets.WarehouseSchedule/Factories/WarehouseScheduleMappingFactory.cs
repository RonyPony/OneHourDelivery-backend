using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Factories
{
    /// <summary>
    /// Represents a model factory for <see cref="WarehouseScheduleMappings" />.
    /// </summary>
    public sealed class WarehouseScheduleMappingFactory : IWarehouseScheduleMappingFactory
    {
        private readonly IRepository<Day> _dayRepository;
        private readonly IRepository<WarehouseScheduleMapping> _warehouseScheduleRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="WarehouseScheduleMappingFactory" />.
        /// </summary>
        /// <param name="dayRepository">Implementation of <see cref="IRepository{T}"> where T is <see cref="typeof(Day)">.</param>
        /// <param name="warehouseScheduleRepository">Implementation of <see cref="IRepository{T}"> where T is <see cref="typeof(WarehouseScheduleMapping)">.</param>
        public WarehouseScheduleMappingFactory(IRepository<Day> dayRepository,
            IRepository<WarehouseScheduleMapping> warehouseScheduleRepository)
        {
            _dayRepository = dayRepository;
            _warehouseScheduleRepository = warehouseScheduleRepository;
        }

        /// <summary>
        /// Gets and prepares an implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.
        /// </summary>
        /// <param name="warehouseId">Id of any Warehouse</param>
        /// <param name="mappings">An implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.</param>
        /// <returns>An implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.</returns>
        public IList<WarehouseScheduleMapping> PrepareWarehouseScheduleMappings(int warehouseId = 0, IList<WarehouseScheduleMapping> mappings = null)
        {
            if (!(mappings is null))
            {
                return mappings;
            }

            IList<WarehouseScheduleMapping> foundWarehouseScheduleMappings = _warehouseScheduleRepository.Table.Where(schedule => schedule.WarehouseId == warehouseId).ToList();
            IList<Day> days = _dayRepository.Table.ToList();

            foreach (Day day in days)
            {
                if (!foundWarehouseScheduleMappings.Any(mapping => mapping.DayId == day.Id))
                {
                    foundWarehouseScheduleMappings.Add(new WarehouseScheduleMapping
                    {
                        WarehouseId = warehouseId,
                        DayId = day.Id,
                        IsActive = false,
                        BeginTime = new System.TimeSpan(),
                        EndTime = new System.TimeSpan()
                    });
                }
            }

            return foundWarehouseScheduleMappings.OrderBy(mapping => mapping.DayId).ToList();
        }
    }
}
