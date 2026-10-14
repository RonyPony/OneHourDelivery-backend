using System.Security.Cryptography;
using System.Text.Json;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.Alanube.Api.Invoices;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Plugin.Misc.Alanube.Domain;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Orders;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeDirectInvoiceService
{
    private static readonly SemaphoreSlim _sequenceSemaphore = new(1, 1);

    private readonly IAddressService _addressService;
    private readonly IAlanubeDocumentLogService _documentLogService;
    private readonly IAlanubeDocumentService _documentService;
    private readonly IAlanubeInvoiceClient _invoiceClient;
    private readonly ILogger _logger;
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ISettingService _settingService;
    private readonly AlanubeSettings _settings;

    public AlanubeDirectInvoiceService(
        IAddressService addressService,
        IAlanubeDocumentLogService documentLogService,
        IAlanubeDocumentService documentService,
        IAlanubeInvoiceClient invoiceClient,
        ILogger logger,
        IOrderService orderService,
        IProductService productService,
        ISettingService settingService,
        AlanubeSettings settings)
    {
        _addressService = addressService;
        _documentLogService = documentLogService;
        _documentService = documentService;
        _invoiceClient = invoiceClient;
        _logger = logger;
        _orderService = orderService;
        _productService = productService;
        _settingService = settingService;
        _settings = settings;
    }

    public async Task EmitAsync(Order order, CancellationToken cancellationToken = default)
    {
        if (order == null)
        {
            await _logger.WarningAsync("Alanube direct invoice skipped because the order is null.");
            return;
        }

        await _logger.InformationAsync($"Alanube direct invoice evaluation started for order {order.Id}. Enabled: {_settings.Enabled}. Emission flow: {_settings.EmissionFlow}. Trigger: {_settings.InvoiceTrigger}.");

        if (!_settings.Enabled)
        {
            await _logger.WarningAsync($"Alanube direct invoice skipped for order {order.Id} because the plugin is disabled.");
            return;
        }

        if (_settings.EmissionFlow != AlanubeEmissionFlow.DirectApi)
        {
            await _logger.InformationAsync($"Alanube direct invoice skipped for order {order.Id} because the configured emission flow is {_settings.EmissionFlow}.");
            return;
        }

        if (await _documentService.ExistsAsync(order.Id, ElectronicDocumentType.Invoice, 1))
        {
            await _logger.InformationAsync($"Alanube direct invoice skipped for order {order.Id} because an Alanube invoice document already exists.");
            return;
        }

        await _sequenceSemaphore.WaitAsync(cancellationToken);
        try
        {
            var currentNumber = _settings.NextFiscalNumber;
            var numeration = currentNumber.ToString("D10");
            await _logger.InformationAsync($"Alanube PAN invoice request building started for order {order.Id}. Numeration: {numeration}.");
            var request = await BuildRequestAsync(order, numeration, cancellationToken);
            var requestJson = JsonSerializer.Serialize(request);
            var document = CreateDocument(order, numeration);
            await _documentService.InsertAsync(document);

            var response = await _invoiceClient.CreateInvoiceAsync(request, cancellationToken);
            document.DocumentStatus = response.IsSuccessStatusCode ? ElectronicDocumentStatus.Processing : ElectronicDocumentStatus.Failed;
            document.ErrorMessage = response.IsSuccessStatusCode ? null : response.Error?.Message;
            document.AlanubeId = ExtractString(response.Body?.Data, "id");
            document.Cufe = ExtractString(response.Body?.Data, "cufe");
            await _documentService.UpdateAsync(document);

            await _documentLogService.InsertAsync(new AlanubeDocumentLog
            {
                AlanubeDocumentId = document.Id,
                Operation = "CreateInvoice",
                Endpoint = "/invoices",
                HttpMethod = "POST",
                RequestJson = requestJson,
                ResponseJson = response.RawResponse,
                HttpStatusCode = response.StatusCode,
                Success = response.IsSuccessStatusCode,
                ErrorMessage = response.Error?.Message
            });

            if (response.IsSuccessStatusCode)
            {
                _settings.NextFiscalNumber = currentNumber + 1;
                await _settingService.SaveSettingAsync(_settings);
                await AddOrderNoteAsync(order.Id, $"Alanube PAN: factura enviada correctamente. Estado: {document.DocumentStatus}. Numeración: {numeration}. HTTP {response.StatusCode} {response.ReasonPhrase}.");
                await _logger.InformationAsync($"Alanube PAN invoice emitted for order {order.Id}. Numeration: {numeration}. HTTP {response.StatusCode} {response.ReasonPhrase}.");
            }
            else
            {
                await AddOrderNoteAsync(order.Id, $"Alanube PAN: factura rechazada o no emitida. Estado: {document.DocumentStatus}. Numeración: {numeration}. HTTP {response.StatusCode} {response.ReasonPhrase}. Error: {Truncate(response.Error?.Message ?? response.RawResponse, 500)}");
                await _logger.ErrorAsync($"Alanube PAN invoice failed for order {order.Id}. Numeration: {numeration}. HTTP {response.StatusCode} {response.ReasonPhrase}. Body: {response.RawResponse}");
            }
        }
        finally
        {
            _sequenceSemaphore.Release();
        }
    }

    private async Task<CreateInvoiceRequest> BuildRequestAsync(Order order, string numeration, CancellationToken cancellationToken)
    {
        var billingAddress = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
        var items = await BuildItemsAsync(order, cancellationToken);
        var total = Round(order.OrderTotal);

        var information = new
        {
            issueType = _settings.IssueType,
            documentType = _settings.DocumentType,
            numeration,
            billingPoint = _settings.BillingPoint,
            securityCode = GenerateSecurityCode(),
            cafe = new
            {
                format = _settings.CafeFormat,
                delivery = _settings.CafeDelivery
            },
            nature = _settings.Nature,
            operationType = _settings.OperationType,
            information = string.Empty,
            destination = _settings.Destination,
            receiverContainer = _settings.ReceiverContainer,
            saleType = _settings.SaleType,
            issueDate = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-5)).ToString("yyyy-MM-ddTHH:mm:sszzz")
        };

        var receiverRuc = string.IsNullOrWhiteSpace(order.VatNumber) ? "000-00-00000" : order.VatNumber.Trim();
        var receiver = new
        {
            type = "01",
            ruc = new
            {
                type = 2,
                ruc = receiverRuc
            },
            name = GetBuyerName(billingAddress),
            address = Truncate(GetReceiverAddress(billingAddress), 250),
            location = new
            {
                code = "8-8-8",
                correction = string.IsNullOrWhiteSpace(billingAddress?.City) ? "Pueblo Nuevo" : billingAddress.City,
                district = "Panama",
                province = "Panama"
            },
            telephones = string.IsNullOrWhiteSpace(billingAddress?.PhoneNumber) ? Array.Empty<string>() : new[] { billingAddress.PhoneNumber },
            emails = string.IsNullOrWhiteSpace(billingAddress?.Email) ? Array.Empty<string>() : new[] { billingAddress.Email },
            country = "PA"
        };

        var totals = new
        {
            change = 0,
            transport = Round(order.OrderShippingExclTax),
            insurance = 0,
            paymentTime = 1,
            paymentMethods = new[]
            {
                new
                {
                    type = "01",
                    amount = total
                }
            },
            otherExpenses = 0
        };

        return new CreateInvoiceRequest
        {
            IdOffice = string.IsNullOrWhiteSpace(_settings.OfficeId) ? null : _settings.OfficeId,
            Information = JsonSerializer.SerializeToElement(information),
            Receiver = JsonSerializer.SerializeToElement(receiver),
            Items = JsonSerializer.SerializeToElement(items),
            Totals = JsonSerializer.SerializeToElement(totals)
        };
    }

    private async Task<IList<object>> BuildItemsAsync(Order order, CancellationToken cancellationToken)
    {
        var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
        var result = new List<object>();
        var lineNumber = 1;

        foreach (var orderItem in orderItems)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
            var name = string.IsNullOrWhiteSpace(product?.Name) ? $"Producto {orderItem.ProductId}" : product.Name;
            var lineAmount = Round(orderItem.PriceExclTax - orderItem.DiscountAmountExclTax);
            var unitPrice = Round(orderItem.UnitPriceExclTax);
            var itemCode = string.IsNullOrWhiteSpace(product?.Sku) ? orderItem.ProductId.ToString() : product.Sku;
            result.Add(new
            {
                number = lineNumber.ToString("D4"),
                description = Truncate(name, 500),
                code = Truncate(itemCode, 20),
                unit = "und",
                quantity = orderItem.Quantity,
                cpbs = new
                {
                    code = "8210",
                    unit = "und"
                },
                information = Truncate(orderItem.AttributeDescription, 5000),
                prices = new
                {
                    transfer = unitPrice > 0 ? unitPrice : lineAmount,
                    discount = Round(orderItem.DiscountAmountExclTax),
                    insurance = 0
                },
                itbms = new
                {
                    rate = "01"
                },
                isc = new
                {
                    rate = 0,
                    amount = 0
                }
            });
            lineNumber++;
        }

        return result;
    }

    private AlanubeDocument CreateDocument(Order order, string encf) => new()
    {
        OrderId = order.Id,
        CustomerId = order.CustomerId,
        StoreId = order.StoreId,
        CompanyId = _settings.CompanyId,
        OfficeId = _settings.OfficeId,
        DocumentType = ElectronicDocumentType.Invoice,
        DocumentStatus = ElectronicDocumentStatus.Pending,
        Version = 1,
        DocumentNumber = encf,
        CurrencyCode = order.CustomerCurrencyCode,
        Subtotal = order.OrderSubtotalExclTax,
        Discount = order.OrderDiscount + order.OrderSubTotalDiscountExclTax,
        Tax = order.OrderTax,
        Total = order.OrderTotal,
        IssueDateUtc = DateTime.UtcNow
    };

    private async Task AddOrderNoteAsync(int orderId, string note)
    {
        if (!_settings.AddAlanubeStatusOrderNotes)
            return;

        await _orderService.InsertOrderNoteAsync(new OrderNote
        {
            OrderId = orderId,
            Note = note,
            DisplayToCustomer = false,
            CreatedOnUtc = DateTime.UtcNow
        });
    }

    private static string GetBuyerName(Address address)
    {
        if (!string.IsNullOrWhiteSpace(address?.Company))
            return address.Company;

        var name = $"{address?.FirstName} {address?.LastName}".Trim();
        return string.IsNullOrWhiteSpace(name) ? "Cliente" : name;
    }

    private static string GetReceiverAddress(Address address)
    {
        var value = address?.Address1;
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        value = address?.City;
        return string.IsNullOrWhiteSpace(value) ? "Panama" : value;
    }

    private static string GenerateSecurityCode()
    {
        var value = RandomNumberGenerator.GetInt32(0, 1_000_000_000);
        return value.ToString("D9");
    }

    private static string ExtractString(IDictionary<string, JsonElement> data, string name)
    {
        if (data == null || !data.TryGetValue(name, out var value))
            return null;

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.GetRawText();
    }

    private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;
}
