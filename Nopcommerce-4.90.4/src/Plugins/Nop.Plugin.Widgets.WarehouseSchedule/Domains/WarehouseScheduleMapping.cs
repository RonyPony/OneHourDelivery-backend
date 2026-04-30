using Nop.Core;
using System;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Domains
{
    /// <summary>
    /// Represents a warehouse schedule of a day.
    /// </summary>
    public partial class WarehouseScheduleMapping : BaseEntity
    {
    
        /// <summary>
        /// Indicates the id of the warehouse.
        /// </summary>
        public int WarehouseId { get; set; }
        
        /// <summary>
        /// Indicates the id of the day.
        /// </summary>
        public int DayId { get; set; }
        
        /// <summary>
        /// indicates if the warehouse is active.
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Indicates the begin time.
        /// </summary>
        public TimeSpan BeginTime { get; set; }
        
        /// <summary>
        /// Indicates the end time.
        /// </summary>
        public TimeSpan EndTime { get; set; }
        
    }
}
