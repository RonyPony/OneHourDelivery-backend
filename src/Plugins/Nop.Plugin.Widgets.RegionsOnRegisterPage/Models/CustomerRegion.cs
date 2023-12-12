using Nop.Core;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Models
{
    /// <summary>
    /// Represent the mapping class between
    /// </summary>
    public class CustomerRegion : BaseEntity
    {
        /// <summary>
        /// Represents the Customer's ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Represents the Region's ID
        /// </summary>
        public int RegionID { get; set; }
    }
}