using Newtonsoft.Json;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Data;
using Nop.Plugin.Widgets.ProductAvailability.Domains;
using Nop.Plugin.Widgets.ProductAvailability.Extensions;
using Nop.Plugin.Widgets.ProductAvailability.Models;
using Nop.Plugin.Widgets.ProductAvailability.Models.Inventory;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductAvailability.Services
{
    /// <summary>
    /// Represents the services implementation for this plugin.
    /// </summary>
    public class ProductAvailabilityService : IProductAvailabilityService
    {
        #region Properties

        private ProductAttribute _configuredProductAttribute;
        private readonly ProductAvailabilitySettings _productAvailabilitySettings;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        private readonly IRepository<WarehousePickupPointMapping> _warehousePickupPointMappingRepository;
        private readonly IProductService _productService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ILogger _logger;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<ProductAttributeMapping> _productAttributeMappingRepository;
        private readonly IRepository<ProductAttributeValue> _productAttributeValueRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="ProductAvailabilityService"/>.
        /// </summary>
        /// <param name="productAvailabilitySettings">An instance of <see cref="ProductAvailabilitySettings"/>.</param>
        /// <param name="notificationService">An implementation of <see cref="INotificationService"/>.</param>
        /// <param name="settingService">An implementation of <see cref="ISettingService"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        /// <param name="warehousePickupPointMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="WarehousePickupPointMapping"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="productAttributeParser">An implementation of <see cref="IProductAttributeParser"/>.</param>
        /// <param name="productAttributeService">An implementation of <see cref="IProductAttributeService"/>.</param>
        /// <param name="logger">An implementation of <see cref="ILogger"/>.</param>
        /// <param name="productAttributeRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="ProductAttribute"/>.</param>
        /// <param name="productAttributeMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="ProductAttributeMapping"/>.</param>
        /// <param name="productRepository">>An implementation of <see cref="IRepository{TEntity}"/> where TEntity is <see cref="Product"/>.</param>
        public ProductAvailabilityService(
            ProductAvailabilitySettings productAvailabilitySettings,
            INotificationService notificationService,
            ISettingService settingService,
            ILocalizationService localizationService,
            IRepository<WarehousePickupPointMapping> warehousePickupPointMappingRepository,
            IProductService productService,
            IProductAttributeParser productAttributeParser,
            IProductAttributeService productAttributeService,
            ILogger logger,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<ProductAttributeMapping> productAttributeMappingRepository,
            IRepository<ProductAttributeValue> productAttributeValueRepository,
            IRepository<Product> productRepository,
            IPictureService pictureService)
        {
            _productAvailabilitySettings = productAvailabilitySettings;
            _notificationService = notificationService;
            _settingService = settingService;
            _localizationService = localizationService;
            _warehousePickupPointMappingRepository = warehousePickupPointMappingRepository;
            _productService = productService;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _logger = logger;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeMappingRepository = productAttributeMappingRepository;
            _productAttributeValueRepository = productAttributeValueRepository;
            _productRepository = productRepository;
            _pictureService = pictureService;
        }

        #endregion

        #region Utilities

        private int GetConfiguredProductAttributeMappingIdByProductId(int productId)
        {
            ProductAttributeMapping configuredProductAttributeMapping = _productAttributeService.GetProductAttributeMappingsByProductId(productId)
                .Where(mapping => mapping.ProductAttributeId == _productAvailabilitySettings.ProductAttributeId).FirstOrDefault();
            if (configuredProductAttributeMapping is null) throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeInfo"));
            return configuredProductAttributeMapping.Id;
        }

        private string GetProductAttributeValueName(string attributesXml, int configuredProductAttributeMappingId)
        {
            ProductAttributeValue productAttributeValue = _productAttributeParser.ParseProductAttributeValues(attributesXml, configuredProductAttributeMappingId).FirstOrDefault();
            if (productAttributeValue is null) throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeValue"));
            return productAttributeValue.Name;
        }

        private void CheckItemAvailability(InventoryRequestResponseModel inventoryResponse, string productAttributeValueName, string pickupPointCellarNumber, string productName, int cartItemQty)
        {
            for (int index = 0; index < inventoryResponse.Tallas.Count(); index++)
            {
                if (inventoryResponse.Tallas[index].Trim().Equals(productAttributeValueName))
                {
                    StoreModel store = inventoryResponse.Sucursales.Where(store => store.NumBodega == pickupPointCellarNumber).FirstOrDefault();
                    if (store is null) throw new Exception(string.Format(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductNotAvailableInSelectedPickupPoint"), productName));
                    if (!int.TryParse(store.Existencia[index], out int availableQty)) throw new Exception(string.Format(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductNotAvailableInSelectedPickupPoint"), productName));
                    if (cartItemQty > availableQty) throw new Exception(string.Format(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductQtyNotAvailableInSelectedPickupPoint"), productName, availableQty));
                }
            }
        }

        private int GetProductAttributeMappingId(int productId)
        {
            ProductAttributeMapping productAttributeMapping = _productAttributeMappingRepository.Table.Where(pam => pam.ProductId == productId
                && pam.ProductAttributeId == _configuredProductAttribute.Id).FirstOrDefault();

            return productAttributeMapping is null ? InsertNewProductAttributeMapping(productId) : productAttributeMapping.Id;
        }

        private int InsertNewProductAttributeMapping(int productId)
        {
            var newProductAttributeMapping = new ProductAttributeMapping
            {
                ProductAttributeId = _configuredProductAttribute.Id,
                ProductId = productId,
                TextPrompt = _configuredProductAttribute.Name,
                IsRequired = true,
                AttributeControlType = AttributeControlType.ImageSquares
            };

            _productAttributeService.InsertProductAttributeMapping(newProductAttributeMapping);

            return newProductAttributeMapping.Id;
        }

        private int InsertNewSizeProductAttributeValue(IList<string> sizes, int index, int productAttributeMappingId)
        {
            int imageSquarePictureId = GetPictureIdForNewSizeProductAttributeValue(sizes[index].Trim());

            var newSizeProductAttributeValue = new ProductAttributeValue
            {
                Name = sizes[index].Trim(),
                ProductAttributeMappingId = productAttributeMappingId,
                Quantity = 1,
                DisplayOrder = index,
                ImageSquaresPictureId = imageSquarePictureId
            };

            _productAttributeService.InsertProductAttributeValue(newSizeProductAttributeValue);

            return newSizeProductAttributeValue.Id;
        }

        private void InsertNewProductAttributeCombination(Product product, int index, int productAttributeMappingId,
            int productAttributeValueId, IList<StoreModel> cellars)
        {
            var newProductAttributeCombination = new ProductAttributeCombination
            {
                Sku = $"{product.Sku}{index + 1}",
                ProductId = product.Id,
                AttributesXml = $"<Attributes>{GetDefaultAttributeXml(productAttributeMappingId, productAttributeValueId)}</Attributes>",
                StockQuantity = GetStockQuantity(cellars, index),
                NotifyAdminForQuantityBelow = 1
            };

            _productAttributeService.InsertProductAttributeCombination(newProductAttributeCombination);
        }

        private void UpdateProductAttributeCombination(Product product, int index, int productAttributeMappingId,
            int productAttributeValueId, IList<StoreModel> cellars)
        {
            IList<ProductAttributeCombination> productAttributeCombinations = _productAttributeService.GetAllProductAttributeCombinations(product.Id);

            ProductAttributeCombination attributeCombination = productAttributeCombinations
                .Where(pac => pac.AttributesXml.Contains(GetDefaultAttributeXml(productAttributeMappingId, productAttributeValueId)))
                .FirstOrDefault();

            if (attributeCombination != null)
            {
                attributeCombination.StockQuantity = GetStockQuantity(cellars, index);
                _productAttributeService.UpdateProductAttributeCombination(attributeCombination);
            }
            else
            {
                InsertNewProductAttributeCombination(product, index, productAttributeMappingId, productAttributeValueId, cellars);
            }
        }

        private string GetDefaultAttributeXml(int productAttributeMappingId, int productAttributeValueId)
            => string.Format(ProductAvailabilityDefault.DefaultProductAttributeXmlFormat, productAttributeMappingId, productAttributeValueId);

        private int GetStockQuantity(IList<StoreModel> cellars, int index)
        {
            WarehousePickupPointMapping warehousePickupPointMapping = _warehousePickupPointMappingRepository.Table
                .Where(mapping => mapping.WarehouseId == _productAvailabilitySettings.InventoryWarehouseId).FirstOrDefault();
            string inventorStoreNumber = warehousePickupPointMapping?.NumTienda ?? ProductAvailabilityDefault.DefaultStoreNumberIfNoneConfigured;
            int.TryParse(cellars.Where(cellar => cellar.NumTienda == inventorStoreNumber).FirstOrDefault()?.Existencia[index], out int result);

            return result;
        }

        private void UpdateProductInventoryTrackingMethod(Product product)
        {
            product.ManageInventoryMethodId = (int)ManageInventoryMethod.ManageStockByAttributes;
            product.WarehouseId = _productAvailabilitySettings.InventoryWarehouseId;
            product.DisplayStockQuantity = true;
            _productRepository.Update(product);
        }

        private void FilterEmptySizesFromInventory(InventoryRequestResponseModel inventoryRequest)
        {
            var indexesToRemoveFromList = new List<int>();
            for (int index = 0; index < inventoryRequest.Tallas.Count; index++)
            {
                bool sizeHasInventoryValues = inventoryRequest.Sucursales
                    .Any(sucursal => !sucursal.Existencia[index].Trim().Equals("0")
                    && !string.IsNullOrWhiteSpace(sucursal.Existencia[index].Trim()));

                if (!sizeHasInventoryValues)
                {
                    indexesToRemoveFromList.Add(index);
                }
            }

            inventoryRequest.Tallas = inventoryRequest.Tallas.SkipIndexes(indexesToRemoveFromList).ToList();
            inventoryRequest.Sucursales.ForEach(store => store.Existencia = store.Existencia.SkipIndexes(indexesToRemoveFromList).ToList());
        }

        private void DeleteProductAttributeMapping(int productId)
        {
            ProductAttributeMapping productAttributeMapping = _productAttributeMappingRepository.Table.FirstOrDefault(mapping =>
                mapping.ProductId == productId && mapping.ProductAttributeId == _configuredProductAttribute.Id);

            if (productAttributeMapping != null)
                _productAttributeMappingRepository.Delete(productAttributeMapping);
        }

        private int GetPictureIdForNewSizeProductAttributeValue(string sizeName)
        {
            const int DEFAULT_PICTURE_ID = 0;
            ProductAttributeValue productAttributeValue = _productAttributeValueRepository.Table.FirstOrDefault(pav => pav.Name == sizeName && pav.ImageSquaresPictureId != DEFAULT_PICTURE_ID);

            if (productAttributeValue is null) return DEFAULT_PICTURE_ID;

            Picture picture = _pictureService.GetPictureById(productAttributeValue.ImageSquaresPictureId);

            if (picture is null) return DEFAULT_PICTURE_ID;

            byte[] binaries = _pictureService.LoadPictureBinary(picture);

            Picture newPicture = _pictureService.InsertPicture(binaries, picture.MimeType, picture.SeoFilename);

            return newPicture.Id;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get the inventory information of a product by it's SKU.
        /// </summary>
        /// <param name="sku">The SKU of the product.</param>
        /// <returns>An instance of <see cref="InventoryRequestResponseModel"/>.</returns>
        public async Task<InventoryRequestResponseModel> GetProductInventoryBySku(string sku, bool filterEmptySizesFromInventory = true)
        {
            if (!PluginIsConfigured())
                return null;

            try
            {
                const int REQUIRED_SKU_LENGHT = 8;

                if (string.IsNullOrEmpty(sku) || string.IsNullOrWhiteSpace(sku) || sku.Length < REQUIRED_SKU_LENGHT)
                    throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.InvalidSku"));

                string token = _productAvailabilitySettings.UseSameTokenForAllRequest ? _productAvailabilitySettings.Token : _productAvailabilitySettings.ProductInventoryUrlToken;
                string formattedSku = sku.Length > REQUIRED_SKU_LENGHT ? sku.Substring(0, REQUIRED_SKU_LENGHT) : sku;

                if (sku.Length < REQUIRED_SKU_LENGHT) return null;

                using var client = new HttpClient();
                var response = await client.GetAsync(string.Format(_productAvailabilitySettings.ProductInventoryUrl, token, formattedSku));

                if (!response.IsSuccessStatusCode)
                    throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingProductInventory"));

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<InventoryRequestResponseModel>(jsonResponse);

                if (!result.Success)
                    throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.RequestNotSuccessful"));

                if (filterEmptySizesFromInventory)
                    FilterEmptySizesFromInventory(result);

                return result;
            }
            catch (Exception e)
            {
                _notificationService.ErrorNotification(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a boolean indicating if the plugin is correctly configured.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool PluginIsConfigured()
        {
            var productAvailabilitySettings = _settingService.LoadSetting<ProductAvailabilitySettings>();

            return !string.IsNullOrWhiteSpace(productAvailabilitySettings.ProductInventoryUrl)
                && !string.IsNullOrWhiteSpace(productAvailabilitySettings.StoresUrl)
                && productAvailabilitySettings.InventoryRequestTries > 0
                && ((productAvailabilitySettings.UseSameTokenForAllRequest && !string.IsNullOrWhiteSpace(productAvailabilitySettings.Token))
                    || (!productAvailabilitySettings.UseSameTokenForAllRequest
                        && !string.IsNullOrWhiteSpace(productAvailabilitySettings.ProductInventoryUrlToken)
                        && !string.IsNullOrWhiteSpace(productAvailabilitySettings.StoresUrlToken)));
        }

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the picup point id.
        /// </summary>
        /// <param name="id">The pickup point id.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        public WarehousePickupPointMapping GetWarehousePickupPointMappingByPickupPointId(int id)
            => _warehousePickupPointMappingRepository.Table.Where(mapping => mapping.PickupPointId == id).FirstOrDefault();

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the warehouse id.
        /// </summary>
        /// <param name="id">The warehouse id.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        public WarehousePickupPointMapping GetWarehousePickupPointMappingByWarehouseId(int id)
            => _warehousePickupPointMappingRepository.Table.Where(mapping => mapping.WarehouseId == id).FirstOrDefault();

        /// <summary>
        /// Gets a <see cref="WarehousePickupPointMapping"/> by the store number.
        /// </summary>
        /// <param name="storeNumber">The store number.</param>
        /// <returns>An instance of <see cref="WarehousePickupPointMapping"/> or <see cref="null"/> if not found.</returns>
        public WarehousePickupPointMapping GetWarehousePickupPointMappingByStoreNumber(string storeNumber)
            => _warehousePickupPointMappingRepository.Table.Where(mapping => mapping.NumTienda == storeNumber).FirstOrDefault();

        /// <summary>
        /// Verifies the availability of the products before placing an order.
        /// </summary>
        /// <param name="pickupPointCellarNumber">The id number for the selected pickup point option.</param>
        /// <param name="cartItems">The items about to be checked out.</param>
        /// <returns>An instance of <see cref="Task{TResult}"/> where TResult is <see cref="CheckoutAvailabilityVerificationResult"/>.</returns>
        public async Task<CheckoutAvailabilityVerificationResult> CheckShoppingCartItemsAvailabilityOnSelectedPickupPointBeforeContinuingCheckout(string pickupPointCellarNumber, IList<ShoppingCartItem> cartItems)
        {
            var result = new CheckoutAvailabilityVerificationResult { Success = true, Errors = new List<string>() };

            foreach (ShoppingCartItem cartItem in cartItems)
            {
                try
                {
                    Product product = _productService.GetProductById(cartItem.ProductId);
                    InventoryRequestResponseModel response = await GetProductInventoryBySku(product.Sku);

                    if (response is null)
                    {
                        _logger.Error(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ErrorRequestingProductInventory"));
                        continue;
                    }

                    int configuredProductAttributeMappingId = GetConfiguredProductAttributeMappingIdByProductId(cartItem.ProductId);
                    string productAttributeValueName = GetProductAttributeValueName(cartItem.AttributesXml, configuredProductAttributeMappingId);
                    CheckItemAvailability(response, productAttributeValueName, pickupPointCellarNumber, product.Name, cartItem.Quantity);
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message, e);
                    result.Success = false;
                    result.Errors.Add(e.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// Retrieves the cellar number for an given pickup point by it's id.
        /// </summary>
        /// <param name="id">The id of the pickup point.</param>
        /// <returns>An <see cref="string"/> containing the cellar number.</returns>
        public string GetCellarNumberByPickupPointId(string id)
        {
            bool success = int.TryParse(id, out int pickupPointId);
            if (!success) throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.PickupPointInfo"));
            WarehousePickupPointMapping warehousePickupPointMapping = GetWarehousePickupPointMappingByPickupPointId(pickupPointId);
            if (warehousePickupPointMapping is null) throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.CellarInfo"));
            return warehousePickupPointMapping.NumBodega;
        }

        /// <summary>
        /// Retrieves the cellar number of the configured inventory warehouse.
        /// </summary>
        /// <returns>An <see cref="string"/> containing the cellar number.</returns>
        public string GetConfiguredWarehouseCellarNumber()
        {
            WarehousePickupPointMapping warehousePickupPointMapping = GetWarehousePickupPointMappingByWarehouseId(_productAvailabilitySettings.InventoryWarehouseId);
            if (warehousePickupPointMapping is null)
                warehousePickupPointMapping = GetWarehousePickupPointMappingByStoreNumber(ProductAvailabilityDefault.DefaultStoreNumberIfNoneConfigured);
            if (warehousePickupPointMapping is null) throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.CellarInfo"));
            return warehousePickupPointMapping.NumBodega;
        }

        /// <inheritdoc/>
        public bool SizeProductAttributeExists()
        {
            try
            {
                _configuredProductAttribute = _productAttributeRepository.Table
                    .Where(attribute => attribute.Id == _productAvailabilitySettings.ProductAttributeId)
                    .FirstOrDefault();

                if (_configuredProductAttribute is null)
                    throw new Exception(_localizationService.GetResource($"{ProductAvailabilityDefault.LocaleResourcesPrefix}.Exception.ProductAttributeNotFound"));

                return true;
            }
            catch (Exception e)
            {
                _logger.Error($"Inventory sync error: {e.Message}", e);
                _notificationService.ErrorNotification($"Inventory sync error: {e.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public void InsertOrUpdateProductAttributeCombination(Product product, InventoryRequestResponseModel inventoryRequestResponse)
        {
            int productAttributeMappingId = GetProductAttributeMappingId(product.Id);
            IList<ProductAttributeValue> productAttributeValues = _productAttributeService.GetProductAttributeValues(productAttributeMappingId);

            for (int index = 0; index < inventoryRequestResponse.Tallas.Count(); index++)
            {
                if (string.IsNullOrWhiteSpace(inventoryRequestResponse.Tallas[index].Trim())) break;

                if (inventoryRequestResponse.Sucursales.All(store => store.Existencia[index].Trim().Equals("0") || string.IsNullOrWhiteSpace(store.Existencia[index]))) continue;

                ProductAttributeValue sizeProductAttributeValue = productAttributeValues.Where(pav => pav.Name == inventoryRequestResponse.Tallas[index].Trim())
                    .FirstOrDefault();

                if (sizeProductAttributeValue is null)
                {
                    int newSizeProductAttributeValueId = InsertNewSizeProductAttributeValue(inventoryRequestResponse.Tallas, index, productAttributeMappingId);
                    InsertNewProductAttributeCombination(product, index, productAttributeMappingId, newSizeProductAttributeValueId, inventoryRequestResponse.Sucursales);
                }
                else
                {
                    UpdateProductAttributeCombination(product, index, productAttributeMappingId, sizeProductAttributeValue.Id, inventoryRequestResponse.Sucursales);
                }
            }

            UpdateProductInventoryTrackingMethod(product);
        }

        ///<inheritdoc/>
        public void DeleteAllProductAttributeCombinationsByProductId(int productId)
        {
            foreach(ProductAttributeCombination combination in _productAttributeService.GetAllProductAttributeCombinations(productId))
            {
                _productAttributeService.DeleteProductAttributeCombination(combination);
            }
        }

        ///<inheritdoc/>
        public void DeleteAllProductAttributeValuesByProductId(int productId)
        {
            int productAttributeMappingId = GetProductAttributeMappingId(productId);

            foreach (ProductAttributeValue mapping in _productAttributeService.GetProductAttributeValues(productAttributeMappingId))
            {
                _productAttributeService.DeleteProductAttributeValue(mapping);
            }
        }

        ///<inheritdoc/>
        public void UpdateOneSizeProductInventory(Product product, InventoryRequestResponseModel inventoryRequestResponse)
        {
            // Here the cellar index for the stock availability will always be 0.
            product.StockQuantity = GetStockQuantity(inventoryRequestResponse.Sucursales, 0);

            WarehousePickupPointMapping warehousePickupPointMapping = _warehousePickupPointMappingRepository.Table
                .Where(mapping => mapping.WarehouseId == _productAvailabilitySettings.InventoryWarehouseId).FirstOrDefault();

            if (warehousePickupPointMapping != null)
                product.WarehouseId = warehousePickupPointMapping.WarehouseId;

            product.ManageInventoryMethod = ManageInventoryMethod.ManageStock;

            _productRepository.Update(product);

            // Deleting all product attribute combinations, attributes values and configured attribute mapping
            DeleteAllProductAttributeCombinationsByProductId(product.Id);
            DeleteAllProductAttributeValuesByProductId(product.Id);
            DeleteProductAttributeMapping(product.Id);
        }

        #endregion
    }
}
