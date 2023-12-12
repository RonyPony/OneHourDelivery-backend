using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Models;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.Admin.Models.Discounts;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents an implementation for <see cref="IDeliveryAppDiscountModelFactory"/>.
    /// </summary>
    public sealed class DeliveryAppDiscountModelFactory : IDeliveryAppDiscountModelFactory
    {
        #region Fields

        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDeliveryAppBaseAdminModelFactory _deliveryAppBaseAdminModelFactory;
        private readonly IDeliveryAppDiscountService _deliveryAppDiscountService;
        private readonly IDiscountPluginManager _discountPluginManager;
        private readonly IDiscountService _discountService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<VendorDiscount> _vendorDiscountRepository;
        private readonly IVendorService _vendorService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        private readonly IRepository<CustomerDiscountMapping> _customerDiscountMappingRepository;
        private readonly IAddressService _addressService;
        private readonly IGenericAttributeService _genericAttributeService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppDiscountModelFactory"/>.
        /// </summary>
        /// <param name="baseAdminModelFactory">An implementation of <see cref="IBaseAdminModelFactory"/>.</param>
        /// <param name="currencyService">An implementation of <see cref="ICurrencyService"/>.</param>
        /// <param name="currencySettings">An instance of <see cref="CurrencySettings"/>.</param>
        /// <param name="dateTimeHelper">An implementation of <see cref="IDateTimeHelper"/>.</param>
        /// <param name="deliveryAppBaseAdminModelFactory">An implementation of <see cref="IDeliveryAppBaseAdminModelFactory"/>.</param>
        /// <param name="deliveryAppDiscountService">An implementation of <see cref="IDeliveryAppDiscountService"/>.</param>
        /// <param name="discountPluginManager">An implementation of <see cref="IDiscountPluginManager"/>.</param>
        /// <param name="discountService">An implementation of <see cref="IDiscountService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="vendorDiscountRepository">An implementation of <see cref="IRepository{VendorDiscount}"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="customerDiscountMappingRepository">An implementation of <see cref="IRepository{CustomerDiscountMapping}"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        public DeliveryAppDiscountModelFactory(
            IBaseAdminModelFactory baseAdminModelFactory,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IDateTimeHelper dateTimeHelper,
            IDeliveryAppBaseAdminModelFactory deliveryAppBaseAdminModelFactory,
            IDeliveryAppDiscountService deliveryAppDiscountService,
            IDiscountPluginManager discountPluginManager,
            IDiscountService discountService,
            ILocalizationService localizationService,
            IRepository<VendorDiscount> vendorDiscountRepository,
            IVendorService vendorService,
            IWebHelper webHelper,
            IWorkContext workContext,
            ICustomerService customerService,
            IRepository<CustomerDiscountMapping> customerDiscountMappingRepository,
            IAddressService addressService,
            IGenericAttributeService genericAttributeService)
        {
            _baseAdminModelFactory = baseAdminModelFactory;
            _currencyService = currencyService;
            _currencySettings = currencySettings;
            _dateTimeHelper = dateTimeHelper;
            _deliveryAppBaseAdminModelFactory = deliveryAppBaseAdminModelFactory;
            _deliveryAppDiscountService = deliveryAppDiscountService;
            _discountPluginManager = discountPluginManager;
            _discountService = discountService;
            _localizationService = localizationService;
            _vendorDiscountRepository = vendorDiscountRepository;
            _vendorService = vendorService;
            _webHelper = webHelper;
            _workContext = workContext;
            _customerService = customerService;
            _customerDiscountMappingRepository = customerDiscountMappingRepository;
            _addressService = addressService;
            _genericAttributeService = genericAttributeService;
        }

        #endregion

        #region Utilities

        private Vendor GetVendorByDiscountIdOrDefault(int discountId)
        {
            VendorDiscount vendorDiscount = _vendorDiscountRepository.Table.FirstOrDefault(mapping => mapping.DiscountId == discountId);
            if (vendorDiscount is null) { return null; }
            Vendor vendor = _vendorService.GetVendorById(vendorDiscount.VendorId);
            return vendor;
        }

        private DiscountUsageHistorySearchModel PrepareDiscountUsageHistorySearchModel(DiscountUsageHistorySearchModel searchModel,
            Discount discount)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            searchModel.DiscountId = discount.Id;

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }

        private DiscountProductSearchModel PrepareDiscountProductSearchModel(DiscountProductSearchModel searchModel, Discount discount)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            searchModel.DiscountId = discount.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }

        private DiscountCategorySearchModel PrepareDiscountCategorySearchModel(DiscountCategorySearchModel searchModel, Discount discount)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            searchModel.DiscountId = discount.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }

        private DiscountManufacturerSearchModel PrepareDiscountManufacturerSearchModel(DiscountManufacturerSearchModel searchModel,
            Discount discount)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            searchModel.DiscountId = discount.Id;

            searchModel.SetGridPageSize();

            return searchModel;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public DeliveryAppDiscountSearchModel PrepareDiscountSearchModel(DeliveryAppDiscountSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (_workContext.CurrentVendor != null) { searchModel.VendorId = _workContext.CurrentVendor.Id; }

            _deliveryAppBaseAdminModelFactory.PrepareDiscountTypes(searchModel.AvailableDiscountTypes);

            _baseAdminModelFactory.PrepareVendors(searchModel.AvailableVendors);

            searchModel.SetGridPageSize();

            return searchModel;
        }

        ///<inheritdoc/>
        public DeliveryAppDiscountListModel PrepareDiscountListModel(DeliveryAppDiscountSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var discountType = searchModel.SearchDiscountTypeId > 0 ? (DiscountType?)searchModel.SearchDiscountTypeId : null;
            var startDateUtc = searchModel.SearchStartDate.HasValue ?
                (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.SearchStartDate.Value, _dateTimeHelper.CurrentTimeZone) : null;
            var endDateUtc = searchModel.SearchEndDate.HasValue ?
                (DateTime?)_dateTimeHelper.ConvertToUtcTime(searchModel.SearchEndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1) : null;

            var discounts = _deliveryAppDiscountService.GetAllDiscounts(showHidden: true,
                discountType: discountType,
                couponCode: searchModel.SearchDiscountCouponCode,
                discountName: searchModel.SearchDiscountName,
                startDateUtc: startDateUtc,
                endDateUtc: endDateUtc,
                vendorId: searchModel.VendorId).ToPagedList(searchModel);

            var model = new DeliveryAppDiscountListModel().PrepareToGrid(searchModel, discounts, () =>
            {
                return discounts.Select(discount =>
                {
                    var discountModel = discount.ToModel<DeliveryAppDiscountModel>();

                    discountModel.DiscountTypeName = _localizationService.GetLocalizedEnum(discount.DiscountType);
                    discountModel.PrimaryStoreCurrencyCode = _currencyService
                        .GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId)?.CurrencyCode;
                    discountModel.TimesUsed = _discountService.GetAllDiscountUsageHistory(discount.Id, pageSize: 1).TotalCount;

                    Vendor discountVendor = GetVendorByDiscountIdOrDefault(discount.Id);
                    discountModel.VendorId = discountVendor == null ? 0 : discountVendor.Id;
                    discountModel.VendorName = discountVendor == null ? "" : $"{discountVendor.Name} ({discountVendor.Email})";

                    return discountModel;
                });
            });

            return model;
        }

        ///<inheritdoc/>
        public DeliveryAppDiscountModel PrepareDiscountModel(DeliveryAppDiscountModel model, Discount discount, bool excludeProperties = false)
        {
            if (discount != null)
            {
                model ??= discount.ToModel<DeliveryAppDiscountModel>();

                var discountRules = _discountPluginManager.LoadAllPlugins();
                foreach (var discountRule in discountRules)
                {
                    model.AvailableDiscountRequirementRules.Add(new SelectListItem
                    {
                        Text = discountRule.PluginDescriptor.FriendlyName,
                        Value = discountRule.PluginDescriptor.SystemName
                    });
                }

                model.AvailableDiscountRequirementRules.Insert(0, new SelectListItem
                {
                    Text = _localizationService.GetResource("Admin.Promotions.Discounts.Requirements.DiscountRequirementType.AddGroup"),
                    Value = "AddGroup"
                });

                model.AvailableDiscountRequirementRules.Insert(0, new SelectListItem
                {
                    Text = _localizationService.GetResource("Admin.Promotions.Discounts.Requirements.DiscountRequirementType.Select"),
                    Value = string.Empty
                });

                var requirementGroups = _discountService.GetAllDiscountRequirements(discount.Id).Where(requirement => requirement.IsGroup);
                model.AvailableRequirementGroups = requirementGroups.Select(requirement =>
                    new SelectListItem { Value = requirement.Id.ToString(), Text = requirement.DiscountRequirementRuleSystemName }).ToList();

                PrepareDiscountUsageHistorySearchModel(model.DiscountUsageHistorySearchModel, discount);
                PrepareDiscountProductSearchModel(model.DiscountProductSearchModel, discount);
                PrepareDiscountCategorySearchModel(model.DiscountCategorySearchModel, discount);
                PrepareDiscountManufacturerSearchModel(model.DiscountManufacturerSearchModel, discount);
            }

            _deliveryAppBaseAdminModelFactory.PrepareDiscountTypes(items: model.AvailableDiscountTypes, withSpecialDefaultItem: false);

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;

            if (model.RequiresCouponCode && !string.IsNullOrEmpty(model.CouponCode))
            {
                model.DiscountUrl = QueryHelpers.AddQueryString(_webHelper.GetStoreLocation().TrimEnd('/'),
                    NopDiscountDefaults.DiscountCouponQueryParameter, model.CouponCode);
            }

            if (discount == null)
                model.LimitationTimes = 1;

            return model;
        }

        ///<inheritdoc/>
        public AddProductToDiscountSearchModel PrepareAddProductToDiscountSearchModel(AddProductToDiscountSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            _baseAdminModelFactory.PrepareCategories(searchModel.AvailableCategories);

            _baseAdminModelFactory.PrepareManufacturers(searchModel.AvailableManufacturers);

            _baseAdminModelFactory.PrepareStores(searchModel.AvailableStores);

            if (_workContext.CurrentVendor != null)
            {
                searchModel.SearchVendorId = _workContext.CurrentVendor.Id;
                searchModel.AvailableVendors.Add(new SelectListItem { Text = _workContext.CurrentVendor.Name, Value = _workContext.CurrentVendor.Id.ToString() });
            }
            else { _baseAdminModelFactory.PrepareVendors(searchModel.AvailableVendors); }

            _baseAdminModelFactory.PrepareProductTypes(searchModel.AvailableProductTypes);

            searchModel.SetPopupGridPageSize();

            return searchModel;
        }

        ///<inheritdoc/>
        public DeliveryAppCustomerDiscountList GetCustomersAssignedToDiscountCoupon(int discountId)
        {
            var customerDiscounts = new List<DeliveryAppCustomerCustomDiscountModel>();
            IList<CustomerDiscountMapping> foundCustomDiscount = _customerDiscountMappingRepository.Table.Where(x => x.DiscountId == discountId).ToList();

            foreach (CustomerDiscountMapping discount in foundCustomDiscount)
            {
                var foundCustomer = _customerService.GetCustomerById(discount.CustomerId);
                if (foundCustomer is null)
                    throw new ArgumentException("CustomerNotFound");

                var foundDiscount = _discountService.GetDiscountById(discount.DiscountId);
                if (foundDiscount is null)
                    throw new ArgumentException("DiscountNotFound");

                string firstName = _genericAttributeService.GetAttribute<string>(foundCustomer, NopCustomerDefaults.FirstNameAttribute);
                string lastName = _genericAttributeService.GetAttribute<string>(foundCustomer, NopCustomerDefaults.LastNameAttribute);

                customerDiscounts.Add(new DeliveryAppCustomerCustomDiscountModel
                {
                    CustomerId = foundCustomer.Id,
                    CustomerName = $"{firstName} {lastName}",
                    DiscountId = foundDiscount.Id,
                    DiscountName = foundDiscount.Name,
                    DiscountAmount = foundDiscount.UsePercentage ? foundDiscount.DiscountPercentage : foundDiscount.DiscountAmount
                });
            }

            PagedList<DeliveryAppCustomerCustomDiscountModel> customerPageList = new
                PagedList<DeliveryAppCustomerCustomDiscountModel>(customerDiscounts, 0, int.MaxValue);

            DeliveryAppCustomerDiscountList discountList = new DeliveryAppCustomerDiscountList()
            .PrepareToGrid(null, customerPageList, () => customerPageList);

            return discountList;
        }

        ///<inheritdoc/>
        public DeliveryAppAddCustomerToDiscountSearchModel PrepareAssignDiscountToCustomer(DeliveryAppAddCustomerToDiscountSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetPopupGridPageSize();

            return searchModel;
        }

        ///<inheritdoc/>
        public DeliveryAppAddCustomerToListModel PrepareAddAssignCustomerToDiscountSearchModel(DeliveryAppAddCustomerToDiscountSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            List<Customer> activeCustomers = _customerService.GetAllCustomers(email: searchModel.SearchCustomerEmail)
                                                             .Where(customer => customer.Active).ToList();

            var customers = new PagedList<Customer>(activeCustomers, 0, int.MaxValue);

            var model = new DeliveryAppAddCustomerToListModel().PrepareToGrid(searchModel, customers, () =>
            {
                return customers.Select(customer =>
                {
                    var customerModel = customer.ToModel<CustomerModel>();
                    customerModel.Id = customer.Id;
                    customerModel.Email = customer.Email;
                    customerModel.FirstName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.FirstNameAttribute);
                    customerModel.LastName = _genericAttributeService.GetAttribute<string>(customer, NopCustomerDefaults.LastNameAttribute);

                    return customerModel;
                });
            });

            return model;
        }

        #endregion
    }
}
