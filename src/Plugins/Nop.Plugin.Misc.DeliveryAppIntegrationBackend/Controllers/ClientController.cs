using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with client entity.
    /// </summary>
    [Route("api/clients")]
    [ApiController]
    public class ClientController : Controller
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IPictureService _pictureService;
        private readonly IDownloadService _downloadService;
        private readonly CustomerSettings _customerSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILogger _logger;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly IAddressService _addressService;
        private readonly IStoreContext _storeContext;
        private readonly ICustomerAddressGeocodingServices _customerAddressGeocodingServices;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IDeliveryAppAccountService _deliveryAppAccountService;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates a new instance of <see cref="ClientController"/>
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/></param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/></param>
        /// <param name="downloadService">An implementation of <see cref="IDownloadService"/></param>
        /// <param name="customerSettings">An instance of <see cref="CustomerSettings"/></param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/></param>
        /// <param name="logguer">An implementation of <see cref="ILogger"/></param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/></param>
        /// <param name="customerRegistrationService">An implementation of <see cref="ICustomerRegistrationService"/></param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="customerAddressGeocodingServices">An implementation of <see cref="ICustomerAddressGeocodingServices"/></param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/></param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/></param>
        /// <param name="workflowMessageService">An implementation of <see cref="IWorkflowMessageService"/></param>
        /// <param name="deliveryAppAccountService">An implementation of <see cref="IDeliveryAppAccountService"/></param>
        public ClientController(
            ICustomerService customerService,
            IPictureService pictureService,
            IDownloadService downloadService,
            CustomerSettings customerSettings,
            IGenericAttributeService genericAttributeService,
            ILogger logguer,
            ILocalizationService localizationService,
            ICustomerRegistrationService customerRegistrationService,
            IAddressService addressService,
            IStoreContext storeContext,
            ICustomerAddressGeocodingServices customerAddressGeocodingServices,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            IWorkContext workContext,
            IWorkflowMessageService workflowMessageService,
            IDeliveryAppAccountService deliveryAppAccountService)
        {
            _customerService = customerService;
            _pictureService = pictureService;
            _downloadService = downloadService;
            _customerSettings = customerSettings;
            _genericAttributeService = genericAttributeService;
            _logger = logguer;
            _localizationService = localizationService;
            _customerRegistrationService = customerRegistrationService;
            _addressService = addressService;
            _storeContext = storeContext;
            _customerAddressGeocodingServices = customerAddressGeocodingServices;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _workContext = workContext;
            _workflowMessageService = workflowMessageService;
            _deliveryAppAccountService = deliveryAppAccountService;
        }

        #endregion

        #region Utilities

        private void ValidateChangeAvatarRequest(IFormFile uploadedFile, Customer customer)
        {
            if (!_customerService.IsRegistered(customer)) throw new ArgumentException("ClientNotRegistered");
            if (!_customerSettings.AllowCustomersToUploadAvatars) throw new ArgumentException("AllowCustomersToUploadAvatarsIsDisabled");
            if (uploadedFile == null) throw new ArgumentException("WasNotFoundUploadedFileFormValue");
            if (string.IsNullOrEmpty(uploadedFile.FileName)) throw new ArgumentException("WasNotFoundUploadedFileName");
        }

        private void ValidateChangePasswordRequest(int id, Customer customer, ChangePasswordRequestModel request)
        {
            if (customer == null) throw new ArgumentException("ClientDoesNotExists");
            if (!_customerService.IsRegistered(customer)) throw new ArgumentException("ClientNotRegistered");
            if (id <= 0) throw new ArgumentException("PasswordCantBeZeroOrLess");
            if (string.IsNullOrWhiteSpace(request.OldPassword)) throw new ArgumentException("OldPasswordCantBeNullOrEmpty");
            if (string.IsNullOrWhiteSpace(request.NewPassword)) throw new ArgumentException("NewPasswordCantBeNullOrEmpty");
        }

        private Address BuildUpdatedAddressModel(UpdateCustomerRequest request, DateTime createdOnUtc, int addressId, int? countryId)
        {
            Address address = _addressService.GetAddressById(addressId);
            address.FirstName = request.FirstName;
            address.LastName = request.LastName;
            address.Email = request.Email;
            address.CountryId = countryId;
            address.StateProvinceId = request.StateProvinceId is null || request.StateProvinceId == 0 ?
                _customerAddressGeocodingServices.TryGetStateProvinceIdByNameAndCountryIdOrDefault(request.StateProvinceName, countryId)
                : request.StateProvinceId;
            address.City = request.City;
            address.Address1 = request.Address1;
            address.Address2 = string.IsNullOrEmpty(request.Address2) || string.IsNullOrWhiteSpace(request.Address2) ? null : request.Address2;
            address.ZipPostalCode = request.ZipPostalCode;
            address.PhoneNumber = request.PhoneNumber;
            address.CreatedOnUtc = createdOnUtc;

            return address;
        }

        private void UpdateAddresses(UpdateCustomerRequest request, Customer customer, int? countryId)
        {
            int addressId = customer.ShippingAddressId ?? (customer.BillingAddressId ?? 0);
            if (addressId != 0)
            {
                Address address = BuildUpdatedAddressModel(request, customer.CreatedOnUtc, addressId, countryId);
                _addressService.UpdateAddress(address);
            }

            // Update address geo coordinates
            _addressGeoCoordinatesService.InsertAddressGeoCoordinates(new AddressGeoCoordinatesMapping
            {
                AddressId = addressId,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            }, addressId);

        }

        private List<AddressData> GetListOfAddressData(IList<Address> addresses)
        {
            var addressDataList = new List<AddressData>();
            foreach (Address address in addresses)
            {
                AddressGeoCoordinatesMapping addressCoordinates = _addressGeoCoordinatesService.GetAddressGeoCoordinates(address.Id);

                if (addressCoordinates != null)
                {
                    AddressData addressData = address.ToModel<AddressData>();
                    addressData.Latitude = addressCoordinates.Latitude;
                    addressData.Longitude = addressCoordinates.Longitude;
                    addressDataList.Add(addressData);
                }
            }
            return addressDataList;
        }

        #endregion

        #region Methods

        #region Client Avatar

        /// <summary>
        /// Updates a client's avatar..
        /// </summary>
        /// <param name="id">Client id. </param>
        /// <param name="uploadedFile">Represents the new avatar file. An instance of <see cref="IFormFile"/></param>
        /// <returns>An instance of <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}/avatar"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                _logger.Error($"There was error updating customer avatar. {ex.Message}", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        #endregion

        #region Client Password

        [HttpPatch("{id}/password"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes CreateClient functionality", typeof(ErrorMessage))]
        public IActionResult UpdatePassword(int id, ChangePasswordRequestModel request)
        {
            Customer customer = _customerService.GetCustomerById(id);

            try
            {
                ValidateChangePasswordRequest(id, customer, request);

                var changePasswordRequest = new ChangePasswordRequest(customer.Email,
                    request.ValidateRequest, _customerSettings.DefaultPasswordFormat, request.NewPassword, request.OldPassword);
                var changePasswordResult = _customerRegistrationService.ChangePassword(changePasswordRequest);

                if (changePasswordResult.Success)
                {
                    return Ok(_localizationService.GetResource("Account.ChangePassword.Success"));
                }
                else
                {
                    string message = changePasswordResult.Errors.FirstOrDefault();
                    _logger.Error($"There was error updating customer password. {message}");

                    return BadRequest(changePasswordResult.Errors.FirstOrDefault());
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"There was error updating customer password. {ex.Message} ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        #endregion

        #region Client

        /// <summary>
        /// Inserts a new customer with it's address and geo coordinates.
        /// </summary>
        /// <param name="request">Aninstance of <see cref="RegisterCustomerRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  CreateClient functionality", typeof(UpdateCustomerResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes CreateClient functionality", typeof(ErrorMessage))]
        public IActionResult CreateClient([FromBody] RegisterCustomerRequest request)
        {
            // Insert the customer as guest
            var customer = _customerService.InsertGuestCustomer();
            customer.RegisteredInStoreId = _storeContext.CurrentStore.Id;

            try
            {
                var registrationRequest = new CustomerRegistrationRequest(customer,
                    request.Email,
                    request.Email,
                    request.Password,
                    _customerSettings.DefaultPasswordFormat,
                    _storeContext.CurrentStore.Id,
                    false);

                var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

                if (!registrationResult.Success)
                {
                    StringBuilder errorMessage = new StringBuilder();

                    foreach (string error in registrationResult.Errors)
                    {
                        errorMessage.AppendLine($"{error}|");
                    }

                    throw new ArgumentException(errorMessage.ToString());
                }

                // Insert customer attributes
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.FirstNameAttribute, request.FirstName);
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.LastNameAttribute, request.LastName);
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.DateOfBirthAttribute,
                    _customerAddressGeocodingServices.ParseDateOfBirth(request));
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.PhoneAttribute, request.PhoneNumber);

                // Email validation message
                _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.AccountActivationTokenAttribute, Guid.NewGuid().ToString());
                _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

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

                CustomerRole customerRole = _customerService.GetAllCustomerRoles().FirstOrDefault(x => x.Name.Equals("Cliente"));

                _customerService.AddCustomerRoleMapping(new CustomerCustomerRoleMapping { CustomerId = customer.Id, CustomerRoleId = customerRole.Id });

                return Ok(new { CustomerId = customer.Id });
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"There was error creating client. {ex.Message}", ex);
                _customerService.DeleteCustomer(customer);
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error creating client. {ex.Message}", ex);
                _customerService.DeleteCustomer(customer);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="request">Aninstance of <see cref="UpdateCustomerRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPatch("{id}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  UpdateCustomer functionality", typeof(UpdateCustomerResponse))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes UpdateCustomer functionality", typeof(ErrorMessage))]
        public IActionResult UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
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
                                                                 .FirstOrDefault(x => x.Key == NopCustomerDefaults.FirstNameAttribute);
                firstNameAttribute.Value = request.FirstName;

                var lastNameAttribute = _genericAttributeService.GetAttributesForEntity(customer.Id, keygroup)
                                                                .FirstOrDefault(x => x.Key == NopCustomerDefaults.LastNameAttribute);
                lastNameAttribute.Value = request.LastName;

                var phoneAttribute = _genericAttributeService.GetAttributesForEntity(customer.Id, keygroup)
                                                             .FirstOrDefault(x => x.Key == NopCustomerDefaults.PhoneAttribute);

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
                _logger.Error($"There was error updating customer. {ex.Message}", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Gets a customer data with it's address and geo coordinates.
        /// </summary>
        /// <param name="customerId">The identification of a customer</param>
        /// <returns>An implementation of <see cref="OkObjectResult"/></returns>
        [HttpGet("{customerId}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  GetCustomerData functionality", typeof(CustomerData))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes GetCustomerData functionality", typeof(ErrorMessage))]
        public IActionResult GetCustomerData(int customerId)
        {
            try
            {
                Customer currentCustomer = _customerService.GetCustomerById(customerId);

                IList<Address> addresses = _customerService.GetAddressesByCustomerId(currentCustomer.Id);

                var customerData = new CustomerData(id: customerId,
                    firstName: _genericAttributeService.GetAttribute<string>(currentCustomer, NopCustomerDefaults.FirstNameAttribute),
                    lastName: _genericAttributeService.GetAttribute<string>(currentCustomer, NopCustomerDefaults.LastNameAttribute),
                    email: currentCustomer.Email,
                    phoneNumber: _genericAttributeService.GetAttribute<string>(currentCustomer, NopCustomerDefaults.PhoneAttribute),
                    imageUrl: _genericAttributeService.GetAttribute<string>(currentCustomer, NopCustomerDefaults.AvatarPictureIdAttribute),
                    shippingAddress: _genericAttributeService.GetAttribute<string>(currentCustomer, NopCustomerDefaults.SelectedShippingOptionAttribute),
                    addresses: GetListOfAddressData(addresses),
                    addressAlias: "", birthDate: _deliveryAppAccountService.GetCustomerDateOfBirthAsString(currentCustomer));

                return Ok(customerData);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error getting customer. {ex.Message}", ex);
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Gets all credit card registered by the customer.
        /// </summary>
        /// <param name="request">Register credit card request</param>
        /// <returns> OK </returns>
        [HttpGet("{customerId}/credit-cards"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Exposes the  GetAllCreditCards functionality", typeof(List<CreditCard>))]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes GetAllCreditCards functionality", typeof(ErrorMessage))]

        public IActionResult GetAllCreditCards()
        {
            return Ok(
                   new List<CreditCard>
                   {
                       new CreditCard{ creditCard = "4622 9431 2701 3705", expirationMonth = 12, expirationYear = 22, cvv = "838" },
                       new CreditCard{ creditCard = "4622 9431 2701 3713", expirationMonth = 12, expirationYear = 22, cvv = "043" },
                   }
                );
        }

        /// <summary>
        /// Registers a credit card info
        /// </summary>
        /// <param name="request">Register credit card request</param>
        /// <returns> OK </returns>
        [HttpPost("{customerId}/credit-cards"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes RegisterCreditCard functionality", typeof(ErrorMessage))]

        public IActionResult RegisterCreditCard([FromBody] RegisterCreditCardRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// Deletes a credit card.
        /// </summary>
        /// <param name="customerId">Customer that is the credit card's owner</param>
        /// <returns> OK </returns>
        [HttpDelete("{customerId}/credit-cards/{creditCardId}"), Authorize(Roles = "Registered, Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes DeleteCreditCard functionality", typeof(ErrorMessage))]
        public IActionResult DeleteCreditCard(int customerId, int creditCardId)
        {
            return Ok();
        }

        #endregion

        #endregion
    }

    public class UpdateCustomerResponse
    {
        public int CustomerId { get; set; }
    }

    public class CreditCard
    {
        public string creditCard { get; set; }
        public int expirationMonth { get; set; }
        public int expirationYear { get; set; }
        public string cvv { get; set; }
    }

}
