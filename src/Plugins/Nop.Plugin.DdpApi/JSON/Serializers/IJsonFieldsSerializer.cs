namespace Nop.Plugin.DdpApi.JSON.Serializers
{
    using Nop.Plugin.DdpApi.DTO;

    public interface IJsonFieldsSerializer
    {
        string Serialize(ISerializableObject objectToSerialize, string fields);
    }
}
