using Nop.Data;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Services
{
    /// <summary>
    /// WarehouseSchedule Service
    /// </summary>
    public sealed class WarehouseScheduleService : IWarehouseScheduleService
    {
        #region Fields

        private readonly IRepository<WarehouseScheduleMapping> _warehouseScheduleRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Declare WarehouseScheduleMapping repository
        /// </summary>
        /// <param name="warehouseScheduleRepository">warehouseSchedule Repository</param>
        public WarehouseScheduleService(IRepository<WarehouseScheduleMapping> warehouseScheduleRepository)
        {
            _warehouseScheduleRepository = warehouseScheduleRepository;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public async Task DeleteWarehouseScheduleAsync(int warehouseId)
        {
            IList<WarehouseScheduleMapping> foundWarehouseSchedule = await _warehouseScheduleRepository.GetAllAsync(query => query.Where(schedule =>
                schedule.WarehouseId == warehouseId));

            if (foundWarehouseSchedule.Count > 0)
            {
                await _warehouseScheduleRepository.DeleteAsync(foundWarehouseSchedule);
            }
        }

        ///<inheritdoc/>
        public async Task<IList<WarehouseScheduleMapping>> GetWarehouseScheduleAsync(int warehouseId)
            => await _warehouseScheduleRepository.GetAllAsync(query => query.Where(schedule => schedule.WarehouseId == warehouseId));

        ///<inheritdoc/>
        public async Task CreateOrUpdateWarehouseScheduledDayAsync(WarehouseScheduleMapping warehouseSchedule)
        {
            WarehouseScheduleMapping schedule = (await _warehouseScheduleRepository.GetAllAsync(query => query
                    .Where(x => x.WarehouseId == warehouseSchedule.WarehouseId && x.DayId == warehouseSchedule.DayId)))
                .FirstOrDefault();

            if (schedule != null)
            {
                schedule.IsActive = warehouseSchedule.IsActive;
                schedule.BeginTime = warehouseSchedule.BeginTime;
                schedule.EndTime = warehouseSchedule.EndTime;
                await _warehouseScheduleRepository.UpdateAsync(schedule);
            }
            else
            {
                await _warehouseScheduleRepository.InsertAsync(warehouseSchedule);
            }
        }

        #endregion
    }
}
