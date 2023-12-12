using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Plugin.Pickup.PickupInStore.Domain;
using Nop.Plugin.Pickup.PickupInStore.Services;
using Nop.Plugin.Widgets.ProductAvailability.Domains;
using Nop.Plugin.Widgets.ProductAvailability.Models.Cellar;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Plugin.Widgets.ProductAvailability.Services;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Shipping;
using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Tasks
{
    /// <summary>
    /// Represents the task model for inventory sync.
    /// </summary>
    public sealed class DdpInventorySyncTask : IScheduleTask
    {
        #region Properties

        private readonly IProductAvailabilityService _productAvailabilityService;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IRepository<WarehousePickupPointMapping> _warehousePickupPointMappingRepository;
        private readonly ProductAvailabilitySettings _productAvailabilitySettings;
        private readonly IShippingService _shippingService;
        private readonly IStorePickupPointService _storePickupPointService;
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productRepository;
        private readonly IAddressService _addressService;
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<StorePickupPoint> _storePickupPointRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="DdpInventorySyncTask"/>.
        /// </summary>
        /// <param name="productAvailabilityService">An implementation of <see cref="IProductAvailabilityService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="warehousePickupPointMappingRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="WarehousePickupPointMapping"/>.</param>
        /// <param name="productAvailabilitySettings">An instance of <see cref="ProductAvailabilitySettings"/>.</param>
        /// <param name="shippingService">An implementation of <see cref="IShippingService"/>.</param>
        /// <param name="storePickupPointService">An implementation of <see cref="IStorePickupPointService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="productRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Product"/>.</param>
        /// <param name="addressService">An implementation of <see cref="IAddressService"/>.</param>
        /// <param name="warehouseRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="Warehouse"/>.</param>
        /// <param name="storePickupPointRepository">An implementation of <see cref="IRepository{T}"/> where T is <see cref="StorePickupPoint"/>.</param>
        public DdpInventorySyncTask(
            IProductAvailabilityService productAvailabilityService,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IRepository<WarehousePickupPointMapping> warehousePickupPointMappingRepository,
            ProductAvailabilitySettings productAvailabilitySettings,
            IShippingService shippingService,
            IStorePickupPointService storePickupPointService,
            ILogger logger,
            IRepository<Product> productRepository,
            IAddressService addressService,
            IRepository<Warehouse> warehouseRepository,
            IRepository<StorePickupPoint> storePickupPointRepository)
        {
            _productAvailabilityService = productAvailabilityService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _warehousePickupPointMappingRepository = warehousePickupPointMappingRepository;
            _productAvailabilitySettings = productAvailabilitySettings;
            _shippingService = shippingService;
            _storePickupPointService = storePickupPointService;
            _logger = logger;
            _productRepository = productRepository;
            _addressService = addressService;
            _warehouseRepository = warehouseRepository;
            _storePickupPointRepository = storePickupPointRepository;
        }

        #endregion

        #region Utilities

        private void DeleteExistingProductAttributeValues(IList<Product> publishedProducts)
        {
            foreach (Product product in publishedProducts)
            {
                _productAvailabilityService.DeleteAllProductAttributeCombinationsByProductId(product.Id);
                _productAvailabilityService.DeleteAllProductAttributeValuesByProductId(product.Id);
            }
        }

        private async System.Threading.Tasks.Task UpdateWarehousesAndPickupPoints()
        {
            try
            {
                CellarRequestResponse result = await RequestBranchOfficesFromWebService();

                foreach (BranchOffice branchOffice in result.Sucursales)
                {
                    Warehouse warehouse = _warehouseRepository.Table.FirstOrDefault(warehouse => warehouse.Name == branchOffice.Nombre);
                    StorePickupPoint storePickupPoint = _storePickupPointRepository.Table.FirstOrDefault(pickupPoint => pickupPoint.Name == branchOffice.Nombre);

                    if (warehouse == null)
                        warehouse = new Warehouse { Id = CreateNewWarehouseFromBranchOffice(branchOffice) };

                    if (storePickupPoint == null)
                        storePickupPoint = new StorePickupPoint { Id = CreateNewStorePickupPointFromBranchOffice(branchOffice) };

                    InsertNewWarehousePickupPointMapping(warehouse.Id, storePickupPoint.Id, branchOffice);
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Inventory sync error: {e.Message}", e);
                _notificationService.WarningNotification(e.Message);
            }
        }

        private async Task<CellarRequestResponse> RequestBranchOfficesFromWebService()
        {
            string token = _productAvailabilitySettings.UseSameTokenForAllRequest ? _productAvailabilitySettings.Token : _productAvailabilitySettings.StoresUrlToken;

            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format(_productAvailabilitySettings.StoresUrl, token));

            if (!response.IsSuccessStatusCode)
                throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingCellars"));

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CellarRequestResponse>(jsonResponse);

            if (!result.Success)
                throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingCellars"));

            return result;
        }

        private int CreateNewWarehouseFromBranchOffice(BranchOffice branchOffice)
        {
            var newWarehouse = new Warehouse
            {
                Name = branchOffice.Nombre,
                AdminComment = string.Format(ProductAvailabilityDefault.WarehouseAdminCommentTemplate, branchOffice.NumBodega, branchOffice.NumTienda),
                AddressId = GetNewAddressId()
            };

            _shippingService.InsertWarehouse(newWarehouse);

            return newWarehouse.Id;
        }

        private int CreateNewStorePickupPointFromBranchOffice(BranchOffice branchOffice)
        {
            var newPickupPoint = new StorePickupPoint
            {
                Name = branchOffice.Nombre,
                Description = branchOffice.NumBodega,
                DisplayOrder = 0,
                PickupFee = 0,
                TransitDays = 0,
                AddressId = GetNewAddressId()
            };

            _storePickupPointService.InsertStorePickupPoint(newPickupPoint);

            return newPickupPoint.Id;
        }

        private int GetNewAddressId()
        {
            var newAddress = new Address();
            _addressService.InsertAddress(newAddress);
            return newAddress.Id;
        }

        private void InsertNewWarehousePickupPointMapping(int warehouseId, int pickupPointId, BranchOffice branchOffice)
        {
            if (_warehousePickupPointMappingRepository.Table.Any(mapping => mapping.WarehouseId == warehouseId && mapping.PickupPointId == pickupPointId))
                return;

            var newMapping = new WarehousePickupPointMapping
            {
                WarehouseId = warehouseId,
                PickupPointId = pickupPointId,
                NumBodega = branchOffice.NumBodega,
                NumTienda = branchOffice.NumTienda
            };

            _warehousePickupPointMappingRepository.Insert(newMapping);
        }

        private async System.Threading.Tasks.Task UpdateProductsInventory(IList<Product> publishedProducts)
        {
            try
            {
                foreach (Product product in publishedProducts)
                {
                    InventoryRequestResponseModel inventoryRequestResponse = await TryToGetProductInventory(product.Sku);

                    if (inventoryRequestResponse is null)
                        continue;

                    if (inventoryRequestResponse.Tallas.Any(size => size.Trim().ToUpper().Equals(_productAvailabilitySettings.OneSizeProductsIdentifierCode)))
                        _productAvailabilityService.UpdateOneSizeProductInventory(product, inventoryRequestResponse);
                    else
                        _productAvailabilityService.InsertOrUpdateProductAttributeCombination(product, inventoryRequestResponse);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task<InventoryRequestResponseModel> TryToGetProductInventory(string productSku)
        {
            int requestTries = 0;
            InventoryRequestResponseModel inventoryRequestResponse = null;

            while (requestTries < _productAvailabilitySettings.InventoryRequestTries && inventoryRequestResponse is null)
            {
                inventoryRequestResponse = await _productAvailabilityService.GetProductInventoryBySku(productSku, filterEmptySizesFromInventory: false);

                if (inventoryRequestResponse is null)
                    _logger.Error($"Inventory sync error: Attempt number {requestTries + 1} to get inventory from web service for product with SKU {productSku} failed.");

                requestTries++;
            }

            return inventoryRequestResponse;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the task of synchronizing the inventory from DDP web service.
        /// </summary>
        public void Execute()
        {
            try
            {

                if (!_productAvailabilityService.PluginIsConfigured())
                    throw new NopException(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PluginNotConfigured"));

                var updateWarehouseTask = UpdateWarehousesAndPickupPoints();
                updateWarehouseTask.Wait();

                if (!_productAvailabilityService.SizeProductAttributeExists())
                    return;

                IList<Product> publishedProducts = _productRepository.Table.Where(product => product.Published && !product.Deleted).ToList();

                if (_productAvailabilitySettings.DeleteExistingProductAttributeValuesOnInventorySync)
                    DeleteExistingProductAttributeValues(publishedProducts);

                var updateProductInventoryTask = UpdateProductsInventory(publishedProducts);
                updateProductInventoryTask.Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
    }
}
