using System.Text.Json;
using Nop.Core.Domain.Common;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public sealed class AlanubeAddressMapper : IAlanubeAddressMapper
{
    public AlanubeMappingResult<JsonElement> Map(Address address)
    {
        var failures = new List<AlanubeMappingFailure>();
        if (address == null)
            failures.Add(new() { Code = "ADDRESS_REQUIRED", Field = "receiver", Message = "A billing address is required." });
        failures.Add(new() { Code = "ADDRESS_CONTRACT_UNRESOLVED", Field = "receiver", Message = "The Alanube receiver address schema is not defined in the validated contract." });
        return new() { Value = JsonSerializer.SerializeToElement(new Dictionary<string, object>()), Failures = failures };
    }
}
