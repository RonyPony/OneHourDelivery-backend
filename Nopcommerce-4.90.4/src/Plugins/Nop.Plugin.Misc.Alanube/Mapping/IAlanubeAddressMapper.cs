using System.Text.Json;
using Nop.Core.Domain.Common;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public interface IAlanubeAddressMapper
{
    AlanubeMappingResult<JsonElement> Map(Address address);
}
