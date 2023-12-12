using System;

namespace Nop.Plugin.Synchronizers.SAPOrders.Helpers
{
    /// <summary>
    /// Static class that contains extension methods for this plug-in
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Formats a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A string formatted DateTime.</returns>
        public static string ToSapDateTime(this DateTime date)
        {
            // The reason we add -1 day is because the date of the SAP server is not always in sync with our server
            // and when we try to save it with today's date (which is always the case), the SAP API returns an error.
            return date.AddDays(-1).ToString("yyyy-MM-dd");
        }
    }
}
