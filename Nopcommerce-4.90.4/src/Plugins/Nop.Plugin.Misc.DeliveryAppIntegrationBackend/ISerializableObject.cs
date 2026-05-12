using System;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO
{
    public interface ISerializableObject
    {
        string GetPrimaryPropertyName();
        Type GetPrimaryPropertyType();
    }
}