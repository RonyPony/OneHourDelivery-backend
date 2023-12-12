using System;

namespace Nop.Plugin.DdpApi.DTO
{
    public interface ISerializableObject
    {
        string GetPrimaryPropertyName();
        Type GetPrimaryPropertyType();
    }
}