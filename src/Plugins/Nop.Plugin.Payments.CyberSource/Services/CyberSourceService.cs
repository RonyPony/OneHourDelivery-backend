using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Models;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Payments.CyberSource.Services
{
    /// <summary>
    /// Service used to log information and process transactions
    /// </summary>
    public class CyberSourceService : ICyberSourceService
    {
        #region Fields

        private readonly IRepository<CyberSourceTransactionLog> _cyberSourceTransactionLogRepository;
        private readonly CyberSourcePaymentSettings _cyberSourcePaymentSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly IEmailAccountService _emailAccountService;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly IStoreService _storeService;
        private readonly IMessageTokenProvider _messageTokenProvider;
        private readonly IEventPublisher _eventPublisher;
        private readonly LocalizationSettings _localizationSettings;
        private readonly IVendorService _vendorService;
        private readonly IAffiliateService _affiliateService;
        private readonly IDeliveryAppPaymentService _deliveryAppPaymentService;

        #endregion

        #region Ctor

        /// <summary>
        ///Initializes a new instance of <see cref="CyberSourceService"/>
        /// </summary>
        /// <param name="cyberSourceTransactionLogRepository"><see cref="CyberSourceTransactionLog"/> repository.</param>
        /// <param name="cyberSourcePaymentSettings">Instance of <see cref="CyberSourcePaymentSettings"/></param>
        /// <param name="addressService">Implementation of <see cref="IAddressService"/></param>
        /// <param name="countryService">Implementation of <see cref="ICountryService"/></param>
        /// <param name="stateProvinceService">Implementation of <see cref="IStateProvinceService"/></param>
        /// <param name="workContext">Implementation of <see cref="IWorkContext"/></param>
        /// <param name="storeContext">Implementation of <see cref="IStoreContext"/></param>
        /// <param name="orderService">Implementation of <see cref="IOrderService"/></param>
        /// <param name="productService">Implementation of <see cref="IProductService"/></param>
        /// <param name="workflowMessageService">Implementation of <see cref="IWorkflowMessageService"/></param>
        /// <param name="messageTemplateService">Implementation of <see cref="IMessageTemplateService"/></param>
        /// <param name="languageService">Implementation of <see cref="ILanguageService"/></param>
        /// <param name="localizationService">Implementation of <see cref="ILocalizationService"/></param>
        /// <param name="emailAccountService">Implementation of <see cref="IEmailAccountService"/></param>
        /// <param name="emailAccountSettings">Instance of <see cref="EmailAccountSettings"/></param>
        /// <param name="storeService">Implementation of <see cref="IStoreService"/></param>
        /// <param name="messageTokenProvider">Implementation of <see cref="IMessageTokenProvider"/></param>
        /// <param name="eventPublisher">Implementation of <see cref="IEventPublisher"/></param>
        /// <param name="localizationSettings">Instance of <see cref="LocalizationSettings"/></param>
        /// <param name="vendorService">Implementation of <see cref="IVendorService"/></param>
        /// <param name="affiliateService">Implementation of <see cref="IAffiliateService"/></param>
        /// <param name="deliveryAppPaymentService">Implementation of <see cref="IDeliveryAppPaymentService"/></param>
        public CyberSourceService(IRepository<CyberSourceTransactionLog> cyberSourceTransactionLogRepository,
            CyberSourcePaymentSettings cyberSourcePaymentSettings, IAddressService addressService,
            ICountryService countryService, IStateProvinceService stateProvinceService,
            IWorkContext workContext, IStoreContext storeContext, IOrderService orderService,
            IProductService productService,
            IWorkflowMessageService workflowMessageService, IMessageTemplateService messageTemplateService,
            ILanguageService languageService, ILocalizationService localizationService,
            IEmailAccountService emailAccountService, EmailAccountSettings emailAccountSettings,
            IStoreService storeService, IMessageTokenProvider messageTokenProvider,
            IEventPublisher eventPublisher, LocalizationSettings localizationSettings, IVendorService vendorService,
            IAffiliateService affiliateService, IDeliveryAppPaymentService deliveryAppPaymentService)
        {
            _cyberSourceTransactionLogRepository = cyberSourceTransactionLogRepository;
            _cyberSourcePaymentSettings = cyberSourcePaymentSettings;
            _addressService = addressService;
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _workContext = workContext;
            _storeContext = storeContext;
            _orderService = orderService;
            _productService = productService;
            _workflowMessageService = workflowMessageService;
            _messageTemplateService = messageTemplateService;
            _languageService = languageService;
            _localizationService = localizationService;
            _emailAccountService = emailAccountService;
            _emailAccountSettings = emailAccountSettings;
            _storeService = storeService;
            _messageTokenProvider = messageTokenProvider;
            _eventPublisher = eventPublisher;
            _localizationSettings = localizationSettings;
            _vendorService = vendorService;
            _affiliateService = affiliateService;
            _deliveryAppPaymentService = deliveryAppPaymentService;
        }

        #endregion

        #region Utilities

        private int AddProductsToRemotePost(RemotePost post, int orderId)
        {
            IList<OrderItem> orderItems = _orderService.GetOrderItems(orderId);

            for (int x = 0; x < orderItems.Count; x++)
            {
                OrderItem orderItem = orderItems[x];
                Product product = _productService.GetProductById(orderItem.ProductId);
                decimal productTaxAmount = orderItem.UnitPriceInclTax - orderItem.UnitPriceExclTax;

                post.Add($"item_{x}_quantity", orderItem.Quantity.ToString());
                post.Add($"item_{x}_sku", product.Sku);
                post.Add($"item_{x}_name", product.Name);
                post.Add($"item_{x}_code", product.Id.ToString());
                post.Add($"item_{x}_unit_price", orderItem.UnitPriceExclTax.ToString("0.00"));
                post.Add($"item_{x}_tax_amount", productTaxAmount.ToString("0.00"));
            }

            return orderItems.Count;
        }

        private string GetUUID() => Guid.NewGuid().ToString();

        private string GetUtcDateTime() => DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

        private IList<MessageTemplate> GetActiveMessageTemplates(string messageTemplateName, int storeId)
        {
            //get message templates by the name
            var messageTemplates = _messageTemplateService.GetMessageTemplatesByName(messageTemplateName, storeId);

            //no template found
            if (!messageTemplates?.Any() ?? true)
                return new List<MessageTemplate>();

            //filter active templates
            messageTemplates = messageTemplates.Where(messageTemplate => messageTemplate.IsActive).ToList();

            return messageTemplates;
        }

        private EmailAccount GetEmailAccountOfMessageTemplate(MessageTemplate messageTemplate, int languageId)
        {
            var emailAccountId = _localizationService.GetLocalized(messageTemplate, mt => mt.EmailAccountId, languageId);
            //some 0 validation (for localizable "Email account" dropdownlist which saves 0 if "Standard" value is chosen)
            if (emailAccountId == 0)
                emailAccountId = messageTemplate.EmailAccountId;

            var emailAccount = (_emailAccountService.GetEmailAccountById(emailAccountId) ?? _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId)) ??
                               _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
            return emailAccount;
        }

        private int EnsureLanguageIsActive(int languageId, int storeId)
        {
            //load language by specified ID
            var language = _languageService.GetLanguageById(languageId);

            if (language == null || !language.Published)
            {
                //load any language from the specified store
                language = _languageService.GetAllLanguages(storeId: storeId).FirstOrDefault();
            }

            if (language == null || !language.Published)
            {
                //load any language
                language = _languageService.GetAllLanguages().FirstOrDefault();
            }

            if (language == null)
                throw new Exception("No active language could be loaded");

            return language.Id;
        }

        private void AddOrderNote(Order order, string note)
        {
            _orderService.InsertOrderNote(new OrderNote
            {
                OrderId = order.Id,
                Note = note,
                DisplayToCustomer = false,
                CreatedOnUtc = DateTime.UtcNow
            });
        }

        private IList<Vendor> GetVendorsInOrder(Order order)
        {
            var pIds = _orderService.GetOrderItems(order.Id).Select(x => x.ProductId).ToArray();

            return _vendorService.GetVendorsByProductIds(pIds);
        }

        private IList<int> SendOrderPaymentDeclinedNotification(Order order, int languageId, string orderPaymentDeclinedTemplateName,
            Vendor vendor = null, string attachmentFilePath = null, string attachmentFileName = null)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            bool isCustomerNotification = orderPaymentDeclinedTemplateName.Equals(CyberSourceDefaults.OrderNotPaidCustomerNotificationTemplateName);
            bool isVendorNotification = orderPaymentDeclinedTemplateName.Equals(CyberSourceDefaults.OrderNotPaidVendorNotificationTemplateName);
            bool isAffiliateNotification = orderPaymentDeclinedTemplateName.Equals(CyberSourceDefaults.OrderNotPaidAffiliateNotificationTemplateName);

            if (isVendorNotification && vendor == null)
                throw new ArgumentNullException(nameof(vendor));

            var affiliate = _affiliateService.GetAffiliateById(order.AffiliateId);

            if (isAffiliateNotification && affiliate == null)
                throw new ArgumentNullException(nameof(affiliate));

            var store = _storeService.GetStoreById(order.StoreId) ?? _storeContext.CurrentStore;
            languageId = EnsureLanguageIsActive(languageId, store.Id);

            var messageTemplates = GetActiveMessageTemplates(orderPaymentDeclinedTemplateName, store.Id);
            if (!messageTemplates.Any())
                return new List<int>();

            //tokens
            var commonTokens = new List<Token>();
            _messageTokenProvider.AddOrderTokens(commonTokens, order, languageId, vendor?.Id ?? 0);
            _messageTokenProvider.AddCustomerTokens(commonTokens, order.CustomerId);

            return messageTemplates.Select(messageTemplate =>
            {
                //email account
                var emailAccount = GetEmailAccountOfMessageTemplate(messageTemplate, languageId);

                var tokens = new List<Token>(commonTokens);
                _messageTokenProvider.AddStoreTokens(tokens, store, emailAccount);

                //event notification
                _eventPublisher.MessageTokensAdded(messageTemplate, tokens);

                var toEmail = string.Empty;
                var toName = string.Empty;

                if (isCustomerNotification)
                {
                    var billingAddress = _addressService.GetAddressById(order.BillingAddressId);
                    toEmail = billingAddress.Email;
                    toName = $"{billingAddress.FirstName} {billingAddress.LastName}";
                }
                else if (isVendorNotification)
                {
                    toEmail = vendor.Email;
                    toName = vendor.Name;
                }
                else if (isAffiliateNotification)
                {
                    var affiliateAddress = _addressService.GetAddressById(affiliate.AddressId);
                    toEmail = affiliateAddress.Email;
                    toName = $"{affiliateAddress.FirstName} {affiliateAddress.LastName}";
                }
                else
                {
                    toEmail = emailAccount.Email;
                    toName = emailAccount.DisplayName;
                }

                return _workflowMessageService.SendNotification(messageTemplate, emailAccount, languageId, tokens, toEmail, toName,
                    attachmentFilePath, attachmentFileName);
            }).ToList();
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void ProcessTransaction(Order order, CyberSourcePaymentInfoModel paymentInfoModel)
        {
            const int MAX_ADDRESS1_FIELD_LENGTH = 40;
            var post = new RemotePost
            {
                FormName = "CyberSource",
                Url = _cyberSourcePaymentSettings.PaymentPageUrl,
                Method = "POST"
            };

            Address billingAddress = _addressService.GetAddressById(order.BillingAddressId);
            Country billingCountry = _countryService.GetCountryById(billingAddress.CountryId.GetValueOrDefault());
            StateProvince billingState = _stateProvinceService.GetStateProvinceById(billingAddress.StateProvinceId.GetValueOrDefault());

            // General information
            post.Add("access_key", _cyberSourcePaymentSettings.AccessKey);
            post.Add("profile_id", _cyberSourcePaymentSettings.MerchantId);
            post.Add("override_custom_receipt_page", _cyberSourcePaymentSettings.RedirectUrl);
            post.Add("transaction_uuid", GetUUID());
            post.Add("unsigned_field_names", "card_type,card_number,card_expiry_date,card_cvn");
            post.Add("signed_date_time", GetUtcDateTime());
            post.Add("locale", _cyberSourcePaymentSettings.Locale);
            post.Add("transaction_type", "sale");
            post.Add("reference_number", order.Id.ToString());

            //Card details
            post.Add("payment_method", CyberSourceDefaults.PaymentMethod);
            post.Add("card_type", paymentInfoModel.CardType);
            post.Add("card_number", paymentInfoModel.CardNumber);
            post.Add("card_expiry_date", $"{paymentInfoModel.ExpirationMonth}-{paymentInfoModel.ExpirationYear}");
            post.Add("card_cvn", paymentInfoModel.Cvv);

            // Billing details
            post.Add("bill_to_address_line1", billingAddress.Address1.Length > MAX_ADDRESS1_FIELD_LENGTH ? billingAddress.Address1.Substring(0, MAX_ADDRESS1_FIELD_LENGTH) : billingAddress.Address1);
            post.Add("bill_to_address_city", billingAddress.City);
            post.Add("bill_to_address_postal_code", billingAddress.ZipPostalCode);
            post.Add("bill_to_address_state", billingState?.Abbreviation ?? "");
            post.Add("bill_to_address_country", billingCountry?.TwoLetterIsoCode ?? "");
            post.Add("bill_to_email", billingAddress?.Email);
            post.Add("bill_to_forename", billingAddress.FirstName);
            post.Add("bill_to_surname", billingAddress.LastName);
            post.Add("bill_to_phone", billingAddress.PhoneNumber);

            // Device and store info
            post.Add("device_fingerprint_id", paymentInfoModel.DeviceFingerprintId);
            post.Add("merchant_defined_data2", _storeContext.CurrentStore.Name);
            post.Add("merchant_defined_data3", _storeContext.CurrentStore.Url);
            post.Add("customer_ip_address", order.CustomerIp);

            // Payment and taxes info
            post.Add("tax_indicator", CyberSourceDefaults.DefaultTaxIndicator);
            post.Add("user_po", order.Id.ToString());
            post.Add("tax_amount", order.OrderTax.ToString("0.00"));
            post.Add("currency", _cyberSourcePaymentSettings.Currency);

            // Order items
            int orderItemsCount = AddProductsToRemotePost(post, order.Id);

            // Order items total
            post.Add("amount", order.OrderTotal.ToString("0.00"));
            post.Add("line_item_count", orderItemsCount.ToString());

            // Signed fields (all of them)
            post.Add("signed_field_names", $"signed_field_names,{string.Join(",", post.Params.AllKeys.Where(key => !key.Contains("card_")))}");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var key in post.Params.AllKeys)
            {
                parameters.Add(key, post.Params[key]);
            }

            // Sign fields
            post.Add("signature", Security.Sign(parameters, _cyberSourcePaymentSettings.SecretKey));

            post.Post();
        }

        /// <inheritdoc/>
        public void ProcessTransactionWithToken(Order order,CyberSourceTokenPaymentInfoModel paymentInfoModel)
        {
            const int MAX_ADDRESS1_FIELD_LENGTH = 40;
            var post = new RemotePost
            {
                FormName = "CyberSource",
                Url = _cyberSourcePaymentSettings.PaymentPageUrl,
                Method = "POST"
            };

            var tokenInformation = _deliveryAppPaymentService.CallAlreadyCustomerRegisteredCard(paymentInfoModel.CustomerId, paymentInfoModel.CardLastFour
                                                                                               , paymentInfoModel.CardExpirationDate);

            // General information
            post.Add("access_key", _cyberSourcePaymentSettings.AccessKey);
            post.Add("profile_id", _cyberSourcePaymentSettings.MerchantId);
            post.Add("override_custom_receipt_page", _cyberSourcePaymentSettings.RedirectUrl);
            post.Add("reference_number", order.Id.ToString());
            post.Add("payment_token", tokenInformation.Token);
            post.Add("transaction_uuid", GetUUID());
            post.Add("consumer_id", paymentInfoModel.CustomerId.ToString());
            post.Add("signed_date_time", GetUtcDateTime());
            post.Add("locale", _cyberSourcePaymentSettings.Locale);
            post.Add("transaction_type", "sale");

            // Payment and taxes info
            post.Add("currency", _cyberSourcePaymentSettings.Currency);

            // Order items total
            post.Add("amount", order.OrderTotal.ToString("0.00"));

            // Signed fields (all of them)
            post.Add("signed_field_names", $"signed_field_names,{string.Join(",", post.Params.AllKeys.Where(key => !key.Contains("card_")))}");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var key in post.Params.AllKeys)
            {
                parameters.Add(key, post.Params[key]);
            }

            // Sign fields
            post.Add("signature", Security.Sign(parameters, _cyberSourcePaymentSettings.SecretKey));

            post.Post();
        }

        ///<inheritdoc/>
        public void Log(CyberSourceTransactionLog logEntry)
        {
            if (logEntry == null)
                throw new ArgumentException("Error trying to log entry to CyberSource transaction log: logEntry cannot be null");

            _cyberSourceTransactionLogRepository.Insert(logEntry);
        }

        ///<inheritdoc/>
        public void SendPaymentDeclinedNotificationsAndSaveNotes(Order order)
        {
            if (order is null)
                throw new ArgumentNullException("");

            var orderPaymentDeclinedStoreOwnerNotificationQueuedEmailIds = SendOrderPaymentDeclinedNotification(order, _localizationSettings.DefaultAdminLanguageId, CyberSourceDefaults.OrderNotPaidStoreOwnerNotificationTemplateName);
            if (orderPaymentDeclinedStoreOwnerNotificationQueuedEmailIds.Any())
                AddOrderNote(order, $"\"Order payment declined\" email (to store owner) has been queued. Queued email identifiers: {string.Join(", ", orderPaymentDeclinedStoreOwnerNotificationQueuedEmailIds)}.");

            var orderPlacedCustomerNotificationQueuedEmailIds = SendOrderPaymentDeclinedNotification(order, order.CustomerLanguageId, CyberSourceDefaults.OrderNotPaidCustomerNotificationTemplateName);
            if (orderPlacedCustomerNotificationQueuedEmailIds.Any())
                AddOrderNote(order, $"\"Order payment declined\" email (to customer) has been queued. Queued email identifiers: {string.Join(", ", orderPlacedCustomerNotificationQueuedEmailIds)}.");

            var vendors = GetVendorsInOrder(order);
            foreach (var vendor in vendors)
            {
                var orderPlacedVendorNotificationQueuedEmailIds = SendOrderPaymentDeclinedNotification(order, _localizationSettings.DefaultAdminLanguageId, CyberSourceDefaults.OrderNotPaidCustomerNotificationTemplateName, vendor: vendor);
                if (orderPlacedVendorNotificationQueuedEmailIds.Any())
                    AddOrderNote(order, $"\"Order payment declined\" email (to vendor) has been queued. Queued email identifiers: {string.Join(", ", orderPlacedVendorNotificationQueuedEmailIds)}.");
            }

            if (order.AffiliateId == 0)
                return;

            var orderPlacedAffiliateNotificationQueuedEmailIds = SendOrderPaymentDeclinedNotification(order, _localizationSettings.DefaultAdminLanguageId, CyberSourceDefaults.OrderNotPaidAffiliateNotificationTemplateName);
            if (orderPlacedAffiliateNotificationQueuedEmailIds.Any())
                AddOrderNote(order, $"\"Order payment declined\" email (to affiliate) has been queued. Queued email identifiers: {string.Join(", ", orderPlacedAffiliateNotificationQueuedEmailIds)}.");
        }

        /// <inheritdoc/>
        public void RegisterNewCard(CyberSourceRegisterSingleCard paymentInfoModel)
        {
            const int MAX_ADDRESS1_FIELD_LENGTH = 40;
            var post = new RemotePost
            {
                FormName = "CyberSource",
                Url = _cyberSourcePaymentSettings.PaymentPageUrl,
                Method = "POST"  
            };

            Country country = _countryService.GetCountryById(paymentInfoModel.CountryId);

            // General information
            post.Add("reference_number", paymentInfoModel.CustomerId.ToString());
            post.Add("transaction_type", "create_payment_token");
            post.Add("locale", _cyberSourcePaymentSettings.Locale);
            post.Add("access_key", _cyberSourcePaymentSettings.AccessKey);
            post.Add("override_custom_receipt_page", _cyberSourcePaymentSettings.RedirectUrl);
            post.Add("profile_id", _cyberSourcePaymentSettings.MerchantId);
            post.Add("transaction_uuid", GetUUID());
            post.Add("signed_date_time", GetUtcDateTime());
            post.Add("unsigned_field_names", "card_type,card_number,card_expiry_date,card_cvn");
            
            //Card details
            post.Add("payment_method", CyberSourceDefaults.PaymentMethod);
            post.Add("card_type", paymentInfoModel.CardType);
            post.Add("card_number", paymentInfoModel.CardNumber);
            post.Add("card_expiry_date", $"{paymentInfoModel.CardExpirationMonth}-{paymentInfoModel.CardExpirationYear}");
            post.Add("card_cvn", paymentInfoModel.CVV);

            // Billing details
            post.Add("bill_to_address_line1", paymentInfoModel.Address);
            post.Add("bill_to_address_city", paymentInfoModel.City);
            post.Add("bill_to_address_postal_code", paymentInfoModel.PostalCode);
            post.Add("bill_to_address_state", "");
            post.Add("bill_to_address_country", country.TwoLetterIsoCode);
            post.Add("bill_to_email", paymentInfoModel.Email);
            post.Add("bill_to_forename", paymentInfoModel.CardHolderName);
            post.Add("bill_to_surname", paymentInfoModel.CardHolderLastName);

            // Payment and taxes info
            post.Add("currency", _cyberSourcePaymentSettings.Currency);

            // Order items total
            post.Add("amount", "0.00");

            // Signed fields (all of them)
            post.Add("signed_field_names", $"signed_field_names,{string.Join(",", post.Params.AllKeys.Where(key => !key.Contains("card_")))}");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var key in post.Params.AllKeys)
            {
                parameters.Add(key, post.Params[key]);
            }

            // Sign fields
            post.Add("signature", Security.Sign(parameters, _cyberSourcePaymentSettings.SecretKey));

            post.Post();
        }
        #endregion
    }
}
