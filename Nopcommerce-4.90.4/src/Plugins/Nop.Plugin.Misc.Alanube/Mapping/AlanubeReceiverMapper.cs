using System.Text.Json;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public sealed class AlanubeReceiverMapper : IAlanubeReceiverMapper
{
    private readonly IAlanubeAddressMapper _addressMapper;

    public AlanubeReceiverMapper(IAlanubeAddressMapper addressMapper) => _addressMapper = addressMapper;

    public AlanubeMappingResult<JsonElement> Map(Customer customer, Address billingAddress)
    {
        var failures = _addressMapper.Map(billingAddress).Failures.ToList();
        if (customer == null)
            failures.Add(new() { Code = "CUSTOMER_REQUIRED", Field = "receiver", Message = "A customer is required." });
        failures.Add(new() { Code = "RECEIVER_CONTRACT_UNRESOLVED", Field = "receiver", Message = "The Alanube receiver field schema and required fiscal identity data are not defined in the validated contract." });
        return new() { Value = JsonSerializer.SerializeToElement(new Dictionary<string, object>()), Failures = failures };
    }
}
