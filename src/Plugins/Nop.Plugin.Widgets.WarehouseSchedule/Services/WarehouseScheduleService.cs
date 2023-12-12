using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using Nop.Services.Events;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Services
{
    /// <summary>
    /// WarehouseSchedule Service
    /// </summary>
    public sealed class WarehouseScheduleService : IWarehouseScheduleService
    {
        #region Fields

        private readonly IRepository<WarehouseScheduleMapping> _warehouseScheduleRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Declare WarehouseScheduleMapping and EventPublisher Repositories
        /// </summary>
        /// <param name="warehouseScheduleRepository">warehouseSchedule Repository</param>
        /// <param name="eventPublisher">eventPublisher Repository</param>
        public WarehouseScheduleService(IRepository<WarehouseScheduleMapping> warehouseScheduleRepository, IEventPublisher eventPublisher)
        {
            _warehouseScheduleRepository = warehouseScheduleRepository;
            _eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void DeleteWarehouseSchedule(int warehouseId)
        {
            IList<WarehouseScheduleMapping> foundWarehouseSchedule = _warehouseScheduleRepository.Table.Where(schedule =>
                schedule.WarehouseId == warehouseId).ToList();

            if (foundWarehouseSchedule.Count > 0)
            {
                foreach (WarehouseScheduleMapping schedule in foundWarehouseSchedule)
                {
                    _warehouseScheduleRepository.Delete(schedule);
                    _eventPublisher.EntityDeleted(schedule);
                }
            }
        }

        ///<inheritdoc/>
        public IList<WarehouseScheduleMapping> GetWarehouseSchedule(int warehouseId)
            => _warehouseScheduleRepository.Table.Where(schedule => schedule.WarehouseId == warehouseId).ToList();

        ///<inheritdoc/>
        public void CreateOrUpdateWarehouseScheduledDay(WarehouseScheduleMapping warehouseSchedule)
        {
            WarehouseScheduleMapping schedule = _warehouseScheduleRepository.Table
                .FirstOrDefault(x => x.WarehouseId == warehouseSchedule.WarehouseId && x.DayId == warehouseSchedule.DayId);

            if (schedule != null)
            {
                schedule.IsActive = warehouseSchedule.IsActive;
                schedule.BeginTime = warehouseSchedule.BeginTime;
                schedule.EndTime = warehouseSchedule.EndTime;
                _warehouseScheduleRepository.Update(schedule);
            }
            else
            {
                _warehouseScheduleRepository.Insert(warehouseSchedule);
            }
        }

        #endregion
    }

}
