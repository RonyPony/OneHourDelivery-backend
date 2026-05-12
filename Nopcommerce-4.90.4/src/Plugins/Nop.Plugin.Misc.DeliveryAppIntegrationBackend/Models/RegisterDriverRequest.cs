namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models
{
    /// <summary>
    /// Represents a request to register a driver.
    /// </summary>
    public class RegisterDriverRequest: RegisterCustomerRequest
    {
        /// <summary>
        /// Indicates the driver identification number.
        /// </summary>
        public string DriverIdentificationNumber { get; set; }

        /// <summary>
        /// Indicates the vehicle type.
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Indicates the vehicle brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Indicates the vehicle model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Indicates the vehicle license plate.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Indicates the vehicle color.
        /// </summary>
        public string Color { get; set; }
    }
}
