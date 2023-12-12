using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Services;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents an implementation of <see cref="IDeliveryAppBaseAdminModelFactory"/>.
    /// </summary>
    public sealed class DeliveryAppBaseAdminModelFactory : IDeliveryAppBaseAdminModelFactory
    {
        #region Fields

        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;
        private readonly IRepository<CheckoutAttribute> _checkoutAttributeRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerCustomerRoleMapping> _customerCustomerRoleMappingRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<OrderPaymentCollectionStatus> _orderPaymentCollectionRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppBaseAdminModelFactory"/>.
        /// </summary>
        /// <param name="checkoutAttributeService">An implementation of <see cref="ICheckoutAttributeService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="checkoutAttributeRepository">An implementation of <see cref="IRepository{CheckoutAttribute}"/>.</param>
        /// <param name="customerRepository">An implementation of <see cref="IRepository{Customer}"/>.</param>
        /// <param name="customerCustomerRoleMappingRepository">An implementation of <see cref="IRepository{CustomerCustomerRoleMapping}"/>.</param>
        /// <param name="customerRoleRepository">An implementation of <see cref="IRepository{CustomerRole}"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="orderPaymentCollectionRepository">An implementation of <see cref="IRepository{OrderPaymentCollectionStatus}"/>.</param>
        public DeliveryAppBaseAdminModelFactory(
            ICheckoutAttributeService checkoutAttributeService,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            ILogger logger,
            IRepository<CheckoutAttribute> checkoutAttributeRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerCustomerRoleMapping> customerCustomerRoleMappingRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IRepository<OrderPaymentCollectionStatus> orderPaymentCollectionRepository,
            IWorkContext workContext)
        {
            _checkoutAttributeService = checkoutAttributeService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _logger = logger;
            _checkoutAttributeRepository = checkoutAttributeRepository;
            _customerRepository = customerRepository;
            _customerCustomerRoleMappingRepository = customerCustomerRoleMappingRepository;
            _customerRoleRepository = customerRoleRepository;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _workContext = workContext;
            _orderPaymentCollectionRepository = orderPaymentCollectionRepository;
        }

        #endregion

        #region Utilities

        private List<SelectListItem> GetDriverList(bool showHidden = true)
        {
            return (from c in _customerRepository.Table.Where(c => !c.Deleted && c.Active).AsQueryable()
                    join ccrm in _customerCustomerRoleMappingRepository.Table on c.Id equals ccrm.CustomerId
                    join cr in _customerRoleRepository.Table on ccrm.CustomerRoleId equals cr.Id
                    where cr.Name == "Mensajero"
                    select c).ToList().Select(customer =>
                    {
                        var firstName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
                        var lastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute);
                        var listItem = new SelectListItem
                        {
                            Text = $"{firstName} {lastName} ({customer.Email})",
                            Value = customer.Id.ToString()
                        };
                        return listItem;
                    }).ToList();
        }

        private List<SelectListItem> GetPaymentMethodList(bool showHidden = true)
        {
            var selectList = new List<SelectListItem>();
            CheckoutAttribute paymentMethodAttribute = _checkoutAttributeRepository.Table.FirstOrDefault(attr => attr.Name == Defaults.PaymentMethodCheckoutAttribute.Name);
            if (paymentMethodAttribute is null) return selectList;
            IList<CheckoutAttributeValue> checkoutAttributeValues = _checkoutAttributeService.GetCheckoutAttributeValues(paymentMethodAttribute.Id);

            foreach (CheckoutAttributeValue checkoutAttributeValue in checkoutAttributeValues)
            {
                selectList.Add(new SelectListItem
                {
                    Text = checkoutAttributeValue.Name,
                    Value = checkoutAttributeValue.Name
                });
            }

            return selectList;
        }

        private void PrepareDefaultItem(IList<SelectListItem> items, bool withSpecialDefaultItem, string defaultItemText = null)
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

        private string GetFormattedCustomerNameAndEmail(Customer customer)
        {
            string firstName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
            string lastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute);

            return $"{firstName} {lastName} ({customer.Email})";
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public void PrepareDrivers(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available drivers
            foreach (var driverItem in GetDriverList())
            {
                items.Add(driverItem);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        ///<inheritdoc/>
        public OrderDeliveryInfoModel PrepareOrderDeliveryInfoModel(int orderId)
        {
            OrderDeliveryStatusMapping orderDelivery = _orderDeliveryStatusMappingRepository.Table.FirstOrDefault(mapping => mapping.OrderId == orderId);
            if (orderDelivery is null) return null;
            var model = orderDelivery.ToModel<OrderDeliveryInfoModel>();
            model.DriverId = orderDelivery.CustomerId ?? 0;
            model.AvailableDrivers = GetDriverList();
            model.AvailableDrivers.Insert(0, new SelectListItem { Value = "0", Text = "N/A", Disabled = true });

            return model;
        }

        ///<inheritdoc/>
        public void PrepareDiscountTypes(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            const string assignedToProductDiscountValue = "2";
            const string assignedToOrderSubtotalDiscountValue = "20";
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            string[] filteredDiscountsTypes = { assignedToProductDiscountValue, assignedToOrderSubtotalDiscountValue};
            //prepare available discount types
            SelectList availableDiscountTypeItems = DiscountType.AssignedToOrderTotal.ToSelectList(false);
            if (_workContext.CurrentVendor != null)
            {
                var filteredDiscountTypesForVendors = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = ((int)DiscountType.AssignedToSkus).ToString(),
                        Text = _localizationService.GetLocalizedEnum(DiscountType.AssignedToSkus)
                    }
                };
                availableDiscountTypeItems = new SelectList(filteredDiscountTypesForVendors, "Value", "Text");
            }

            foreach (var discountTypeItem in availableDiscountTypeItems)
            {
                if (filteredDiscountsTypes.Contains(discountTypeItem.Value))
                {
                    items.Add(discountTypeItem);
                }
            }
            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }

        ///<inheritdoc/>
        public void PreparePaymentMethods(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available payment methods
            foreach (var paymentMethod in GetPaymentMethodList())
            {
                items.Add(paymentMethod);
            }

            //insert special item for the default value
            items.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = string.Empty });
        }

        ///<inheritdoc/>
        public OrderPaymentCollectionModel PrepareOrderPaymentCollectionModel(int orderId)
        {
            try
            {
                OrderPaymentCollectionStatus paymentCollectionStatus = _orderPaymentCollectionRepository.Table.FirstOrDefault(mapping => mapping.OrderId == orderId);
                if (paymentCollectionStatus is null) { throw new NullReferenceException($"The payment collection status for order id {orderId} was not found."); }
                var model = paymentCollectionStatus.ToModel<OrderPaymentCollectionModel>();

                if (paymentCollectionStatus.PaymentCollectionStatus == PaymentCollectionStatus.Collected)
                {
                    Customer foundCustomer = _customerRepository.Table.FirstOrDefault(customer => customer.Id == paymentCollectionStatus.CollectedByCustomerId);
                    if (foundCustomer is null) { throw new NullReferenceException($"The customer that collected the payment for order id {orderId} was not found."); }
                    model.CollectedByCustomerName = GetFormattedCustomerNameAndEmail(foundCustomer);
                }

                return model;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);

                return null;
            }
        }

        #endregion
    }
}
