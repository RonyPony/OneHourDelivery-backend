using System;
using System.Collections.Generic;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Mapping
{
    /// <summary>
    /// <see cref="WarehouseScheduleMapping" /> and <see cref="Day" /> instance of backward compatibility of table naming.
    /// </summary>
    public partial class NameCompatibility : INameCompatibility
    {
        /// <summary>
        /// Indicates the name of each model as a table in the database.
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string> 
        {
            { typeof(WarehouseScheduleMapping), "Warehouse_Schedule_Mapping" },
            { typeof(Day), "Day" }
        };

        /// <summary>
        /// Indicates the name of each properties as a column in each table.
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string> 
        {
            {(typeof(WarehouseScheduleMapping), "WarehouseId"), "WarehouseId"},
            {(typeof(WarehouseScheduleMapping),  "DayId"), "DayId"},
            {(typeof(WarehouseScheduleMapping),  "IsActive"), "IsActive"},
            {(typeof(WarehouseScheduleMapping),  "BeginTime"), "BeginTime"},
            {(typeof(WarehouseScheduleMapping),  "EndTime"), "EndTime"},
            {(typeof(Day),  "Name"), "Name"}

        };
    }
}
