using Nop.Core;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Domains
{
    /// <summary>
    /// Represents a day of the week.
    /// </summary>
    public partial class Day : BaseEntity
    {
        /// <sumary>
        /// Indicates the name of the day.
        /// </sumary>
        public string Name { get; set; }
    }
}
