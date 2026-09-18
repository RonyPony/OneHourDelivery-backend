using System.Text.Json;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public interface IAlanubeOrderItemMapper
{
    Task<AlanubeMappingResult<JsonElement>> MapAsync(OrderItem orderItem, int lineNumber, CancellationToken cancellationToken = default);
}
