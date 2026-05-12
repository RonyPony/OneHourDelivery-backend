using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Logging;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with Delivery app driver entity.
    /// </summary>
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : Controller
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerAddressGeocodingServices _customerAddressGeocodingServices;
        private readonly IAddressService _addressService;
        private readonly ILogger _logger;
        private readonly IPictureService _pictureService;
        private readonly IDownloadService _downloadService;
        private readonly IDeliveryAppDriverService _deliveryAppDriverService;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerAttributeParser _customerAttributeParser;


        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="DriverController"/>
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/></param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/></param>
        /// <param name="customerSettings">An implementation of <see cref="CustomerSettings"/></param>
        /// <param name="customerRegistrationService">An implementation of <see cref="ICustomerRegistrationService"/></param>
        /// <param name="customerAddressGeocodingServices">An implementation of <see cref="ICustomerAddressGeocodingServices"/></param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/></param>
        /// <param name="logger">An implementation of <see cref="ILogger"/></param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/></param>
        /// <param name="downloadService">An implementation of <see cref="IDownloadService"/></param>
        /// <param name="deliveryAppDriverService">An implementation of <see cref="IDeliveryAppDriverService"/></param>
        /// <param name="customerAttributeService">An implementation of <see cref="ICustomerAttributeService"/></param>
        /// <param name="customerAttributeParser">An implementation of <see cref="ICustomerAttributeParser"/></param>
        public DriverController(ICustomerService customerService,
            IStoreContext storeContext,
            IGenericAttributeService genericAttributeService,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerAddressGeocodingServices customerAddressGeocodingServices,
            IAddressService addressService,
            ILogger logger,
            IPictureService pictureService,
            IDownloadService downloadService,
            IDeliveryAppDriverService deliveryAppDriverService,
            ICustomerAttributeService customerAttributeService,
            ICustomerAttributeParser customerAttributeParser)
        {
            _customerService = customerService;
            _storeContext = storeContext;
            _genericAttributeService = genericAttributeService;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _customerSettings = customerSettings;
            _customerRegistrationService = customerRegistrationService;
            _customerAddressGeocodingServices = customerAddressGeocodingServices;
            _addressService = addressService;
            _logger = logger;
            _pictureService = pictureService;
            _downloadService = downloadService;
            _deliveryAppDriverService = deliveryAppDriverService;
            _customerAttributeService = customerAttributeService;
            _customerAttributeParser = customerAttributeParser;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Inserts a new driver with it's address and geo coordinates.
        /// </summary>
        /// <param name="request">An instance of <see cref="RegisterDriverRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  CreateClient functionality", typeof(UpdateCustomerResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes CreateClient functionality", typeof(ErrorMessage))]
        public IActionResult CreateDriver([FromBody] RegisterDriverRequest request)
        {
            // Insert the customer as guest
            var customer = _customerService.InsertGuestCustomer();
            customer.RegisteredInStoreId = _storeContext.GetCurrentStore().Id;

            try
            {
                var registrationRequest = new CustomerRegistrationRequest(customer,
                    request.Email,
                    request.Email,
                    request.Password,
                    _customerSettings.DefaultPasswordFormat,
                    _storeContext.GetCurrentStore().Id,
                    false);

                var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

                if (!registrationResult.Success)
                {
                    string errorMessage = "";

                    foreach (string error in registrationResult.Errors)
                    {
                        errorMessage += $"{error}|";
                    }

                    throw new ArgumentException(errorMessage);
                }

                // Insert customer attributes
                _genericAttributeService.SaveAttribute(customer, "FirstName", request.FirstName);
                _genericAttributeService.SaveAttribute(customer, "LastName", request.LastName);
                _genericAttributeService.SaveAttribute(customer, "DateOfBirth",
                    _customerAddressGeocodingServices.ParseDateOfBirth(request));
                _genericAttributeService.SaveAttribute(customer, "Phone", request.PhoneNumber);

                SaveDriverCustomerAttributes(request, customer);

                // Insert the address
                var countryId = request.CountryId is null || request.CountryId == 0 ?
                    _customerAddressGeocodingServices.TryGetCountryIdByNameOrDefault(request.CountryName) : request.CountryId;
                var defaultAddress = new Address
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    CountryId = countryId,
                    StateProvinceId = request.StateProvinceId is null || request.StateProvinceId == 0 ?
                        _customerAddressGeocodingServices.TryGetStateProvinceIdByNameAndCountryIdOrDefault(request.StateProvinceName, countryId)
                        : request.StateProvinceId,
                    City = request.City,
                    Address1 = request.Address1,
                    Address2 = string.IsNullOrEmpty(request.Address2) || string.IsNullOrWhiteSpace(request.Address2) ? null : request.Address2,
                    ZipPostalCode = request.ZipPostalCode,
                    PhoneNumber = request.PhoneNumber,
                    CreatedOnUtc = customer.CreatedOnUtc
                };

                _addressService.InsertAddress(defaultAddress);

                // Associate address to customer
                _customerService.InsertCustomerAddress(customer, defaultAddress);

                customer.BillingAddressId = defaultAddress.Id;
                customer.ShippingAddressId = defaultAddress.Id;

                _customerService.UpdateCustomer(customer);

                // Insert address geo coordinates
                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(new AddressGeoCoordinatesMapping
                {
                    AddressId = defaultAddress.Id,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                }, defaultAddress.Id);

                AddDriverRoleTo(customer);

                return Ok(new { CustomerId = customer.Id });
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"There was error creating driver. {ex.Message},  Full exception: {ex}, ", ex);
                _customerService.DeleteCustomer(customer);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error creating driver. {ex.Message},  Full exception: {ex}, ", ex);
                _customerService.DeleteCustomer(customer);
                return BadRequest(new { Message = ex.Message });
            }
        }

        private void AddDriverRoleTo(Customer customer)
        {
            CustomerRole customerRole = _customerService.GetAllCustomerRoles().FirstOrDefault(x => x.Name.Equals("Mensajero"));

            _customerService.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerId = customer.Id, CustomerRoleId = customerRole.Id });
        }

        private void SaveDriverCustomerAttributes(RegisterDriverRequest request, Customer customer)
        {
            CustomerAttribute driverIdentificationNumberAttribute = _customerAttributeService
                                                                    .GetAllCustomerAttributes()
                                                                    .FirstOrDefault(atrr => atrr.Name == Defaults.DriverIdentificationNumberAttribute.Name);

            CustomerAttribute vehicleTypeAttribute = _customerAttributeService
                                                    .GetAllCustomerAttributes()
                                                    .FirstOrDefault(atrr => atrr.Name == Defaults.VehicleTypeAttribute.Name);

            CustomerAttribute brandAttribute = _customerAttributeService
                                    .GetAllCustomerAttributes()
                                    .FirstOrDefault(atrr => atrr.Name == Defaults.VehicleBrandAttribute.Name);

            CustomerAttribute modelAttribute = _customerAttributeService
                                    .GetAllCustomerAttributes()
                                    .FirstOrDefault(atrr => atrr.Name == Defaults.VehicleModelAttribute.Name);

            CustomerAttribute licensePlateAttribute = _customerAttributeService
                                    .GetAllCustomerAttributes()
                                    .FirstOrDefault(atrr => atrr.Name == Defaults.VehicleLicensePlateAttribute.Name);

            CustomerAttribute colorAttribute = _customerAttributeService
                    .GetAllCustomerAttributes()
                    .FirstOrDefault(atrr => atrr.Name == Defaults.VehicleColorAttribute.Name);

            ValidateDriverAttributes(driverIdentificationNumberAttribute,
                                     vehicleTypeAttribute,
                                     brandAttribute,
                                     modelAttribute,
                                     licensePlateAttribute,
                                     colorAttribute);

            SaveCustomerAttributeAttribute(request.DriverIdentificationNumber, customer, Defaults.DriverIdentificationNumberAttribute.Name);
            SaveCustomerAttributeAttribute(request.VehicleType, customer, Defaults.VehicleTypeAttribute.Name);
            SaveCustomerAttributeAttribute(request.Brand, customer, Defaults.VehicleBrandAttribute.Name);
            SaveCustomerAttributeAttribute(request.Model, customer, Defaults.VehicleModelAttribute.Name);

            SaveCustomerAttributeAttribute(request.LicensePlate, customer, Defaults.VehicleLicensePlateAttribute.Name);
            SaveCustomerAttributeAttribute(request.Color, customer, Defaults.VehicleColorAttribute.Name);
        }

        private void SaveCustomerAttributeAttribute(string value, Customer customer, string AttributeName)
        {
            CustomerAttribute customerAttribute = _customerAttributeService.GetAllCustomerAttributes().FirstOrDefault(x => x.Name == AttributeName);

            if (customerAttribute == null)
            {
                throw new ArgumentException($"{AttributeName} could not be found");
            }


            string customerAttributes = _genericAttributeService.GetAttribute<string>(customer, "CustomCustomerAttributes", storeId: 0);
            customerAttributes = customerAttributes ?? "";

            customerAttributes = _customerAttributeParser.AddCustomerAttribute(customerAttributes,
                                        customerAttribute, value);

            _genericAttributeService.SaveAttribute(customer, "CustomCustomerAttributes", customerAttributes, 0);

        }

        private void ValidateDriverAttributes(CustomerAttribute driverIdentificationNumberAttribute, CustomerAttribute vehicleTypeAttribute, CustomerAttribute brandAttribute, CustomerAttribute modelAttribute, CustomerAttribute licensePlateAttribute, CustomerAttribute colorAttribute)
        {
            if (driverIdentificationNumberAttribute == null) throw new ArgumentException($"{Defaults.DriverIdentificationNumberAttribute} attribute does not exists");
            if (vehicleTypeAttribute == null) throw new ArgumentException($"{Defaults.VehicleTypeAttribute} customer attribute does not exists");
            if (brandAttribute == null) throw new ArgumentException($"{Defaults.VehicleBrandAttribute.Name} customer attribute does not exists");
            if (modelAttribute == null) throw new ArgumentException($"{Defaults.VehicleModelAttribute.Name} customer attribute does not exists");
            if (licensePlateAttribute == null) throw new ArgumentException($"{Defaults.VehicleLicensePlateAttribute.Name} customer attribute does not exists");
            if (colorAttribute == null) throw new ArgumentException($"{Defaults.VehicleColorAttribute.Name} customer attribute does not exists");
        }

        /// <summary>
        /// Updates a driver's avatar..
        /// </summary>
        /// <param name="id">Driver id. </param>
        /// <param name="uploadedFile">Represents the new avatar file. An instance of <see cref="IFormFile"/></param>
        /// <returns>An instance of <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}/avatar"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes CreateClient functionality", typeof(ErrorMessage))]
        public IActionResult UpdateAvatar(int id, IFormFile uploadedFile)
        {
            Customer customer = _customerService.GetCustomerById(id);

            try
            {
                ValidateChangeAvatarRequest(uploadedFile, customer);

                Picture customerAvatar = _pictureService.GetPictureById(_genericAttributeService.GetAttribute<int>(customer, NopCustomerDefaults.AvatarPictureIdAttribute));
                var avatarMaxSize = _customerSettings.AvatarMaximumSizeBytes;
                if (uploadedFile.Length > avatarMaxSize)
                {
                    return BadRequest(new { message = "MaximumUploadedFileSizeExceded" });
                }

                byte[] customerPictureBinary = _downloadService.GetDownloadBits(uploadedFile);
                if (customerAvatar != null)
                {
                    customerAvatar = _pictureService.UpdatePicture(customerAvatar.Id, customerPictureBinary, uploadedFile.ContentType, null);
                }
                else
                {
                    customerAvatar = _pictureService.InsertPicture(customerPictureBinary, uploadedFile.ContentType, null);
                }

                var customerAvatarId = 0;
                if (customerAvatar != null)
                {
                    customerAvatarId = customerAvatar.Id;
                }

                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.AvatarPictureIdAttribute, customerAvatarId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error updating customer avatar. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="request">Aninstance of <see cref="UpdateCustomerRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  UpdateCustomer functionality", typeof(UpdateCustomerResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes UpdateCustomer functionality", typeof(ErrorMessage))]
        public IActionResult UpdateDriver(int id, [FromBody] UpdateCustomerRequest request)
        {
            try
            {
                Customer customer = _customerService.GetCustomerById(id);

                if (customer == null)
                {
                    throw new ArgumentException("CustomerDoesNotExists");
                }

                string keygroup = customer.GetType().Name;
                customer.Email = request.Email;

                // Update customer attributes
                var firstNameAttribute = _genericAttributeService.GetAttributesForEntity(customer.Id, keygroup)
                                                                 .FirstOrDefault(x => x.Key == "FirstName");
                firstNameAttribute.Value = request.FirstName;

                var lastNameAttribute = _genericAttributeService.GetAttributesForEntity(customer.Id, keygroup)
                                                                .FirstOrDefault(x => x.Key == "LastName");
                lastNameAttribute.Value = request.LastName;

                var phoneAttribute = _genericAttributeService.GetAttributesForEntity(customer.Id, keygroup)
                                                             .FirstOrDefault(x => x.Key == "Phone");

                if ((!customer.Email.Equals(request.Email)) && _customerAddressGeocodingServices.EmailIsAlreadyRegistered(request.Email))
                {
                    throw new ArgumentException("EmailAlreadyRegistered");
                }

                if ((!phoneAttribute.Value.Equals(request.PhoneNumber)) && _customerAddressGeocodingServices.PhoneNumberIsAlreadyRegistered(request.PhoneNumber))
                {
                    throw new ArgumentException("PhoneAlreadyRegistered");
                }


                phoneAttribute.Value = request.PhoneNumber;

                _genericAttributeService.UpdateAttribute(firstNameAttribute);
                _genericAttributeService.UpdateAttribute(lastNameAttribute);
                _genericAttributeService.UpdateAttribute(phoneAttribute);

                _customerService.UpdateCustomer(customer);

                return Ok(new UpdateCustomerResponse { CustomerId = customer.Id });
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error updating customer. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves the profit for a driver.
        /// </summary>
        /// <param name="id">The customer id for the driver.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpGet("{id}/profit"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetProfitByDriverId(int id)
        {
            try
            {
                IList<DriverProfitModel> ordersDeliveredByDriver = _deliveryAppDriverService.GetOrdersPendingToClosePaymentByDriverId(id);
                return Ok(ordersDeliveredByDriver);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Register driver valoration.
        /// </summary>
        /// <param name="driverRatingMappingRequest">driver request.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("valoration"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult RegisterDriverRatingValoration([FromBody] DriverRatingMappingRequest driverRatingMappingRequest)
        {
            try
            {
                _deliveryAppDriverService.RegisterDriverRatingValoration(driverRatingMappingRequest);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Gets cash order collection information.
        /// </summary>
        /// <param name="driverId">The Id of the driver</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpGet("{driverId}/payments-collection"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetDriverCashCollectionStatus(int driverId)
        {
            try
            {
                DriverPaymentCollection result = _deliveryAppDriverService.GetPaymentCollection(driverId);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Validates that the customer and the image are valid when attempting to change the customer's avatar/
        /// </summary>
        /// <param name="uploadedFile">The image to validate</param>
        /// <param name="customer">The customer to validate</param>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateChangeAvatarRequest(IFormFile uploadedFile, Customer customer)
        {
            if (!_customerService.IsRegistered(customer)) throw new ArgumentException("ClientNotRegistered");
            if (!_customerSettings.AllowCustomersToUploadAvatars) throw new ArgumentException("AllowCustomersToUploadAvatarsIsDisabled");
            if (uploadedFile == null) throw new ArgumentException("WasNotFoundUploadedFileFormValue");
            if (string.IsNullOrEmpty(uploadedFile.FileName)) throw new ArgumentException("WasNotFoundUploadedFileName");
        }

        #endregion
    }
}
