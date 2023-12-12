using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domain;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DeliveryAppPaymentService : IDeliveryAppPaymentService
    {
        #region Fields

        private readonly DeliveryAppBackendConfigurationSettings _deliveryAppBackendConfigurationSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public DeliveryAppPaymentService(
            DeliveryAppBackendConfigurationSettings deliveryAppBackendConfigurationSettings,
            IAddressService addressService,
            ICountryService countryService,
            IOrderService orderService,
            IProductService productService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext)
        {
            _deliveryAppBackendConfigurationSettings = deliveryAppBackendConfigurationSettings;
            _addressService = addressService;
            _countryService = countryService;
            _orderService = orderService;
            _productService = productService;
            _stateProvinceService = stateProvinceService;
            _storeContext = storeContext;
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
            Order order = _orderService.GetOrderById(orderId);
            Address billingAddress = _addressService.GetAddressById(order.BillingAddressId);
            Country billingCountry = _countryService.GetCountryById(billingAddress.CountryId.GetValueOrDefault());
            StateProvince billingState = _stateProvinceService.GetStateProvinceById(billingAddress.StateProvinceId.GetValueOrDefault());

            var post = new RemotePost
            {
                FormName = "cybersource",
                Url = _deliveryAppBackendConfigurationSettings.PaymentPageUrl,
                Method = "POST",
                Target = "payment-iframe"
            };

            // General information
            post.Add("access_key", _deliveryAppBackendConfigurationSettings.AccessKey);
            post.Add("profile_id", _deliveryAppBackendConfigurationSettings.ProfileId);
            post.Add("override_custom_receipt_page", _deliveryAppBackendConfigurationSettings.RedirectUrl);
            post.Add("transaction_uuid", GetUUID());
            post.Add("unsigned_field_names", "card_type,card_number,card_expiry_date,card_cvn");
            post.Add("signed_date_time", GetUtcDateTime());
            post.Add("locale", _deliveryAppBackendConfigurationSettings.Locale);
            post.Add("transaction_type", "sale");
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
            post.Add("currency", _deliveryAppBackendConfigurationSettings.Currency);

            // Order items
            int orderItemsCount = AddProductsToRemotePost(post, order.Id);

            // Order items total
            post.Add("amount", order.OrderTotal.ToString("0.00"));
            post.Add("line_item_count", orderItemsCount.ToString());

            // Device and merchant defined data (MDDs)
            post.Add("device_fingerprint_id", GetTime().ToString());
            post.Add("customer_ip_address", order.CustomerIp);
            post.Add("merchant_defined_data1", _deliveryAppBackendConfigurationSettings.MerchantId); // Merchant ID
            post.Add("merchant_defined_data2", "WEB"); // Canal de ingreso (callcenter, web, mobile)
            post.Add("merchant_defined_data3", order.Id.ToString()); // Business ID (ID que brinda el comercio al momento de la compra)
            post.Add("merchant_defined_data4", "DELIVERY"); // Categoria de verticales (retail)
            post.Add("merchant_defined_data5", _storeContext.CurrentStore.Name); // Nombre del Comercio (Razon comercial)
            post.Add("merchant_defined_data6", ""); // What category do the product(s) belong to (servicio que ofrecen en la compra)
            post.Add("merchant_defined_data7", _storeContext.CurrentStore.CompanyName); // ID Document(si no mantiene este datos pueden colocar el email, RUC o nombre del cliente) 
            post.Add("merchant_defined_data8", "no"); // Si la compra la realizo por medio de un Tercero (SI/NO)
            post.Add("merchant_defined_data10", "yes"); // Was the Email entered manually or copied and pasted? (Yes or No)
            post.Add("merchant_defined_data18", "r"); // Type of delivery address (r = Residencial c = Commercial)
            post.Add("merchant_defined_data19", GetUtcDateTime()); // Fecha de Transacción
            post.Add("merchant_defined_data21", order.Id.ToString()); // Nro. de Factura
            post.Add("merchant_defined_data23", "registered"); // Registered or Guest Account
            post.Add("merchant_defined_data24", "yes"); // token registration (yes or no)
            post.Add("merchant_defined_data25", billingAddress.Address1); // Direccion de entrega de compra

            // Signed fields (all of them)
            post.Add("signed_field_names", $"signed_field_names,{string.Join(",", post.Params.AllKeys.Where(key => !key.Contains("card_")))}");

            IDictionary<string, string> parameters = new Dictionary<string, string>();

            foreach (var key in post.Params.AllKeys)
            {
                parameters.Add(key, post.Params[key]);
            }

            // Sign fields
            post.Add("signature", Security.Sign(parameters, _deliveryAppBackendConfigurationSettings.SecretKey));

            return post;
        }

        #endregion
    }
}
