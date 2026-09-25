using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nop.Plugin.Misc.Alanube.Api.Offices;

public sealed class OfficeResponseDto
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }
    public string Coordinates { get; set; }
    public string Address { get; set; }
    public string Telephone { get; set; }
    public string Location { get; set; }

    public string Name { get; set; }
    public string Province { get; set; }
    public string Municipality { get; set; }

    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalData { get; set; }
}
