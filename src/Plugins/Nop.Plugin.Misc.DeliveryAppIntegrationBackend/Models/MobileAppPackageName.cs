using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents each of the registered app package names in the notification center.
    /// </summary>
    public class MobileAppPackageName : StringEnum
    {
        private MobileAppPackageName(string value) : base(value) { }

        /// <summary>
        /// The clients app. 
        /// </summary>
        public static readonly MobileAppPackageName ClientApp = new MobileAppPackageName("OHDClient");

        /// <summary>
        /// The drivers app
        /// </summary>
        public static readonly MobileAppPackageName DriverApp = new MobileAppPackageName("OHDDriver");

        /// <summary>
        /// The commerces app
        /// </summary>
        public static readonly MobileAppPackageName CommerceApp = new MobileAppPackageName("OHDCommerce");
    }
}