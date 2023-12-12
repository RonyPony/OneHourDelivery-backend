using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Services
{
    /// <summary>
    /// Represents a contract for the warehouse schedule services..
    /// </summary>
    public interface IWarehouseScheduleService
    {
        /// <summary>
        /// Retrieves a warehouse schedule mappings.
        /// </summary>
        /// <param name="warehouseId">The id of the warehouse to get its schedule mappings.</param>
        /// <returns>An implementation of <see cref="IList{T}"> where T is <see cref="typeof(WarehouseScheduleMapping)">.</returns>
        void DeleteWarehouseSchedule(int warehouseId);
        
        /// <summary>
        /// Retrieves a warehouse schedule mappings.
        /// </summary>
        /// <param name="warehouseId">The id of the warehouse to get its schedule mappings.</param>
        /// <returns>An implementation of <see cref="IList{T}"> where T is <see cref="typeof(WarehouseScheduleMapping)">.</returns>
        IList<WarehouseScheduleMapping> GetWarehouseSchedule(int warehouseId);

        /// <summary>
        /// Inserts a warehouse scheduled day if doesn't exists, otherwise updates it.
        /// </summary>
        /// <param name="warehouseSchedule">The scheduled day.</param>
        void CreateOrUpdateWarehouseScheduledDay(WarehouseScheduleMapping warehouseSchedule);
    }
}
