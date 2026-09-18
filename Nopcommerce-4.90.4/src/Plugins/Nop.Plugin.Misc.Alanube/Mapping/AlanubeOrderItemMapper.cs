using System.Text.Json;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Misc.Alanube.Services;
using Nop.Services.Catalog;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public sealed class AlanubeOrderItemMapper : IAlanubeOrderItemMapper
{
    private readonly IProductService _productService;
    private readonly IAlanubeProductMappingService _productMappingService;

    public AlanubeOrderItemMapper(IProductService productService, IAlanubeProductMappingService productMappingService)
    {
        _productService = productService;
        _productMappingService = productMappingService;
    }

    public async Task<AlanubeMappingResult<JsonElement>> MapAsync(OrderItem orderItem, int lineNumber, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(orderItem);
        cancellationToken.ThrowIfCancellationRequested();
        var failures = new List<AlanubeMappingFailure>();
        var product = await _productService.GetProductByIdAsync(orderItem.ProductId);
        if (product == null)
        {
            failures.Add(new() { Code = "PRODUCT_NOT_FOUND", Field = "items[].description", Message = $"Product {orderItem.ProductId} was not found." });
            return new() { Value = JsonSerializer.SerializeToElement(new Dictionary<string, object>()), Failures = failures };
        }

        var mapping = await _productMappingService.GetByProductIdAsync(orderItem.ProductId);
        if (mapping == null)
            failures.Add(new() { Code = "PRODUCT_MAPPING_REQUIRED", Field = "items[]", Message = $"Product {orderItem.ProductId} has no Alanube fiscal mapping." });
        else
        {
            if (string.IsNullOrWhiteSpace(mapping.GoodsServiceCode))
                failures.Add(new() { Code = "CPBS_REQUIRED", Field = "items[].cpbs", Message = $"Product {orderItem.ProductId} has no CPBS code." });
            if (string.IsNullOrWhiteSpace(mapping.MeasurementUnitCode))
                failures.Add(new() { Code = "MEASUREMENT_UNIT_REQUIRED", Field = "items[].unit", Message = $"Product {orderItem.ProductId} has no measurement unit code." });
        }

        var description = string.IsNullOrWhiteSpace(mapping?.DescriptionOverride) ? product.Name : mapping.DescriptionOverride;
        if (string.IsNullOrWhiteSpace(description) || description.Length is < 2 or > 500)
            failures.Add(new() { Code = "DESCRIPTION_INVALID", Field = "items[].description", Message = "The item description must contain between 2 and 500 characters." });
        if (lineNumber is < 1 or > 9999)
            failures.Add(new() { Code = "LINE_NUMBER_INVALID", Field = "items[].number", Message = "The item line number must be between 1 and 9999." });
        if (orderItem.Quantity <= 0)
            failures.Add(new() { Code = "QUANTITY_INVALID", Field = "items[].quantity", Message = "The item quantity must be greater than zero." });

        var item = new Dictionary<string, object>
        {
            ["number"] = lineNumber.ToString("D4"),
            ["description"] = description,
            ["quantity"] = orderItem.Quantity
        };
        if (!string.IsNullOrWhiteSpace(product.Sku) && product.Sku.Length <= 20)
            item["code"] = product.Sku;
        if (!string.IsNullOrWhiteSpace(mapping?.MeasurementUnitCode) && mapping.MeasurementUnitCode.Length <= 20)
            item["unit"] = mapping.MeasurementUnitCode;

        if (!string.IsNullOrWhiteSpace(mapping?.GoodsServiceCode))
            failures.Add(new() { Code = "CPBS_CONTRACT_UNRESOLVED", Field = "items[].cpbs", Message = "The internal JSON structure of the CPBS group is not defined in the validated contract." });

        return new() { Value = JsonSerializer.SerializeToElement(item), Failures = failures };
    }
}
