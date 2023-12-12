using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.DTO;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.JSON.Serializers
{
    public interface IJsonFieldsSerializer
    {
        string Serialize(ISerializableObject objectToSerialize, string fields);
    }
}
