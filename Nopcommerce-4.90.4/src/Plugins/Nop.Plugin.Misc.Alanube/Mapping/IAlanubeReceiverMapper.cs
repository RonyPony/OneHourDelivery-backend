using System.Text.Json;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;

namespace Nop.Plugin.Misc.Alanube.Mapping;

public interface IAlanubeReceiverMapper
{
    AlanubeMappingResult<JsonElement> Map(Customer customer, Address billingAddress);
}
