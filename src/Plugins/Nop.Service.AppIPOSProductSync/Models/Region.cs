using Nop.Core;

namespace Nop.Service.AppIPOSSync.Clients
{
    /// <summary>
    /// Represent a country Region
    /// </summary>
    public class Region : BaseEntity
    {
        /// <summary>
        /// Represents the name of the Region
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Represents a comment about a specific Region
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Represents the cost of the Region
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// Represents whether the Region is active or not
        /// </summary>
        public bool Active { get; set; }
    }
}