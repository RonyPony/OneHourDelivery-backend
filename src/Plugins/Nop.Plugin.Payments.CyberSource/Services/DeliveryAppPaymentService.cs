using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Payments.CyberSource.Domains;
using Nop.Plugin.Payments.CyberSource.Helpers;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Payments.CyberSource.Services
{
    /// <summary>
    /// Responsible of payment interactions with cybersource API.
    /// </summary>
    public sealed class DeliveryAppPaymentService : IDeliveryAppPaymentService
    {
        #region Fields

        private readonly CyberSourcePaymentSettings _cybersourceConfigurationSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<CustomerPaymentTokenMapping> _customerPaymentTokenMappingRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="DeliveryAppPaymentService"/>
        /// </summary>
        /// <param name="cybersourceConfigurationSettings"></param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/></param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/></param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        /// <param name="productService">An implementation of <see cref="IProductService"/></param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/></param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/></param>
        /// <param name="customerPaymentTokenMappingRepository">An implementation of <see cref="IRepository{CustomerPaymentTokenMapping}"/></param>
        public DeliveryAppPaymentService(
            CyberSourcePaymentSettings cybersourceConfigurationSettings,
            IAddressService addressService,
            ICountryService countryService,
            IOrderService orderService,
            IProductService productService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext,
            IRepository<CustomerPaymentTokenMapping> customerPaymentTokenMappingRepository)
        {
            _cybersourceConfigurationSettings = cybersourceConfigurationSettings;
            _addressService = addressService;
            _countryService = countryService;
            _orderService = orderService;
            _productService = productService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
            _customerPaymentTokenMappingRepository = customerPaymentTokenMappingRepository;
        }

        #endregion

        #region Utilities

        private int GetTime()
        {
            var time = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            return (int)(time.TotalMilliseconds + 0.5);
        }

        private string GetUtcDateTime() => DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");

        private string GetUUID() => Guid.NewGuid().ToString();

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

        #endregion

        #region Methods

        ///<inheritdoc/>
        public RemotePost GetPostForPaymentForm(int orderId)
        {
            if (orderId == 0)
            {
                throw new Exception("Please specify the order");
            }
            Order order = _orderService.GetOrderById(orderId);
            Address billingAddress = _addressService.GetAddressById(order.BillingAddressId);
            Country billingCountry = _countryService.GetCountryById(billingAddress.CountryId.GetValueOrDefault());
            StateProvince billingState = _stateProvinceService.GetStateProvinceById(billingAddress.StateProvinceId.GetValueOrDefault());

            var post = new RemotePost
            {
                FormName = "cybersource",
                Url = _cybersourceConfigurationSettings.PaymentPageUrl,
                Method = "POST",
                Target = "payment-iframe"
            };

            // General information
            post.Add("access_key", _cybersourceConfigurationSettings.AccessKey);
            post.Add("profile_id", _cybersourceConfigurationSettings.SerialNumber);
            post.Add("override_custom_receipt_page", _cybersourceConfigurationSettings.RedirectUrl);

            post.Add("transaction_uuid", GetUUID());
            post.Add("unsigned_field_names", "card_type,card_number,card_expiry_date,card_cvn");
            post.Add("signed_date_time", GetUtcDateTime());
            post.Add("locale", _cybersourceConfigurationSettings.Locale);
            post.Add("transaction_type", _cybersourceConfigurationSettings.TransactionType);
            post.Add("reference_number", orderId.ToString());

            //Card details
            post.Add("payment_method", "card");
            post.Add("card_type", "");
            post.Add("card_number", "");
            post.Add("card_expiry_date", "");
            post.Add("card_cvn", "");

            // Billing details
            post.Add("bill_to_address_line1", billingAddress.Address1);
            post.Add("bill_to_address_city", billingAddress.City);
            post.Add("bill_to_address_postal_code", billingAddress.ZipPostalCode);
            post.Add("bill_to_address_state", billingState?.Abbreviation ?? "");
            post.Add("bill_to_address_country", billingCountry?.TwoLetterIsoCode ?? "");
            post.Add("bill_to_email", billingAddress?.Email);
            post.Add("bill_to_forename", billingAddress.FirstName);
            post.Add("bill_to_surname", billingAddress.LastName);

            // Payment and taxes info
            post.Add("tax_indicator", "Y");
            post.Add("user_po", order.Id.ToString());
            post.Add("tax_amount", order.OrderTax.ToString("0.00"));
            post.Add("currency", _cybersourceConfigurationSettings.Currency);

            // Order items
            int orderItemsCount = AddProductsToRemotePost(post, order.Id);

            // Order items total
            post.Add("amount", order.OrderTotal.ToString("0.00"));
            post.Add("line_item_count", orderItemsCount.ToString());

            // Device and merchant defined data (MDDs)
            post.Add("device_fingerprint_id", GetTime().ToString());
            post.Add("customer_ip_address", order.CustomerIp);
            post.Add("merchant_defined_data1", _cybersourceConfigurationSettings.MerchantId);
            post.Add("merchant_defined_data2", "WEB");
            post.Add("merchant_defined_data3", order.Id.ToString());
            post.Add("merchant_defined_data4", "DELIVERY");
            post.Add("merchant_defined_data5", _storeContext.CurrentStore.Name);
            post.Add("merchant_defined_data6", "");
            post.Add("merchant_defined_data7", _storeContext.CurrentStore.CompanyName);
            post.Add("merchant_defined_data8", "no");
            post.Add("merchant_defined_data10", "yes");
            post.Add("merchant_defined_data18", "r");
            post.Add("merchant_defined_data19", GetUtcDateTime());
            post.Add("merchant_defined_data21", order.Id.ToString());
            post.Add("merchant_defined_data23", "registered");
            post.Add("merchant_defined_data24", "yes");
            post.Add("merchant_defined_data25", billingAddress.Address1);

            // Signed fields (all of them)
            post.Add("signed_field_names", $"signed_field_names,{string.Join(",", post.Params.AllKeys.Where(key => !key.Contains("card_")))}");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var key in post.Params.AllKeys)
            {
                parameters.Add(key, post.Params[key]);
            }

            // Sign fields
            post.Add("signature", Security.Sign(parameters, _cybersourceConfigurationSettings.SecretKey));

            return post;
        }

        ///<inheritdoc/>
        public void InsertTokenPayment(CustomerPaymentTokenMapping customerPayment)
        {
            if (customerPayment is null)
                throw new ArgumentException("InvalidCustomerPaymentRequest");

            _customerPaymentTokenMappingRepository.Insert(customerPayment);
        }

        ///<inheritdoc/>
        public CustomerPaymentTokenMapping CallAlreadyCustomerRegisteredCard(int customerId, string cardDigits, string cardExpirationDate)
        {
            if (string.IsNullOrWhiteSpace(cardDigits))
                throw new ArgumentException("InvalidPaymentToken");

            var tokenInformation = (_customerPaymentTokenMappingRepository
                 .Table
                 .Where(token => token.CustomerId == customerId && token.CardLastFourDigits.Contains(cardDigits)
                 && token.CardExpirationDate.Equals(cardExpirationDate))).FirstOrDefault();

            if (tokenInformation == null)
                throw new ArgumentException("InvalidCardInformation");

            return tokenInformation;
        }

        ///<inheritdoc/>
        public bool ValidateCardAlreadyExist(int customerId, string cardLastFourDigits, string cardExpirationDate)
        {
            bool exist = _customerPaymentTokenMappingRepository.Table.Any(x=> x.CustomerId.Equals(customerId)
            && x.CardLastFourDigits.Contains(cardLastFourDigits) && x.CardExpirationDate.Equals(cardExpirationDate));

            return exist;
        }

        ///<inheritdoc/>
        public IEnumerable<RegisteredCard> GetCustomerRegisteredCards(int customerId)
        {
            List<CustomerPaymentTokenMapping> customerRegisteredCards = _customerPaymentTokenMappingRepository.Table
                                                                        .Where(x => x.CustomerId.Equals(customerId)).ToList();

            return customerRegisteredCards.Select(card =>
                new RegisteredCard
                {
                    CardType = card.CardType,
                    CardExpirationDate = card.CardExpirationDate,
                    CardLastFourDigits = card.CardLastFourDigits,
                    IsDefaultPaymentMethod = card.IsDefaultPaymentMethod,
                    IsExpired = IsExpiredCard(card.CardExpirationDate)
                }).ToList();
        }

        ///<inheritdoc/>
        public void DeleteRegisteredCard(int customerId, RegisteredCard cardToDelete)
        {
            CustomerPaymentTokenMapping foundCard = _customerPaymentTokenMappingRepository.Table
                                                    .Where(x => x.CustomerId.Equals(customerId) && x.CardLastFourDigits.Equals(cardToDelete.CardLastFourDigits)
                                                            && x.CardExpirationDate.Equals(cardToDelete.CardExpirationDate)).FirstOrDefault();

            _customerPaymentTokenMappingRepository.Delete(foundCard);
        }

        /// <summary>
        /// Validates whether or a registered card is expired
        /// </summary>
        /// <param name="expirationDate">Expiration date of the card pending to validate</param>
        /// <returns></returns>
        private bool IsExpiredCard(string expirationDate)
        {
            DateTime now = DateTime.Now;
            string[] splittedExpirationDate = expirationDate.Split('-');
            int expirationMonth = int.Parse(splittedExpirationDate[0]);
            int expirationYear = int.Parse(splittedExpirationDate[1]);

            if (expirationMonth < now.Month && expirationYear <= now.Year)
                return true;

            return false;
        }

        #endregion
    }
}
