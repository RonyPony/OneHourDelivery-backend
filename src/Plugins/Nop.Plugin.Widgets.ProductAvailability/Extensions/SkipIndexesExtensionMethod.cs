using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.ProductAvailability.Extensions
{
    /// <summary>
    /// Represents a container object for th SkipIndexes extension method.
    /// </summary>
    public static class SkipIndexesExtensionMethod
    {
        /// <summary>
        /// Skips the elements of an enumerable that has it's index contained in a specified indexes enumaration.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="elements">Elements to filter.</param>
        /// <param name="indexesToSkip">Indexes to skip.</param>
        public static IEnumerable<T> SkipIndexes<T>(this IEnumerable<T> elements, IEnumerable<int> indexesToSkip)
        {
            return elements.Select((value, index) => new { Value = value, Index = index })
                .Where(element => !indexesToSkip.Contains(element.Index)).Select(size => size.Value).ToList();
        }
    }
}
