using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains.Enums;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Helpers;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.ExportImport;
using Nop.Services.ExportImport.Help;
using Nop.Services.Media;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Areas.Admin.Factories
{
    /// <summary>
    /// Represents an implementation for <see cref="IOrderExportManager"/>.
    /// </summary>
    public class OrderExportManager : IOrderExportManager
    {
        #region Fields
        private readonly Services.IDeliveryAppDriverService _driverService;
        private readonly CatalogSettings _catalogSettings;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly IProductService _productService;
        private readonly IProductTagService _productTagService;
        private readonly IRepository<OrderDeliveryStatusMapping> _orderDeliveryStatusMappingRepository;
        private readonly IShipmentService _shipmentService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private readonly OrderSettings _orderSettings;
        private readonly ProductEditorSettings _productEditorSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="OrderExportManager"/>.
        /// </summary>
        /// <param name="catalogSettings">An instance of <see cref="CatalogSettings"/>.</param>
        /// <param name="categoryService">An implementation of <see cref="ICategoryService"/>.</param>
        /// <param name="customerService">An implementation of <see cref="ICustomerService"/>.</param>
        /// <param name="genericAttributeService">An implementation of <see cref="IGenericAttributeService"/>.</param>
        /// <param name="manufacturerService">An implementation of <see cref="IManufacturerService"/>.</param>
        /// <param name="orderService">An implementation of <see cref="IOrderService"/>.</param>
        /// <param name="pictureService">An implementation of <see cref="IPictureService"/>.</param>
        /// <param name="productService">An implementation of <see cref="IProductService"/>.</param>
        /// <param name="productTagService">An implementation of <see cref="IProductTagService"/>.</param>
        /// <param name="orderDeliveryStatusMappingRepository">An implementation of <see cref="IRepository{OrderDeliveryStatusMapping}"/>.</param>
        /// <param name="shipmentService">An implementation of <see cref="IShipmentService"/>.</param>
        /// <param name="storeMappingService">An implementation of <see cref="IStoreMappingService"/>.</param>
        /// <param name="storeService">An implementation of <see cref="IStoreService"/>.</param>
        /// <param name="vendorService">An implementation of <see cref="IVendorService"/>.</param>
        /// <param name="workContext">An implementation of <see cref="IWorkContext"/>.</param>
        /// <param name="orderSettings">An instance of <see cref="OrderSettings"/>.</param>
        /// <param name="productEditorSettings">An instance of <see cref="ProductEditorSettings"/>.</param>
        public OrderExportManager(CatalogSettings catalogSettings,
  
            ICategoryService categoryService,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService, 
            IManufacturerService manufacturerService, 
            IOrderService orderService, 
            IPictureService pictureService, 
            IProductService productService, 
            IProductTagService productTagService,
            IRepository<OrderDeliveryStatusMapping> orderDeliveryStatusMappingRepository,
            IShipmentService shipmentService,
            IStoreMappingService storeMappingService, 
            IStoreService storeService,
            Services.IDeliveryAppDriverService driverService,
            IVendorService vendorService,
            IWorkContext workContext, 
            OrderSettings orderSettings, 
            ProductEditorSettings productEditorSettings)
        {
            _catalogSettings = catalogSettings;
            _categoryService = categoryService;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _manufacturerService = manufacturerService;
            _orderService = orderService;
            _driverService = driverService;
            _pictureService = pictureService;
            _productService = productService;
            _productTagService = productTagService;
            _orderDeliveryStatusMappingRepository = orderDeliveryStatusMappingRepository;
            _shipmentService = shipmentService;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _vendorService = vendorService;
            _workContext = workContext;
            _orderSettings = orderSettings;
            _productEditorSettings = productEditorSettings;
        }

        #endregion

        #region Utilities

        private byte[] ExportOrderToXlsxWithProducts(PropertyByName<OrderPendingToClosePayment>[] properties, IEnumerable<OrderPendingToClosePayment> itemsToExport)
        {
            var orderItemProperties = new[]
            {
                new PropertyByName<OrderItem>("Name", oi => _productService.GetProductById(oi.ProductId).Name),
                new PropertyByName<OrderItem>("Sku", oi => _productService.GetProductById(oi.ProductId).Sku),
                new PropertyByName<OrderItem>("PriceExclTax", oi => oi.UnitPriceExclTax),
                new PropertyByName<OrderItem>("PriceInclTax", oi => oi.UnitPriceInclTax),
                new PropertyByName<OrderItem>("Quantity", oi => oi.Quantity),
                new PropertyByName<OrderItem>("DiscountExclTax", oi => oi.DiscountAmountExclTax),
                new PropertyByName<OrderItem>("DiscountInclTax", oi => oi.DiscountAmountInclTax),
                new PropertyByName<OrderItem>("TotalExclTax", oi => oi.PriceExclTax),
                new PropertyByName<OrderItem>("TotalInclTax", oi => oi.PriceInclTax)
            };

            var orderItemsManager = new PropertyManager<OrderItem>(orderItemProperties, _catalogSettings);

            using var stream = new MemoryStream();
            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handles to the worksheets
                var worksheet = xlPackage.Workbook.Worksheets.Add("Ordenes Pendientes Por Pagar");
                var fpWorksheet = xlPackage.Workbook.Worksheets.Add("DataForProductsFilters");
                fpWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                //create Headers and format them 
                var manager = new PropertyManager<OrderPendingToClosePayment>(properties, _catalogSettings);
                manager.WriteCaption(worksheet);

                var row = 2;
                foreach (var order in itemsToExport)
                {
                    manager.CurrentObject = order;
                    manager.WriteToXlsx(worksheet, row++);

                    //a vendor should have access only to his products
                    var orderItems = _orderService.GetOrderItems(order.Id, vendorId: _workContext.CurrentVendor?.Id ?? 0);

                    if (!orderItems.Any())
                        continue;

                    orderItemsManager.WriteCaption(worksheet, row, 2);
                    worksheet.Row(row).OutlineLevel = 1;
                    worksheet.Row(row).Collapsed = true;

                    foreach (var orederItem in orderItems)
                    {
                        row++;
                        orderItemsManager.CurrentObject = orederItem;
                        orderItemsManager.WriteToXlsx(worksheet, row, 2, fpWorksheet);
                        worksheet.Row(row).OutlineLevel = 1;
                        worksheet.Row(row).Collapsed = true;
                    }

                    row++;
                }

                xlPackage.Save();
            }

            return stream.ToArray();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the list of limited to stores for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of store</returns>
        protected virtual string GetLimitedToStores(Product product)
        {
            string limitedToStores = null;
            foreach (var storeMapping in _storeMappingService.GetStoreMappings(product))
            {
                var store = _storeService.GetStoreById(storeMapping.StoreId);

                limitedToStores += _catalogSettings.ExportImportRelatedEntitiesByName ? store.Name : store.Id.ToString();

                limitedToStores += ";";
            }

            return limitedToStores;
        }

        /// <summary>
        /// Returns the list of product tag for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of product tag</returns>
        protected virtual string GetProductTags(Product product)
        {
            string productTagNames = null;

            var productTags = _productTagService.GetAllProductTagsByProductId(product.Id);

            if (!productTags?.Any() ?? true)
                return null;

            foreach (var productTag in productTags)
            {
                productTagNames += _catalogSettings.ExportImportRelatedEntitiesByName
                    ? productTag.Name
                    : productTag.Id.ToString();

                productTagNames += ";";
            }

            return productTagNames;
        }

        /// <summary>
        /// Returns the list of manufacturer for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of manufacturer</returns>
        protected virtual string GetManufacturers(Product product)
        {
            string manufacturerNames = null;
            foreach (var pm in _manufacturerService.GetProductManufacturersByProductId(product.Id, true))
            {
                if (_catalogSettings.ExportImportRelatedEntitiesByName)
                {
                    var manufacturer = _manufacturerService.GetManufacturerById(pm.ManufacturerId);
                    manufacturerNames += manufacturer.Name;
                }
                else
                {
                    manufacturerNames += pm.ManufacturerId.ToString();
                }

                manufacturerNames += ";";
            }

            return manufacturerNames;
        }

        /// <summary>
        /// Returns the list of categories for a product separated by a ";"
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>List of categories</returns>
        protected virtual string GetCategories(Product product)
        {
            string categoryNames = null;
            foreach (var pc in _categoryService.GetProductCategoriesByProductId(product.Id, true))
            {
                if (_catalogSettings.ExportImportRelatedEntitiesByName)
                {
                    var category = _categoryService.GetCategoryById(pc.CategoryId);
                    categoryNames += _catalogSettings.ExportImportProductCategoryBreadcrumb
                        ? _categoryService.GetFormattedBreadCrumb(category)
                        : category.Name;
                }
                else
                {
                    categoryNames += pc.CategoryId.ToString();
                }

                categoryNames += ";";
            }

            return categoryNames;
        }

        protected virtual bool IgnoreExportLimitedToStore()
        {
            return _catalogSettings.IgnoreStoreLimitations ||
                   !_catalogSettings.ExportImportProductUseLimitedToStores ||
                   _storeService.GetAllStores().Count == 1;
        }

        /// <summary>
        /// Returns the path to the image file by ID
        /// </summary>
        /// <param name="pictureId">Picture ID</param>
        /// <returns>Path to the image file</returns>
        protected virtual string GetPictures(int pictureId)
        {
            var picture = _pictureService.GetPictureById(pictureId);
            return _pictureService.GetThumbLocalPath(picture);
        }

        protected virtual bool IgnoreExportPoductProperty(Func<ProductEditorSettings, bool> func)
        {
            var productAdvancedMode = true;
            try
            {
                productAdvancedMode = _genericAttributeService.GetAttribute<bool>(_workContext.CurrentCustomer, "product-advanced-mode");
            }
            catch (ArgumentNullException)
            {
            }

            return !productAdvancedMode && !func(_productEditorSettings);
        }

        ///<inheritdoc/>
        public byte[] ExportOrdersToXlsx(IList<OrderPendingToClosePayment> orders)
        {
            //a vendor should have access only to part of order information
            var ignore = _workContext.CurrentVendor != null;

            //property array
            var properties = new[]
            {
                new PropertyByName<OrderPendingToClosePayment>("Numero Orden", p => p.CustomOrderNumber),
                new PropertyByName<OrderPendingToClosePayment>("Id de la orden", p => p.OrderId),
                new PropertyByName<OrderPendingToClosePayment>("Suplidor", p =>
                {
                    Vendor orderStore = _vendorService.GetVendorById(p.VendorId);

                    return orderStore != null ? $"{orderStore.Name} ({orderStore.Email})" : "";
                }),
                new PropertyByName<OrderPendingToClosePayment>("Estatus de pago del suplidor", p => p.VendorPaymentStatus.ToString()),
                new PropertyByName<OrderPendingToClosePayment>("Subtotal de la orden sin impuesto", p => p.OrderSubtotalExclTax),
                new PropertyByName<OrderPendingToClosePayment>("Impuesto", p => p.OrderTax),
                new PropertyByName<OrderPendingToClosePayment>("Porcentaje de ganancia del suplidor", p =>  $"{p.OrderTotalVendorPercentage}%"),
                new PropertyByName<OrderPendingToClosePayment>("Ganancia total del suplidor del total de la orden", p => p.OrderTotalVendorProfitAmount),
                new PropertyByName<OrderPendingToClosePayment>("Porcentaje de ganancia de OHD", p => $"{p.OrderTotalAdministrativePercentage}%"),
                new PropertyByName<OrderPendingToClosePayment>("Ganancia total de OHD del total de la orden", p => p.OrderTotalAdministrativeProfitAmount),
                new PropertyByName<OrderPendingToClosePayment>("Motorizado", p =>
                {
                    Customer driverCustomer = _customerService.GetCustomerById(_orderDeliveryStatusMappingRepository.Table
                                            .FirstOrDefault(mapping => mapping.OrderId == p.OrderId
                                             && mapping.DeliveryStatusId == (int)DeliveryStatus.Delivered)?.CustomerId ?? 0);

                    if (driverCustomer is null) return "";

                    string firstName = _genericAttributeService.GetAttribute<string>(driverCustomer, NopCustomerDefaults.FirstNameAttribute);
                    string lastName = _genericAttributeService.GetAttribute<string>(driverCustomer, NopCustomerDefaults.LastNameAttribute);

                    return $"{firstName} {lastName} ({driverCustomer.Email})";
                }),
                new PropertyByName<OrderPendingToClosePayment>("Estatus de pago del motorizado", p => p.DriverPaymentStatus),
                new PropertyByName<OrderPendingToClosePayment>("OrderShippingInclTax", p => p.OrderShippingInclTax),
                new PropertyByName<OrderPendingToClosePayment>("Porcentaje de ganancia del motorizado del delivery", p => $"{p.ShippingTaxMessengerPercentage}%"),
                new PropertyByName<OrderPendingToClosePayment>("Ganancia total del motorizado del delivery", p => p.ShippingTaxMessengerProfitAmount),
                new PropertyByName<OrderPendingToClosePayment>("Porcentaje de ganancia de OHD del delivery", p => $"{p.ShippingTaxAdministrativePercentage}%"),
                new PropertyByName<OrderPendingToClosePayment>("Ganancia total del motorizado de OHD", p => p.ShippingTaxAdministrativeProfitAmount),
                new PropertyByName<OrderPendingToClosePayment>("Total de la orden", p => p.OrderTotal),
                new PropertyByName<OrderPendingToClosePayment>("Fecha creacion", p => p.CreatedOnUtc.ToString())
            };

            return _orderSettings.ExportWithProducts
                ? ExportOrderToXlsxWithProducts(properties, orders)
                : new PropertyManager<OrderPendingToClosePayment>(properties, _catalogSettings).ExportToXlsx(orders);
        }

        ///<inheritdoc/>
        public string ExportOrdersToXml(IList<OrderPendingToClosePayment> orders)
        {
            //a vendor should have access only to part of order information
            var ignore = _workContext.CurrentVendor != null;

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Orders");
            xmlWriter.WriteAttributeString("Version", NopVersion.CurrentVersion);

            foreach (var order in orders)
            {
                xmlWriter.WriteStartElement("Order");

                xmlWriter.WriteString("Numero Orden", order.CustomOrderNumber);
                xmlWriter.WriteString("Id de la orden", order.OrderId);
                xmlWriter.WriteString("Id del suplidor", order.VendorId);
                xmlWriter.WriteString("Estatus de pago del suplidor", order.VendorPaymentStatus.ToString());
                xmlWriter.WriteString("Subtotal de la orden sin inpuesto", order.OrderSubtotalExclTax);
                xmlWriter.WriteString("Porcentaje de ganancia del suplidor", $"{order.OrderTotalVendorPercentage}%");
                xmlWriter.WriteString("Ganancia total del suplidor del total de la orden", order.OrderTotalVendorProfitAmount);
                xmlWriter.WriteString("Porcentaje de ganancia de OHD", $"{order.OrderTotalAdministrativePercentage}%");
                xmlWriter.WriteString("Ganancia total de OHD del total de la orden", order.OrderTotalAdministrativeProfitAmount);

                xmlWriter.WriteString("Estatus de pago del motorizado", order.DriverPaymentStatus.ToString());
                xmlWriter.WriteString("Costo de delivery de la orden", order.OrderShippingInclTax);
                xmlWriter.WriteString("Porcentaje de ganancia del motorizado del delivery", $"{order.ShippingTaxMessengerPercentage}%");
                xmlWriter.WriteString("Ganancia total del motorizado del delivery", order.ShippingTaxMessengerProfitAmount);
                xmlWriter.WriteString("Porcentaje de ganancia de OHD del delivery", $"{order.ShippingTaxAdministrativePercentage}%");
                xmlWriter.WriteString("Ganancia total del motorizado de OHD", order.ShippingTaxAdministrativeProfitAmount);
                xmlWriter.WriteString("Total de la orden", order.OrderTotal);
                xmlWriter.WriteString("Fecha creacion", order.CreatedOnUtc);

                if (_orderSettings.ExportWithProducts)
                {
                    //a vendor should have access only to his products
                    var orderItems = _orderService.GetOrderItems(order.Id, vendorId: _workContext.CurrentVendor?.Id ?? 0);

                    if (orderItems.Any())
                    {
                        xmlWriter.WriteStartElement("OrderItems");
                        foreach (var orderItem in orderItems)
                        {
                            var product = _productService.GetProductById(orderItem.ProductId);

                            xmlWriter.WriteStartElement("OrderItem");
                            xmlWriter.WriteString("Id", orderItem.Id);
                            xmlWriter.WriteString("OrderItemGuid", orderItem.OrderItemGuid);
                            xmlWriter.WriteString("Name", product.Name);
                            xmlWriter.WriteString("Sku", product.Sku);
                            xmlWriter.WriteString("PriceExclTax", orderItem.UnitPriceExclTax);
                            xmlWriter.WriteString("PriceInclTax", orderItem.UnitPriceInclTax);
                            xmlWriter.WriteString("Quantity", orderItem.Quantity);
                            xmlWriter.WriteString("DiscountExclTax", orderItem.DiscountAmountExclTax);
                            xmlWriter.WriteString("DiscountInclTax", orderItem.DiscountAmountInclTax);
                            xmlWriter.WriteString("TotalExclTax", orderItem.PriceExclTax);
                            xmlWriter.WriteString("TotalInclTax", orderItem.PriceInclTax);
                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();
                    }
                }

                //shipments
                var shipments = _shipmentService.GetShipmentsByOrderId(order.Id).OrderBy(x => x.CreatedOnUtc).ToList();
                if (shipments.Any())
                {
                    xmlWriter.WriteStartElement("Shipments");
                    foreach (var shipment in shipments)
                    {
                        xmlWriter.WriteStartElement("Shipment");
                        xmlWriter.WriteElementString("ShipmentId", null, shipment.Id.ToString());
                        xmlWriter.WriteElementString("TrackingNumber", null, shipment.TrackingNumber);
                        xmlWriter.WriteElementString("TotalWeight", null, shipment.TotalWeight?.ToString() ?? string.Empty);
                        xmlWriter.WriteElementString("ShippedDateUtc", null, shipment.ShippedDateUtc.HasValue ? shipment.ShippedDateUtc.ToString() : string.Empty);
                        xmlWriter.WriteElementString("DeliveryDateUtc", null, shipment.DeliveryDateUtc?.ToString() ?? string.Empty);
                        xmlWriter.WriteElementString("CreatedOnUtc", null, shipment.CreatedOnUtc.ToString(CultureInfo.InvariantCulture));
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        ///<inheritdoc/>
        public byte[] ExportVendorOrdersEarningToXlsx(IList<OrderPendingToClosePayment> orders)
        {
            //a vendor should have access only to part of order information
            var ignore = _workContext.CurrentVendor != null;

            orders.OrderBy(ord=>ord.Id);
            orders.Distinct().ToList();
            //property array
            var properties = new[]
            {
                new PropertyByName<OrderPendingToClosePayment>("Numero Orden", p => p.CustomOrderNumber),
                new PropertyByName<OrderPendingToClosePayment>("Id de la orden", p => p.OrderId),
                new PropertyByName<OrderPendingToClosePayment>("Suplidor", p =>
                {
                    Vendor orderStore = _vendorService.GetVendorById(p.VendorId);

                    return orderStore != null ? $"{orderStore.Name} ({orderStore.Email})" : "";
                }),
                 new PropertyByName<OrderPendingToClosePayment>("Estado de pago a Suplidor",p=>PaymentStatusText(p.VendorPaymentStatus)),
                 new PropertyByName<OrderPendingToClosePayment>("Subtotal de la orden sin impuesto", p => p.OrderSubtotalExclTax),
                 new PropertyByName<OrderPendingToClosePayment>("Subtotal de la orden con impuesto", p => p.OrderSubtotalInclTax),
                 new PropertyByName<OrderPendingToClosePayment>("Impuesto", p => p.OrderTax),
                 new PropertyByName<OrderPendingToClosePayment>("Metodo de pago", p => GetPaymentMethod(p.CheckoutAttributeDescription)),
                 new PropertyByName<OrderPendingToClosePayment>("Descuento total de la orden", p => p.OrderDiscount),
                 new PropertyByName<OrderPendingToClosePayment>("% comercio del subtotal de la orden", p => p.OrderTotalVendorProfitAmount),
                 new PropertyByName<OrderPendingToClosePayment>("% administrativo del subtotal de la orden", p => p.OrderTotalAdministrativeProfitAmount),
                new PropertyByName<OrderPendingToClosePayment>("Motorizado",p=>_driverService.GetDriverInfo(p.OrderId).DriverName),
                new PropertyByName<OrderPendingToClosePayment>("Estado de pago a Motorizado",p=>PaymentStatusText(p.DriverPaymentStatus)),
                new PropertyByName<OrderPendingToClosePayment>("Costo de envio",p=>p.OrderShippingInclTax),
                new PropertyByName<OrderPendingToClosePayment>("% para el motorizado del costo de envio",p=>calulateDriverPercentage(p.ShippingTaxMessengerPercentage,p.OrderShippingInclTax)+" ("+ Math.Round(p.ShippingTaxMessengerPercentage,2)+"%)"),
                new PropertyByName<OrderPendingToClosePayment>("% Administrativo del costo de envio",p=>p.ShippingTaxAdministrativePercentage),
                new PropertyByName<OrderPendingToClosePayment>("Total de la orden", p => (p.OrderTotal - p.OrderShippingInclTax)),
                new PropertyByName<OrderPendingToClosePayment>("Fecha creacion", p => p.CreatedOnUtc.ToString())

            };

            return _orderSettings.ExportWithProducts
                ? ExportOrderToXlsxWithProducts(properties, orders)
                : new PropertyManager<OrderPendingToClosePayment>(properties, _catalogSettings).ExportToXlsx(orders);
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

        public static decimal calulateDriverPercentage(decimal shippingTaxMessengerPercentage, decimal orderShippingInclTax)
        {
            shippingTaxMessengerPercentage = Math.Round(shippingTaxMessengerPercentage, 1);
            orderShippingInclTax = Math.Round(orderShippingInclTax, 1);
            decimal result = 0;
            if (orderShippingInclTax > 0)
            {
                result = (decimal)Math.Round((decimal)(shippingTaxMessengerPercentage / 100) * orderShippingInclTax, 2);
            }

            return result;
        }

        public string PaymentStatusText(PaymentStatus status)
        {
            switch (status)
            {
                case PaymentStatus.Paid:
                    return "Pagado";
                case PaymentStatus.Pending:
                    return "Pendiente";
                case PaymentStatus.Refunded:
                    return "Retornado";
                case PaymentStatus.PartiallyRefunded:
                    return "Parcialmente Retornado";
                case PaymentStatus.Authorized:
                    return "Autorizado";

                default:
                    return "Desconocido";            
            }
        }


        ///<inheritdoc/>
        public string ExportVendorOrdersEarningToXml(IList<OrderPendingToClosePayment> orders)
        {
            //a vendor should have access only to part of order information
            var ignore = _workContext.CurrentVendor != null;

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Orders");
            xmlWriter.WriteAttributeString("Version", NopVersion.CurrentVersion);

            foreach (var order in orders)
            {
                xmlWriter.WriteStartElement("Order");

                xmlWriter.WriteString("Numero Orden", order.CustomOrderNumber);
                xmlWriter.WriteString("Id de la orden", order.OrderId);
                xmlWriter.WriteString("Id del suplidor", order.VendorId);
                xmlWriter.WriteString("Descuento de la orden", order.OrderDiscount);
                xmlWriter.WriteString("Subtotal de la orden sin inpuesto", order.OrderSubtotalExclTax);
                xmlWriter.WriteString("Ganancia de OHD del total de la orden", order.OrderTotalAdministrativeProfitAmount);
                xmlWriter.WriteString("Ganancia total del suplidor del total de la orden", order.OrderTotalVendorProfitAmount);
                xmlWriter.WriteString("Total de la orden", order.OrderTotal - order.OrderShippingInclTax);
                xmlWriter.WriteString("Fecha creacion", order.CreatedOnUtc);

                if (_orderSettings.ExportWithProducts)
                {
                    //a vendor should have access only to his products
                    var orderItems = _orderService.GetOrderItems(order.Id, vendorId: _workContext.CurrentVendor?.Id ?? 0);

                    if (orderItems.Any())
                    {
                        xmlWriter.WriteStartElement("OrderItems");
                        foreach (var orderItem in orderItems)
                        {
                            var product = _productService.GetProductById(orderItem.ProductId);

                            xmlWriter.WriteStartElement("OrderItem");
                            xmlWriter.WriteString("Id", orderItem.Id);
                            xmlWriter.WriteString("OrderItemGuid", orderItem.OrderItemGuid);
                            xmlWriter.WriteString("Name", product.Name);
                            xmlWriter.WriteString("Sku", product.Sku);
                            xmlWriter.WriteString("PriceExclTax", orderItem.UnitPriceExclTax);
                            xmlWriter.WriteString("PriceInclTax", orderItem.UnitPriceInclTax);
                            xmlWriter.WriteString("Quantity", orderItem.Quantity);
                            xmlWriter.WriteString("DiscountExclTax", orderItem.DiscountAmountExclTax);
                            xmlWriter.WriteString("DiscountInclTax", orderItem.DiscountAmountInclTax);
                            xmlWriter.WriteString("TotalExclTax", orderItem.PriceExclTax);
                            xmlWriter.WriteString("TotalInclTax", orderItem.PriceInclTax);
                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();
                    }
                }

                //shipments
                var shipments = _shipmentService.GetShipmentsByOrderId(order.Id).OrderBy(x => x.CreatedOnUtc).ToList();
                if (shipments.Any())
                {
                    xmlWriter.WriteStartElement("Shipments");
                    foreach (var shipment in shipments)
                    {
                        xmlWriter.WriteStartElement("Shipment");
                        xmlWriter.WriteElementString("ShipmentId", null, shipment.Id.ToString());
                        xmlWriter.WriteElementString("TrackingNumber", null, shipment.TrackingNumber);
                        xmlWriter.WriteElementString("TotalWeight", null, shipment.TotalWeight?.ToString() ?? string.Empty);
                        xmlWriter.WriteElementString("ShippedDateUtc", null, shipment.ShippedDateUtc.HasValue ? shipment.ShippedDateUtc.ToString() : string.Empty);
                        xmlWriter.WriteElementString("DeliveryDateUtc", null, shipment.DeliveryDateUtc?.ToString() ?? string.Empty);
                        xmlWriter.WriteElementString("CreatedOnUtc", null, shipment.CreatedOnUtc.ToString(CultureInfo.InvariantCulture));
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }

        #endregion
    }
}
