using Nop.Plugin.Widgets.WarehouseSchedule.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Factories
{
    /// <summary>
    /// Represents a contract for the <see cref="WarehouseScheduleMappings"/> model factory.
    /// </summary>
    public interface IWarehouseScheduleMappingFactory
    {
        /// <summary>
        /// Gets and prepares an implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.
        /// </summary>
        /// <param name="warehouseId">Id of any Warehouse</param>
        /// <param name="mappings">An implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.</param>
        /// <returns>An implementation of <see cref="IList{T}" /> where T is <see cref="typeof(WarehouseScheduleMapping)" />.</returns>        
        Task<IList<WarehouseScheduleMapping>> PrepareWarehouseScheduleMappingsAsync(int warehouseId = 0, IList<WarehouseScheduleMapping> mappings = null);
    }
}
