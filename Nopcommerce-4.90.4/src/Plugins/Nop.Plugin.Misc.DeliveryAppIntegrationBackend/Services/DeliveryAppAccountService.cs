using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Vendors;
using System;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a implementation for the delivery app account services.
    /// </summary>
    public sealed class DeliveryAppAccountService : IDeliveryAppAccountService
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IPictureService _pictureService;
        private readonly MediaSettings _mediaSettings;
        private readonly IEmailAccountService _emailAccountService;
        private readonly ILocalizationService _localizationService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly EmailAccountSettings _emailAccountSettings;

        private readonly IRepository<GenericAttribute> _genericAttributeRepository;
        private readonly IVendorService _vendorService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppAccountService"/>.
        /// </summary>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/>.</param>
        /// <param name="mediaSettings">An instance of <see cref="MediaSettings"/>.</param>
        /// <param name="emailAccountService">An implementation of <see cref="IEmailAccountService"/>..</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>..</param>
        /// <param name="queuedEmailService">An implementation of <see cref="IQueuedEmailService"/>..</param>
        /// <param name="emailAccountSettings">An instance of <see cref="EmailAccountSettings"/>..</param>
        /// <param name="genericAttributeRepository">An instance of <see cref="IRepository{GenericAttribute}"/>..</param>
        /// <param name="vendorService">An instance of <see cref="IVendorService"/>..</param>
        public DeliveryAppAccountService(
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            IPictureService pictureService,
            MediaSettings mediaSettings,
            IEmailAccountService emailAccountService,
            ILocalizationService localizationService,
            IQueuedEmailService queuedEmailService,
            EmailAccountSettings emailAccountSettings,
            IRepository<GenericAttribute> genericAttributeRepository, 
            IVendorService vendorService)
        {
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _pictureService = pictureService;
            _mediaSettings = mediaSettings;
            _emailAccountService = emailAccountService;
            _localizationService = localizationService;
            _queuedEmailService = queuedEmailService;
            _emailAccountSettings = emailAccountSettings;
            _genericAttributeRepository = genericAttributeRepository;
            _vendorService = vendorService;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public CustomerAccount GetCustomerAccountByEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentException("InvalidEmail");
            Customer customer = _customerService.GetCustomerByEmail(email);

            if (customer is null) throw new ArgumentException("CustomerNotFound");

            string pictureUrl = "";

            if (customer.VendorId != 0)
            {
                Vendor vendor = _vendorService.GetVendorById(customer.VendorId);

                pictureUrl = vendor != null? _pictureService.GetPictureUrl(vendor.PictureId): _pictureService.GetDefaultPictureUrl();
            }
            else
            {
                pictureUrl = _pictureService.GetPictureUrl(_genericAttributeService.GetAttribute<int>(customer, NopCustomerDefaults.AvatarPictureIdAttribute));
            }
                                                     

            return new CustomerAccount
            {
                CustomerId = customer.Id,
                Email = customer.Email,
                FirstName = _genericAttributeService.GetAttribute<string>(customer, "FirstName"),
                LastName = _genericAttributeService.GetAttribute<string>(customer, "LastName"),
                Birthday = GetCustomerDateOfBirthAsString(customer),
                Phone = _genericAttributeService.GetAttribute<string>(customer, "Phone"),
                PictureUrl = pictureUrl,
                VendorId = customer.VendorId
            };
        }

        ///<inheritdoc/>
        public string GetCustomerDateOfBirthAsString(Customer customer)
        {
            var dateOfBirth = _genericAttributeService.GetAttribute<DateTime?>(customer, "DateOfBirth");
            if (!dateOfBirth.HasValue) return string.Empty;
            return dateOfBirth.Value.ToString("yyyy/MM/dd");
        }

        /// <summary>
        /// Inserts an email with verification code to the email's queue
        /// </summary>
        /// <param name="customer">The customer who receives the message.</param>
        /// <param name="codeValue">The verification code that is sent in the message.</param>
        /// <returns>An instance of <see cref="SendEmailResult"/>.</returns>
        public SendEmailResult SendVerificationCode(Customer customer, int codeValue)
        {
            try
            {
                if (customer is null)
                    throw new ArgumentException("CustomerNotFound");
                if (string.IsNullOrWhiteSpace(customer.Email))
                    throw new ArgumentException("CustomerEmailIsEmpty");
                if (!CommonHelper.IsValidEmail(customer.Email))
                    throw new ArgumentException("CustomerEmailIsNotValid");

                var emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
                if (emailAccount is null)
                    emailAccount = _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
                if (emailAccount is null)
                    throw new ArgumentException("EmailAccountCantBeLoaded");

                var email = new QueuedEmail
                {
                    Priority = QueuedEmailPriority.High,
                    EmailAccountId = emailAccount.Id,
                    FromName = emailAccount.DisplayName,
                    From = emailAccount.Email,
                    ToName = _customerService.GetCustomerFullName(customer),
                    To = customer.Email,
                    Subject = _localizationService.GetResource(Defaults.VerificationCodeMessageSubject),
                    Body = string.Format(codeValue.ToString()),
                    CreatedOnUtc = DateTime.UtcNow
                };

                _queuedEmailService.InsertQueuedEmail(email);

                return new SendEmailResult { Success = true };
            }
            catch (Exception e)
            {
                return new SendEmailResult { Success = false, Message = e.Message };
            }
        }


        /// <summary>
        /// Retrieves a value that indicates if a given phone number is already registered.
        /// </summary>
        /// <param name="phoneNumber">The phone number to verify.</param>
        /// <param name="customerId">A customer id.</param>
        /// <returns>
        /// <see cref="true"/> if the contact is found, also <see cref="true"/> when <paramref name="customerId"/> is provided
        /// and the phone number is found by any other customer id, otherwise <see cref="false"/>.
        /// </returns>
        public bool PhoneNumberAlreadyRegistered(string phoneNumber, int customerId = 0)
        {
            string customerKeyGroup = new Customer().GetType().Name;

            if (customerId != 0)
            {
                return _genericAttributeRepository.Table
                    .FirstOrDefault(attribute => attribute.KeyGroup == customerKeyGroup
                                                 && attribute.Key == "Phone"
                                                 && attribute.EntityId != customerId
                                                 && attribute.Value == phoneNumber) != null;
            }

            return _genericAttributeRepository.Table
                .FirstOrDefault(attribute => attribute.KeyGroup == customerKeyGroup
                                             && attribute.Key == "Phone"
                                             && attribute.Value == phoneNumber) != null;
        }

        public bool deleteAccount(Customer customerToDelete)
        {
            try
            {
                _customerService.DeleteCustomer(customerToDelete);
                Customer validation = _customerService.GetCustomerById(customerToDelete.Id);
                if (validation.Deleted)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
            
        }
        #endregion
    }
}
