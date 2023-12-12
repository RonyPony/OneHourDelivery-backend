    using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for customer account interactions.
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : Controller
    {
        #region Fields

        private readonly IDeliveryAppAccountService _deliveryAppAccountService;
        private readonly ILogger _logger;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;

        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="AccountController"/>.
        /// </summary>
        /// <param name="deliveryAppAccountService">An implementation of <see cref="IDeliveryAppAccountService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="customerSettings">An implementation of <see cref="CustomerSettings"/>.</param>
        /// <param name="customerRegistrationService">An implementation of <see cref="ICustomerRegistrationService"/>.</param>
        /// <param name="encryptionService">An implementation of <see cref="IEncryptionService"/>.</param>
        /// <param name="workflowMessageService">An implementation of <see cref="IWorkflowMessageService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        public AccountController(
            IDeliveryAppAccountService deliveryAppAccountService,
            ILogger logger,
            ILocalizationService localizationService,
            ICustomerService customerService,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            IEncryptionService encryptionService, 
            IWorkflowMessageService workflowMessageService, 
            IWorkContext workContext)
        {
            _deliveryAppAccountService = deliveryAppAccountService;
            _localizationService = localizationService;
            _customerService = customerService;
            _customerSettings = customerSettings;
            _customerRegistrationService = customerRegistrationService;
            _logger = logger;
            _workflowMessageService = workflowMessageService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        [HttpGet("customers"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCustomerAccountByEmail([FromQuery] string email)
        {
            try
            {
                CustomerAccount customerAccount = _deliveryAppAccountService.GetCustomerAccountByEmail(email);

                return Ok(customerAccount);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("drivers"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetDriverAccountByEmail([FromQuery] string email)
        {
            try
            {
                CustomerAccount customerAccount = _deliveryAppAccountService.GetCustomerAccountByEmail(email);

                return Ok(customerAccount);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }


        [HttpGet("commerces"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetCommerceAccountByEmail([FromQuery] string email)
        {
            try
            {
                CustomerAccount customerAccount = _deliveryAppAccountService.GetCustomerAccountByEmail(email);

                return Ok(customerAccount);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpDelete("deleteAccount/{customerId}")] //Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult deleteCustomerAccount([FromQuery] int customerId)
        {
            try
            {
                if (customerId == 0)
                {
                    return BadRequest("customerIdNotProvided");
                }
                Customer customer = _customerService.GetCustomerById(customerId);
                bool customerAccountDeleted = _deliveryAppAccountService.deleteAccount(customer);
                if (customerAccountDeleted)
                {
                    return Ok("Deleted Successfully");
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
                
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }


        [HttpPatch("{customerId}/password"), Authorize(Roles = "Cliente, Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Exposes CreateClient functionality", typeof(ErrorMessage))]
        public IActionResult UpdatePassword(int customerId, ChangePasswordRequestModel request)
        {
            Customer customer = _customerService.GetCustomerById(customerId);

            try
            {
                ValidateChangePasswordRequest(customerId, customer, request);

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
                _logger.Error($"There was error updating customer password. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        private void ValidateChangePasswordRequest(int id, Customer customer, ChangePasswordRequestModel request)
        {
            if (customer == null) throw new ArgumentException("CustomerNotFound");
            if (!_customerService.IsRegistered(customer)) throw new ArgumentException("ClientNotRegistered");
            if (id <= 0) throw new ArgumentException("CustomerIdCantBeZeroOrLess");
            if (string.IsNullOrWhiteSpace(request.OldPassword)) throw new ArgumentException("OldPasswordCantBeNullOrEmpty");
            if (string.IsNullOrWhiteSpace(request.NewPassword)) throw new ArgumentException("NewPasswordCantBeNullOrEmpty");
        }

        /// <summary>
        /// Sends verification code to costumer email.
        /// </summary>
        /// <param name="customerEmail">The email of a customer</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("verification-code")]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SendVerificationCode([FromBody] string customerEmail)
        {
            try
            {
                var random = new Random();
                int codeValue = random.Next(1000, 9999);

                Customer currentCustomer = _customerService.GetCustomerByEmail(customerEmail);

                if (currentCustomer == null)
                    return NotFound(new { message = "CustomerNotFound" });

                _deliveryAppAccountService.SendVerificationCode(currentCustomer, codeValue);

                return Ok(codeValue);
            }
            catch (Exception ex)
            {
                _logger.Error($"There was error sending verification code email. {ex.Message}", ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Change forgotten customer password.
        /// </summary>
        /// <param name="model">An instance of <see cref="ChangeForgottenPasswordRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("change-forgotten-password")]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult ChangeForgottenPassword([FromBody] ChangeForgottenPasswordRequest model)
        {
            try
            {
                Customer currentCustomer = _customerService.GetCustomerByEmail(model.Email);

                if (currentCustomer == null)
                    return NotFound(new { message = "User not found" });

                PasswordFormat passFormat = PasswordFormat.Clear;
                //CustomerPassword oldPassword = _customerService.GetCurrentPassword(currentCustomer.Id);

                //var changePasswordRequest = new ChangePasswordRequest(model.Email,
                //    true,passFormat, model.NewPassword, oldPassword.Password);
                //changePasswordRequest.HashedPasswordFormat = "1";

                //var result = _customerRegistrationService.ChangePasswordWithOldPasswordHashed(changePasswordRequest);


                //var result = _customerRegistrationService.ChangePassword(changePasswordRequest);

                //if (!result.Success)
                //{
                //    _logger.Error($"There was an error changing forgotten customer password. {result.Errors.FirstOrDefault()}");
                //    return BadRequest(new { message = result.Errors.FirstOrDefault() });
                //}



                if (model.NewPassword!=model.ConfirmNewPassword)
                {
                    return BadRequest("PasswordsDoesntMatch");
                }

                var customerPassword = new CustomerPassword
                {
                    CustomerId = currentCustomer.Id,
                    PasswordFormat = passFormat,
                    Password = model.NewPassword,
                    CreatedOnUtc = DateTime.UtcNow,
                    PasswordSalt = "Account Password reseted by customer"
                };
                IList<CustomerPassword>previousPasswords = _customerService.GetCustomerPasswords(currentCustomer.Id);
                IEnumerable<CustomerPassword> previusPass = previousPasswords.Where(e => e.Password == customerPassword.Password);
                if (previusPass.Count()>=1)
                {
                    return BadRequest("passCanNotBeDuplicated");
                }
                _customerService.InsertCustomerPassword(customerPassword);

                

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"There was an error changing forgotten customer password. {ex.Message},  Full exception: {ex}, ", ex);
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Inserts an email validation to the emails queue by customer id.
        /// </summary>
        /// <param name="customerId">The id of the customer to send the email.</param>
        /// <returns>An instance of <see cref="IActionResult"/>.</returns>
        [HttpGet("send-email-validation/{customerId}")]
        public IActionResult SendEmailValidationByCustomerId(int customerId)
        {
            try
            {
                var customer = _customerService.GetCustomerById(customerId);

                if (customer is null)
                    throw new ArgumentException("CustomerNotFound");

                if (!CommonHelper.IsValidEmail(customer.Email))
                    throw new ArgumentException("InvalidEmail");

                _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }


        /// <summary>
        /// Verifies that a given phone number isn't registered by another customer.
        /// </summary>
        /// <param name="phoneNumber">The phone number to verify.</param>
        /// <returns>An instance of <see cref="IActionResult"/>.</returns>
        [HttpGet("{phoneNumber}/check-phone-duplicated")]
        public IActionResult CheckContactIsNotDuplicated(string phoneNumber)
        {
            try
            {
                return Ok(new { IsDuplicated = _deliveryAppAccountService.PhoneNumberAlreadyRegistered(phoneNumber) });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }

        #endregion
    }
}
