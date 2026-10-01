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
            var encf = $"{_settings.FiscalReceiptPrefix}{currentNumber.ToString($"D{_settings.FiscalReceiptNumberLength}")}";
            await _logger.InformationAsync($"Alanube direct invoice request building started for order {order.Id}. eNCF: {encf}.");
            var request = await BuildRequestAsync(order, encf, cancellationToken);
            var requestJson = JsonSerializer.Serialize(request);
            var document = CreateDocument(order, encf);
            await _documentService.InsertAsync(document);

            var response = await _invoiceClient.CreateDirectFiscalInvoiceAsync(request, cancellationToken);
            document.DocumentStatus = response.IsSuccessStatusCode ? ElectronicDocumentStatus.Processing : ElectronicDocumentStatus.Failed;
            document.ErrorMessage = response.IsSuccessStatusCode ? null : response.Error?.Message;
            document.AlanubeId = ExtractString(response.Body?.Data, "id");
            document.Cufe = ExtractString(response.Body?.Data, "cufe");
            await _documentService.UpdateAsync(document);

            await _documentLogService.InsertAsync(new AlanubeDocumentLog
            {
                AlanubeDocumentId = document.Id,
                Operation = "CreateDirectFiscalInvoice",
                Endpoint = "/fiscal-invoices",
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
                await AddOrderNoteAsync(order.Id, $"Alanube: comprobante enviado correctamente. Estado: {document.DocumentStatus}. eNCF: {encf}. HTTP {response.StatusCode} {response.ReasonPhrase}.");
                await _logger.InformationAsync($"Alanube direct invoice emitted for order {order.Id}. eNCF: {encf}. HTTP {response.StatusCode} {response.ReasonPhrase}.");
            }
            else
            {
                await AddOrderNoteAsync(order.Id, $"Alanube: comprobante rechazado o no emitido. Estado: {document.DocumentStatus}. eNCF: {encf}. HTTP {response.StatusCode} {response.ReasonPhrase}. Error: {Truncate(response.Error?.Message ?? response.RawResponse, 500)}");
                await _logger.ErrorAsync($"Alanube direct invoice failed for order {order.Id}. eNCF: {encf}. HTTP {response.StatusCode} {response.ReasonPhrase}. Body: {response.RawResponse}");
            }
        }
        finally
        {
            _sequenceSemaphore.Release();
        }
    }

    private async Task<DirectFiscalInvoiceRequest> BuildRequestAsync(Order order, string encf, CancellationToken cancellationToken)
    {
        var billingAddress = await _addressService.GetAddressByIdAsync(order.BillingAddressId);
        var items = await BuildItemsAsync(order, cancellationToken);
        var taxableAmount = Round(order.OrderSubtotalExclTax - order.OrderSubTotalDiscountExclTax);
        var tax = Round(order.OrderTax);
        var total = Round(order.OrderTotal);

        return new DirectFiscalInvoiceRequest
        {
            IdDoc = new DirectFiscalInvoiceIdDoc
            {
                Encf = encf,
                SequenceDueDate = (_settings.SequenceDueDateUtc ?? DateTime.UtcNow.Date.AddYears(1)).ToString("yyyy-MM-dd")
            },
            Sender = new DirectFiscalInvoiceSender
            {
                Rnc = _settings.SenderRnc,
                CompanyName = _settings.SenderCompanyName,
                Tradename = string.IsNullOrWhiteSpace(_settings.SenderTradeName) ? _settings.SenderCompanyName : _settings.SenderTradeName,
                Address = _settings.SenderAddress,
                Province = _settings.SenderProvince,
                Municipality = _settings.SenderMunicipality,
                StampDate = DateTime.UtcNow.ToString("yyyy-MM-dd")
            },
            Buyer = new DirectFiscalInvoiceBuyer
            {
                Rnc = string.IsNullOrWhiteSpace(order.VatNumber) ? _settings.SenderRnc : order.VatNumber,
                CompanyName = GetBuyerName(billingAddress)
            },
            Totals = new DirectFiscalInvoiceTotals
            {
                TotalTaxedAmount = taxableAmount,
                I1AmountTaxed = taxableAmount,
                ItbisS1 = taxableAmount > 0 ? Round(tax / taxableAmount * 100) : 0,
                ItbisTotal = tax,
                Itbis1Total = tax,
                TotalAmount = total
            },
            ItemDetails = items
        };
    }

    private async Task<IList<DirectFiscalInvoiceItem>> BuildItemsAsync(Order order, CancellationToken cancellationToken)
    {
        var orderItems = await _orderService.GetOrderItemsAsync(order.Id);
        var result = new List<DirectFiscalInvoiceItem>();
        var lineNumber = 1;

        foreach (var orderItem in orderItems)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
            var name = string.IsNullOrWhiteSpace(product?.Name) ? $"Producto {orderItem.ProductId}" : product.Name;
            result.Add(new DirectFiscalInvoiceItem
            {
                LineNumber = lineNumber++,
                ItemName = Truncate(name, 80),
                ItemDescription = Truncate(name, 250),
                QuantityItem = orderItem.Quantity,
                UnitPriceItem = Round(orderItem.UnitPriceExclTax),
                ItemAmount = Round(orderItem.PriceExclTax - orderItem.DiscountAmountExclTax)
            });
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

    private static string ExtractString(IDictionary<string, JsonElement> data, string name)
    {
        if (data == null || !data.TryGetValue(name, out var value))
            return null;

        return value.ValueKind == JsonValueKind.String ? value.GetString() : value.GetRawText();
    }

    private static decimal Round(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;
}
