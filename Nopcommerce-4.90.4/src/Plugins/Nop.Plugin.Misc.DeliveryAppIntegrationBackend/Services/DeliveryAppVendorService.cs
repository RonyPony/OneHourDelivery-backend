using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Configuration;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Helpers;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Shipping;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents the services implementation to interact with foundVendor on Delivery App integration.
    /// </summary>
    public class DeliveryAppVendorService : IVendorDeliveryAppService
    {
        #region Fields

        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IPictureService _pictureService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly VendorAttributesHelper _vendorAttributesHelper;
        private readonly IVendorAttributeService _vendorAttributeService;
        private readonly IRepository<AddressGeoCoordinatesMapping> _addressGeoCoordinateRepository;
        private readonly ISettingService _settingService;
        private readonly IDeliveryAppProductService _deliveryAppProductService;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<VendorWarehouseMapping> _vendorWarehouseMappingRepository;
        private readonly IRepository<WarehouseUserCreatorMapping> _warehouseUserCreatorMappingRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IDeliveryAppAddressService _deliveryAppAddressService;
        private readonly IRepository<VendorReviewMapping> _vendorReviewMapping;
        private readonly IRepository<DiscountProductMapping> _discountProductMappingRepository;
        private readonly IRepository<Discount> _discountRepository;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRepository<OrderPendingToClosePayment> _orderPendingToClosePaymentRepository;
        private readonly IOrderService _orderService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IRepository<CustomerFavoriteMapping> _customerFavoriteMappingRepository;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IRepository<SpecificationAttribute> _specificationAttributeRepository;
        private readonly IRepository<SpecificationAttributeOption> _specificationAttributteOptionRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates an instance of <see cref="DeliveryAppVendorService"/>
        /// </summary>
        /// <param name="vendorRepository">An implementation of <see cref="IRepository{Vendor}"/></param>
        /// <param name="categoryRepository">An implementation of <see cref="IRepository{Category}"/></param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{Product}"/></param>
        /// <param name="productCategoryRepository">An implementation of <see cref="IRepository{ProductCategory}"/></param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/></param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/></param>
        /// <param name="vendorAttributesHelper">An implementation of <see cref="VendorAttributesHelper"/></param>
        /// <param name="vendorAttributeService">An implementation of <see cref="IVendorAttributeService"/></param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/></param>
        /// <param name="deliveryAppProductService">An implementation of <see cref="IDeliveryAppProductService"/></param>
        /// <param name="warehouseRepository">An implementation of <see cref="IRepository{Warehouse}"/></param>
        /// <param name="vendorWarehouseMappingRepository">An implementation of <see cref="IRepository{VendorWarehouseMapping}"/></param>
        /// <param name="warehouseUserCreatorMappingRepository">An implementation of <see cref="IRepository{WarehouseUserCreatorMapping}"/></param>
        /// <param name="customerRepository">An implementation of <see cref="IRepository{Customer}"/></param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/></param>
        /// <param name="deliveryAppAddressService">An implementation of <see cref="IDeliveryAppAddressService"/></param>
        /// <param name="vendorReviewMapping">An implementation of <see cref="IRepository{VendorReviewMapping}"/></param>
        /// <param name="discountProductMappingRepository">An implementation of <see cref="IRepository{DiscountProductMapping}"/></param>
        /// <param name="discountRepository">An implementation of <see cref="IRepository{Discount}"/></param>
        /// <param name="addressGeoCoordinateRepository">An implementation of <see cref="IRepository{AddressGeoCoordinatesMapping}"/></param>
        /// <param name="warehouseScheduleService">An implementation of <see cref="IWarehouseScheduleService"/></param>
        /// <param name="dateTimeHelper">An implementation of <see cref="IDateTimeHelper"/></param>
        /// <param name="orderPendingToClosePaymentRepository">An implementation of <see cref="IRepository{OrderPendingToClosePayment}"/></param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/></param>
        /// <param name="productAttributeService">An implementation of <see cref="IProductAttributeService"/></param>
        /// <param name="customerFavoriteMappingRepository">An implementation of <see cref="IRepository{CustomerFavoriteMapping}"/></param>
        /// <param name="warehouseScheduleMappingRepository">An implementation of <see cref="IRepository{WarehouseScheduleMapping}"/></param>
        public DeliveryAppVendorService(
            IRepository<Vendor> vendorRepository,
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IPictureService pictureService,
            IGenericAttributeService genericAttributeService,
            VendorAttributesHelper vendorAttributesHelper,
            IVendorAttributeService vendorAttributeService,
            IRepository<AddressGeoCoordinatesMapping> addressGeoCoordinateRepository,
            ISettingService settingService,
            IDeliveryAppProductService deliveryAppProductService,
            IRepository<Warehouse> warehouseRepository,
            IRepository<VendorWarehouseMapping> vendorWarehouseMappingRepository,
            IRepository<WarehouseUserCreatorMapping> warehouseUserCreatorMappingRepository,
            IRepository<Customer> customerRepository,
            ICustomerService customerService,
            IDeliveryAppAddressService deliveryAppAddressService,
            IRepository<VendorReviewMapping> vendorReviewMapping,
            IRepository<DiscountProductMapping> discountProductMappingRepository,
            IRepository<Discount> discountRepository,
            IDateTimeHelper dateTimeHelper,
            IRepository<OrderPendingToClosePayment> orderPendingToClosePaymentRepository,
            IOrderService orderService,
            IProductAttributeService productAttributeService,
            IRepository<CustomerFavoriteMapping> customerFavoriteMappingRepository,
            ISpecificationAttributeService specificationAttributeService,
            IRepository<SpecificationAttribute> specificationAttributeRepository,
            IRepository<SpecificationAttributeOption> specificationAttributteOptionRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            IRepository<ProductPicture> productPictureRepository)
        {
            _vendorRepository = vendorRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _pictureService = pictureService;
            _genericAttributeService = genericAttributeService;
            _vendorAttributesHelper = vendorAttributesHelper;
            _vendorAttributeService = vendorAttributeService;
            _addressGeoCoordinateRepository = addressGeoCoordinateRepository;
            _settingService = settingService;
            _deliveryAppProductService = deliveryAppProductService;
            _warehouseRepository = warehouseRepository;
            _vendorWarehouseMappingRepository = vendorWarehouseMappingRepository;
            _warehouseUserCreatorMappingRepository = warehouseUserCreatorMappingRepository;
            _customerRepository = customerRepository;
            _customerService = customerService;
            _deliveryAppAddressService = deliveryAppAddressService;
            _vendorReviewMapping = vendorReviewMapping;
            _discountProductMappingRepository = discountProductMappingRepository;
            _discountRepository = discountRepository;
            _dateTimeHelper = dateTimeHelper;
            _orderPendingToClosePaymentRepository = orderPendingToClosePaymentRepository;
            _orderService = orderService;
            _productAttributeService = productAttributeService;
            _customerFavoriteMappingRepository = customerFavoriteMappingRepository;
            _specificationAttributeService = specificationAttributeService;
            _specificationAttributeRepository = specificationAttributeRepository;
            _specificationAttributteOptionRepository = specificationAttributteOptionRepository;
            _productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            _productPictureRepository = productPictureRepository;
        }

        #endregion

        #region Utilities


        private IList<WarehouseScheduleMappingSnapshot> GetWarehouseScheduleByReflection(int warehouseId)
        {
            var serviceType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException exception)
                    {
                        return exception.Types.Where(type => type != null);
                    }
                })
                .FirstOrDefault(type => type.FullName == "Nop.Plugin.Widgets.WarehouseSchedule.Services.IWarehouseScheduleService");

            if (serviceType == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var service = Nop.Core.Infrastructure.EngineContext.Current.Resolve(serviceType);

            if (service == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var method = serviceType.GetMethod("GetWarehouseScheduleAsync");

            if (method == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var task = method.Invoke(service, new object[] { warehouseId }) as Task;

            if (task == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            task.GetAwaiter().GetResult();

            var resultProperty = task.GetType().GetProperty("Result");

            if (resultProperty == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var result = resultProperty.GetValue(task) as System.Collections.IEnumerable;

            if (result == null)
                return new List<WarehouseScheduleMappingSnapshot>();

            var mappings = new List<WarehouseScheduleMappingSnapshot>();

            foreach (var item in result)
            {
                var itemType = item.GetType();

                mappings.Add(new WarehouseScheduleMappingSnapshot
                {
                    Id = GetPropertyValue<int>(item, itemType, "Id"),
                    WarehouseId = GetPropertyValue<int>(item, itemType, "WarehouseId"),
                    DayId = GetPropertyValue<int>(item, itemType, "DayId"),
                    BeginTime = GetPropertyValue<TimeSpan>(item, itemType, "BeginTime"),
                    EndTime = GetPropertyValue<TimeSpan>(item, itemType, "EndTime"),
                    IsActive = GetPropertyValue<bool>(item, itemType, "IsActive")
                });
            }

            return mappings;
        }

        private T GetPropertyValue<T>(object instance, Type instanceType, string propertyName)
        {
            var property = instanceType.GetProperty(propertyName);

            if (property == null)
                return default;

            var value = property.GetValue(instance);

            if (value == null)
                return default;

            return (T)value;
        }

        private sealed class WarehouseScheduleMappingSnapshot
        {
            public int Id { get; set; }

            public int WarehouseId { get; set; }

            public int DayId { get; set; }

            public TimeSpan BeginTime { get; set; }

            public TimeSpan EndTime { get; set; }

            public bool IsActive { get; set; }
        }

        private string RemoveHtmlParagraphTags(string info)
        {
            return info.Replace("<p>", "").Replace("</p>", "");
        }

        private IEnumerable<DeliveryAppCategoryWithProducts> BuildCategoriesWithProducts(int vendorId)
        {

            List<Category> categories = (from p in _productRepository.Table
                                         join pc in _productCategoryRepository.Table on p.Id equals pc.ProductId
                                         join cat in _categoryRepository.Table on pc.CategoryId equals cat.Id
                                         where p.VendorId.Equals(vendorId) && !p.Deleted
                                         select cat).ToList();

            if (categories.Count == 0)
            {
                return new List<DeliveryAppCategoryWithProducts>();
            }

            List<DeliveryAppCategoryWithProducts> categoriesWithProductsResult = new List<DeliveryAppCategoryWithProducts>();

            foreach (Category item in categories.GroupBy(p => p.Id).Select(grp => grp.FirstOrDefault()))
            {
                ICollection<DeliveryAppProduct> products = GetProductsByCategory(item.Id, vendorId);

                categoriesWithProductsResult.Add(new DeliveryAppCategoryWithProducts
                {
                    Name = item.Name,
                    Products = products.Where(x => !ExistsInCategory(x.Id, categoriesWithProductsResult)).ToList()

                });
            }

            return categoriesWithProductsResult;
        }

        private bool ExistsInCategory(int productId, List<DeliveryAppCategoryWithProducts> categoryWithProducts)
            => categoryWithProducts.Any(x => x.Products.Any(x => x.Id.Equals(productId)));

        private ICollection<DeliveryAppProduct> GetProductsByCategory(int categoryId, int vendorId)
        {
            ICollection<DeliveryAppProduct> result = (from p in _productRepository.Table
                                                      join pc in _productCategoryRepository.Table on p.Id equals pc.ProductId
                                                      where pc.CategoryId.Equals(categoryId) && !p.Deleted && p.VendorId.Equals(vendorId)
                                                      select new DeliveryAppProduct
                                                      {
                                                          Id = p.Id,
                                                          Description = p.ShortDescription,
                                                          Name = p.Name,
                                                          Price = p.Price,
                                                          HasPromotion = HasACurrentDiscount(p.Id),
                                                          DiscountSpecification = GetCurrentDiscount(p.Id),
                                                          IsAvailable = IsProductAvalible(p),
                                                          ProductAttributes = GetProductAttributes(p.Id)
                                                      })
                                                      .ToList();

            foreach (var product in result)
            {
                Picture productPicture = _pictureService.GetPicturesByProductId(product.Id).FirstOrDefault();

                product.ImageUrl = productPicture != null ? _pictureService.GetPictureUrl(productPicture.Id) : "";

                product.RelatedProducts = _deliveryAppProductService.GetProductRelatedProducts(product.Id);
            }

            return result;
        }

        private bool IsProductAvalible(Product product)
        {

            if (product.Published && product.ManageInventoryMethodId == (int)ManageInventoryMethod.DontManageStock)
            {
                return true;
            }

            if (product.Published && product.ManageInventoryMethodId == (int)ManageInventoryMethod.ManageStock
                && product.StockQuantity > 0)
            {
                return true;
            }

            return false;
        }


        private IList<ProductAttributeModel> GetProductAttributes(int productId)
        {
            var productAttributeList = new List<ProductAttributeModel>();
            var productAttributeMapping = _productAttributeService
                .GetProductAttributeMappingsByProductId(productId);

            foreach (ProductAttributeMapping mapping in productAttributeMapping)
            {
                switch (mapping.AttributeControlType)
                {
                    case AttributeControlType.TextBox:
                        productAttributeList.Add(GetProductAttribute(mapping));
                        break;
                    case AttributeControlType.Checkboxes:
                    case AttributeControlType.RadioList:
                        ProductAttributeModel attribute = GetProductAttribute(mapping);
                        attribute.ProductAttributeValueModels = GetProductAttributeValues(mapping.Id);
                        productAttributeList.Add(attribute);
                        break;
                }
            }

            return productAttributeList.ToList();
        }

        private ProductAttributeModel GetProductAttribute(ProductAttributeMapping attributeMapping)
        {
            var productAttribute = _productAttributeService.GetProductAttributeById(attributeMapping.ProductAttributeId);
            return new ProductAttributeModel
            {
                ProductAttributeId = attributeMapping.Id,
                ControlTypeId = attributeMapping.AttributeControlTypeId,
                ProductAttributeName = productAttribute.Name
            };
        }

        private IList<ProductAttributeValueModel> GetProductAttributeValues(int productAttributeMappingId)
        {
            var productAttributeValues = _productAttributeService.GetProductAttributeValues(productAttributeMappingId);
            var productAttributeValueList = new List<ProductAttributeValueModel>();

            foreach (ProductAttributeValue value in productAttributeValues)
            {
                productAttributeValueList.Add(new ProductAttributeValueModel
                {
                    ProductAttributeName = value.Name,
                    ProductAttributeValueId = value.Id,
                    PriceAdjustment = value.PriceAdjustment,
                    PriceAdjustmentUsePercentage = value.PriceAdjustmentUsePercentage,
                    AdditionalCost = value.Cost,
                    IsPreselected = value.IsPreSelected,
                });
            }
            return productAttributeValueList;
        }

        private IList<StoreResponseModel> BuildStoresWithClosestProximity(IList<AddressGeoCoordinatesMapping> addresses, decimal latitud, decimal longitud)
        {
            return addresses.Where(coordinate => _deliveryAppAddressService.GetDistanceOnMeters(coordinate, latitud, longitud) <= 30000)
                            .Select(coordinate =>
                                {
                                    Warehouse warehouse = _warehouseRepository.Table.FirstOrDefault(x => x.AddressId == coordinate.AddressId);
                                    VendorWarehouseMapping warehouseVendorMapping = _vendorWarehouseMappingRepository.Table.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                                    Vendor vendor =
                                        _vendorRepository.Table.FirstOrDefault(data => data.Id == warehouseVendorMapping.VendorId && data.Active && !data.Deleted);
                                    if (vendor != null)
                                    {
                                        IEnumerable<DeliveryAppCategoryWithProducts> categoriesWithProductsResult = BuildCategoriesWithProducts(vendor.Id);
                                        Customer customerAssociatedToVendor = _customerRepository.Table.FirstOrDefault(x => x.VendorId == vendor.Id);
                                        string ratingValue = GetRatingAttribute(vendor.Id);
                                        var estimatedTime = GetEstimatedTimeAttribute(vendor.Id);
                                        return new StoreResponseModel
                                        {
                                            VendorId = vendor.Id,
                                            Name = vendor.Name,
                                            ProximityInMeters = _deliveryAppAddressService.GetDistanceOnMeters(coordinate, latitud, longitud),
                                            LogoUrl = _pictureService.GetPictureUrl(vendor.PictureId),
                                            VendorTags = categoriesWithProductsResult.Select(category => category.Name),
                                            EstimatedPreparationTime = estimatedTime,
                                            Rating = ratingValue != null ? double.Parse(ratingValue) : 0,
                                            AdultsLimitated = GetVendorAdultsLimitatedAttribute(vendor.Id),
                                            HasPromotions = HasProductsWithPromotions(vendorId: vendor.Id),
                                            IsOpen = IsVendorWarehouseOpen(vendor),
                                            VendorCategory = GetVendorCategoryAttribute(vendor.Id),
                                            StoreSchedule = GetVendorWarehouseTodaySchedule(vendor),
                                            CreatedDateOnUtc = customerAssociatedToVendor.CreatedOnUtc
                                        };
                                    }
                                    else
                                    {
                                        return new StoreResponseModel
                                        {

                                        };
                                    }
                                }).ToList();
        }
        private IList<StoreResponseModel> BuildVendorsWithClosestProximity(IList<AddressGeoCoordinatesMapping> addresses, decimal latitud, decimal longitud)
        {
            return addresses.Where(coordinate => _deliveryAppAddressService.GetDistanceOnMeters(coordinate, latitud, longitud) <= 30000)
                            .Select(coordinate =>
                            {
                                Warehouse warehouse = _warehouseRepository.Table.FirstOrDefault(x => x.AddressId == coordinate.AddressId);
                                VendorWarehouseMapping warehouseVendorMapping = _vendorWarehouseMappingRepository.Table.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                                Vendor vendor =
                                    _vendorRepository.Table.FirstOrDefault(data => data.Id == warehouseVendorMapping.VendorId && data.Active && !data.Deleted);
                                if (vendor != null)
                                {
                                    IEnumerable<DeliveryAppCategoryWithProducts> categoriesWithProductsResult = BuildCategoriesWithProducts(vendor.Id);
                                    Customer customerAssociatedToVendor = _customerRepository.Table.FirstOrDefault(x => x.VendorId == vendor.Id);
                                    string ratingValue = GetRatingAttribute(vendor.Id);
                                    var estimatedTime = GetEstimatedTimeAttribute(vendor.Id);
                                    return new StoreResponseModel
                                    {
                                        VendorId = vendor.Id,
                                        Name = vendor.Name,
                                        ProximityInMeters = _deliveryAppAddressService.GetDistanceOnMeters(coordinate, latitud, longitud),
                                        LogoUrl = _pictureService.GetPictureUrl(vendor.PictureId),
                                        VendorTags = categoriesWithProductsResult.Select(category => category.Name),
                                        EstimatedPreparationTime = estimatedTime,
                                        Rating = ratingValue != null ? double.Parse(ratingValue) : 0,
                                        AdultsLimitated = GetVendorAdultsLimitatedAttribute(vendor.Id),
                                        HasPromotions = HasProductsWithPromotions(vendorId: vendor.Id),
                                        IsOpen = IsVendorWarehouseOpen(vendor),
                                        VendorCategory = GetVendorCategoryAttribute(vendor.Id),
                                        StoreSchedule = GetVendorWarehouseTodaySchedule(vendor),
                                        CreatedDateOnUtc = customerAssociatedToVendor.CreatedOnUtc
                                    };
                                }
                                else
                                {
                                    return new StoreResponseModel
                                    {

                                    };
                                }
                            }).ToList();
        }
        private string GetVendorAttributeValueByVendorIdAndAttributeName(int vendorId, string vendorAttributeName)
        {
            VendorAttribute vendorAttribute = _vendorAttributeService.GetAllVendorAttributes()
                .FirstOrDefault(x => x.Name.Equals(vendorAttributeName));
            GenericAttribute vendorGenericAttributes = _genericAttributeService.GetAttributesForEntity(vendorId, "Vendor")
                .FirstOrDefault(x => x.Key == Defaults.GenericAttributeKeyForVendorAttributes);
            if (vendorGenericAttributes == null) return "";
            return _vendorAttributesHelper.GetVendorAttributeValue(vendorGenericAttributes.Value, vendorAttribute.Id);
        }
        private bool HasProductsWithPromotions(int vendorId)
        {
            IEnumerable<Product> productsWithActiveDiscounts = (from dproduct in _discountProductMappingRepository.Table
                                                                join product in _productRepository.Table on dproduct.EntityId equals product.Id
                                                                join disc in _discountRepository.Table on dproduct.DiscountId equals disc.Id
                                                                where disc.DiscountTypeId == (int)DiscountType.AssignedToSkus &&
                                                                 (!disc.StartDateUtc.HasValue || disc.StartDateUtc <= DateTime.UtcNow &&
                                                                     (!disc.EndDateUtc.HasValue || disc.EndDateUtc >= DateTime.UtcNow)) &&
                                                                     !product.Deleted && product.VendorId == vendorId
                                                                select product).ToList();
            return productsWithActiveDiscounts.Any(x => x.VendorId == vendorId);
        }

        private bool HasACurrentDiscount(int productId)
        {
            IEnumerable<Product> productsWithActiveDiscounts = (from dproduct in _discountProductMappingRepository.Table
                                                                join product in _productRepository.Table on dproduct.EntityId equals product.Id
                                                                join disc in _discountRepository.Table on dproduct.DiscountId equals disc.Id
                                                                where disc.DiscountTypeId == (int)DiscountType.AssignedToSkus && dproduct.EntityId == productId &&
                                                                 (!disc.StartDateUtc.HasValue || disc.StartDateUtc <= DateTime.UtcNow &&
                                                                     (!disc.EndDateUtc.HasValue || disc.EndDateUtc >= DateTime.UtcNow)) &&
                                                                     !product.Deleted
                                                                select product).ToList();

            return productsWithActiveDiscounts.Any();
        }

        private IEnumerable<string> GetCurrentDiscount(int productId)
        {
            IEnumerable<Discount> productsWithActiveDiscounts = (from dproduct in _discountProductMappingRepository.Table
                                                                 join product in _productRepository.Table on dproduct.EntityId equals product.Id
                                                                 join disc in _discountRepository.Table on dproduct.DiscountId equals disc.Id
                                                                 where disc.DiscountTypeId == (int)DiscountType.AssignedToSkus && dproduct.EntityId == productId &&
                                                                  (!disc.StartDateUtc.HasValue || disc.StartDateUtc <= DateTime.UtcNow &&
                                                                      (!disc.EndDateUtc.HasValue || disc.EndDateUtc >= DateTime.UtcNow)) &&
                                                                     !product.Deleted
                                                                 select disc).ToList();
            var result = productsWithActiveDiscounts.Select(x => x.DiscountAmount == 0.00m ? $"{x.DiscountPercentage:#.##}%" : $"${x.DiscountAmount:#.##}");

            return productsWithActiveDiscounts.Any() ? result : new List<string>();
        }


        private bool IsVendorWarehouseOpen(Vendor vendor)
        {
            const int SECOND_TIME_BIGGER_THAN_FIRST_TIME_VALUE = -1;
            TimeSpan baseUtcOffset = _dateTimeHelper.DefaultStoreTimeZone.BaseUtcOffset;
            WarehouseScheduleMappingSnapshot todaySchedule = GetTodayScheduleByVendorId(vendor.Id, baseUtcOffset);
            if (todaySchedule == null) return false;

            TimeSpan timeOfDayUtcWithSubstractedBaseUtcOffset = DateTime.UtcNow.Subtract(baseUtcOffset.Negate()).TimeOfDay;

            int beginTimeComparation = TimeSpan.Compare(todaySchedule.BeginTime, timeOfDayUtcWithSubstractedBaseUtcOffset);
            int endTimeComparation = TimeSpan.Compare(timeOfDayUtcWithSubstractedBaseUtcOffset, todaySchedule.EndTime);

            return beginTimeComparation == SECOND_TIME_BIGGER_THAN_FIRST_TIME_VALUE
                && endTimeComparation == SECOND_TIME_BIGGER_THAN_FIRST_TIME_VALUE;
        }

        private string GetVendorWarehouseTodaySchedule(Vendor vendor)
        {
            TimeSpan baseUtcOffset = _dateTimeHelper.DefaultStoreTimeZone.BaseUtcOffset;
            WarehouseScheduleMappingSnapshot todaySchedule = GetTodayScheduleByVendorId(vendor.Id, baseUtcOffset);
            if (todaySchedule == null) return "";

            string beginTime = DateTime.Parse(todaySchedule.BeginTime.ToString()).ToString("hh:mm tt");
            string endTime = DateTime.Parse(todaySchedule.EndTime.ToString()).ToString("hh:mm tt");

            return $"{beginTime} - {endTime}";
        }

        private WarehouseScheduleMappingSnapshot GetTodayScheduleByVendorId(int vendorId, TimeSpan baseUtcOffset)
        {
            VendorWarehouseMapping vendorWarehouseMapping = _vendorWarehouseMappingRepository.Table.FirstOrDefault(mapping => mapping.VendorId == vendorId);

            if (vendorWarehouseMapping is null)
                return null;

            IList<WarehouseScheduleMappingSnapshot> warehouseSchedule = GetWarehouseScheduleByReflection(vendorWarehouseMapping.WarehouseId);

            if (warehouseSchedule.Count == 0)
                return null;

            return warehouseSchedule.FirstOrDefault(mapping =>
                mapping.DayId == (int)DateTime.UtcNow.AddHours(baseUtcOffset.TotalHours).DayOfWeek &&
                mapping.IsActive);
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public DeliveryAppVendor GetVendorProductsGroupByCategories(int vendorId, int customerId)
        {
            IEnumerable<DeliveryAppCategoryWithProducts> categoriesWithProductsResult = BuildCategoriesWithProducts(vendorId);
            Vendor currentVendor = _vendorRepository.GetById(vendorId);
            if (currentVendor == null) throw new ArgumentException("VendorNotFound");
            Setting shippingRate = _settingService.GetSetting(Defaults.AdditionalShippingChargeKey);

            IList<Product> products = _productRepository.Table
                .Where(product => product.VendorId == vendorId && !product.Deleted)
                .ToList();

            string productImage = string.Empty;

            foreach (Product product in products)
            {
                var ProductSpecificationAttribute = _productSpecificationAttributeRepository
                    .Table.FirstOrDefault(attribute => attribute.ProductId == product.Id);
                if (ProductSpecificationAttribute != null)
                {
                    var specificationOption = _specificationAttributeService
                    .GetSpecificationAttributeOptionById(ProductSpecificationAttribute.SpecificationAttributeOptionId);

                    if (specificationOption.Name.Equals(Defaults.VendorDefaultProductAffirmation))
                    {
                        var productPicture = _productPictureRepository.Table
                            .FirstOrDefault(picture => picture.ProductId == product.Id);

                        productImage = _pictureService.GetPictureUrl(productPicture.PictureId);
                    }
                }


            }

            return new DeliveryAppVendor
            {
                VendorId = vendorId,
                Name = currentVendor.Name,
                Image = string.IsNullOrWhiteSpace(productImage) ? _pictureService.GetPictureUrl(currentVendor.PictureId) : productImage,
                Categories = categoriesWithProductsResult,
                Info = currentVendor.Description is null ? $"{currentVendor.Name} no tiene descripción."
                : RemoveHtmlParagraphTags(currentVendor.Description),
                VendorTags = new List<string>(),
                Rating = GetRatingAttribute(vendorId) != null ? double.Parse(GetRatingAttribute(vendorId)) : 0,
                EstimatedWaitTime = GetEstimatedTimeAttribute(vendorId) != null ? GetEstimatedTimeAttribute(vendorId) : "25-30m",
                IsFreeShipping = _settingService.LoadSetting<ShippingSettings>().FreeShippingOverXEnabled,
                AdditionalShippingCharge = shippingRate == null ? 0.0m : decimal.Parse(shippingRate.Value),
                AdultsLimitated = GetVendorAdultsLimitatedAttribute(vendorId),
                IsOpen = IsVendorWarehouseOpen(currentVendor),
                IsFavorite = _customerFavoriteMappingRepository.Table
                .Any(customerFavorite => customerFavorite.CustomerId == customerId
                && customerFavorite.VendorId == vendorId),
                CategoryName = GetVendorCategoryAttribute(vendorId)
            };
        }

        ///<inheritdoc/>
        public IList<StoreResponseModel> GetClosestStores(decimal latitud, decimal longitud)
        {
            //IList<AddressGeoCoordinatesMapping> warehousesAddresses = [];

            //warehousesAddresses =
            //   (from vw in _vendorWarehouseMappingRepository.Table
            //    join w in _warehouseRepository.Table on vw.WarehouseId equals w.Id
            //    join agc in _addressGeoCoordinateRepository.Table on w.AddressId equals agc.AddressId
            //    join vend in _vendorRepository.Table on vw.VendorId equals vend.Id
            //    join customer in _customerRepository.Table on vend.Id equals customer.VendorId
            //    where customer.VendorId != 0 && !vend.Deleted
            //    select agc).ToList();

            IList<AddressGeoCoordinatesMapping> warehousesAddresses = new List<AddressGeoCoordinatesMapping>();

            var vendorWarehouseMappings = _vendorWarehouseMappingRepository.Table.ToList();

            var warehouses = _warehouseRepository.Table.ToList();

            var addressGeoCoordinates = _addressGeoCoordinateRepository.Table.ToList();

            var vendors = _vendorRepository.Table.ToList();

            var customers = _customerRepository.Table.ToList();

            var warehouseJoin = vendorWarehouseMappings
                .Join(
                    warehouses,
                    vendorWarehouseMapping => vendorWarehouseMapping.WarehouseId,
                    warehouse => warehouse.Id,
                    (vendorWarehouseMapping, warehouse) => new
                    {
                        VendorWarehouseMapping = vendorWarehouseMapping,
                        Warehouse = warehouse
                    })
                .ToList();

            var addressJoin = warehouseJoin
                .Join(
                    addressGeoCoordinates,
                    warehouseData => warehouseData.Warehouse.AddressId,
                    addressGeoCoordinate => addressGeoCoordinate.AddressId,
                    (warehouseData, addressGeoCoordinate) => new
                    {
                        warehouseData.VendorWarehouseMapping,
                        warehouseData.Warehouse,
                        AddressGeoCoordinate = addressGeoCoordinate
                    })
                .ToList();

            var vendorJoin = addressJoin
                .Join(
                    vendors,
                    addressData => addressData.VendorWarehouseMapping.VendorId,
                    vendor => vendor.Id,
                    (addressData, vendor) => new
                    {
                        addressData.VendorWarehouseMapping,
                        addressData.Warehouse,
                        addressData.AddressGeoCoordinate,
                        Vendor = vendor
                    })
                .ToList();

            var customerJoin = vendorJoin
                .Join(
                    customers,
                    vendorData => vendorData.Vendor.Id,
                    customer => customer.VendorId,
                    (vendorData, customer) => new
                    {
                        vendorData.VendorWarehouseMapping,
                        vendorData.Warehouse,
                        vendorData.AddressGeoCoordinate,
                        vendorData.Vendor,
                        Customer = customer
                    })
                .ToList();

            var filteredCustomerJoin = customerJoin
                .Where(data => data.Customer.VendorId != 0 && !data.Vendor.Deleted)
                .ToList();

            warehousesAddresses = filteredCustomerJoin
                .Select(data => data.AddressGeoCoordinate)
                .ToList();

            List<StoreResponseModel> closestStores = warehousesAddresses == null || warehousesAddresses.Count == 0 ?
                                                      GetAllStores().ToList() :
                                                      BuildStoresWithClosestProximity(warehousesAddresses, latitud, longitud).ToList();

            List<StoreResponseModel> openStores = closestStores.Where(x => x.IsOpen).ToList();
            List<StoreResponseModel> closedStores = closestStores.Where(x => !x.IsOpen).ToList();
            closestStores.Clear();
            closestStores.AddRange(openStores);
            closestStores.AddRange(closedStores);

            return closestStores;
        }

        ///<inheritdoc/>
        public IList<StoreResponseModel> GetAllStores()
        {
            return _vendorRepository.Table.AsEnumerable()
                .Where(vendor => !vendor.Deleted)
                .Select(vendor =>
                {
                    IEnumerable<DeliveryAppCategoryWithProducts> categoriesWithProductsResult = BuildCategoriesWithProducts(vendor.Id);

                    string ratingValue = GetRatingAttribute(vendor.Id);
                    var estimatedTime = GetEstimatedTimeAttribute(vendor.Id);

                    return new StoreResponseModel
                    {
                        VendorId = vendor.Id,
                        Name = vendor.Name,
                        ProximityInMeters = 200.1d,
                        LogoUrl = _pictureService.GetPictureUrl(vendor.PictureId),
                        VendorTags = categoriesWithProductsResult.Select(category => category.Name),
                        EstimatedPreparationTime = estimatedTime,
                        Rating = ratingValue == null || ratingValue.Equals("") ?
                          0 : double.Parse(ratingValue),
                        AdultsLimitated = GetVendorAdultsLimitatedAttribute(vendor.Id),
                        HasPromotions = HasProductsWithPromotions(vendorId: vendor.Id),
                        IsOpen = IsVendorWarehouseOpen(vendor),
                        StoreSchedule = GetVendorWarehouseTodaySchedule(vendor),
                        VendorCategory = GetVendorCategoryAttribute(vendor.Id)
                    };
                }).OrderByDescending(x => x.IsOpen)
                  .ThenBy(x => x.ProximityInMeters)
                .ToList();
        }

        ///<inheritdoc/>
        public IList<StoreResponseModel> GetAllStoresByCategory(string vendorCategory, decimal latitude, decimal longitude)
        {
            IEnumerable<int> activeVendorsIds = GetActiveVendorsIdsByCategory(vendorCategory);
            IList<int> vendorWarehousesIds = _vendorWarehouseMappingRepository.Table
                .Where(vendorWarehouse => activeVendorsIds.Contains(vendorWarehouse.VendorId))
                .Select(vendorWarehouse => vendorWarehouse.WarehouseId)
                .ToList();
            IList<int> warehouseAddressIds = _warehouseRepository.Table
                .Where(warehouse => vendorWarehousesIds.Contains(warehouse.Id))
                .Select(warehouse => warehouse.AddressId)
                .ToList();
            IList<AddressGeoCoordinatesMapping> warehousesAddresses = _addressGeoCoordinateRepository.Table
                .Where(mapping => warehouseAddressIds.Contains(mapping.AddressId)).ToList();

            IEnumerable<StoreResponseModel> closestStores = warehousesAddresses == null || warehousesAddresses.Count == 0 ?
                                                      new List<StoreResponseModel>() :
                                                      BuildStoresWithClosestProximity(warehousesAddresses, latitude, longitude);

            return closestStores
                              .OrderByDescending(x => x.IsOpen)
                              .ThenBy(x => x.ProximityInMeters)
                              .ToList();
        }

        private IEnumerable<int> GetActiveVendorsIdsByCategory(string vendorCategory)
        {
            IEnumerable<int> customerVendorsIds = _customerRepository.Table
                .Where(customer => customer.Active && !customer.Deleted && customer.VendorId != 0)
                .Select(customer => customer.VendorId)
                .ToList();

            IEnumerable<int> vendorIds = _vendorRepository.Table
                .Where(vendor => vendor.Active &&
                                !vendor.Deleted && customerVendorsIds.Contains(vendor.Id))
                .Select(vendor => vendor.Id)
                .ToList();


            return vendorIds.Where(vendorId => GetVendorCategoryAttribute(vendorId).Equals(vendorCategory))
                            .ToList();
        }

        ///<inheritdoc/>
        public WarehouseListModel PrepareVendorWarehouseListModel(int vendorId, WarehouseSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get vemdor warehouses
            var warehouses = GetAllWarehousesFromVendor(vendorId, searchModel.SearchName).ToPagedList(searchModel);

            //prepare list model
            var model = new WarehouseListModel().PrepareToGrid(searchModel, warehouses, () =>
            {
                //fill in model values from the entity
                return warehouses.Select(warehouse => warehouse.ToModel<WarehouseModel>());
            });

            return model;
        }

        ///<inheritdoc/>
        public IList<Warehouse> GetAllWarehousesFromVendor(int vendorId, string name = null)
        {
            Customer customer = _customerRepository.Table.Where(c => c.VendorId == vendorId).FirstOrDefault();
            if (customer is null) throw new ArgumentException($"VendorNotBoundToCustomer");

            var warehouses = new List<Warehouse>();
            IList<int> warehousesIds = _warehouseUserCreatorMappingRepository.Table
                .Where(vw => vw.CustomerId == customer.Id)
                .Select(vw => vw.WarehouseId)
                .ToList();

            foreach (int warehouseId in warehousesIds)
            {
                Warehouse warehouse = _warehouseRepository.Table.Where(warehouse => warehouse.Id == warehouseId).FirstOrDefault();
                if (warehouse != null) warehouses.Add(warehouse);
            }

            if (!string.IsNullOrEmpty(name))
                warehouses = warehouses.Where(wh => wh.Name.Contains(name)).ToList();

            return warehouses;
        }

        ///<inheritdoc/>
        public bool IsDeliveryAppVendor(Customer customer)
        {
            IList<CustomerRole> customerRoles = _customerService.GetCustomerRoles(customer, true);
            return customerRoles.Any(role => role.SystemName == "Vendors") && customer.VendorId != 0;
        }

        ///<inheritdoc/>
        public int? GetVendorWarehouseIdByVendorId(int vendorId)
            => _vendorWarehouseMappingRepository.Table.Where(vw => vw.VendorId == vendorId).FirstOrDefault()?.WarehouseId;

        ///<inheritdoc/>
        public void InsertOrUpdateVendorWarehouseMapping(VendorWarehouseMapping vendorWarehouse)
        {
            var existingVendorWarehouse = _vendorWarehouseMappingRepository.Table.Where(vw => vw.VendorId == vendorWarehouse.VendorId).FirstOrDefault();

            if (existingVendorWarehouse is null)
            {
                _vendorWarehouseMappingRepository.Insert(vendorWarehouse);
            }
            else
            {
                existingVendorWarehouse.WarehouseId = vendorWarehouse.WarehouseId;
                _vendorWarehouseMappingRepository.Update(existingVendorWarehouse);
            }
        }

        ///<inheritdoc/>
        public bool GetVendorAdultsLimitatedAttribute(int vendorId)
        {
            string attributeValueOptionId = GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorAdultsLimitedAttribute.Name);
            bool attributeValueOptionIdParseResult = int.TryParse(attributeValueOptionId, out int optionId);
            if (!attributeValueOptionIdParseResult) return true;
            var attributeValue = _vendorAttributeService.GetVendorAttributeValueById(optionId);
            if (attributeValue is null) return true;
            return attributeValue.Name.ToLower() == "yes";
        }

        ///<inheritdoc/>
        public string GetRatingAttribute(int vendorId)
        {
            return GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorRatingAttribute.Name);
        }

        ///<inheritdoc/>
        public string GetEstimatedTimeAttribute(int vendorId)
        {
            return GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorEstimatedWaitTimeAttribute.Name);
        }

        ///<inheritdoc/>
        public string GetVendorCategoryAttribute(int vendorId)
        {
            string optionId = GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorCategoryAttribute.Name);
            if (string.IsNullOrEmpty(optionId)) return "";
            var attributeValue = _vendorAttributeService.GetVendorAttributeValueById(int.Parse(optionId));
            if (attributeValue == null) return "";
            return attributeValue.Name;
        }

        public int GetVendorPopularityAttribute(int vendorId)
        {
            var attribute = GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorPopularityAttribute.Name);
            return string.IsNullOrWhiteSpace(attribute) ? 0 : int.Parse(attribute);
        }

        ///<inheritdoc/>
        public ProfitVendorModel GetVendorProfit(int vendorId)
        {
            IList<OrderProfitModel> orderProfitModels = new List<OrderProfitModel>();
            Vendor vendor = _vendorRepository.GetById(vendorId);

            if (vendor is null) throw new ArgumentException("VendorNotFound");

            decimal vendorRatingMapping = _vendorReviewMapping.Table
                .GroupBy(group => group.VendorId == vendor.Id)
                .Select(select => select.Average(a => a.Rating)).Sum();

            IList<OrderPendingToClosePayment> ordersPayment = _orderPendingToClosePaymentRepository.Table
                .Where(payment => payment.VendorId == vendorId)
                .GroupBy(x => new { x.OrderId }).Select(x => x.FirstOrDefault()).ToList();

            foreach (OrderPendingToClosePayment payment in ordersPayment)
            {
                orderProfitModels.Add(new OrderProfitModel
                {
                    OrderId = payment.OrderId,
                    VendorProfit = payment.OrderTotalVendorProfitAmount,
                    CreatedOn = payment.CreatedOnUtc
                });
            }

            return new ProfitVendorModel
            {
                Orders = orderProfitModels.OrderByDescending(x => x.CreatedOn).ToList(),
                VendorName = vendor.Name,
                VendorPictureUrl = _pictureService.GetPictureUrl(vendor.PictureId, showDefaultPicture: true),
                VendorRating = vendorRatingMapping,
                EarnedProfitTotal = ordersPayment.Sum(payment => payment.OrderTotalVendorProfitAmount)
            };
        }

        ///<inheritdoc/>
        public List<StoreResponseModel> GetAllNewStore(decimal latitud, decimal longitud)
        {
            var closestStores = GetClosestStores(latitud, longitud);
            const int maxAgeInDays = 30;
            const int firstFifteenStores = 15;

            DateTime oldestDate = DateTime.Now.Subtract(new TimeSpan(maxAgeInDays, 0, 0, 0, 0));

            return closestStores.Where(x => x.CreatedDateOnUtc >= oldestDate.ToUniversalTime()).Take(firstFifteenStores).ToList();

        }

        ///<inheritdoc/>
        public List<StoreResponseModel> GetAllPopularStore(decimal latitud, decimal longitud)
        {
            var closestStores = GetClosestStores(latitud, longitud).ToList();
            const int firstFifteenStores = 15;

            List<StoreResponseModel> openStores = closestStores.Where(x => x.IsOpen).ToList();
            List<StoreResponseModel> closedStores = closestStores.Where(x => !x.IsOpen).ToList();
            closestStores.Clear();

            openStores = openStores.Where(x => x.VendorCategory == Defaults.RestaurantVendorCategoryName)
                                         .OrderByDescending(vendorStore => GetRatingAttribute(vendorStore.VendorId))
                                         .OrderByDescending(vendorStore => GetVendorPopularityAttribute(vendorStore.VendorId)).ToList();

            closedStores = closedStores.Where(x => x.VendorCategory == Defaults.RestaurantVendorCategoryName)
                                         .OrderByDescending(vendorStore => GetRatingAttribute(vendorStore.VendorId))
                                         .OrderByDescending(vendorStore => GetVendorPopularityAttribute(vendorStore.VendorId)).ToList();

            closestStores.AddRange(openStores);
            closestStores.AddRange(closedStores);

            return closestStores.Take(firstFifteenStores).ToList();
        }

        ///<inheritdoc/>
        public List<StoreResponseModel> GetAllNearStore(decimal latitud, decimal longitud)
        {
            var closestStores = GetClosestStores(latitud, longitud);
            const int firstFifteenStores = 15;

            return closestStores.Take(firstFifteenStores).ToList();
        }

        ///<inheritdoc/>
        public List<StoreResponseModel> GetAllStoresWithPromotions(decimal latitud, decimal longitud)
        {
            var closestStores = GetClosestStores(latitud, longitud);
            const int firstFifteenStores = 15;

            return closestStores.Where(x => x.HasPromotions)
                                                    .Take(firstFifteenStores)
                                                    .ToList();
        }

        ///<inheritdoc/>
        public decimal GetOrderPaymentPercentageByVendorId(int vendorId)
        {
            string orderPaymentPercentage = GetVendorAttributeValueByVendorIdAndAttributeName(vendorId, Defaults.VendorOrderPaymentPercentageAttribute.Name);
            decimal.TryParse(orderPaymentPercentage, out decimal value);
            return value;


        }

        ///<inheritdoc/>
        public IList<StoreResponseModel> GetVendorsBySearchText(string searchText, decimal latitude, decimal longitude)
        {

            IList<Vendor> foundVendorsWithProductsOrVendorNameMatchSearchText = (from vendors in _vendorRepository.Table
                                                                                 join products in _productRepository.Table on vendors.Id equals products.VendorId
                                                                                 where (products.Name.Contains(searchText) || vendors.Name.Contains(searchText))
                                                                                 && !products.Deleted && !vendors.Deleted
                                                                                 select vendors).Distinct().ToList();

            IList<int> vendorIDs = foundVendorsWithProductsOrVendorNameMatchSearchText.Select(vendor => vendor.Id).ToList();

            IList<int> vendorWarehousesIds = _vendorWarehouseMappingRepository.Table
                .Where(vendorWarehouse => vendorIDs.Contains(vendorWarehouse.VendorId))
                .Select(vendorWarehouse => vendorWarehouse.WarehouseId)
                .ToList();
            IList<int> warehouseAddressIds = _warehouseRepository.Table
                .Where(warehouse => vendorWarehousesIds.Contains(warehouse.Id))
                .Select(warehouse => warehouse.AddressId)
                .ToList();

            IList<AddressGeoCoordinatesMapping> warehousesAddresses = _addressGeoCoordinateRepository.Table
               .Where(mapping => warehouseAddressIds.Contains(mapping.AddressId)).ToList();

            IEnumerable<StoreResponseModel> closestStores = foundVendorsWithProductsOrVendorNameMatchSearchText == null || foundVendorsWithProductsOrVendorNameMatchSearchText.Count == 0 ?
                                                  new List<StoreResponseModel>() :
                                                  BuildStoresWithClosestProximity(warehousesAddresses, latitude, longitude);

            return closestStores.OrderBy(store => store.IsOpen).ToList();

            //return foundVendorsWithProductsOrVendorNameMatchSearchText.Select(foundVendor => new StoreResponseModel
            //{
            //    VendorId = foundVendor.Id,
            //    Name = foundVendor.Name,
            //    LogoUrl = _pictureService.GetPictureUrl(foundVendor.PictureId),
            //    Rating = string.IsNullOrWhiteSpace(GetRatingAttribute(foundVendor.Id)) ? 0 :
            //        double.Parse(GetRatingAttribute(foundVendor.Id)),
            //    AdultsLimitated = GetVendorAdultsLimitatedAttribute(foundVendor.Id),
            //    HasPromotions = HasProductsWithPromotions(foundVendor.Id),
            //    ProximityInMeters = 200.1d,
            //    VendorTags = BuildCategoriesWithProducts(foundVendor.Id).Select(category => category.Name),
            //    EstimatedPreparationTime = GetEstimatedTimeAttribute(foundVendor.Id),
            //    IsOpen = IsVendorWarehouseOpen(foundVendor)
            //})
            //   .OrderBy(store => store.IsOpen)
            //   .ToList();
        }

        ///<inheritdoc/>
        public VendorInfo GetVendorInfo(int vendorId)
        {
            Vendor foundVendor = _vendorRepository.Table
                .FirstOrDefault(v => v.Id == vendorId);

            if (foundVendor is null)
                throw new ArgumentException("VendorNotFound");

            int vendorWareHouseId = GetVendorWarehouseIdByVendorId(foundVendor.Id).Value;

            IList<VendorWarehouseInfo> foundWarehouseSchedules = GetWarehouseScheduleByReflection(vendorWareHouseId)
    .Select(schedule => new VendorWarehouseInfo
    {
        IsOpen = schedule.IsActive,
        DayId = schedule.DayId,
        BeginTime = Convert.ToDateTime(schedule.BeginTime.ToString()),
        EndTime = Convert.ToDateTime(schedule.EndTime.ToString())
    })
    .OrderByDescending(order => order.DayId)
    .ToList();

            int maxDay = 6;

            for (int dayToAdd = maxDay; dayToAdd >= 0; dayToAdd--)
            {
                if (foundWarehouseSchedules.Any(day => day.DayId == dayToAdd)) continue;
                foundWarehouseSchedules.Add(new VendorWarehouseInfo
                {
                    IsOpen = false,
                    DayId = dayToAdd,
                    BeginTime = DateTime.Today,
                    EndTime = DateTime.Today,
                });
            }

            IList<string> specialities = BuildCategoriesWithProducts(foundVendor.Id)
                .Select(select => select.Name).ToList();

            return new VendorInfo
            {
                VendorId = foundVendor.Id,
                Name = foundVendor.Name,
                Specialities = specialities,
                EstimatedTime = GetEstimatedTimeAttribute(foundVendor.Id),
                Rating = string.IsNullOrWhiteSpace(GetRatingAttribute(foundVendor.Id)) ? 0 :
                    decimal.Parse(GetRatingAttribute(foundVendor.Id)),
                PictureUrl = _pictureService.GetPictureUrl(foundVendor.PictureId),
                VendorSchedules = foundWarehouseSchedules
            };
        }

        #endregion
    }
}
