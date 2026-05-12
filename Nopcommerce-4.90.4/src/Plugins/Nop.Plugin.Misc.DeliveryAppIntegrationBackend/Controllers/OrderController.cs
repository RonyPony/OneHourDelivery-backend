using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Api.Delta;
using Nop.Plugin.Api.DTO.Errors;
using Nop.Plugin.Api.DTO.OrderItems;
using Nop.Plugin.Api.DTO.Orders;
using Nop.Plugin.Api.Factories;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.MappingExtensions;
using Nop.Plugin.Api.ModelBinders;
using Nop.Plugin.Api.Services;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Attributes;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Shipping;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Controllers
{
    /// <summary>
    /// Responsible for interactions with Orders entity.
    /// </summary>
    [Route("api/delivery-orders")]
    [ApiController]
    public sealed class OrderController : Controller
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly IDeliveryAppOrderService _deliveryAppOrderService;
        private readonly IDeliveryAppShippingService _deliveryAppShippingService;
        private readonly ILogger _logger;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        private readonly ICustomerService _customerService;
        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly IProductService _productService;
        private readonly Services.IProductAttributeConverter _productAttributeConverter;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShippingService _shippingService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IStoreContext _storeContext;
        private readonly IFactory<Order> _factory;
        private readonly ILocalizationService _localizationService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IDTODeliveryAppHelper _dtoHelper;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly IOrderService _orderService;
        private readonly IGoogleDirectionService _googleDirectionService;
        private readonly INotificationCenterService _notificationCenterService;
        private readonly INotificationRequestBuilder _notificationRequestBuilder;
        private readonly IOrderApiService _orderApiService;
        private readonly IDeliveryAppDriverService _deliveryAppDriverService;
        private readonly IDeliveryAppProductService _deliveryAppProductService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IDiscountService _discountService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderController"/>.
        /// </summary>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="deliveryAppOrderService">An implementation of <see cref="IDeliveryAppOrderService"/>.</param>
        /// <param name="deliveryAppShippingService">An implementation of <see cref="IDeliveryAppShippingService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="jsonFieldsSerializer">An implementation of <see cref="IJsonFieldsSerializer"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="productAttributeConverter">An implementation of <see cref="IProductAttributeConverter"/>.</param>
        /// <param name="shoppingCartService">An implementation of <see cref="IShoppingCartService"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="storeContext">An implementation of <see cref="IStoreContext"/>.</param>
        /// <param name="factory">An implementation of <see cref="IFactory{Order}"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="orderProcessingService">An implementation of <see cref="IOrderProcessingService"/>.</param>
        /// <param name="customerActivityService">An implementation of <see cref="ICustomerActivityService"/>.</param>
        /// <param name="dtoHelper">An implementation of <see cref="IDTOHelper"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="ICheckoutAttributeService"/>.</param>
        /// <param name="checkoutAttributeServic">An implementation of <see cref="ICheckoutAttributeService"/>.</param>
        /// <param name="checkoutAttributeParser">An implementation of <see cref="ICheckoutAttributeParser"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="notificationCenterService">An implementation of <see cref="INotificationCenterService"/>.</param>
        /// <param name="notificationRequestBuilder">An implementation of <see cref="INotificationRequestBuilder"/>.</param>
        /// <param name="orderApiService">An implementation of <see cref="IOrderApiService"/>.</param>
        /// <param name="deliveryAppDriverService">An implementation of <see cref="IDeliveryAppDriverService"/>.</param>
        /// <param name="orderRepository">An implementation of <see cref="IRepository{Order}"/>.</param>
        public OrderController(
            IAddressService addressService,
            IDeliveryAppOrderService deliveryAppOrderService,
            IDeliveryAppShippingService deliveryAppShippingService,
            ILogger logger,
            IJsonFieldsSerializer jsonFieldsSerializer,
            ICustomerService customerService,
            IProductService productService,
            Services.IProductAttributeConverter productAttributeConverter,
            IShoppingCartService shoppingCartService,
            IShippingService shippingService,
            IGenericAttributeService genericAttributeService,
            IStoreContext storeContext,
            IFactory<Order> factory,
            ILocalizationService localizationService,
            IOrderProcessingService orderProcessingService,
            ICustomerActivityService customerActivityService,
            IDTODeliveryAppHelper dtoHelper,
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ICheckoutAttributeService checkoutAttributeServic,
            ICheckoutAttributeParser checkoutAttributeParser,
            IOrderService orderService,
            IGoogleDirectionService googleDirectionService,
            INotificationCenterService notificationCenterService,
            INotificationRequestBuilder notificationRequestBuilder,
            IOrderApiService orderApiService,
            IDeliveryAppDriverService deliveryAppDriverService,
            IDeliveryAppProductService deliveryAppProductService,
            IDiscountService discountService,
        IRepository<Order> orderRepository)
        {
            _addressService = addressService;
            _deliveryAppOrderService = deliveryAppOrderService;
            _deliveryAppShippingService = deliveryAppShippingService;
            _logger = logger;
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _customerService = customerService;
            _productService = productService;
            _productAttributeConverter = productAttributeConverter;
            _shoppingCartService = shoppingCartService;
            _shippingService = shippingService;
            _genericAttributeService = genericAttributeService;
            _storeContext = storeContext;
            _factory = factory;
            _localizationService = localizationService;
            _orderProcessingService = orderProcessingService;
            _customerActivityService = customerActivityService;
            _dtoHelper = dtoHelper;
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _checkoutAttributeService = checkoutAttributeServic;
            _checkoutAttributeParser = checkoutAttributeParser;
            _orderService = orderService;
            _googleDirectionService = googleDirectionService;
            _notificationCenterService = notificationCenterService;
            _notificationRequestBuilder = notificationRequestBuilder;
            _orderApiService = orderApiService;
            _deliveryAppDriverService = deliveryAppDriverService;
            _deliveryAppProductService = deliveryAppProductService;
            _orderRepository = orderRepository;
            _discountService = discountService;
        }

        #endregion

        #region Methods

        private IActionResult Error(HttpStatusCode statusCode = (HttpStatusCode)422, string propertyKey = "", string errorMessage = "")
        {
            var errors = new Dictionary<string, List<string>>();

            if (!string.IsNullOrEmpty(errorMessage) && !string.IsNullOrEmpty(propertyKey))
            {
                var errorsList = new List<string>() { errorMessage };
                errors.Add(propertyKey, errorsList);
            }

            foreach (var item in ModelState)
            {
                var errorMessages = item.Value.Errors.Select(x => x.ErrorMessage);

                var validErrorMessages = new List<string>();

                validErrorMessages.AddRange(errorMessages.Where(message => !string.IsNullOrEmpty(message)));

                if (validErrorMessages.Count > 0)
                {
                    if (errors.ContainsKey(item.Key))
                    {
                        errors[item.Key].AddRange(validErrorMessages);
                    }
                    else
                    {
                        errors.Add(item.Key, validErrorMessages.ToList());
                    }
                }
            }

            var errorsRootObject = new ErrorsRootObject()
            {
                Errors = errors
            };

            var errorsJson = _jsonFieldsSerializer.Serialize(errorsRootObject, null);

            return new ErrorActionResult(errorsJson, statusCode);
        }

        [HttpPost]
        public IActionResult CreateOrder([ModelBinder(typeof(JsonModelBinder<OrderDto>))] Delta<OrderDto> orderDelta)
        {
            // Here we display the errors if the validation has failed at some poinclass OrderService extends OrderServiceContract {
            if (!ModelState.IsValid)
            {
                return Error();
            }

            if (orderDelta.Dto.CustomerId == null)
            {
                return Error();
            }

            if (string.IsNullOrWhiteSpace(orderDelta.Dto.PaymentMethodCheckoutAttribute))
            {
                return BadRequest(new { message = "PaymentMethodCheckoutAttributeCantBeNull" });
            }

            // We don't have to check for value because this is done by the order validator.
            var customer = _customerService.GetCustomerById(orderDelta.Dto.CustomerId.Value);

            if (customer == null)
            {
                return Error(HttpStatusCode.NotFound, "customer", "not found");
            }

            // Clear the shopping cart before creating the order
            _shoppingCartService.GetShoppingCart(customer)
                       .ToList()
                       .ForEach(item => _shoppingCartService.DeleteShoppingCartItem(item, true));

            // Check that all products belong to the same vendor
            int? lastId = null;

            foreach (OrderItemDto item in orderDelta.Dto.OrderItems)
            {
                var vendorId = _productService.GetProductById((int)item.ProductId)?.VendorId;

                if (lastId == null)
                {
                    lastId = vendorId;
                }
                else if (lastId != vendorId)
                {
                    return BadRequest(new { message = "ProductFromAnotherVendor" });
                }
                else
                {
                    lastId = vendorId;
                }
            }

            SavePaymentMethodCheckoutAttribute(orderDelta, customer);

            if (!string.IsNullOrWhiteSpace(orderDelta.Dto.OrderDeliveryIndicationsCheckoutAttribute))
            {
                SaveOrderDeliveryIndicationsCheckoutAttribute(orderDelta.Dto.OrderDeliveryIndicationsCheckoutAttribute, customer);
            }

            var shippingRequired = false;

            if (orderDelta.Dto.OrderItems != null)
            {
                var shouldReturnError = AddOrderItemsToCart(orderDelta.Dto.OrderItems, customer, orderDelta.Dto.StoreId ?? _storeContext.GetCurrentStore().Id);
                if (shouldReturnError)
                {
                    _shoppingCartService.GetShoppingCart(customer)
                        .ToList()
                        .ForEach(item => _shoppingCartService.DeleteShoppingCartItem(item, true));

                    return Error(HttpStatusCode.BadRequest);
                }

                shippingRequired = IsShippingAddressRequired(orderDelta.Dto.OrderItems);
            }

            ////validateProductDiscounts each one
            //List<Discount> discList = new List<Discount>();
            //decimal discount = 0;
            //foreach (OrderItemDto prodInfo in orderDelta.Dto.OrderItems)
            //{
            //    Product receivedProduct = _productService.GetProductById(prodInfo.ProductId.Value);
            //    if (receivedProduct == null)
            //    {
            //        //return NotFound("No product found with the provided id");
            //    }
            //    IList<DiscountProductMapping> productDiscounts = _productService.GetAllDiscountsAppliedToProduct(receivedProduct.Id);
            //    if (productDiscounts == null || productDiscounts.Count <= 0)
            //    {
            //        //return Ok("No Discounts found");
            //    }
                
            //    foreach (DiscountProductMapping dd2 in productDiscounts)
            //    {
            //        Discount finalDiscountElement = _discountService.GetDiscountById(dd2.DiscountId);
            //        discList.Add(finalDiscountElement);
            //        discount = discount+finalDiscountElement.DiscountAmount;
            //    }
            //    //return Ok(discList);
            //}
            
            

            if (shippingRequired)
            {
                var isValid = true;

                isValid &= SetShippingOption(orderDelta.Dto.ShippingRateComputationMethodSystemName,
                                             orderDelta.Dto.ShippingMethod,
                                             orderDelta.Dto.StoreId ?? _storeContext.GetCurrentStore().Id,
                                             customer,
                                             BuildShoppingCartItemsFromOrderItemDtos(orderDelta.Dto.OrderItems.ToList(),
                                                                                     customer.Id,
                                                                                     orderDelta.Dto.StoreId ?? _storeContext.GetCurrentStore().Id));

                if (!isValid)
                {
                    return Error(HttpStatusCode.BadRequest);
                }
            }

            var newOrder = _factory.Initialize();
            orderDelta.Merge(newOrder);
            newOrder.BillingAddressId = orderDelta.Dto.BillingAddress.Id;
            newOrder.ShippingAddressId = orderDelta.Dto.ShippingAddress.Id;
            //newOrder.OrderDiscount = discount;
            // If the customer has something in the cart it will be added too. Should we clear the cart first? 
            newOrder.CustomerId = customer.Id;

            // The default value will be the currentStore.id, but if it isn't passed in the json we need to set it by hand.
            if (!orderDelta.Dto.StoreId.HasValue)
            {
                newOrder.StoreId = _storeContext.GetCurrentStore().Id;
            }

            bool existPendingOrder = _orderRepository.Table.Any(order => order.CustomerId == customer.Id &&
                                                                order.OrderStatusId == (int)OrderStatus.Pending &&
                                                                !order.Deleted);

            if (existPendingOrder)
            {
                return Error(HttpStatusCode.BadRequest, "customer", "CustomerHasAPendingOrder");
            }

            if (!string.IsNullOrEmpty(orderDelta.Dto.OrderDiscountCouponCode))
            {
                _customerService.ApplyDiscountCouponCode(customer, orderDelta.Dto.OrderDiscountCouponCode);
            }

            // !-- THESE LINES WHERE NOT REMOVED FOR NOW FOR FALLBACK PURPOSES
            //Address shippingAddressToSave = orderDelta.Dto.ShippingAddress.ToEntity();
            //_addressService.InsertAddress(shippingAddressToSave);
            //_customerService.InsertCustomerAddress(customer, shippingAddressToSave);
            //customer.ShippingAddressId = shippingAddressToSave.Id;
            //_customerService.UpdateCustomer(customer);

            var placeOrderResult = PlaceOrder(newOrder, customer);

            if (!placeOrderResult.Success)
            {
                foreach (var error in placeOrderResult.Errors)
                {
                    ModelState.AddModelError("order placement", error);
                }

                return Error(HttpStatusCode.BadRequest);
            }

            if (placeOrderResult.PlacedOrder != null)
            {
                if (placeOrderResult.PlacedOrder.ShippingAddressId == null ||
                placeOrderResult.PlacedOrder.ShippingAddressId == 0)
                {
                    Address shippingAddress = orderDelta.Dto.ShippingAddress.ToEntity();
                    _addressService.InsertAddress(shippingAddress);
                    orderDelta.Dto.ShippingAddress.Id = shippingAddress.Id;
                    _customerService.InsertCustomerAddress(customer, shippingAddress);
                    placeOrderResult.PlacedOrder.ShippingAddressId = shippingAddress.Id;
                    placeOrderResult.PlacedOrder.BillingAddressId = shippingAddress.Id;
                    orderDelta.Dto.BillingAddress.Id = shippingAddress.Id;
                    _orderService.UpdateOrder(placeOrderResult.PlacedOrder);
                }

            }

            if (placeOrderResult.PlacedOrder != null &&
                placeOrderResult.PlacedOrder.ShippingAddressId != null &&
                placeOrderResult.PlacedOrder.ShippingAddressId != 0)
            {
                Address shippingAddressToSave = orderDelta.Dto.ShippingAddress.ToEntity();
                shippingAddressToSave.Id = (int)placeOrderResult.PlacedOrder.ShippingAddressId;
                shippingAddressToSave.CreatedOnUtc = DateTime.Now;
                _addressService.UpdateAddress(shippingAddressToSave);
            }

            if (placeOrderResult.PlacedOrder != null &&
                placeOrderResult.PlacedOrder.BillingAddressId != 0)
            {
                Address billingAddressToSave = orderDelta.Dto.BillingAddress.ToEntity();
                billingAddressToSave.Id = placeOrderResult.PlacedOrder.BillingAddressId;
                billingAddressToSave.CreatedOnUtc = DateTime.Now;
                _addressService.UpdateAddress(billingAddressToSave);
            }

            customer.BillingAddressId = placeOrderResult.PlacedOrder.BillingAddressId;
            customer.ShippingAddressId = placeOrderResult.PlacedOrder.ShippingAddressId;

            Address billingAddress = _addressService.GetAddressById(placeOrderResult.PlacedOrder.BillingAddressId);
            Address placerOrderShippingAddress = _addressService.GetAddressById(placeOrderResult.PlacedOrder.ShippingAddressId.Value);
            _customerService.InsertCustomerAddress(customer, billingAddress);
            _customerService.InsertCustomerAddress(customer, placerOrderShippingAddress);
            _customerService.UpdateCustomer(customer);

            _customerActivityService.InsertActivity("AddNewOrder",
                                                   _localizationService.GetResource("ActivityLog.AddNewOrder"), newOrder);

            var ordersRootObject = new OrdersRootObject();

            var placedOrderDto = _dtoHelper.PrepareOrderDTO(placeOrderResult.PlacedOrder);

            ordersRootObject.Orders.Add(placedOrderDto);

            _orderService.InsertOrderNote(new OrderNote { OrderId = placedOrderDto.Id, Note = $"{Defaults.PaymentMethodCheckoutAttribute}:{orderDelta.Dto.PaymentMethodCheckoutAttribute}" });

            if (orderDelta.Dto.BillingAddressWithCoordinates != null)
            {
                var coordinates = new AddressGeoCoordinatesMapping
                {
                    AddressId = placedOrderDto.ShippingAddress.Id,
                    Latitude = orderDelta.Dto.BillingAddressWithCoordinates.Latitude,
                    Longitude = orderDelta.Dto.BillingAddressWithCoordinates.Longitude,
                };

                _addressGeoCoordinatesService.InsertAddressGeoCoordinates(coordinates, placedOrderDto.ShippingAddress.Id);
            }

            var json = _jsonFieldsSerializer.Serialize(ordersRootObject, string.Empty);

            if (placedOrderDto.PaymentMethodSystemName != "Payments.CyberSource")
            {
                _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.ClientClientOrderCreated, placedOrderDto.Id));
                _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.ClientCommerceNewOrder, placedOrderDto.Id));
            }

            return new RawJsonActionResult(json);
        }

        private void SavePaymentMethodCheckoutAttribute(Delta<OrderDto> orderDelta, Customer customer)
        {
            CheckoutAttribute paymentMethodCheckoutAttribute = _checkoutAttributeService.GetAllCheckoutAttributes().FirstOrDefault(x => x.Name == Defaults.PaymentMethodCheckoutAttribute.Name);

            if (paymentMethodCheckoutAttribute == null)
            {
                throw new ArgumentException("PaymentMethodCheckoutAttribute could not be found");
            }

            CheckoutAttributeValue paymentMethodAttributeValue = _checkoutAttributeService.GetCheckoutAttributeValues(paymentMethodCheckoutAttribute.Id).FirstOrDefault(x => x.Name == (orderDelta.Dto.PaymentMethodCheckoutAttribute ?? Defaults.CashPaymentCheckoutAttributeName));

            if (paymentMethodAttributeValue == null)
            {
                throw new ArgumentException($"{orderDelta.Dto.PaymentMethodCheckoutAttribute} could not be found");
            }

            string checkoutAttributes = "";

            checkoutAttributes = _checkoutAttributeParser.AddCheckoutAttribute(checkoutAttributes,
                                        paymentMethodCheckoutAttribute, paymentMethodAttributeValue.Id.ToString());

            _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CheckoutAttributes, checkoutAttributes, _storeContext.GetCurrentStore().Id);

        }

        private void SaveOrderDeliveryIndicationsCheckoutAttribute(string orderIndications, Customer customer)
        {
            CheckoutAttribute OrderDeliveryIndicationsCheckoutAttribute = _checkoutAttributeService.GetAllCheckoutAttributes().FirstOrDefault(x => x.Name == Defaults.OrderDeliveryIndicationsCheckoutAttribute.Name);

            if (OrderDeliveryIndicationsCheckoutAttribute == null)
            {
                throw new ArgumentException("OrderDeliveryIndicationsCheckoutAttribute could not be found");
            }


            string checkoutAttributes = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.CheckoutAttributes, storeId: 1);
            checkoutAttributes = checkoutAttributes ?? "";

            checkoutAttributes = _checkoutAttributeParser.AddCheckoutAttribute(checkoutAttributes,
                                        OrderDeliveryIndicationsCheckoutAttribute, orderIndications);

            _genericAttributeService.SaveAttribute(customer, NopCustomerDefaults.CheckoutAttributes, checkoutAttributes, _storeContext.GetCurrentStore().Id);

        }

        private List<ShoppingCartItem> BuildShoppingCartItemsFromOrderItemDtos(List<OrderItemDto> orderItemDtos, int customerId, int storeId)
        {
            var shoppingCartItems = new List<ShoppingCartItem>();

            foreach (var orderItem in orderItemDtos)
            {
                if (orderItem.ProductId != null)
                {
                    shoppingCartItems.Add(new ShoppingCartItem
                    {
                        ProductId = orderItem.ProductId.Value, // required field
                        CustomerId = customerId,
                        Quantity = orderItem.Quantity ?? 1,
                        RentalStartDateUtc = orderItem.RentalStartDateUtc,
                        RentalEndDateUtc = orderItem.RentalEndDateUtc,
                        StoreId = storeId,
                        ShoppingCartType = ShoppingCartType.ShoppingCart
                    });
                }
            }

            return shoppingCartItems;
        }

        private PlaceOrderResult PlaceOrder(Order newOrder, Customer customer)
        {
            var processPaymentRequest = new ProcessPaymentRequest
            {
                StoreId = newOrder.StoreId,
                CustomerId = customer.Id,
                PaymentMethodSystemName = newOrder.PaymentMethodSystemName,
                OrderGuid = newOrder.OrderGuid
            };

            var placeOrderResult = _orderProcessingService.PlaceOrder(processPaymentRequest);

            return placeOrderResult;
        }

        private bool SetShippingOption(string shippingRateComputationMethodSystemName,
                string shippingOptionName,
                int storeId,
                Customer customer,
                List<ShoppingCartItem> shoppingCartItems)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(shippingRateComputationMethodSystemName))
            {
                isValid = false;

                ModelState.AddModelError("shipping_rate_computation_method_system_name",
                                         "Please provide shipping_rate_computation_method_system_name");
            }
            else if (string.IsNullOrEmpty(shippingOptionName))
            {
                isValid = false;

                ModelState.AddModelError("shipping_option_name", "Please provide shipping_option_name");
            }
            else
            {
                var shippingOptionResponse = _shippingService.GetShippingOptions(shoppingCartItems, _customerService.GetCustomerShippingAddress(customer), customer,
                                                                                 shippingRateComputationMethodSystemName, storeId);

                if (shippingOptionResponse.Success)
                {
                    var shippingOptions = shippingOptionResponse.ShippingOptions.ToList();

                    var shippingOption = shippingOptions
                        .Find(so => !string.IsNullOrEmpty(so.Name) && so.Name.Equals(shippingOptionName, StringComparison.InvariantCultureIgnoreCase));

                    _genericAttributeService.SaveAttribute(customer,
                                                           NopCustomerDefaults.SelectedShippingOptionAttribute,
                                                           shippingOption, storeId);
                }
                else
                {
                    isValid = false;

                    foreach (var errorMessage in shippingOptionResponse.Errors)
                    {
                        ModelState.AddModelError("shipping_option", errorMessage);
                    }
                }
            }

            return isValid;
        }

        private bool IsShippingAddressRequired(ICollection<OrderItemDto> orderItems)
        {
            var shippingAddressRequired = false;

            foreach (var orderItem in orderItems)
            {
                if (orderItem.ProductId != null)
                {
                    var product = _productService.GetProductById(orderItem.ProductId.Value);

                    shippingAddressRequired |= product.IsShipEnabled;
                }
            }

            return shippingAddressRequired;
        }

        private bool AddOrderItemsToCart(ICollection<OrderItemDto> orderItems, Customer customer, int storeId)
        {
            var shouldReturnError = false;

            foreach (var orderItem in orderItems)
            {
                if (orderItem.ProductId != null)
                {
                    var product = _productService.GetProductById(orderItem.ProductId.Value);

                    if (!product.IsRental)
                    {
                        orderItem.RentalStartDateUtc = null;
                        orderItem.RentalEndDateUtc = null;
                    }

                    var attributesXml = _productAttributeConverter.ConvertToXml(orderItem.Attributes.ToList(), product.Id);

                    var errors = _shoppingCartService.AddToCart(customer, product,
                        ShoppingCartType.ShoppingCart, storeId, attributesXml,
                        0M, orderItem.RentalStartDateUtc, orderItem.RentalEndDateUtc,
                        orderItem.Quantity ?? 1);

                    if (errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            ModelState.AddModelError("order", error);
                        }

                        shouldReturnError = true;
                    }
                }
            }

            return shouldReturnError;
        }

        [HttpGet("pending-to-deliver"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrdersPendingToDelivery([FromQuery] int driverId, [FromQuery] decimal latitude, [FromQuery] decimal longitude)
        {
            try
            {
                var ordersRootObject = new OrdersRootObject()
                {
                    Orders = _deliveryAppOrderService.GetOrdersReadyToPickup(driverId, latitude, longitude)
                };

                return Ok(ordersRootObject);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("{id}/set-ready-to-pickup"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SetOrderReadyToPickup(int id)
        {
            try
            {
                _deliveryAppOrderService.SetOrderReadyToPickup(id);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Updates the delivery status of an order to accepted by a messenger.
        /// </summary>
        /// <param name="id">The id of the order.</param>
        /// <param name="driverId">The customer id of the messenger.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPut("{id}/accepted-by-messenger/{driverId}"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SetOrderAcceptedByMessenger(int id, int driverId)
        {
            try
            {
                _deliveryAppOrderService.ChangeStatusToAcceptedByMessenger(id, driverId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("{id}/set-retrieved-by-messenger"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SetOrderRetrievedByMessenger(int id)
        {
            try
            {
                _deliveryAppOrderService.ChangeStatusToRetrievedByMessenger(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("{id}/set-delivered"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SetOrderDelivered(int id)
        {
            try
            {
                _deliveryAppOrderService.ChangeStatusToDelivered(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Inserts a new declined order by messenger request.
        /// </summary>
        /// <param name="id">The id of the order.</param>
        /// <param name="driverId">The customer id of the messenger.</param>
        /// <param name="request">An instance of <see cref="DeclineOrderRequest"/>.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpPost("{id}/declined-by-messenger/{driverId}"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult DeclineOrderByMessenger(int id, int driverId, [FromBody] DeclineOrderRequest request)
        {
            try
            {
                _deliveryAppOrderService.RegisterOrderDeclinedByMessenger(id, request.DeclineReason, driverId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Retrieves the orders assigned to a messenger.
        /// </summary>
        /// <param name="driverId">The customer id of the messenger.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpGet("assigned-to-messenger"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrdersAssignedToMessenger([FromQuery] int driverId)
        {
            try
            {
                var ordersRootObject = new OrdersRootObject()
                {
                    Orders = _deliveryAppOrderService.GetOrdersAssignedToMessenger(driverId)
                };

                return Ok(ordersRootObject);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Retrieves the orders delivered by a messenger.
        /// </summary>
        /// <param name="driverId">The customer id of the messenger.</param>
        /// <returns>An implementation of <see cref="IActionResult"/>.</returns>
        [HttpGet("delivered-by-messenger"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrdersDeliveredByMessenger([FromQuery] int driverId)
        {
            try
            {
                var ordersRootObject = new OrdersRootObject()
                {
                    Orders = _deliveryAppOrderService.GetOrdersDeliveredByMessenger(driverId)
                };

                return Ok(ordersRootObject);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("shipping-method-by-distance"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetShippingMethodByDistance([FromQuery] int fromVendorId, [FromQuery] decimal toLatitude, [FromQuery] decimal toLongitude)
        {
            try
            {
                var shippingMethodnameResult = _deliveryAppShippingService.GetShippingMethodNameByAddressesDistance(fromVendorId, toLatitude, toLongitude);

                if (shippingMethodnameResult.Success)
                {
                    if (string.IsNullOrEmpty(shippingMethodnameResult.Entity.ShippingMethodName)) throw new ArgumentException("DeliveryDistanceExceedsShippingOptions");

                    return Ok(shippingMethodnameResult.Entity);
                }
                else
                {
                    _logger.InsertLog(Core.Domain.Logging.LogLevel.Error, shippingMethodnameResult.Message, shippingMethodnameResult.MessageDetail);
                    return BadRequest(shippingMethodnameResult.Message);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("{id}/status/accepted-by-store"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult SetOrderAcceptedByStore(int id, [FromQuery] int vendorId)
        {
            try
            {
                _deliveryAppOrderService.SetOrderAcceptedByStore(id, vendorId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("{id}/rate"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult InsertOrderRating(int id, OrderRatingModel request)
        {
            try
            {
                _deliveryAppOrderService.InsertOrderRating(id, request);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut("{id}/cancel-order"), Authorize(Roles = "Comercio", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult CancelOrder(int id, [FromBody] DeclinedOrderByStore request)
        {
            try
            {
                _deliveryAppOrderService.CancelOrder(id, request.VendorId, request.Message);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpDelete("{id}/cancel"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _deliveryAppOrderService.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/coordinates"), Authorize(Roles = "Cliente , Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrderCoordinates(int id)
        {
            try
            {
                var coordinate = _deliveryAppOrderService.GetOrderCoordinates(id);
                return Ok(coordinate);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/driver-info"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetDriverInfoAssignedToOrder(int id)
        {
            try
            {
                var driver = _deliveryAppOrderService.GetDriverInfoByOrderId(id);
                return Ok(driver);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("route-destination-indications"), Authorize(Roles = "Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetGoogleDirectionResource([FromQuery] GoogleDirecctionRequest request)
        {
            try
            {
                IOperationResult<GoogleDirectionResourceModel> routeIndications =
                    _googleDirectionService.GetGoogleDirections(request);

                if (!routeIndications.Success)
                {
                    _logger.Error(routeIndications.Message, new ArgumentException(routeIndications.Message));
                }

                return Ok(routeIndications.Entity);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/messenger-location-info"), Authorize(Roles = "Cliente , Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetDriverlocationInfo(int id)
        {
            try
            {
                DriverLocationInfoMapping locationInfo = _deliveryAppOrderService.GetDriverlocationInfo(id);

                return Ok(locationInfo);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("messenger-location-info"), Authorize(Roles = "Cliente , Mensajero", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult CreateDriverlocationUpdateInfo([FromBody] DriverLocationInfoRequest driverRequest)
        {
            try
            {
                _deliveryAppOrderService.DriverlocationCreateInfo(driverRequest);
                return Ok();
            }
            catch (Exception e)
            {

                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPut, Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult UpdateOrder([ModelBinder(typeof(JsonModelBinder<OrderDto>))] Delta<OrderDto> orderDelta)
        {
            try
            {
                _deliveryAppOrderService.UpdateOrder(orderDelta);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Retrieve all orders for customer that were shipped.
        /// </summary>
        /// <param name="customer_id">Id of the customer whoes orders you want to get</param>
        /// <response code="200">OK</response>
        /// <response code="401">Unauthorized</response>
        [HttpGet]
        [Route("customer/{customer_id}/delivered")]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrdersDeliveredByCustomerId(int customer_id)
        {
            try
            {
                IList<OrderDeliveryAppDto> ordersForCustomer = _orderApiService.GetOrdersByCustomerId(customer_id)
                                                            .Where(x => x.ShippingStatus == ShippingStatus.Delivered)
                                                            .Select(x => _dtoHelper.PrepareOrderDTO(x))
                                                            .ToList();

                var ordersRootObject = new DeliveryAppOrderRootObject()
                {
                    Orders = ordersForCustomer
                };

                return Ok(ordersRootObject);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{id}"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult UpdateOrderPaymenMethod(int id, [FromQuery] string paymentMethod)
        {
            try
            {
                _deliveryAppOrderService.ChangeOrderPaymentMethod(id, paymentMethod);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("{id}/delivery-info"), Authorize(Roles = "Mensajero , Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetDriverInfo(int id)
        {
            try
            {
                DriverInfoModel driverInfo = _deliveryAppDriverService.GetDriverInfo(orderId: id);

                return Ok(driverInfo);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost("tax-amount"), Authorize(Roles = "Mensajero , Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetTaxAmountOfOrderProducts(GetOrderTotalTaxRequest productInfoRequest)
        {
            try
            {
                decimal taxAmount = _deliveryAppProductService
                    .GetTaxAmountOfOrderProducts(productInfoRequest);

                return Ok(taxAmount);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("discount-coupon"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetOrderCoupon([FromQuery] string code, [FromQuery] int customerId)
        {
            try
            {
                CouponModel coupon = _deliveryAppOrderService.GetDiscountCouponToApplyOrder(code, customerId);
                return Ok(coupon);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("validateProductDiscount"), Authorize(Roles = "Cliente", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetProductDiscount([FromQuery] int productId)
        {
            List<Discount> discList = new List<Discount>();
            try
            {
                if (productId <= 0)
                {
                    return BadRequest("Product Id not provided");
                }

                Product receivedProduct = _productService.GetProductById(productId);
                if (receivedProduct == null)
                {
                    return NotFound("No product found with the provided id");
                }
                IList<DiscountProductMapping> productDiscounts = _productService.GetAllDiscountsAppliedToProduct(receivedProduct.Id);
                if (productDiscounts ==null || productDiscounts.Count<=0)
                {
                    return Ok(discList);
                }
                
                foreach (DiscountProductMapping dd2 in productDiscounts)
                {
                    discList.Add(_discountService.GetDiscountById(dd2.DiscountId));
                }
                return Ok(discList);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
                return BadRequest(new { e.Message });
            }
        }

        [HttpGet("notify-paid-order")]
        public IActionResult OnOrderPaidByCyberSource([FromQuery] int orderId)
        {
            Order order = _orderRepository.GetById(orderId);

            if (order is null) return BadRequest("OrderNotFound");

            if (order.PaymentMethodSystemName != "Payments.CyberSource") return BadRequest("OrderNotPaidByCyberSource");

            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.ClientClientOrderCreated, order.Id));
            _notificationCenterService.SendNotification(_notificationRequestBuilder.Build(NotificationTemplateType.ClientCommerceNewOrder, order.Id));
            return Ok();
        }

        [HttpGet("request-tracking-update")]
        public IActionResult UpdateOrderTracking([FromQuery, Required] int orderId)
        {
            _notificationCenterService.SendDriverCoordinateTrackingUpdate(new DriverLocationInfoRequest
            {
                OrderId = orderId,
            });

            return NoContent();
        }

        #endregion
    }

}


