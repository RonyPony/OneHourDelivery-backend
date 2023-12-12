using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Tasks;
using Nop.Core.Domain.Tax;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Plugin.Shipping.FixedByWeightByTotal;
using Nop.Plugin.Tax.FixedOrByCountryStateZip.Domain;
using Nop.Plugin.Tax.FixedOrByCountryStateZip.Services;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tasks;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend
{
    /// <summary>
    /// Represents the main class for this plugin.
    /// </summary>
    public class DeliveryAppIntegrationPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly IAddressAttributeService _addressAttributeService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IVendorAttributeService _vendorAttributeService;
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly IShippingService _shippingService;
        private readonly IRepository<ShippingMethod> _shippingMethodRepository;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICustomerService _customerService;
        private readonly IPermissionService _permissionService;
        private readonly IWebHelper _webHelper;
        private readonly IRepository<PermissionRecordCustomerRoleMapping> _permisionRecordRoleMappingRepository;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductAttributeMapping> _productAttributeRepository;
        private readonly ITaxCategoryService _taxCategoryService;
        private readonly IRepository<TaxCategory> _taxCategoryRepository;
        private readonly ICountryStateZipService _countryStateZipService;
        private readonly IRepository<TaxRate> _taxRateRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IStoreService _storeService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        private readonly IRepository<SpecificationAttributeOption> _specificationAttributteOptionRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;


        #endregion 

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DeliveryAppIntegrationBackendPlugin"/>.
        /// </summary>
        /// <param name="addressAttributeService">An implementation of <see cref="IAddressAttributeService"/>.</param>
        /// <param name="languageService">An implementation of <see cref="ILanguageService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="vendorAttributeService">An implementation of <see cref="IVendorAttributeService"/>.</param>
        /// <param name="scheduleTaskService">An implementation of <see cref="IScheduleTaskService"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="shippingMethodRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="ShippingMethod"/>.</param>
        /// <param name="customerAttributeService">An implementation of <see cref="ICustomerAttributeService"/> where T is <see cref="ShippingMethod"/>.</param>
        /// <param name="checkoutAttributeService">An implementation of <see cref="ICheckoutAttributeService"/> </param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/> </param>
        /// <param name="permissionService">An implementation of <see cref="IPermissionService"/> </param>
        /// <param name="permisionRecordRoleMappingRepository">An implementation of <see cref="IRepository{PermissionRecordCustomerRoleMapping}"/> </param>
        /// <param name="productAttributeService">An implementation of <see cref="IProductAttributeService"/>.</param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{Product}"/>.</param>
        /// <param name="webHelper">An implementation of <see cref="IWebHelper"/>.</param>
        /// <param name="productAttributeRepository">An implementation of <see cref="IRepository{ProductAttributeMapping}"/>.</param>
        /// <param name="taxCategoryService">An implementation of <see cref="ITaxCategoryService"/>.</param>
        /// <param name="taxCategoryRepository">An implementation of <see cref="IRepository{TaxCategory}"/>.</param>
        /// <param name="storeService">An implementation of <see cref="IStoreService"/>.</param>
        /// <param name="specificationAttributeService">An implementation of <see cref="ISpecificationAttributeService"/>.</param>
        /// <param name="specificationAttributeRepository">An implementation of <see cref="IRepository{SpecificationAttribute}"/>.</param>
        /// <param name="specificationAttributteOptionRepository">An implementation of <see cref="IRepository{SpecificationAttributeOption}"/>.</param>
        /// <param name="productSpecificationAttributeRepository">An implementation of <see cref="IRepository{ProductSpecificationAttribute}"/>.</param>
        public DeliveryAppIntegrationPlugin(
            IAddressAttributeService addressAttributeService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IVendorAttributeService vendorAttributeService,
            IScheduleTaskService scheduleTaskService,
            IShippingService shippingService,
            IRepository<ShippingMethod> shippingMethodRepository,
            ICheckoutAttributeService checkoutAttributeService,
            ICustomerAttributeService customerAttributeService,
            ICustomerService customerService,
            IPermissionService permissionService,
            IWebHelper webHelper,
            IRepository<PermissionRecordCustomerRoleMapping> permisionRecordRoleMappingRepository,
            IProductAttributeService productAttributeService,
            IRepository<Product> productRepository,
            IRepository<ProductAttributeMapping> productAttributeRepository,
            ITaxCategoryService taxCategoryService,
            IRepository<TaxCategory> taxCategoryRepository,
            ICountryStateZipService countryStateZipService,
            IRepository<TaxRate> taxRateRepository,
            IRepository<Country> countryRepository, 
            IStoreService storeService,
            ISpecificationAttributeService specificationAttributeService ,
            IRepository<SpecificationAttribute> specificationAttributeRepository ,
            IRepository<SpecificationAttributeOption> specificationAttributteOptionRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository)
        {
            _addressAttributeService = addressAttributeService;
            _languageService = languageService;
            _localizationService = localizationService;
            _settingService = settingService;
            _vendorAttributeService = vendorAttributeService;
            _scheduleTaskService = scheduleTaskService;
            _shippingService = shippingService;
            _shippingMethodRepository = shippingMethodRepository;
            _customerAttributeService = customerAttributeService;
            _checkoutAttributeService = checkoutAttributeService;
            _customerService = customerService;
            _permissionService = permissionService;
            _webHelper = webHelper;
            _permisionRecordRoleMappingRepository = permisionRecordRoleMappingRepository;
            _productAttributeService = productAttributeService;
            _productRepository = productRepository;
            _productAttributeRepository = productAttributeRepository;
            _taxCategoryService = taxCategoryService;
            _taxCategoryRepository = taxCategoryRepository;
            _countryStateZipService = countryStateZipService;
            _taxRateRepository = taxRateRepository;
            _countryRepository = countryRepository;
            _storeService = storeService;
            _specificationAttributeService = specificationAttributeService;
            _specificationAttributeRepository = specificationAttributeRepository;
            _specificationAttributteOptionRepository = specificationAttributteOptionRepository;
            _productSpecificationAttributeRepository = productSpecificationAttributeRepository;
        }

        #endregion

        #region Utilities

        private void InsertLanguagesResources()
        {
            var language = _languageService.GetAllLanguages().FirstOrDefault(x => x.LanguageCulture.Contains("es-"));

            if (language != null)
            {
                AddLocaleResources(LocaleResources.SpanishResources, language.LanguageCulture);
            }

            AddLocaleResources(LocaleResources.EnglishResources, "en-US");
        }

        private void AddLocaleResources(Dictionary<string, string> resources, string languageLanguageCulture)
        {
            foreach (var resource in resources)
            {
                _localizationService.AddOrUpdatePluginLocaleResource(resource.Key, resource.Value, languageLanguageCulture);
            }
        }

        private void InsertRecalculateVendorReviewScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(Defaults.TaskType) != null)
                return;

            var scheduleTask = new ScheduleTask
            {
                Name = Defaults.TaskName,
                Type = Defaults.TaskType,
                Seconds = Defaults.TaskDuration
            };

            _scheduleTaskService.InsertTask(scheduleTask);
        }

        private void DeleteRecalculateVendorReviewsScheduleTask()
        {
            if (_scheduleTaskService.GetTaskByType(Defaults.TaskType) is ScheduleTask task)
                _scheduleTaskService.DeleteTask(task);
        }

        private void AllowCustomerUploadAvatarPicture()
        {
            var customerSettings = _settingService.LoadSetting<CustomerSettings>();
            customerSettings.AllowCustomersToUploadAvatars = true;
            _settingService.SaveSetting(customerSettings);
            _settingService.ClearCache();
        }

        private void RemoveAppDeliveryShippingSetting()
        {
            var setting = _settingService.GetSetting(Defaults.AdditionalShippingChargeKey);
            if (setting != null) _settingService.DeleteSetting(setting);
        }

        private void InsertDeliveryAppRoles()
        {
            IEnumerable<CustomerRole> customerRoles = _customerService.GetAllCustomerRoles();

            foreach (var roleName in Defaults.CustomerRolesPermissions.Keys)
            {
                var currentCustomerRole = customerRoles.FirstOrDefault(x => x.Name.Equals(roleName));

                if (currentCustomerRole != null)
                {
                    currentCustomerRole.IsSystemRole = false;
                    _customerService.UpdateCustomerRole(currentCustomerRole);
                }
                else
                {
                    _customerService.InsertCustomerRole(new CustomerRole
                    {
                        Active = true,
                        IsSystemRole = false,
                        SystemName = roleName == "Comercio" ? "Vendors" : roleName,
                        Name = roleName,
                        EnablePasswordLifetime = true
                    });
                }
            }

            customerRoles = _customerService.GetAllCustomerRoles();

            InsertDeliveryAppRolesPermissions(customerRoles);
        }

        private void InsertDeliveryAppRolesPermissions(IEnumerable<CustomerRole> customerRoles)
        {
            IEnumerable<PermissionRecord> allPermissions = _permissionService.GetAllPermissionRecords();

            foreach (string roleName in Defaults.CustomerRolesPermissions.Keys)
            {
                CustomerRole currentCustomerRole = customerRoles.FirstOrDefault(x => x.Name.Equals(roleName));

                if (currentCustomerRole != null)
                {
                    foreach (string rolePermissionName in Defaults.CustomerRolesPermissions[currentCustomerRole.Name])
                    {
                        PermissionRecord permission = allPermissions.FirstOrDefault(x => x.Name.Equals(rolePermissionName));

                        if (_permisionRecordRoleMappingRepository.Table
                           .FirstOrDefault(permisionRecord => permisionRecord.CustomerRoleId == currentCustomerRole.Id
                           && permisionRecord.PermissionRecordId == permission.Id) is null)
                        {
                            _permissionService.InsertPermissionRecordCustomerRoleMapping(new PermissionRecordCustomerRoleMapping
                            {
                                CustomerRoleId = currentCustomerRole.Id,
                                PermissionRecordId = permission.Id,
                            });
                        }
                    }
                }
            }
        }

        private void DeleteDeliveryAppRoles()
        {
            IEnumerable<CustomerRole> allCustomerRoles = _customerService.GetAllCustomerRoles();
            IEnumerable<PermissionRecord> allPermissionsRecord = _permissionService.GetAllPermissionRecords();

            foreach (var roleName in Defaults.CustomerRolesPermissions.Keys)
            {
                CustomerRole customerRole = allCustomerRoles.FirstOrDefault(x => x.Name.Equals(roleName));

                if (customerRole != null)
                {
                    foreach (var permissionRecordName in Defaults.CustomerRolesPermissions[customerRole.Name])
                    {
                        var currentPermission = allPermissionsRecord.FirstOrDefault(x => x.Name.Equals(permissionRecordName));

                        try
                        {
                            if (currentPermission != null)
                                _permissionService.DeletePermissionRecordCustomerRoleMapping(currentPermission.Id, customerRole.Id);
                        }
                        catch (Exception ex){}
                    }
                }
            }
        }

        private void InsertDefaultsShippingMethods()
        {
            foreach (KeyValuePair<string, decimal> keyValue in Defaults.ShippingMethodsByKm)
            {
                ShippingMethod foundShippingMethod = _shippingMethodRepository.Table.FirstOrDefault(shipping => shipping.Name == keyValue.Key);
                if (foundShippingMethod != null) continue;
                var newShippingMethod = new ShippingMethod
                {
                    Name = keyValue.Key,
                    DisplayOrder = 0,
                    Description = ""
                };

                _shippingService.InsertShippingMethod(newShippingMethod);
                _settingService.SetSetting(string.Format(FixedByWeightByTotalDefaults.FixedRateSettingsKey, newShippingMethod.Id), keyValue.Value, 0);
            }
        }

        private void InsertProductSpecialSpecificationAttributeToAllTheProducts()
        {
            var products = _productRepository.Table.Where(x => !x.Deleted).ToList();

            var productAttibute = _productAttributeService.GetAllProductAttributes().
                FirstOrDefault(attribute => attribute.Name.Equals(Defaults.ProductSpecialSpecificationAttribute.Name));

            foreach (Product product in products)
            {
                if (!_productAttributeRepository.Table.Any(x => x.ProductId == product.Id && x.ProductAttributeId == productAttibute.Id))
                {
                    _productAttributeService.InsertProductAttributeMapping(new ProductAttributeMapping
                    {
                        ProductId = product.Id,
                        ProductAttributeId = productAttibute.Id,
                        AttributeControlType = AttributeControlType.TextBox,
                        IsRequired = false
                    });
                }
            }
        }

        private void InsertVendorDefaultPictureToAllProduct()
        {
            CreateSpecificationAttribute();
            CreateAffirmationSpecificationOption();
            CreateNegationSpecificationOption();
            AssignProductSpecificationAttribute();
        }

        private void CreateSpecificationAttribute()
        {
            if (!_specificationAttributeRepository.Table
                .Any(attributte => attributte.Name.Equals(Defaults.VendorDefaultProduct)))
            {
                _specificationAttributeService.InsertSpecificationAttribute(new SpecificationAttribute
                {
                    Name = Defaults.VendorDefaultProduct,
                    DisplayOrder = 1
                });
            }
        }

        private void CreateAffirmationSpecificationOption()
        {
            var specificationAttribute = new SpecificationAttribute();

            if (!_specificationAttributteOptionRepository.
                Table.Any(attribute => attribute.Name.Equals(Defaults.VendorDefaultProductAffirmation)))
            {
                specificationAttribute = _specificationAttributeRepository.Table
               .FirstOrDefault(attributte => attributte.Name.Equals(Defaults.VendorDefaultProduct));

                _specificationAttributeService.InsertSpecificationAttributeOption(new SpecificationAttributeOption
                {
                    Name = Defaults.VendorDefaultProductAffirmation,
                    SpecificationAttributeId = specificationAttribute.Id
                });
            }
        }

        private void CreateNegationSpecificationOption()
        {
            var specificationAttribute = new SpecificationAttribute();

            if (!_specificationAttributteOptionRepository.
                Table.Any(attribute => attribute.Name.Equals(Defaults.VendorDefaultProductNegation)))
            {
                specificationAttribute = _specificationAttributeRepository.Table
                .FirstOrDefault(attributte => attributte.Name.Equals(Defaults.VendorDefaultProduct));

                _specificationAttributeService.InsertSpecificationAttributeOption(new SpecificationAttributeOption
                {
                    Name = Defaults.VendorDefaultProductNegation,
                    SpecificationAttributeId = specificationAttribute.Id
                });
            }

        }

        private void AssignProductSpecificationAttribute()
        {
            IList<Product> products = _productRepository.Table.Where(x => !x.Deleted).ToList();
            SpecificationAttributeOption specificationAttributeOption = _specificationAttributteOptionRepository
                .Table.FirstOrDefault(attribute => attribute.Name.Equals(Defaults.VendorDefaultProductNegation));

            foreach (Product product in products)
            {
                if (!_productSpecificationAttributeRepository
                    .Table.Any(attribute => attribute.ProductId == product.Id
                    && attribute.SpecificationAttributeOptionId == specificationAttributeOption.Id))
                {
                    _specificationAttributeService.InsertProductSpecificationAttribute(new ProductSpecificationAttribute
                    {
                        ProductId = product.Id,
                        SpecificationAttributeOptionId = specificationAttributeOption.Id,
                        DisplayOrder = 1,
                        AttributeTypeId = 0,
                        AllowFiltering = false,
                    });
                }
            }
        }

        private void InsertOrdersTaxCategories()
        {
            if (!_taxCategoryRepository.Table.Any(tax => tax.Name.Equals(TaxCategoryType.FoodCategory)))
            {
                _taxCategoryService.InsertTaxCategory(new TaxCategory
                {
                    DisplayOrder = Defaults.DefaultDisplayOrder,
                    Name = TaxCategoryType.FoodCategory
                });

            }

            if (!_taxCategoryRepository.Table.Any(tax => tax.Name.Equals(TaxCategoryType.BeverageCategory)))
            {
                _taxCategoryService.InsertTaxCategory(new TaxCategory
                {
                    DisplayOrder = Defaults.DefaultDisplayOrder,
                    Name = TaxCategoryType.BeverageCategory
                });
            }
        }

        private void InsertOrdersTaxRate()
        {
            Store defaultStore = _storeService.GetAllStores().FirstOrDefault();

            TaxCategory foodCategory = _taxCategoryRepository.Table
                .FirstOrDefault(tax => tax.Name.Equals(TaxCategoryType.FoodCategory));

            TaxCategory beverageCategory = _taxCategoryRepository.Table
                .FirstOrDefault(tax => tax.Name.Equals(TaxCategoryType.BeverageCategory));

            var foundCountry = _countryRepository.Table
                .FirstOrDefault(c => c.Name.Equals(Defaults.Country));

            if (foodCategory != null && !_taxRateRepository.Table.Any(rate => rate.TaxCategoryId == foodCategory.Id))
            {
                _countryStateZipService.InsertTaxRate(new TaxRate
                {
                    CountryId = foundCountry.Id,
                    Percentage = Defaults.FoodTax,
                    StoreId = defaultStore.Id,
                    TaxCategoryId = foodCategory.Id,
                    Zip = string.Empty,
                });
            }

            if (beverageCategory != null && !_taxRateRepository.Table.Any(rate => rate.TaxCategoryId == beverageCategory.Id))
            {
                _countryStateZipService.InsertTaxRate(new TaxRate
                {
                    CountryId = foundCountry.Id,
                    Percentage = Defaults.BeverageTax,
                    StoreId = defaultStore.Id,
                    TaxCategoryId = beverageCategory.Id,
                    Zip = string.Empty,
                });
            }
        }

        #region Custom Attributes

        private void InsertAddressAttributes()
        {
            foreach (DeliveryAppCustomAttribute attribute in Defaults.AddressAttributes)
            {
                if (_addressAttributeService.GetAllAddressAttributes().Any(x => x.Name == attribute.Name))
                    continue;
                InsertDeliveryAppAddressAttribute(attribute);
            }
        }

        private void InsertDeliveryAppAddressAttribute(DeliveryAppCustomAttribute attribute)
        {
            var newAttribute = new AddressAttribute
            {
                Name = attribute.Name,
                IsRequired = false,
                AttributeControlType = attribute.ControlType,
                AttributeControlTypeId = (int)attribute.ControlType,
                DisplayOrder = 0
            };

            _addressAttributeService.InsertAddressAttribute(newAttribute);

            if (attribute.Options != null && attribute.Options.Any())
            {
                for (int index = 0; index < attribute.Options.Count; index++)
                {
                    _addressAttributeService.InsertAddressAttributeValue(new AddressAttributeValue
                    {
                        AddressAttributeId = newAttribute.Id,
                        Name = attribute.Options[index],
                        DisplayOrder = index,
                        IsPreSelected = false
                    });
                }
            }
        }

        private void InsertCheckoutAttributes()
        {
            foreach (DeliveryAppCustomAttribute attribute in Defaults.CheckoutAttributes)
            {
                if (_checkoutAttributeService.GetAllCheckoutAttributes().Any(x => x.Name == attribute.Name))
                    continue;
                InsertDeliveryAppCheckoutAttribute(attribute);
            }
        }

        private void InsertDeliveryAppCheckoutAttribute(DeliveryAppCustomAttribute attribute)
        {
            var newAttribute = new CheckoutAttribute
            {
                Name = attribute.Name,
                IsRequired = false,
                AttributeControlType = attribute.ControlType,
                AttributeControlTypeId = (int)attribute.ControlType,
                DisplayOrder = 0
            };

            _checkoutAttributeService.InsertCheckoutAttribute(newAttribute);

            if (attribute.Options != null && attribute.Options.Any())
            {
                for (int index = 0; index < attribute.Options.Count; index++)
                {
                    _checkoutAttributeService.InsertCheckoutAttributeValue(new CheckoutAttributeValue
                    {
                        CheckoutAttributeId = newAttribute.Id,
                        Name = attribute.Options[index],
                        DisplayOrder = index,
                        IsPreSelected = false
                    });
                }
            }
        }

        private void InsertCustomerAttributes()
        {
            foreach (DeliveryAppCustomAttribute attribute in Defaults.CustomerAttributes)
            {
                if (_customerAttributeService.GetAllCustomerAttributes().Any(x => x.Name == attribute.Name))
                    continue;
                InsertDeliveryAppCustomerAttribute(attribute);
            }
        }

        private void InsertDeliveryAppCustomerAttribute(DeliveryAppCustomAttribute attribute)
        {
            var newAttribute = new CustomerAttribute
            {
                Name = attribute.Name,
                IsRequired = false,
                AttributeControlType = attribute.ControlType,
                AttributeControlTypeId = (int)attribute.ControlType,
                DisplayOrder = 0
            };

            _customerAttributeService.InsertCustomerAttribute(newAttribute);

            if (attribute.Options != null && attribute.Options.Any())
            {
                for (int index = 0; index < attribute.Options.Count; index++)
                {
                    _customerAttributeService.InsertCustomerAttributeValue(new Core.Domain.Customers.CustomerAttributeValue
                    {
                        CustomerAttributeId = newAttribute.Id,
                        Name = attribute.Options[index],
                        DisplayOrder = index,
                        IsPreSelected = false
                    });
                }
            }
        }

        private void InsertProductAttributes()
        {
            foreach (DeliveryAppCustomAttribute attribute in Defaults.ProductAttributes)
            {
                if (_productAttributeService.GetAllProductAttributes().Any(x => x.Name == attribute.Name))
                    continue;

                InsertDeliveryAppProductAttribute(attribute);
            }
        }

        private void InsertDeliveryAppProductAttribute(DeliveryAppCustomAttribute attribute)
        {
            var newAttribute = new ProductAttribute
            {
                Name = attribute.Name
            };

            _productAttributeService.InsertProductAttribute(newAttribute);
        }

        private void InsertVendorAttributes()
        {
            foreach (DeliveryAppCustomAttribute attribute in Defaults.VendorAttributes)
            {
                if (_vendorAttributeService.GetAllVendorAttributes().Any(x => x.Name == attribute.Name))
                    continue;
                InsertDeliveryAppVendorAttribute(attribute);
            }
        }

        private void InsertDeliveryAppVendorAttribute(DeliveryAppCustomAttribute attribute)
        {
            var newAttribute = new VendorAttribute
            {
                Name = attribute.Name,
                IsRequired = false,
                AttributeControlType = attribute.ControlType,
                AttributeControlTypeId = (int)attribute.ControlType,
                DisplayOrder = 0
            };

            _vendorAttributeService.InsertVendorAttribute(newAttribute);

            if (attribute.Options != null && attribute.Options.Any())
            {
                for (int index = 0; index < attribute.Options.Count; index++)
                {
                    _vendorAttributeService.InsertVendorAttributeValue(new VendorAttributeValue
                    {
                        VendorAttributeId = newAttribute.Id,
                        Name = attribute.Options[index],
                        DisplayOrder = index,
                        IsPreSelected = false
                    });
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether to hide or not the plugin in the widget list.
        /// </summary>
        public bool HideInWidgetList => true;

        /// <summary>
        /// Installs this plugin.
        /// </summary>
        public override void Install()
        {
            InsertLanguagesResources();
            AllowCustomerUploadAvatarPicture();
            InsertRecalculateVendorReviewScheduleTask();
            InsertDefaultsShippingMethods();
            InsertDeliveryAppRoles();

            InsertAddressAttributes();
            InsertCheckoutAttributes();
            InsertCustomerAttributes();
            InsertProductAttributes();
            InsertVendorAttributes();

            InsertProductSpecialSpecificationAttributeToAllTheProducts();
            InsertVendorDefaultPictureToAllProduct();

            InsertOrdersTaxCategories();
            InsertOrdersTaxRate();

            base.Install();
        }

        /// <summary>
        /// Uninstalls this plugin.
        /// </summary>
        public override void Uninstall()
        {
            DeleteRecalculateVendorReviewsScheduleTask();
            RemoveAppDeliveryShippingSetting();

            DeleteDeliveryAppRoles();

            _localizationService.DeletePluginLocaleResources(Defaults.ResourcesNamePrefix);

            base.Uninstall();
        }

        /// <summary>
        /// Returns the widget zones for this plugin.
        /// </summary>
        /// <returns>An instance of <see cref="List{T}"/> where T the name of a widget zone used by this plugin.</returns>
        public IList<string> GetWidgetZones() => new List<string>
        {
            AdminWidgetZones.WarehouseListButtons,
            AdminWidgetZones.VendorDetailsBlock,
            AdminWidgetZones.DiscountListButtons,
            AdminWidgetZones.DashboardTop
        };

        /// <summary>
        /// Gets a name of a view component for displaying widget.
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone.</param>
        /// <returns>The name of the view component for this plugin.</returns>
        public string GetWidgetViewComponentName(string widgetZone)
            => Defaults.WidgetZonesViewComponentsDictionary[widgetZone];

        ///<inheritdoc/>
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return;

            var pluginMainMenu = new SiteMapNode
            {
                Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.Main.Title"),
                Visible = true,
                SystemName = "DeliveryApp-Main-Menu",
                IconClass = "fa-motorcycle"
            };

            if (_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
            {
                pluginMainMenu.ChildNodes.Add(new SiteMapNode
                {
                    Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.PendingOrders.Title"),
                    Url = $"{_webHelper.GetStoreLocation()}Admin/DeliveryAppApiAdmin/List",
                    Visible = true,
                    SystemName = "DeliveryApp-PendingOrders-Menu",
                    IconClass = "fa-dot-circle-o"
                });

                pluginMainMenu.ChildNodes.Add(new SiteMapNode
                {
                    Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.TracingOrders.Title"),
                    Url = $"{_webHelper.GetStoreLocation()}Admin/DeliveryAppApiAdmin/TracingList",
                    Visible = true,
                    SystemName = "DeliveryApp-TracingOrders-Menu",
                    IconClass = "fa-dot-circle-o"
                });

                pluginMainMenu.ChildNodes.Add(new SiteMapNode
                {
                    Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.VendorOrdersEarningList.Title"),
                    Url = $"{_webHelper.GetStoreLocation()}Admin/DeliveryAppApiAdmin/VendorOrdersEarningList",
                    Visible = true,
                    SystemName = "DeliveryApp-VendorOrdersEarning-Menu",
                    IconClass = "fa-dot-circle-o"
                });

                pluginMainMenu.ChildNodes.Add(new SiteMapNode
                {
                    Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.Settings.Title"),
                    Url = GetConfigurationPageUrl(),
                    Visible = true,
                    SystemName = "DeliveryApp-Settings-Menu",
                    IconClass = "fa-cog"
                });
            }
            else
            {

                pluginMainMenu.ChildNodes.Add(new SiteMapNode
                {
                    Title = _localizationService.GetResource($"{Defaults.LocaleResourcesPrefix}.AdminMenu.VendorOrdersEarningList.Title"),
                    Url = $"{_webHelper.GetStoreLocation()}Admin/DeliveryAppApiAdmin/VendorOrdersEarningList",
                    Visible = true,
                    SystemName = "DeliveryApp-VendorOrdersEarning-Menu",
                    IconClass = "fa-dot-circle-o"
                });
            }

            rootNode.ChildNodes.Add(pluginMainMenu);
        }

        /// <inheritdoc />
        public override string GetConfigurationPageUrl()
            => $"{_webHelper.GetStoreLocation()}Admin/DeliveryAppIntegration/Configure";

        #endregion
    }
}
