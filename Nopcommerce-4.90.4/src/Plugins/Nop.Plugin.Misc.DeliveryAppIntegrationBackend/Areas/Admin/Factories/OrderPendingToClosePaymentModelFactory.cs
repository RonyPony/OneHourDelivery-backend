using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Areas.Admin.Models.Common;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Areas.Admin.Models.Reports;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IStoreService = Nop.Services.Stores.IStoreService;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the order pending to close payment model factory implementation.
    /// </summary>
    public partial class OrderPendingToClosePaymentModelFactory : IOrderPendingToClosePaymentModelFactory
    {
        #region Fields

        private readonly AddressSettings _addressSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAddressAttributeModelFactory _addressAttributeModelFactory;
        private readonly IAddressService _addressService;
        private readonly IAffiliateService _affiliateService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDeliveryAppBaseAdminModelFactory _deliveryAppBaseAdminModelFactory;
        private readonly IDiscountService _discountService;
        private readonly IDownloadService _downloadService;
        private readonly IGiftCardService _giftCardService;
        private readonly ILocalizationService _localizationService;
        private readonly IMeasureService _measureService;
        private readonly IOrderReportService _orderReportService;
        private readonly IOrderPendingToPayReportServices _orderPendingToPayReportServices;
        private readonly IOrderPendingToClosePaymentService _orderPendingToClosePaymentService;
        private readonly IPaymentPluginManager _paymentPluginManager;
        private readonly IPictureService _pictureService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IRewardPointService _rewardPointService;
        private readonly IShipmentService _shipmentService;
        private readonly IShippingService _shippingService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreService _storeService;
        private readonly ITaxService _taxService;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private readonly MeasureSettings _measureSettings;
        private readonly OrderSettings _orderSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly IUrlRecordService _urlRecordService;
        private readonly TaxSettings _taxSettings;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IOrderService _orderService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDeliveryAppOrderReportService _deliverAppOrderReportService;
        private readonly IRepository<OrderPaymentCollectionStatus> _orderPaymentCollectionStatusRepository;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;

        #endregion

        #region Ctor

        public OrderPendingToClosePaymentModelFactory(AddressSettings addressSettings,
            CatalogSettings catalogSettings,
            CurrencySettings currencySettings,
            IActionContextAccessor actionContextAccessor,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAddressAttributeModelFactory addressAttributeModelFactory,
            IAddressService addressService,
            IAffiliateService affiliateService,
            IBaseAdminModelFactory baseAdminModelFactory,
            ICountryService countryService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IDeliveryAppBaseAdminModelFactory deliveryAppBaseAdminModelFactory,
            IDiscountService discountService,
            IDownloadService downloadService,
            IEncryptionService encryptionService,
            IGiftCardService giftCardService,
            ILocalizationService localizationService,
            IMeasureService measureService,
            IOrderProcessingService orderProcessingService,
            IOrderReportService orderReportService,
            IOrderPendingToPayReportServices orderPendingToPayReportServices,
            IOrderPendingToClosePaymentService orderPendingToClosePaymentService,
            IPaymentPluginManager paymentPluginManager,
            IPictureService pictureService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IProductAttributeService productAttributeService,
            IProductService productService,
            IReturnRequestService returnRequestService,
            IRewardPointService rewardPointService,
            IShipmentService shipmentService,
            IShippingService shippingService,
            IStateProvinceService stateProvinceService,
            IStoreService storeService,
            ITaxService taxService,
            IUrlHelperFactory urlHelperFactory,
            IVendorService vendorService,
            IWorkContext workContext,
            MeasureSettings measureSettings,
            OrderSettings orderSettings,
            ShippingSettings shippingSettings,
            IUrlRecordService urlRecordService,
            TaxSettings taxSettings,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IOrderService orderService,
            IGenericAttributeService genericAttributeService,
            IDeliveryAppOrderReportService deliverAppOrderReportService,
            IRepository<OrderPaymentCollectionStatus> orderPaymentCollectionStatusRepository,
            ICheckoutAttributeService checkoutAttributeService,
            ICheckoutAttributeParser checkoutAttributeParser)
        {
            _addressSettings = addressSettings;
            _catalogSettings = catalogSettings;
            _currencySettings = currencySettings;
            _actionContextAccessor = actionContextAccessor;
            _addressAttributeFormatter = addressAttributeFormatter;
            _addressAttributeModelFactory = addressAttributeModelFactory;
            _addressService = addressService;
            _affiliateService = affiliateService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _countryService = countryService;
            _currencyService = currencyService;
            _customerService = customerService;
            _dateTimeHelper = dateTimeHelper;
            _deliveryAppBaseAdminModelFactory = deliveryAppBaseAdminModelFactory;
            _discountService = discountService;
            _downloadService = downloadService;
            _giftCardService = giftCardService;
            _localizationService = localizationService;
            _measureService = measureService;
            _orderReportService = orderReportService;
            _orderPendingToPayReportServices = orderPendingToPayReportServices;
            _orderPendingToClosePaymentService = orderPendingToClosePaymentService;
            _paymentPluginManager = paymentPluginManager;
            _pictureService = pictureService;
            _priceCalculationService = priceCalculationService;
            _priceFormatter = priceFormatter;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _returnRequestService = returnRequestService;
            _rewardPointService = rewardPointService;
            _shipmentService = shipmentService;
            _shippingService = shippingService;
            _stateProvinceService = stateProvinceService;
            _storeService = storeService;
            _taxService = taxService;
            _urlHelperFactory = urlHelperFactory;
            _vendorService = vendorService;
            _workContext = workContext;
            _measureSettings = measureSettings;
            _orderSettings = orderSettings;
            _shippingSettings = shippingSettings;
            _urlRecordService = urlRecordService;
            _taxSettings = taxSettings;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _orderService = orderService;
            _genericAttributeService = genericAttributeService;
            _deliverAppOrderReportService = deliverAppOrderReportService;
            _orderPaymentCollectionStatusRepository = orderPaymentCollectionStatusRepository;
            _checkoutAttributeService = checkoutAttributeService;
            _checkoutAttributeParser = checkoutAttributeParser;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare address model
        /// </summary>
        /// <param name="model">Address model</param>
        /// <param name="address">Address</param>
        protected virtual void PrepareAddressModel(AddressModel model, Address address)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.FormattedCustomAddressAttributes = _addressAttributeFormatter.FormatAttributes(address.CustomAttributes);

            //set some of address fields as enabled and required
            model.FirstNameEnabled = true;
            model.FirstNameRequired = true;
            model.LastNameEnabled = true;
            model.LastNameRequired = true;
            model.EmailEnabled = true;
            model.EmailRequired = true;
            model.CompanyEnabled = _addressSettings.CompanyEnabled;
            model.CompanyRequired = _addressSettings.CompanyRequired;
            model.CountryEnabled = _addressSettings.CountryEnabled;
            model.CountryRequired = _addressSettings.CountryEnabled;
            model.StateProvinceEnabled = _addressSettings.StateProvinceEnabled;
            model.CountyEnabled = _addressSettings.CountyEnabled;
            model.CountyRequired = _addressSettings.CountyRequired;
            model.CityEnabled = _addressSettings.CityEnabled;
            model.CityRequired = _addressSettings.CityRequired;
            model.StreetAddressEnabled = _addressSettings.StreetAddressEnabled;
            model.StreetAddressRequired = _addressSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = _addressSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = _addressSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = _addressSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = _addressSettings.ZipPostalCodeRequired;
            model.PhoneEnabled = _addressSettings.PhoneEnabled;
            model.PhoneRequired = _addressSettings.PhoneRequired;
            model.FaxEnabled = _addressSettings.FaxEnabled;
            model.FaxRequired = _addressSettings.FaxRequired;
        }

        /// <summary>
        /// Prepare order item models
        /// </summary>
        /// <param name="models">List of order item models</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderItemModels(IList<OrderItemModel> models, OrderPendingToClosePayment order)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);

            //get order items
            var orderItems = _orderPendingToClosePaymentService.GetOrderItems(order.Id, vendorId: _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult()?.Id ?? 0);

            foreach (var orderItem in orderItems)
            {
                var product = _productService.GetProductById(orderItem.ProductId);

                //fill in model values from the entity
                var orderItemModel = new OrderItemModel
                {
                    Id = orderItem.Id,
                    ProductId = orderItem.ProductId,
                    ProductName = product.Name,
                    Quantity = orderItem.Quantity,
                    IsDownload = product.IsDownload,
                    DownloadCount = orderItem.DownloadCount,
                    DownloadActivationType = product.DownloadActivationType,
                    IsDownloadActivated = orderItem.IsDownloadActivated,
                    UnitPriceInclTaxValue = orderItem.UnitPriceInclTax,
                    UnitPriceExclTaxValue = orderItem.UnitPriceExclTax,
                    DiscountInclTaxValue = orderItem.DiscountAmountInclTax,
                    DiscountExclTaxValue = orderItem.DiscountAmountExclTax,
                    SubTotalInclTaxValue = orderItem.PriceInclTax,
                    SubTotalExclTaxValue = orderItem.PriceExclTax,
                    AttributeInfo = orderItem.AttributeDescription
                };

                //fill in additional values (not existing in the entity)
                orderItemModel.Sku = _productService.FormatSku(product, orderItem.AttributesXml);
                orderItemModel.VendorName = _vendorService.GetVendorById(product.VendorId)?.Name;

                //picture
                var orderItemPicture = _pictureService.GetProductPicture(product, orderItem.AttributesXml);
                orderItemModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(ref orderItemPicture, 75);

                //license file
                if (orderItem.LicenseDownloadId.HasValue)
                {
                    orderItemModel.LicenseDownloadGuid = _downloadService
                        .GetDownloadById(orderItem.LicenseDownloadId.Value)?.DownloadGuid ?? Guid.Empty;
                }

                var languageId = _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().Id;

                //unit price
                orderItemModel.UnitPriceInclTax = _priceFormatter
                    .FormatPrice(orderItem.UnitPriceInclTax, true, primaryStoreCurrency, languageId, true, true);
                orderItemModel.UnitPriceExclTax = _priceFormatter
                    .FormatPrice(orderItem.UnitPriceExclTax, true, primaryStoreCurrency, languageId, false, true);

                //discounts
                orderItemModel.DiscountInclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountInclTax, true,
                    primaryStoreCurrency, languageId, true, true);
                orderItemModel.DiscountExclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountExclTax, true,
                    primaryStoreCurrency, languageId, false, true);

                //subtotal
                orderItemModel.SubTotalInclTax = _priceFormatter.FormatPrice(orderItem.PriceInclTax, true, primaryStoreCurrency,
                    languageId, true, true);
                orderItemModel.SubTotalExclTax = _priceFormatter.FormatPrice(orderItem.PriceExclTax, true, primaryStoreCurrency,
                    languageId, false, true);

                //recurring info
                if (product.IsRecurring)
                {
                    orderItemModel.RecurringInfo = string.Format(_localizationService.GetResource("Admin.Orders.Products.RecurringPeriod"),
                        product.RecurringCycleLength, _localizationService.GetLocalizedEnum(product.RecurringCyclePeriod));
                }

                //rental info
                if (product.IsRental)
                {
                    var rentalStartDate = orderItem.RentalStartDateUtc.HasValue
                        ? _productService.FormatRentalDate(product, orderItem.RentalStartDateUtc.Value) : string.Empty;
                    var rentalEndDate = orderItem.RentalEndDateUtc.HasValue
                        ? _productService.FormatRentalDate(product, orderItem.RentalEndDateUtc.Value) : string.Empty;
                    orderItemModel.RentalInfo = string.Format(_localizationService.GetResource("Order.Rental.FormattedDate"),
                        rentalStartDate, rentalEndDate);
                }

                //prepare return request models
                PrepareReturnRequestBriefModels(orderItemModel.ReturnRequests, orderItem);

                //gift card identifiers
                orderItemModel.PurchasedGiftCardIds = _giftCardService
                    .GetGiftCardsByPurchasedWithOrderItemId(orderItem.Id).Select(card => card.Id).ToList();

                models.Add(orderItemModel);
            }
        }

        /// <summary>
        /// Prepare order pending to close payment item models
        /// </summary>
        /// <param name="models">List of order item models</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderPendingToCLosePaymentItemModels(IList<OrderItemModel> models, Order order)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);

            //get order items
            var orderItems = _orderService.GetOrderItems(order.Id, vendorId: _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult()?.Id ?? 0);

            foreach (var orderItem in orderItems)
            {
                var product = _productService.GetProductById(orderItem.ProductId);

                //fill in model values from the entity
                var orderItemModel = new OrderItemModel
                {
                    Id = orderItem.Id,
                    ProductId = orderItem.ProductId,
                    ProductName = product.Name,
                    Quantity = orderItem.Quantity,
                    IsDownload = product.IsDownload,
                    DownloadCount = orderItem.DownloadCount,
                    DownloadActivationType = product.DownloadActivationType,
                    IsDownloadActivated = orderItem.IsDownloadActivated,
                    UnitPriceInclTaxValue = orderItem.UnitPriceInclTax,
                    UnitPriceExclTaxValue = orderItem.UnitPriceExclTax,
                    DiscountInclTaxValue = orderItem.DiscountAmountInclTax,
                    DiscountExclTaxValue = orderItem.DiscountAmountExclTax,
                    SubTotalInclTaxValue = orderItem.PriceInclTax,
                    SubTotalExclTaxValue = orderItem.PriceExclTax,
                    AttributeInfo = orderItem.AttributeDescription
                };

                //fill in additional values (not existing in the entity)
                orderItemModel.Sku = _productService.FormatSku(product, orderItem.AttributesXml);
                orderItemModel.VendorName = _vendorService.GetVendorById(product.VendorId)?.Name;

                //picture
                var orderItemPicture = _pictureService.GetProductPicture(product, orderItem.AttributesXml);
                orderItemModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(ref orderItemPicture, 75);

                //license file
                if (orderItem.LicenseDownloadId.HasValue)
                {
                    orderItemModel.LicenseDownloadGuid = _downloadService
                        .GetDownloadById(orderItem.LicenseDownloadId.Value)?.DownloadGuid ?? Guid.Empty;
                }

                var languageId = _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().Id;

                //unit price
                orderItemModel.UnitPriceInclTax = _priceFormatter
                    .FormatPrice(orderItem.UnitPriceInclTax, true, primaryStoreCurrency, languageId, true, true);
                orderItemModel.UnitPriceExclTax = _priceFormatter
                    .FormatPrice(orderItem.UnitPriceExclTax, true, primaryStoreCurrency, languageId, false, true);

                //discounts
                orderItemModel.DiscountInclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountInclTax, true,
                    primaryStoreCurrency, languageId, true, true);
                orderItemModel.DiscountExclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountExclTax, true,
                    primaryStoreCurrency, languageId, false, true);

                //subtotal
                orderItemModel.SubTotalInclTax = _priceFormatter.FormatPrice(orderItem.PriceInclTax, true, primaryStoreCurrency,
                    languageId, true, true);
                orderItemModel.SubTotalExclTax = _priceFormatter.FormatPrice(orderItem.PriceExclTax, true, primaryStoreCurrency,
                    languageId, false, true);

                //recurring info
                if (product.IsRecurring)
                {
                    orderItemModel.RecurringInfo = string.Format(_localizationService.GetResource("Admin.Orders.Products.RecurringPeriod"),
                        product.RecurringCycleLength, _localizationService.GetLocalizedEnum(product.RecurringCyclePeriod));
                }

                //rental info
                if (product.IsRental)
                {
                    var rentalStartDate = orderItem.RentalStartDateUtc.HasValue
                        ? _productService.FormatRentalDate(product, orderItem.RentalStartDateUtc.Value) : string.Empty;
                    var rentalEndDate = orderItem.RentalEndDateUtc.HasValue
                        ? _productService.FormatRentalDate(product, orderItem.RentalEndDateUtc.Value) : string.Empty;
                    orderItemModel.RentalInfo = string.Format(_localizationService.GetResource("Order.Rental.FormattedDate"),
                        rentalStartDate, rentalEndDate);
                }

                //prepare return request models
                PrepareReturnRequestBriefModels(orderItemModel.ReturnRequests, orderItem);

                //gift card identifiers
                orderItemModel.PurchasedGiftCardIds = _giftCardService
                    .GetGiftCardsByPurchasedWithOrderItemId(orderItem.Id).Select(card => card.Id).ToList();

                models.Add(orderItemModel);
            }
        }

        /// <summary>
        /// Prepare return request brief models
        /// </summary>
        /// <param name="models">List of return request brief models</param>
        /// <param name="orderItem">Order item</param>
        protected virtual void PrepareReturnRequestBriefModels(IList<OrderItemModel.ReturnRequestBriefModel> models, OrderItem orderItem)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            var returnRequests = _returnRequestService.SearchReturnRequests(orderItemId: orderItem.Id);
            foreach (var returnRequest in returnRequests)
            {
                models.Add(new OrderItemModel.ReturnRequestBriefModel
                {
                    CustomNumber = returnRequest.CustomNumber,
                    Id = returnRequest.Id
                });
            }
        }

        /// <summary>
        /// Prepare return request brief models
        /// </summary>
        /// <param name="models">List of return request brief models</param>
        /// <param name="orderItem">Order item</param>
        protected virtual void PrepareReturnRequestBriefModels(IList<OrderItemModel.ReturnRequestBriefModel> models, OrderPendingToClosePaymentItem orderItem)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem));

            var returnRequests = _returnRequestService.SearchReturnRequests(orderItemId: orderItem.Id);
            foreach (var returnRequest in returnRequests)
            {
                models.Add(new OrderItemModel.ReturnRequestBriefModel
                {
                    CustomNumber = returnRequest.CustomNumber,
                    Id = returnRequest.Id
                });
            }
        }

        /// <summary>
        /// Prepare order model totals
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderModelTotals(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            var languageId = _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().Id;

            //subtotal
            model.OrderSubtotalInclTax = _priceFormatter.FormatPrice(order.OrderSubtotalInclTax, true, primaryStoreCurrency,
                languageId, true);
            model.OrderSubtotalExclTax = _priceFormatter.FormatPrice(order.OrderSubtotalExclTax, true, primaryStoreCurrency,
                languageId, false);
            model.OrderSubtotalInclTaxValue = order.OrderSubtotalInclTax;
            model.OrderSubtotalExclTaxValue = order.OrderSubtotalExclTax;

            //discount (applied to order subtotal)
            var orderSubtotalDiscountInclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountInclTax, true,
                primaryStoreCurrency, languageId, true);
            var orderSubtotalDiscountExclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountExclTax, true,
                primaryStoreCurrency, languageId, false);
            if (order.OrderSubTotalDiscountInclTax > decimal.Zero)
                model.OrderSubTotalDiscountInclTax = orderSubtotalDiscountInclTaxStr;
            if (order.OrderSubTotalDiscountExclTax > decimal.Zero)
                model.OrderSubTotalDiscountExclTax = orderSubtotalDiscountExclTaxStr;
            model.OrderSubTotalDiscountInclTaxValue = order.OrderSubTotalDiscountInclTax;
            model.OrderSubTotalDiscountExclTaxValue = order.OrderSubTotalDiscountExclTax;

            //shipping
            model.OrderShippingInclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingInclTax, true,
                primaryStoreCurrency, languageId, true);
            model.OrderShippingExclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingExclTax, true,
                primaryStoreCurrency, languageId, false);
            model.OrderShippingInclTaxValue = order.OrderShippingInclTax;
            model.OrderShippingExclTaxValue = order.OrderShippingExclTax;

            //payment method additional fee
            if (order.PaymentMethodAdditionalFeeInclTax > decimal.Zero)
            {
                model.PaymentMethodAdditionalFeeInclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(
                    order.PaymentMethodAdditionalFeeInclTax, true, primaryStoreCurrency, languageId, true);
                model.PaymentMethodAdditionalFeeExclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(
                    order.PaymentMethodAdditionalFeeExclTax, true, primaryStoreCurrency, languageId, false);
            }

            model.PaymentMethodAdditionalFeeInclTaxValue = order.PaymentMethodAdditionalFeeInclTax;
            model.PaymentMethodAdditionalFeeExclTaxValue = order.PaymentMethodAdditionalFeeExclTax;

            //tax
            model.Tax = _priceFormatter.FormatPrice(order.OrderTax, true, false);
            var taxRates = _orderPendingToClosePaymentService.ParseTaxRates(order, order.TaxRates);
            var displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
            var displayTax = !displayTaxRates;
            foreach (var tr in taxRates)
            {
                model.TaxRates.Add(new OrderPendingToClosePaymentModel.TaxRate
                {
                    Rate = _priceFormatter.FormatTaxRate(tr.Key),
                    Value = _priceFormatter.FormatPrice(tr.Value, true, false)
                });
            }

            model.DisplayTaxRates = displayTaxRates;
            model.DisplayTax = displayTax;
            model.TaxValue = order.OrderTax;
            model.TaxRatesValue = order.TaxRates;

            //discount
            if (order.OrderDiscount > 0)
                model.OrderTotalDiscount = _priceFormatter.FormatPrice(-order.OrderDiscount, true, false);
            model.OrderTotalDiscountValue = order.OrderDiscount;

            //reward points
            if (order.RedeemedRewardPointsEntryId.HasValue && _rewardPointService.GetRewardPointsHistoryEntryById(order.RedeemedRewardPointsEntryId.Value) is RewardPointsHistory redeemedRewardPointsEntry)
            {
                model.RedeemedRewardPoints = -redeemedRewardPointsEntry.Points;
                model.RedeemedRewardPointsAmount =
                    _priceFormatter.FormatPrice(-redeemedRewardPointsEntry.UsedAmount, true, false);
            }

            //total
            model.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false);
            model.OrderTotalValue = order.OrderTotal;

            //refunded amount
            if (order.RefundedAmount > decimal.Zero)
                model.RefundedAmount = _priceFormatter.FormatPrice(order.RefundedAmount, true, false);

            //used discounts
            var duh = _discountService.GetAllDiscountUsageHistory(orderId: order.Id);
            foreach (var d in duh)
            {
                var discount = _discountService.GetDiscountById(d.DiscountId);

                model.UsedDiscounts.Add(new OrderPendingToClosePaymentModel.UsedDiscountModel
                {
                    DiscountId = d.DiscountId,
                    DiscountName = discount.Name
                });
            }

            //profit (hide for vendors)
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return;

            var profit = _orderReportService.ProfitReport(orderId: order.Id);
            model.Profit = _priceFormatter.FormatPrice(profit, true, false);
        }

        protected virtual void PrepareOrderPendingToClosePaymentModelTotals(OrderPendingToClosePaymentModel model, Order order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            var languageId = _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().Id;

            //subtotal
            model.OrderSubtotalInclTax = _priceFormatter.FormatPrice(order.OrderSubtotalInclTax, true, primaryStoreCurrency,
                languageId, true);
            model.OrderSubtotalExclTax = _priceFormatter.FormatPrice(order.OrderSubtotalExclTax, true, primaryStoreCurrency,
                languageId, false);
            model.OrderSubtotalInclTaxValue = order.OrderSubtotalInclTax;
            model.OrderSubtotalExclTaxValue = order.OrderSubtotalExclTax;

            //discount (applied to order subtotal)
            var orderSubtotalDiscountInclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountInclTax, true,
                primaryStoreCurrency, languageId, true);
            var orderSubtotalDiscountExclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountExclTax, true,
                primaryStoreCurrency, languageId, false);
            if (order.OrderSubTotalDiscountInclTax > decimal.Zero)
                model.OrderSubTotalDiscountInclTax = orderSubtotalDiscountInclTaxStr;
            if (order.OrderSubTotalDiscountExclTax > decimal.Zero)
                model.OrderSubTotalDiscountExclTax = orderSubtotalDiscountExclTaxStr;
            model.OrderSubTotalDiscountInclTaxValue = order.OrderSubTotalDiscountInclTax;
            model.OrderSubTotalDiscountExclTaxValue = order.OrderSubTotalDiscountExclTax;

            //shipping
            model.OrderShippingInclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingInclTax, true,
                primaryStoreCurrency, languageId, true);
            model.OrderShippingExclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingExclTax, true,
                primaryStoreCurrency, languageId, false);
            model.OrderShippingInclTaxValue = order.OrderShippingInclTax;
            model.OrderShippingExclTaxValue = order.OrderShippingExclTax;

            //payment method additional fee
            if (order.PaymentMethodAdditionalFeeInclTax > decimal.Zero)
            {
                model.PaymentMethodAdditionalFeeInclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(
                    order.PaymentMethodAdditionalFeeInclTax, true, primaryStoreCurrency, languageId, true);
                model.PaymentMethodAdditionalFeeExclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(
                    order.PaymentMethodAdditionalFeeExclTax, true, primaryStoreCurrency, languageId, false);
            }

            model.PaymentMethodAdditionalFeeInclTaxValue = order.PaymentMethodAdditionalFeeInclTax;
            model.PaymentMethodAdditionalFeeExclTaxValue = order.PaymentMethodAdditionalFeeExclTax;

            //tax
            model.Tax = _priceFormatter.FormatPrice(order.OrderTax, true, false);
            var taxRates = _orderService.ParseTaxRates(order, order.TaxRates);
            var displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
            var displayTax = !displayTaxRates;
            foreach (var tr in taxRates)
            {
                model.TaxRates.Add(new OrderPendingToClosePaymentModel.TaxRate
                {
                    Rate = _priceFormatter.FormatTaxRate(tr.Key),
                    Value = _priceFormatter.FormatPrice(tr.Value, true, false)
                });
            }

            model.DisplayTaxRates = displayTaxRates;
            model.DisplayTax = displayTax;
            model.TaxValue = order.OrderTax;
            model.TaxRatesValue = order.TaxRates;

            //discount
            if (order.OrderDiscount > 0)
                model.OrderTotalDiscount = _priceFormatter.FormatPrice(-order.OrderDiscount, true, false);
            model.OrderTotalDiscountValue = order.OrderDiscount;

            //gift cards
            foreach (var gcuh in _giftCardService.GetGiftCardUsageHistory(order))
            {
                model.GiftCards.Add(new OrderPendingToClosePaymentModel.GiftCard
                {
                    CouponCode = _giftCardService.GetGiftCardById(gcuh.GiftCardId).GiftCardCouponCode,
                    Amount = _priceFormatter.FormatPrice(-gcuh.UsedValue, true, false)
                });
            }

            //reward points
            if (order.RedeemedRewardPointsEntryId.HasValue && _rewardPointService.GetRewardPointsHistoryEntryById(order.RedeemedRewardPointsEntryId.Value) is RewardPointsHistory redeemedRewardPointsEntry)
            {
                model.RedeemedRewardPoints = -redeemedRewardPointsEntry.Points;
                model.RedeemedRewardPointsAmount =
                    _priceFormatter.FormatPrice(-redeemedRewardPointsEntry.UsedAmount, true, false);
            }

            //total
            model.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false);
            model.OrderTotalValue = order.OrderTotal;

            //refunded amount
            if (order.RefundedAmount > decimal.Zero)
                model.RefundedAmount = _priceFormatter.FormatPrice(order.RefundedAmount, true, false);

            //used discounts
            var duh = _discountService.GetAllDiscountUsageHistory(orderId: order.Id);
            foreach (var d in duh)
            {
                var discount = _discountService.GetDiscountById(d.DiscountId);

                model.UsedDiscounts.Add(new OrderPendingToClosePaymentModel.UsedDiscountModel
                {
                    DiscountId = d.DiscountId,
                    DiscountName = discount.Name
                });
            }

            //profit (hide for vendors)
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                return;

            var profit = _orderReportService.ProfitReport(orderId: order.Id);
            model.Profit = _priceFormatter.FormatPrice(profit, true, false);
        }

        /// <summary>
        /// Prepare order model payment info
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderModelPaymentInfo(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var billingAddress = _addressService.GetAddressById(order.BillingAddressId);

            //prepare billing address
            model.BillingAddress = billingAddress.ToModel(model.BillingAddress);

            model.BillingAddress.CountryName = _countryService.GetCountryByAddress(billingAddress)?.Name;
            model.BillingAddress.StateProvinceName = _stateProvinceService.GetStateProvinceByAddress(billingAddress)?.Name;

            PrepareAddressModel(model.BillingAddress, billingAddress);

            model.CanMarkOrderAsPaid = true;

            //payment transaction info
            model.AuthorizationTransactionId = order.AuthorizationTransactionId;
            model.CaptureTransactionId = order.CaptureTransactionId;
            model.SubscriptionTransactionId = order.SubscriptionTransactionId;

            //payment method info
            var pm = _paymentPluginManager.LoadPluginBySystemName(order.PaymentMethodSystemName);
            model.PaymentMethod = pm != null ? pm.PluginDescriptor.FriendlyName : order.PaymentMethodSystemName;
            model.VendorPaymentStatus = _localizationService.GetLocalizedEnum(order.VendorPaymentStatus);
            model.DriverPaymentStatus = _localizationService.GetLocalizedEnum(order.DriverPaymentStatus);

            model.VendorPaymentStatusId = (int)order.VendorPaymentStatus;
            model.DriverPaymentStatusId = (int)order.DriverPaymentStatus;

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId)?.CurrencyCode;
            model.MaxAmountToRefund = order.OrderTotal - order.RefundedAmount;

            //recurring payment record
            model.RecurringPaymentId = _orderPendingToClosePaymentService.SearchRecurringPayments(initialOrderId: order.Id, showHidden: true).FirstOrDefault()?.Id ?? 0;
        }

        /// <summary>
        /// Prepare order pending to close payment model payment info
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderPendingToClosePaymentModelPaymentInfo(OrderPendingToClosePaymentModel model, Order order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var billingAddress = _addressService.GetAddressById(order.BillingAddressId);

            //prepare billing address
            model.BillingAddress = billingAddress.ToModel(model.BillingAddress);

            model.BillingAddress.CountryName = _countryService.GetCountryByAddress(billingAddress)?.Name;
            model.BillingAddress.StateProvinceName = _stateProvinceService.GetStateProvinceByAddress(billingAddress)?.Name;

            PrepareAddressModel(model.BillingAddress, billingAddress);

            model.CanMarkOrderAsPaid = true;

            //payment transaction info
            model.AuthorizationTransactionId = order.AuthorizationTransactionId;
            model.CaptureTransactionId = order.CaptureTransactionId;
            model.SubscriptionTransactionId = order.SubscriptionTransactionId;

            //payment method info
            model.PaymentMethod = GetOrderPaymentMethodName(order);

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId)?.CurrencyCode;
            model.MaxAmountToRefund = order.OrderTotal - order.RefundedAmount;

            //recurring payment record
            model.RecurringPaymentId = _orderPendingToClosePaymentService.SearchRecurringPayments(initialOrderId: order.Id, showHidden: true).FirstOrDefault()?.Id ?? 0;
        }

        /// <summary>
        /// Prepare order model shipping info
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderModelShippingInfo(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            model.IsShippable = true;
            model.ShippingMethod = order.ShippingMethod;
            model.PickupInStore = order.PickupInStore;
            if (!order.PickupInStore)
            {
                var shippingAddress = _addressService.GetAddressById(order.ShippingAddressId.Value);
                var shippingCountry = _countryService.GetCountryByAddress(shippingAddress);

                model.ShippingAddress = shippingAddress.ToModel(model.ShippingAddress);
                model.ShippingAddress.CountryName = shippingCountry?.Name;
                model.ShippingAddress.StateProvinceName = _stateProvinceService.GetStateProvinceByAddress(shippingAddress)?.Name;
                PrepareAddressModel(model.ShippingAddress, shippingAddress);
                model.ShippingAddressGoogleMapsUrl = "https://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q=" +
                    $"{WebUtility.UrlEncode(shippingAddress.Address1 + " " + shippingAddress.ZipPostalCode + " " + shippingAddress.City + " " + (shippingCountry?.Name ?? string.Empty))}";
            }
            else
            {
                if (order.PickupAddressId is null)
                    return;

                var pickupAddress = _addressService.GetAddressById(order.PickupAddressId.Value);

                var pickupCountry = _countryService.GetCountryByAddress(pickupAddress);

                model.PickupAddress = pickupAddress.ToModel(model.PickupAddress);
                model.PickupAddressGoogleMapsUrl = $"https://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q=" +
                    $"{WebUtility.UrlEncode($"{pickupAddress.Address1} {pickupAddress.ZipPostalCode} {pickupAddress.City} {(pickupCountry?.Name ?? string.Empty)}")}";
            }
        }

        /// <summary>
        /// Prepare order model shipping info
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        protected virtual void PrepareOrderPendingToClosePaymentModelShippingInfo(OrderPendingToClosePaymentModel model, Order order)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            model.IsShippable = true;
            model.ShippingMethod = order.ShippingMethod;
            model.CanAddNewShipments = _orderService.HasItemsToAddToShipment(order);
            model.PickupInStore = order.PickupInStore;
            if (!order.PickupInStore)
            {
                var shippingAddress = _addressService.GetAddressById(order.ShippingAddressId.Value);
                var shippingCountry = _countryService.GetCountryByAddress(shippingAddress);

                model.ShippingAddress = shippingAddress.ToModel(model.ShippingAddress);
                model.ShippingAddress.CountryName = shippingCountry?.Name;
                model.ShippingAddress.StateProvinceName = _stateProvinceService.GetStateProvinceByAddress(shippingAddress)?.Name;
                PrepareAddressModel(model.ShippingAddress, shippingAddress);
                model.ShippingAddressGoogleMapsUrl = "https://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q=" +
                    $"{WebUtility.UrlEncode(shippingAddress.Address1 + " " + shippingAddress.ZipPostalCode + " " + shippingAddress.City + " " + (shippingCountry?.Name ?? string.Empty))}";
            }
            else
            {
                if (order.PickupAddressId is null)
                    return;

                var pickupAddress = _addressService.GetAddressById(order.PickupAddressId.Value);

                var pickupCountry = _countryService.GetCountryByAddress(pickupAddress);

                model.PickupAddress = pickupAddress.ToModel(model.PickupAddress);
                model.PickupAddressGoogleMapsUrl = $"https://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q=" +
                    $"{WebUtility.UrlEncode($"{pickupAddress.Address1} {pickupAddress.ZipPostalCode} {pickupAddress.City} {(pickupCountry?.Name ?? string.Empty)}")}";
            }
        }

        /// <summary>
        /// Prepare product attribute models
        /// </summary>
        /// <param name="models">List of product attribute models</param>
        /// <param name="order">Order</param>
        /// <param name="product">Product</param>
        protected virtual void PrepareProductAttributeModels(IList<AddProductToOrderModel.ProductAttributeModel> models, OrderPendingToClosePayment order, Product product)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var attributes = _productAttributeService.GetProductAttributeMappingsByProductId(product.Id);
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddProductToOrderModel.ProductAttributeModel
                {
                    Id = attribute.Id,
                    ProductAttributeId = attribute.ProductAttributeId,
                    Name = _productAttributeService.GetProductAttributeById(attribute.ProductAttributeId).Name,
                    TextPrompt = attribute.TextPrompt,
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                    HasCondition = !string.IsNullOrEmpty(attribute.ConditionAttributeXml)
                };
                if (!string.IsNullOrEmpty(attribute.ValidationFileAllowedExtensions))
                {
                    attributeModel.AllowedFileExtensions = attribute.ValidationFileAllowedExtensions
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }

                if (attribute.ShouldHaveValues())
                {
                    var customer = _customerService.GetCustomerById(order.CustomerId);

                    //values
                    var attributeValues = _productAttributeService.GetProductAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        //price adjustment
                        var priceAdjustment = _taxService.GetProductPrice(product,
                            _priceCalculationService.GetProductAttributeValuePriceAdjustment(product, attributeValue, customer), out _);

                        var priceAdjustmentStr = string.Empty;
                        if (priceAdjustment != 0)
                        {
                            if (attributeValue.PriceAdjustmentUsePercentage)
                            {
                                priceAdjustmentStr = attributeValue.PriceAdjustment.ToString("G29");
                                priceAdjustmentStr = priceAdjustment > 0 ? $"+{priceAdjustmentStr}%" : $"{priceAdjustmentStr}%";
                            }
                            else
                            {
                                priceAdjustmentStr = priceAdjustment > 0 ? $"+{_priceFormatter.FormatPrice(priceAdjustment, false, false)}" : $"-{_priceFormatter.FormatPrice(-priceAdjustment, false, false)}";
                            }
                        }

                        attributeModel.Values.Add(new AddProductToOrderModel.ProductAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.Name,
                            IsPreSelected = attributeValue.IsPreSelected,
                            CustomerEntersQty = attributeValue.CustomerEntersQty,
                            Quantity = attributeValue.Quantity,
                            PriceAdjustment = priceAdjustmentStr,
                            PriceAdjustmentValue = priceAdjustment
                        });
                    }
                }

                models.Add(attributeModel);
            }
        }

        /// <summary>
        /// Prepare order note search model
        /// </summary>
        /// <param name="searchModel">Order note search model</param>
        /// <param name="order">Order</param>
        /// <returns>Order note search model</returns>
        protected virtual OrderNoteSearchModel PrepareOrderNoteSearchModel(OrderNoteSearchModel searchModel, OrderPendingToClosePayment order)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            searchModel.OrderId = order.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare order note search model
        /// </summary>
        /// <param name="searchModel">Order note search model</param>
        /// <param name="order">Order</param>
        /// <returns>Order note search model</returns>
        protected virtual OrderNoteSearchModel PrepareOrderNoteSearchModel(OrderNoteSearchModel searchModel, Order order)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            searchModel.OrderId = order.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        protected virtual void PrepareDefaultItem(IList<SelectListItem> items, bool withSpecialDefaultItem, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if (!withSpecialDefaultItem)
                return;

            //at now we use "0" as the default value
            const string value = "0";

            //prepare item text
            defaultItemText ??= _localizationService.GetResource("Admin.Common.All");

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = value });
        }

        protected virtual void PrepareDeliveryStatuses(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available shipping statuses
            var availableStatusItems = DeliveryStatus.AssignedToMessenger.ToSelectList(false);
            foreach (var statusItem in availableStatusItems)
            {
                items.Add(statusItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        protected virtual void PreparePaymentCollectionStatuses(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available shipping statuses
            var availableStatusItems = PaymentCollectionStatus.DoesNotApply.ToSelectList(false);
            foreach (var statusItem in availableStatusItems)
            {
                items.Add(statusItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        protected virtual string GetOrderPaymentMethodName(Order order)
        {
            IList<CheckoutAttributeValue> paymentMethodAttributeValues = _checkoutAttributeParser.ParseCheckoutAttributeValues(order.CheckoutAttributesXml)
                .Where(attr => attr.attribute.Name.Equals(Defaults.PaymentMethodCheckoutAttribute.Name))
                .Select(attr => attr.values.ToList()).FirstOrDefault();
            return paymentMethodAttributeValues != null && paymentMethodAttributeValues.Any() ? paymentMethodAttributeValues.First().Name : string.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare order search model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order search model</returns>
        public virtual OrderPendingToClosePaymentSearchModel PrepareOrderSearchModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.IsLoggedInAsVendor = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null;
            searchModel.BillingPhoneEnabled = _addressSettings.PhoneEnabled;

            //prepare available order, payment and shipping statuses
            _baseAdminModelFactory.PrepareOrderStatuses(searchModel.AvailableOrderStatuses);
            if (searchModel.AvailableOrderStatuses.Any())
            {
                if (searchModel.OrderStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.OrderStatusIds.Select(id => id.ToString());
                    searchModel.AvailableOrderStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableOrderStatuses.FirstOrDefault().Selected = true;
            }

            _baseAdminModelFactory.PreparePaymentStatuses(searchModel.AvailableVendorPaymentStatuses);
            if (searchModel.AvailableVendorPaymentStatuses.Any())
            {
                if (searchModel.VendorPaymentStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.VendorPaymentStatusIds.Select(id => id.ToString());
                    searchModel.AvailableVendorPaymentStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableVendorPaymentStatuses.FirstOrDefault().Selected = true;
            }

            _baseAdminModelFactory.PreparePaymentStatuses(searchModel.AvailableDriverPaymentStatuses);
            if (searchModel.AvailableDriverPaymentStatuses.Any())
            {
                if (searchModel.DriverPaymentStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.DriverPaymentStatusIds.Select(id => id.ToString());
                    searchModel.AvailableDriverPaymentStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableDriverPaymentStatuses.FirstOrDefault().Selected = true;
            }

            //prepare available drivers
            _deliveryAppBaseAdminModelFactory.PrepareDrivers(searchModel.AvailableDrivers);

            //prepare available stores
            _baseAdminModelFactory.PrepareStores(searchModel.AvailableStores);

            //prepare available vendors
            _baseAdminModelFactory.PrepareVendors(searchModel.AvailableVendors);

            //prepare available warehouses
            _baseAdminModelFactory.PrepareWarehouses(searchModel.AvailableWarehouses);

            
            //prepare available payment methods
            searchModel.AvailablePaymentMethods = _paymentPluginManager.LoadAllPlugins().Select(method =>
                new SelectListItem { Text = method.PluginDescriptor.FriendlyName, Value = method.PluginDescriptor.SystemName }).ToList();
            searchModel.AvailablePaymentMethods.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = string.Empty });

            //prepare available billing countries
            searchModel.AvailableCountries = _countryService.GetAllCountriesForBilling(showHidden: true)
                .Select(country => new SelectListItem { Text = country.Name, Value = country.Id.ToString() }).ToList();
            searchModel.AvailableCountries.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //prepare grid
            searchModel.SetGridPageSize();

            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            return searchModel;
        }

        ///<inheritdoc/>
        public virtual OrderTracingSearchModel PrepareTracingOrderSearchModel(OrderTracingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.IsLoggedInAsVendor = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null;
            searchModel.BillingPhoneEnabled = _addressSettings.PhoneEnabled;

            //prepare available order, payment and shipping statuses
            _baseAdminModelFactory.PrepareOrderStatuses(searchModel.AvailableOrderStatuses);
            if (searchModel.AvailableOrderStatuses.Any())
            {
                if (searchModel.OrderStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.OrderStatusIds.Select(id => id.ToString());
                    searchModel.AvailableOrderStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableOrderStatuses.FirstOrDefault().Selected = true;
            }

            _baseAdminModelFactory.PreparePaymentStatuses(searchModel.AvailablePaymentStatuses);
            if (searchModel.AvailablePaymentStatuses.Any())
            {
                if (searchModel.PaymentStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.PaymentStatusIds.Select(id => id.ToString());
                    searchModel.AvailablePaymentStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailablePaymentStatuses.FirstOrDefault().Selected = true;
            }

            _baseAdminModelFactory.PrepareShippingStatuses(searchModel.AvailableShippingStatuses);
            if (searchModel.AvailableShippingStatuses.Any())
            {
                if (searchModel.ShippingStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.ShippingStatusIds.Select(id => id.ToString());
                    searchModel.AvailableShippingStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableShippingStatuses.FirstOrDefault().Selected = true;
            }

            PrepareDeliveryStatuses(searchModel.AvailableDeliveryStatuses);
            if (searchModel.AvailableDeliveryStatuses.Any())
            {
                if (searchModel.DeliveryStatusIds?.Any() ?? false)
                {
                    var ids = searchModel.DeliveryStatusIds.Select(id => id.ToString());
                    searchModel.AvailableDeliveryStatuses.Where(statusItem => ids.Contains(statusItem.Value)).ToList()
                        .ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailableDeliveryStatuses.FirstOrDefault().Selected = true;
            }

            PreparePaymentCollectionStatuses(searchModel.AvailablePaymentCollectionStatuses);
            if (searchModel.AvailablePaymentCollectionStatuses.Any())
            {
                if (searchModel.PaymentCollectionStatusId > 0)
                {
                    searchModel.AvailablePaymentCollectionStatuses
                        .Where(statusItem => statusItem.Value.Equals(searchModel.PaymentCollectionStatusId.ToString()))
                        .ToList().ForEach(statusItem => statusItem.Selected = true);
                }
                else
                    searchModel.AvailablePaymentCollectionStatuses.FirstOrDefault().Selected = true;
            }

            //prepare available drivers
            _deliveryAppBaseAdminModelFactory.PrepareDrivers(searchModel.AvailableDrivers);

            //prepare available stores
            _baseAdminModelFactory.PrepareStores(searchModel.AvailableStores);

            //prepare available vendors
            _baseAdminModelFactory.PrepareVendors(searchModel.AvailableVendors);

            //prepare available warehouses
            _baseAdminModelFactory.PrepareWarehouses(searchModel.AvailableWarehouses);

            //prepare available payment methods
            _deliveryAppBaseAdminModelFactory.PreparePaymentMethods(searchModel.AvailablePaymentMethods);

            //prepare available billing countries
            searchModel.AvailableCountries = _countryService.GetAllCountriesForBilling(showHidden: true)
                .Select(country => new SelectListItem { Text = country.Name, Value = country.Id.ToString() }).ToList();
            searchModel.AvailableCountries.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //prepare grid
            searchModel.SetGridPageSize();

            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged order pending to close payment list model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order list model</returns>
        public virtual OrderPendingToClosePaymentListModel PrepareOrderListModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter orders
            var vendorPaymentStatusIds = (searchModel.VendorPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.VendorPaymentStatusIds.ToList();
            var driverPaymentStatusIds = (searchModel.DriverPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.DriverPaymentStatusIds.ToList();
            var startDateValue = !searchModel.StartDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());
            var endDateValue = !searchModel.EndDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);

            //get orders
            var orders = _orderPendingToClosePaymentService.SearchOrders(
                driverId: searchModel.DriverId,
                vendorId: searchModel.VendorId,
                paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverPaymentStatusIds,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
            var model = new OrderPendingToClosePaymentListModel().PrepareToGrid(searchModel, orders, () =>
            {
                //fill in model values from the entity
                return orders.Select(order =>
                {
                    OrderDeliveryStatusMapping orderDelivery = _orderDeliveryStatusMappingRepository.Table
                        .Where(mapping => mapping.OrderId == order.OrderId && mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered)
                        .FirstOrDefault();
                    Customer driverCustomer = _customerService.GetCustomerById(orderDelivery?.CustomerId ?? 0);
                    //fill in model values from the entity
                    var orderModel = new OrderPendingToClosePaymentModel
                    {
                        Id = order.Id,
                        OrderStatusId = order.OrderStatusId,
                        VendorPaymentStatusId = order.VendorPaymentStatusId,
                        DriverPaymentStatusId = order.DriverPaymentStatusId,
                        CustomerId = order.CustomerId,
                        Tax=order.OrderTax.ToString(),
                        CustomOrderNumber = order.CustomOrderNumber,
                        OrderShippingInclTax = order.OrderShippingInclTax.ToString(),
                        ShippingTaxAdministrativePercentage = order.ShippingTaxAdministrativePercentage,
                        ShippingTaxAdministrativeProfitAmount = order.ShippingTaxAdministrativeProfitAmount,
                        ShippingTaxMessengerPercentage = order.ShippingTaxMessengerPercentage,
                        ShippingTaxMessengerProfitAmount = order.ShippingTaxMessengerProfitAmount,
                        OrderSubtotalExclTax = order.OrderSubtotalExclTax.ToString(),
                        OrderTotalAdministrativePercentage = order.OrderTotalAdministrativePercentage,
                        OrderTotalAdministrativeProfitAmount = order.OrderTotalAdministrativeProfitAmount,
                        OrderTotalVendorPercentage = order.OrderTotalVendorPercentage,
                        OrderTotalVendorProfitAmount = order.OrderTotalVendorProfitAmount,
                        DriverCustomerId = orderDelivery?.CustomerId ?? 0,
                        PaymentMethod= GetPaymentMethod(order.CheckoutAttributeDescription),
                        DriverCustomerEmail = driverCustomer?.Email ?? "",
                        OrderTotalDiscount = _priceFormatter.FormatPrice(order.OrderDiscount, true, false)
                    };

                    //convert dates to the user time
                    orderModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);

                    //fill in additional values (not existing in the entity)
                    orderModel.StoreName = _storeService.GetStoreById(order.StoreId)?.Name ?? "Deleted";
                    orderModel.VendorName = _vendorService.GetVendorById(order.VendorId)?.Name ?? "Deleted";
                    orderModel.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
                    orderModel.VendorPaymentStatus = _localizationService.GetLocalizedEnum(order.VendorPaymentStatus);
                    orderModel.DriverPaymentStatus = _localizationService.GetLocalizedEnum(order.DriverPaymentStatus);
                    orderModel.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false);

                    return orderModel;
                });
            });

            return model;
        }

        public string GetPaymentMethod(string checkoutAttributeDescription)
        {
            if (checkoutAttributeDescription.Contains(Defaults.CreditCardPaymentCheckoutAttributeName))
            {
                return Defaults.CreditCardPaymentCheckoutAttributeName;

            }
            else if (checkoutAttributeDescription.Contains(Defaults.ClaveCardPaymentCheckoutAttributeName))
            {
                return Defaults.ClaveCardPaymentCheckoutAttributeName;
            }
            else
            {
                return Defaults.CashPaymentCheckoutAttributeName;
            }
        }

        ///<inheritdoc/>
        public virtual OrderTracingListModel PrepareOrderTracingListModel(OrderTracingSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter orders
            var orderStatusIds = (searchModel.OrderStatusIds?.Contains(0) ?? true) ? null : searchModel.OrderStatusIds.ToList();
            var paymentStatusIds = (searchModel.PaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.PaymentStatusIds.ToList();
            var shippingStatusIds = (searchModel.ShippingStatusIds?.Contains(0) ?? true) ? null : searchModel.ShippingStatusIds.ToList();
            if (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null)
                searchModel.VendorId = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id;
            var startDateValue = !searchModel.StartDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());
            var endDateValue = !searchModel.EndDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);
            var product = _productService.GetProductById(searchModel.ProductId);
            var filterByProductId = product != null && (_workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() == null || product.VendorId == _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult().Id)
                ? searchModel.ProductId : 0;

            //get orders
            var orders = _orderService.SearchOrders(storeId: searchModel.StoreId,
                vendorId: searchModel.VendorId,
                productId: filterByProductId,
                warehouseId: searchModel.WarehouseId,
                paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                osIds: orderStatusIds,
                psIds: paymentStatusIds,
                ssIds: shippingStatusIds,
                billingPhone: searchModel.BillingPhone,
                billingEmail: searchModel.BillingEmail,
                billingLastName: searchModel.BillingLastName,
                billingCountryId: searchModel.BillingCountryId,
                orderNotes: searchModel.PaymentMethodName,
                pageIndex: searchModel.Page-1, pageSize: searchModel.PageSize);

            IList<int> ordersIds = orders.Select(o => o.Id).ToList();

            //prepare list model
            var model = new OrderTracingListModel().PrepareToGrid(searchModel, orders, () =>
            {
                var orderDeliveries = _orderDeliveryStatusMappingRepository.Table;
                var deliveryStatusIds = (searchModel.DeliveryStatusIds?.Contains(0) ?? true) ? null : searchModel.DeliveryStatusIds.ToList();

                if (deliveryStatusIds != null && deliveryStatusIds.Any())
                {
                    IList<int> orderIdsFilteredByDeliveryStatus = orderDeliveries
                        .Where(mapping => searchModel.DeliveryStatusIds.Contains(mapping.DeliveryStatusId))
                        .Select(mapping => mapping.OrderId).ToList();

                    ordersIds = ordersIds.Where(oid => orderIdsFilteredByDeliveryStatus.Contains(oid)).ToList();
                }

                if (searchModel.DriverId != 0)
                {
                    IList<int> orderIdsFilteredByDriverId;
                    var x = orderDeliveries
                        .Where(mapping => mapping.CustomerId == searchModel.DriverId);

                    var y =x.Select(mapping => mapping.OrderId).ToList();

                    orderIdsFilteredByDriverId = y;

                      orders = _orderService.SearchOrders(storeId: searchModel.StoreId,
                      vendorId: searchModel.VendorId,
                      productId: filterByProductId,
                      warehouseId: searchModel.WarehouseId,
                      paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                      createdFromUtc: startDateValue,
                      createdToUtc: endDateValue,
                      osIds: orderStatusIds,
                      psIds: paymentStatusIds,
                      ssIds: shippingStatusIds,
                      billingPhone: searchModel.BillingPhone,
                      billingEmail: searchModel.BillingEmail,
                      billingLastName: searchModel.BillingLastName,
                      billingCountryId: searchModel.BillingCountryId,
                      orderNotes: searchModel.PaymentMethodName);

                        ordersIds = orderIdsFilteredByDriverId;// ordersIds.Where(oid => orderIdsFilteredByDriverId.Contains(oid)).ToList();
                }

                if (searchModel.PaymentCollectionStatusId > 0)
                {
                    IList<int> orderIdsFilteredByCollectionStatus = _orderPaymentCollectionStatusRepository.Table
                        .Where(mapping => mapping.PaymentCollectionStatusId == searchModel.PaymentCollectionStatusId)
                        .Select(mapping => mapping.OrderId).ToList();

                    ordersIds = ordersIds.Where(oid => orderIdsFilteredByCollectionStatus.Contains(oid)).ToList();
                }

                //fill in model values from the entity
                return orders.Where(o => ordersIds.Contains(o.Id)).Select(order =>
                {
                    var billingAddress = _addressService.GetAddressById(order.BillingAddressId);
                    var orderDelivery = orderDeliveries.FirstOrDefault(mapping => mapping.OrderId == order.Id);
                    var paymentCollection = _orderPaymentCollectionStatusRepository.Table.FirstOrDefault(mapping => mapping.OrderId == order.Id);

                    //fill in model values from the entity
                    var orderModel = new OrderTracingModel
                    {
                        Id = order.Id,
                        OrderStatusId = order.OrderStatusId,
                        PaymentStatusId = order.PaymentStatusId,
                        ShippingStatusId = order.ShippingStatusId,
                        CustomerEmail = billingAddress.Email,
                        CustomerFullName = $"{billingAddress.FirstName} {billingAddress.LastName}",
                        CustomerId = order.CustomerId,
                        CustomOrderNumber = order.CustomOrderNumber,
                        DeliveryStatusId = orderDelivery?.DeliveryStatusId ?? -1,
                        DeliveryStatus = orderDelivery != null ? _localizationService.GetLocalizedEnum((DeliveryStatus)orderDelivery.DeliveryStatusId) : "",
                        PaymentCollectionStatusId = paymentCollection?.PaymentCollectionStatusId ?? (int)PaymentCollectionStatus.DoesNotApply,
                        PaymentCollectionStatus = _localizationService.GetLocalizedEnum(paymentCollection?.PaymentCollectionStatus ?? PaymentCollectionStatus.DoesNotApply),
                        PaymentMethod = GetOrderPaymentMethodName(order)
                    };

                    //convert dates to the user time
                    orderModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);

                    //fill in additional values (not existing in the entity)
                    orderModel.StoreName = _storeService.GetStoreById(order.StoreId)?.Name ?? "Deleted";
                    orderModel.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
                    orderModel.PaymentStatus = _localizationService.GetLocalizedEnum(order.PaymentStatus);
                    orderModel.ShippingStatus = _localizationService.GetLocalizedEnum(order.ShippingStatus);
                    orderModel.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false);

                    string vendorName = GetVendorName(order.Id);

                    if (!string.IsNullOrWhiteSpace(vendorName))
                    {
                        orderModel.VendorName = vendorName;
                    }

                    string driverName = GetDriverName(order.Id);

                    if (!string.IsNullOrWhiteSpace(driverName))
                    {
                        orderModel.DriverName = driverName;
                    }

                    return orderModel;
                });
            });

            return model;
        }

        private string GetDriverName(int orderId)
        {
            Customer driverCustomer = _customerService.GetCustomerById(_orderDeliveryStatusMappingRepository.Table
                        .FirstOrDefault(mapping => mapping.OrderId == orderId)?.CustomerId ?? 0);

            if (driverCustomer is null) return "";

            string firstName = _genericAttributeService.GetAttribute<string>(driverCustomer, "FirstName");
            string lastName = _genericAttributeService.GetAttribute<string>(driverCustomer, "LastName");

            return $"{firstName} {lastName} ({driverCustomer.Email})";
        }

        private string GetVendorName(int orderId)
        {
            OrderItem orderItem = _orderService.GetOrderItems(orderId).FirstOrDefault();

            if (orderItem == null) return "";

            int orderItemProductId = orderItem.ProductId;

            Product product = _productService.GetProductById(orderItemProductId);

            if (product == null) return "";

            Vendor vendor = _vendorService.GetVendorById(product.VendorId);

            return vendor != null ? $"{vendor.Name} ({vendor.Email})" : "";
        }

        /// <summary>
        /// Prepare paged order list model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order list model</returns>
        public virtual OrderPendingToClosePaymentListModel PrepareVendorOrderEarningListModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter orders
            var vendorPaymentStatusIds = (searchModel.VendorPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.VendorPaymentStatusIds.ToList();
            var driverPaymentStatusIds = (searchModel.DriverPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.DriverPaymentStatusIds.ToList();
            var startDateValue = !searchModel.StartDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());
            var endDateValue = !searchModel.EndDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);

            //get orders
            var orders = _orderPendingToClosePaymentService.SearchOrders(
                driverId: searchModel.DriverId,
                vendorId: searchModel.VendorId,
                paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverPaymentStatusIds,
                pageIndex: searchModel.Page - 1,
                pageSize: searchModel.PageSize);

            //prepare list model
            var model = new OrderPendingToClosePaymentListModel().PrepareToGrid(searchModel, orders, () =>
            {
                //fill in model values from the entity
                return orders.Select(order =>
                {
                    OrderDeliveryStatusMapping orderDelivery = _orderDeliveryStatusMappingRepository.Table
                        .Where(mapping => mapping.OrderId == order.OrderId && mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered)
                        .FirstOrDefault();
                    Customer driverCustomer = _customerService.GetCustomerById(orderDelivery?.CustomerId ?? 0);

                    //fill in model values from the entity
                    var orderModel = new OrderPendingToClosePaymentModel
                    {
                        Id = order.Id,
                        OrderStatusId = order.OrderStatusId,
                        VendorPaymentStatusId = order.VendorPaymentStatusId,
                        DriverPaymentStatusId = order.DriverPaymentStatusId,
                        CustomerId = order.CustomerId,
                        CustomOrderNumber = order.CustomOrderNumber,
                        OrderShippingInclTax = order.OrderShippingInclTax.ToString(),
                        ShippingTaxAdministrativePercentage = order.ShippingTaxAdministrativePercentage,
                        ShippingTaxAdministrativeProfitAmount = order.ShippingTaxAdministrativeProfitAmount,
                        ShippingTaxMessengerPercentage = order.ShippingTaxMessengerPercentage,
                        ShippingTaxMessengerProfitAmount = order.ShippingTaxMessengerProfitAmount,
                        OrderSubtotalExclTax = order.OrderSubtotalExclTax.ToString(),
                        OrderTotalAdministrativePercentage = order.OrderTotalAdministrativePercentage,
                        OrderTotalAdministrativeProfitAmount = order.OrderTotalAdministrativeProfitAmount,
                        OrderTotalVendorPercentage = order.OrderTotalVendorPercentage,
                        OrderTotalVendorProfitAmount = order.OrderTotalVendorProfitAmount,
                        DriverCustomerId = orderDelivery?.CustomerId ?? 0,
                        DriverCustomerEmail = driverCustomer?.Email ?? "",
                        OrderTotalDiscount = _priceFormatter.FormatPrice(order.OrderDiscount, true, false)
                    };

                    //convert dates to the user time
                    orderModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);

                    //fill in additional values (not existing in the entity)
                    orderModel.StoreName = _storeService.GetStoreById(order.StoreId)?.Name ?? "Deleted";
                    orderModel.VendorName = _vendorService.GetVendorById(order.VendorId)?.Name ?? "Deleted";
                    orderModel.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
                    orderModel.VendorPaymentStatus = _localizationService.GetLocalizedEnum(order.VendorPaymentStatus);
                    orderModel.DriverPaymentStatus = _localizationService.GetLocalizedEnum(order.DriverPaymentStatus);
                    orderModel.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal - order.OrderShippingInclTax, true, false);

                    return orderModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare order aggregator model
        /// </summary>
        /// <param name="searchModel">Order search model</param>
        /// <returns>Order aggregator model</returns>
        public virtual OrderAggreratorModel PrepareOrderAggregatorModel(OrderPendingToClosePaymentSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get parameters to filter orders
            List<int> vendorPaymentStatusIds = (searchModel.VendorPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.VendorPaymentStatusIds.ToList();
            List<int> driverpaymentStatusIds = (searchModel.DriverPaymentStatusIds?.Contains(0) ?? true) ? null : searchModel.DriverPaymentStatusIds.ToList();
            var startDateValue = !searchModel.StartDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.StartDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult());
            var endDateValue = !searchModel.EndDate.HasValue ? null
                : (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.EndDate.Value, _dateTimeHelper.GetCurrentTimeZoneAsync().GetAwaiter().GetResult()).AddDays(1);

            //prepare additional model data
            OrderAverageReportLine reportSummary = _orderPendingToPayReportServices.GetOrderAverageReportLine(
                driverId: searchModel.DriverId,
                vendorId: searchModel.VendorId,
                paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverpaymentStatusIds,
                startTimeUtc: startDateValue,
                endTimeUtc: endDateValue);

            var profit = _orderPendingToPayReportServices.ProfitReport(
                driverId: searchModel.DriverId,
                vendorId: searchModel.VendorId,
                paymentMethodSystemName: searchModel.PaymentMethodSystemName,
                psIdsVendor: vendorPaymentStatusIds,
                psIdsDriver: driverpaymentStatusIds,
                startTimeUtc: startDateValue,
                endTimeUtc: endDateValue);

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            var shippingSum = _priceFormatter
                .FormatShippingPrice(reportSummary.SumShippingExclTax, true, primaryStoreCurrency, _workContext.GetWorkingLanguageAsync().GetAwaiter().GetResult().Id, false);
            var taxSum = _priceFormatter.FormatPrice(reportSummary.SumTax, true, false);
            var totalSum = _priceFormatter.FormatPrice(reportSummary.SumOrders, true, false);
            var profitSum = _priceFormatter.FormatPrice(profit, true, false);

            var model = new OrderAggreratorModel
            {
                aggregatorprofit = profitSum,
                aggregatorshipping = shippingSum,
                aggregatortax = taxSum,
                aggregatortotal = totalSum
            };

            return model;
        }

        /// <summary>
        /// Prepare order model
        /// </summary>
        /// <param name="model">Order model</param>
        /// <param name="order">Order</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Order model</returns>
        public virtual OrderPendingToClosePaymentModel PrepareOrderModel(OrderPendingToClosePaymentModel model, OrderPendingToClosePayment order, bool excludeProperties = false)
        {
            if (order != null)
            {
                //fill in model values from the entity
                model ??= new OrderPendingToClosePaymentModel
                {
                    Id = order.Id,
                    OrderStatusId = order.OrderStatusId,
                    VatNumber = order.VatNumber,
                    CheckoutAttributeInfo = order.CheckoutAttributeDescription
                };

                var customer = _customerService.GetCustomerById(order.CustomerId);

                model.OrderGuid = order.OrderGuid;
                model.CustomOrderNumber = order.CustomOrderNumber;
                model.CustomerIp = order.CustomerIp;
                model.CustomerId = customer.Id;
                model.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
                model.StoreName = _storeService.GetStoreById(order.StoreId)?.Name ?? "Deleted";
                model.CustomerInfo = _customerService.IsRegistered(customer) ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);

                var affiliate = _affiliateService.GetAffiliateById(order.AffiliateId);
                if (affiliate != null)
                {
                    model.AffiliateId = affiliate.Id;
                    model.AffiliateName = _affiliateService.GetAffiliateFullName(affiliate);
                }

                //prepare order totals
                PrepareOrderModelTotals(model, order);

                //prepare order items
                PrepareOrderItemModels(model.Items, order);
                model.HasDownloadableProducts = model.Items.Any(item => item.IsDownload);

                //prepare payment info
                PrepareOrderModelPaymentInfo(model, order);

                //prepare shipping info
                PrepareOrderModelShippingInfo(model, order);

                //prepare nested search model
                PrepareOrderNoteSearchModel(model.OrderNoteSearchModel, order);
            }

            model.IsLoggedInAsVendor = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null;
            model.AllowCustomersToSelectTaxDisplayType = _taxSettings.AllowCustomersToSelectTaxDisplayType;
            model.TaxDisplayType = _taxSettings.TaxDisplayType;

            return model;
        }

        /// <summary>
        /// Prepare order pending to close payment model from order
        /// </summary>
        /// <param name="model">Order pending to close payment model</param>
        /// <param name="order">Order</param>
        /// <param name="excludeProperties">Whether to exclude populating of some properties of model</param>
        /// <returns>Order pending to close payment model</returns>
        public virtual OrderPendingToClosePaymentModel PrepareOrderPendingToClosePaymentModelFromOrder(OrderPendingToClosePaymentModel model, Order order, bool excludeProperties = false)
        {
            if (order != null)
            {
                //fill in model values from the entity
                model ??= new OrderPendingToClosePaymentModel
                {
                    Id = order.Id,
                    OrderStatusId = order.OrderStatusId,
                    VatNumber = order.VatNumber,
                    CheckoutAttributeInfo = order.CheckoutAttributeDescription
                };

                var customer = _customerService.GetCustomerById(order.CustomerId);

                model.OrderGuid = order.OrderGuid;
                model.CustomOrderNumber = order.CustomOrderNumber;
                model.CustomerIp = order.CustomerIp;
                model.CustomerId = customer.Id;
                model.OrderStatus = _localizationService.GetLocalizedEnum(order.OrderStatus);
                model.StoreName = _storeService.GetStoreById(order.StoreId)?.Name ?? "Deleted";
                model.CustomerInfo = _customerService.IsRegistered(customer) ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);

                var affiliate = _affiliateService.GetAffiliateById(order.AffiliateId);
                if (affiliate != null)
                {
                    model.AffiliateId = affiliate.Id;
                    model.AffiliateName = _affiliateService.GetAffiliateFullName(affiliate);
                }

                //prepare order totals
                PrepareOrderPendingToClosePaymentModelTotals(model, order);

                //prepare order items
                PrepareOrderPendingToCLosePaymentItemModels(model.Items, order);
                model.HasDownloadableProducts = model.Items.Any(item => item.IsDownload);

                //prepare payment info
                PrepareOrderPendingToClosePaymentModelPaymentInfo(model, order);

                //prepare shipping info
                PrepareOrderPendingToClosePaymentModelShippingInfo(model, order);

                PrepareOrderNoteSearchModel(model.OrderNoteSearchModel, order);
            }

            model.IsLoggedInAsVendor = _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult() != null;
            model.AllowCustomersToSelectTaxDisplayType = _taxSettings.AllowCustomersToSelectTaxDisplayType;
            model.TaxDisplayType = _taxSettings.TaxDisplayType;

            return model;
        }

        /// <summary>
        /// Prepare product search model to add to the order
        /// </summary>
        /// <param name="searchModel">Product search model to add to the order</param>
        /// <param name="order">Order</param>
        /// <returns>Product search model to add to the order</returns>
        public virtual AddProductToOrderSearchModel PrepareAddProductToOrderSearchModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            searchModel.OrderId = order.Id;

            //prepare available categories
            _baseAdminModelFactory.PrepareCategories(searchModel.AvailableCategories);

            //prepare available manufacturers
            _baseAdminModelFactory.PrepareManufacturers(searchModel.AvailableManufacturers);

            //prepare available product types
            _baseAdminModelFactory.PrepareProductTypes(searchModel.AvailableProductTypes);

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        /// <summary>
        /// Prepare paged product list model to add to the order
        /// </summary>
        /// <param name="searchModel">Product search model to add to the order</param>
        /// <param name="order">Order</param>
        /// <returns>Product search model to add to the order</returns>
        public virtual AddProductToOrderListModel PrepareAddProductToOrderListModel(AddProductToOrderSearchModel searchModel, OrderPendingToClosePayment order)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get products
            var products = _productService.SearchProducts(showHidden: true,
                categoryIds: new List<int> { searchModel.SearchCategoryId },
                manufacturerId: searchModel.SearchManufacturerId,
                productType: searchModel.SearchProductTypeId > 0 ? (ProductType?)searchModel.SearchProductTypeId : null,
                keywords: searchModel.SearchProductName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare grid model
            var model = new AddProductToOrderListModel().PrepareToGrid(searchModel, products, () =>
            {
                //fill in model values from the entity
                return products.Select(product =>
                {
                    var productModel = product.ToModel<ProductModel>();
                    productModel.SeName = _urlRecordService.GetSeName(product, 0, true, false);

                    return productModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare product model to add to the order
        /// </summary>
        /// <param name="model">Product model to add to the order</param>
        /// <param name="order">Order</param>
        /// <param name="product">Product</param>
        /// <returns>Product model to add to the order</returns>
        public virtual AddProductToOrderModel PrepareAddProductToOrderModel(AddProductToOrderModel model, OrderPendingToClosePayment order, Product product)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var customer = _customerService.GetCustomerById(order.CustomerId);

            model.ProductId = product.Id;
            model.OrderId = order.Id;
            model.Name = product.Name;
            model.IsRental = product.IsRental;
            model.ProductType = product.ProductType;
            model.AutoUpdateOrderTotals = _orderSettings.AutoUpdateOrderTotalsOnEditingOrder;

            var presetQty = 1;
            var presetPrice = _priceCalculationService.GetFinalPrice(product, customer, decimal.Zero, true, presetQty);
            var presetPriceInclTax = _taxService.GetProductPrice(product, presetPrice, true, customer, out _);
            var presetPriceExclTax = _taxService.GetProductPrice(product, presetPrice, false, customer, out _);
            model.UnitPriceExclTax = presetPriceExclTax;
            model.UnitPriceInclTax = presetPriceInclTax;
            model.Quantity = presetQty;
            model.SubTotalExclTax = presetPriceExclTax;
            model.SubTotalInclTax = presetPriceInclTax;

            //attributes
            PrepareProductAttributeModels(model.ProductAttributes, order, product);
            model.HasCondition = model.ProductAttributes.Any(attribute => attribute.HasCondition);

            //gift card
            model.GiftCard.IsGiftCard = product.IsGiftCard;
            if (model.GiftCard.IsGiftCard)
                model.GiftCard.GiftCardType = product.GiftCardType;

            return model;
        }

        /// <summary>
        /// Prepare order address model
        /// </summary>
        /// <param name="model">Order address model</param>
        /// <param name="order">Order</param>
        /// <param name="address">Address</param>
        /// <returns>Order address model</returns>
        public virtual OrderAddressModel PrepareOrderAddressModel(OrderAddressModel model, OrderPendingToClosePayment order, Address address)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (address == null)
                throw new ArgumentNullException(nameof(address));

            model.OrderId = order.Id;

            //prepare address model
            model.Address = address.ToModel(model.Address);
            PrepareAddressModel(model.Address, address);

            //prepare available countries
            _baseAdminModelFactory.PrepareCountries(model.Address.AvailableCountries);

            //prepare available states
            _baseAdminModelFactory.PrepareStatesAndProvinces(model.Address.AvailableStates, model.Address.CountryId);

            //prepare custom address attributes
            _addressAttributeModelFactory.PrepareCustomAddressAttributes(model.Address.CustomAddressAttributes, address);

            return model;
        }

        /// <summary>
        /// Prepare paged order note list model
        /// </summary>
        /// <param name="searchModel">Order note search model</param>
        /// <param name="order">Order</param>
        /// <returns>Order note list model</returns>
        public virtual OrderNoteListModel PrepareOrderNoteListModel(OrderNoteSearchModel searchModel, OrderPendingToClosePayment order)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            //get notes
            var orderNotes = _orderPendingToClosePaymentService.GetOrderNotesByOrderId(order.Id).OrderByDescending(on => on.CreatedOnUtc).ToList().ToPagedList(searchModel);

            //prepare list model
            var model = new OrderNoteListModel().PrepareToGrid(searchModel, orderNotes, () =>
            {
                //fill in model values from the entity
                return orderNotes.Select(orderNote =>
                {
                    //fill in model values from the entity
                    var orderNoteModel = orderNote.ToModel<OrderNoteModel>();

                    //convert dates to the user time
                    orderNoteModel.CreatedOn = _dateTimeHelper.ConvertToUserTime(orderNote.CreatedOnUtc, DateTimeKind.Utc);

                    //fill in additional values (not existing in the entity)
                    orderNoteModel.Note = _orderPendingToClosePaymentService.FormatOrderNoteText(orderNote);
                    orderNoteModel.DownloadGuid = _downloadService.GetDownloadById(orderNote.DownloadId)?.DownloadGuid ?? Guid.Empty;

                    return orderNoteModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare bestseller brief search model
        /// </summary>
        /// <param name="searchModel">Bestseller brief search model</param>
        /// <returns>Bestseller brief search model</returns>
        public virtual BestsellerBriefSearchModel PrepareBestsellerBriefSearchModel(BestsellerBriefSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize(5);

            return searchModel;
        }

        /// <summary>
        /// Prepare paged bestseller brief list model
        /// </summary>
        /// <param name="searchModel">Bestseller brief search model</param>
        /// <returns>Bestseller brief list model</returns>
        public virtual BestsellerBriefListModel PrepareBestsellerBriefListModel(BestsellerBriefSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get bestsellers
            var bestsellers = _orderReportService.BestSellersReport(showHidden: true,
                vendorId: _workContext.GetCurrentVendorAsync().GetAwaiter().GetResult()?.Id ?? 0,
                orderBy: searchModel.OrderBy,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new BestsellerBriefListModel().PrepareToGrid(searchModel, bestsellers, () =>
            {
                //fill in model values from the entity
                return bestsellers.Select(bestseller =>
                {
                    //fill in model values from the entity
                    var bestsellerModel = new BestsellerModel
                    {
                        ProductId = bestseller.ProductId,
                        TotalQuantity = bestseller.TotalQuantity
                    };

                    //fill in additional values (not existing in the entity)
                    bestsellerModel.ProductName = _productService.GetProductById(bestseller.ProductId)?.Name;
                    bestsellerModel.TotalAmount = _priceFormatter.FormatPrice(bestseller.TotalAmount, true, false);

                    return bestsellerModel;
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare order average line summary report list model
        /// </summary>
        /// <param name="searchModel">Order average line summary report search model</param>
        /// <returns>Order average line summary report list model</returns>
        public virtual OrderAverageReportListModel PrepareOrderAverageReportListModel(OrderAverageReportSearchModel searchModel, int vendorId)
        {
            //get report
            var report = new List<OrderAverageReportLineSummary>
            {
                _deliverAppOrderReportService.OrderAverageReport(0, OrderStatus.Pending,vendorId),
                _deliverAppOrderReportService.OrderAverageReport(0, OrderStatus.Processing,vendorId),
                _deliverAppOrderReportService.OrderAverageReport(0, OrderStatus.Complete,vendorId),
                _deliverAppOrderReportService.OrderAverageReport(0, OrderStatus.Cancelled,vendorId)
            };

            var pagedList = new PagedList<OrderAverageReportLineSummary>(report, 0, int.MaxValue);

            //prepare list model
            var model = new OrderAverageReportListModel().PrepareToGrid(searchModel, pagedList, () =>
            {
                //fill in model values from the entity
                return pagedList.Select(reportItem => new OrderAverageReportModel
                {
                    OrderStatus = _localizationService.GetLocalizedEnum(reportItem.OrderStatus),
                    SumTodayOrders = _priceFormatter.FormatPrice(reportItem.SumTodayOrders, true, false),
                    SumThisWeekOrders = _priceFormatter.FormatPrice(reportItem.SumThisWeekOrders, true, false),
                    SumThisMonthOrders = _priceFormatter.FormatPrice(reportItem.SumThisMonthOrders, true, false),
                    SumThisYearOrders = _priceFormatter.FormatPrice(reportItem.SumThisYearOrders, true, false),
                    SumAllTimeOrders = _priceFormatter.FormatPrice(reportItem.SumAllTimeOrders, true, false)
                });
            });

            return model;
        }

        /// <summary>
        /// Prepare incomplete order report list model
        /// </summary>
        /// <param name="searchModel">Incomplete order report search model</param>
        /// <returns>Incomplete order report list model</returns>
        public virtual OrderIncompleteReportListModel PrepareOrderIncompleteReportListModel(OrderIncompleteReportSearchModel searchModel, int vendorId)
        {
            var orderIncompleteReportModels = new List<OrderIncompleteReportModel>();

            //get URL helper
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);

            //not paid
            var orderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<int>().Where(os => os != (int)OrderStatus.Cancelled).ToList();
            var paymentStatuses = new List<int> { (int)PaymentStatus.Pending };
            var psPending = _orderReportService.GetOrderAverageReportLine(psIds: paymentStatuses, osIds: orderStatuses, vendorId: vendorId);
            orderIncompleteReportModels.Add(new OrderIncompleteReportModel
            {
                Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalUnpaidOrders"),
                Count = psPending.CountOrders,
                Total = _priceFormatter.FormatPrice(psPending.SumOrders, true, false),
                ViewLink = urlHelper.Action("List", "Order", new
                {
                    orderStatuses = string.Join(",", orderStatuses),
                    paymentStatuses = string.Join(",", paymentStatuses)
                })
            });

            //not shipped
            var shippingStatuses = new List<int> { (int)ShippingStatus.NotYetShipped };
            var ssPending = _orderReportService.GetOrderAverageReportLine(osIds: orderStatuses, ssIds: shippingStatuses, vendorId: vendorId);
            orderIncompleteReportModels.Add(new OrderIncompleteReportModel
            {
                Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalNotShippedOrders"),
                Count = ssPending.CountOrders,
                Total = _priceFormatter.FormatPrice(ssPending.SumOrders, true, false),
                ViewLink = urlHelper.Action("List", "Order", new
                {
                    orderStatuses = string.Join(",", orderStatuses),
                    shippingStatuses = string.Join(",", shippingStatuses)
                })
            });

            //pending
            orderStatuses = new List<int> { (int)OrderStatus.Pending };
            var osPending = _orderReportService.GetOrderAverageReportLine(osIds: orderStatuses, vendorId: vendorId);
            orderIncompleteReportModels.Add(new OrderIncompleteReportModel
            {
                Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalIncompleteOrders"),
                Count = osPending.CountOrders,
                Total = _priceFormatter.FormatPrice(osPending.SumOrders, true, false),
                ViewLink = urlHelper.Action("List", "Order", new { orderStatuses = string.Join(",", orderStatuses) })
            });

            var pagedList = new PagedList<OrderIncompleteReportModel>(orderIncompleteReportModels, 0, int.MaxValue);

            //prepare list model
            var model = new OrderIncompleteReportListModel().PrepareToGrid(searchModel, pagedList, () => pagedList);
            return model;
        }

        #endregion
    }
}
