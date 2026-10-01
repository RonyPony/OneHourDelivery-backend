using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Services.Events;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeOrderEventConsumer :
    IConsumer<OrderPlacedEvent>,
    IConsumer<OrderPaidEvent>,
    IConsumer<OrderStatusChangedEvent>
{
    private readonly AlanubeDirectInvoiceService _directInvoiceService;
    private readonly ILogger _logger;
    private readonly AlanubeSettings _settings;

    public AlanubeOrderEventConsumer(AlanubeDirectInvoiceService directInvoiceService, ILogger logger, AlanubeSettings settings)
    {
        _directInvoiceService = directInvoiceService;
        _logger = logger;
        _settings = settings;
    }

    public async Task HandleEventAsync(OrderPlacedEvent eventMessage)
    {
        await _logger.InformationAsync($"Alanube order event received: OrderPlaced. OrderId: {eventMessage.Order?.Id}. Configured trigger: {_settings.InvoiceTrigger}. Emission flow: {_settings.EmissionFlow}.");

        if (_settings.InvoiceTrigger == InvoiceTrigger.OrderPlaced)
            await EmitAsync(eventMessage.Order);
    }

    public async Task HandleEventAsync(OrderPaidEvent eventMessage)
    {
        await _logger.InformationAsync($"Alanube order event received: OrderPaid. OrderId: {eventMessage.Order?.Id}. Configured trigger: {_settings.InvoiceTrigger}. Emission flow: {_settings.EmissionFlow}.");

        if (_settings.InvoiceTrigger == InvoiceTrigger.PaymentPaid)
            await EmitAsync(eventMessage.Order);
    }

    public async Task HandleEventAsync(OrderStatusChangedEvent eventMessage)
    {
        await _logger.InformationAsync($"Alanube order event received: OrderStatusChanged. OrderId: {eventMessage.Order?.Id}. Previous status: {eventMessage.PreviousOrderStatus}. Current status: {eventMessage.Order?.OrderStatus}. Configured trigger: {_settings.InvoiceTrigger}. Emission flow: {_settings.EmissionFlow}.");

        if (_settings.InvoiceTrigger == InvoiceTrigger.Processing && eventMessage.Order.OrderStatus == OrderStatus.Processing)
            await EmitAsync(eventMessage.Order);

        if (_settings.InvoiceTrigger == InvoiceTrigger.Complete && eventMessage.Order.OrderStatus == OrderStatus.Complete)
            await EmitAsync(eventMessage.Order);
    }

    private async Task EmitAsync(Order order)
    {
        try
        {
            await _directInvoiceService.EmitAsync(order);
        }
        catch (Exception exception)
        {
            await _logger.ErrorAsync($"Alanube direct invoice emission failed for order {order?.Id}.", exception);
        }
    }
}
