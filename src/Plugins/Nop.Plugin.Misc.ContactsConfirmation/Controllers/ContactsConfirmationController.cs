using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Plugin.Misc.ContactsConfirmation.Helpers;
using Nop.Plugin.Misc.ContactsConfirmation.Services;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using System;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Misc.ContactsConfirmation.Controllers
{
    /// <summary>
    /// Represents the main controller for Contacts Confirmation plugin.
    /// </summary>
    [Route("api/contact-confirmation")]
    [ApiController]
    public class ContactsConfirmationController : BasePluginController
    {
        #region Fields

        private readonly IContactConfirmationService _contactConfirmationService;
        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ContactsConfirmationController"/>.
        /// </summary>
        /// <param name="contactConfirmationService">An implementation of <see cref="IContactConfirmationService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="encryptionService">An implementation of <see cref="IEncryptionService"/>.</param>
        public ContactsConfirmationController(
            IContactConfirmationService contactConfirmationService,
            ICustomerService customerService,
            IEncryptionService encryptionService
            )
        {
            _contactConfirmationService = contactConfirmationService;
            _customerService = customerService;
            _encryptionService = encryptionService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Marks a given contact as validated.
        /// </summary>
        /// <param name="validationCode">The encrypted contact to validate</param>
        /// <returns>An instance of <see cref="IActionResult"/>.</returns>
        [HttpGet("validate/{validationCode}")]
        public IActionResult ValidateContactById(string validationCode)
        {
            try
            {
                byte[] validationCodeBytes = Convert.FromBase64String(validationCode);
                string encriptedContact = Encoding.UTF8.GetString(validationCodeBytes);
                string contact = _encryptionService.DecryptText(encriptedContact);

                _contactConfirmationService.ValidateContact(contact);

                Customer customer = _customerService.GetCustomerByEmail(contact);

                if (customer is null)
                    throw new Exception("Customer not found.");

                customer.Active = true;
                _customerService.UpdateCustomer(customer);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }


        #endregion
    }
}
