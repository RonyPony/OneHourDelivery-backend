using System.Text.Json;
using Nop.Plugin.Misc.Alanube.Api;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Api.Offices;

public sealed class AlanubeOfficeClient : IAlanubeOfficeClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IAlanubeClient _alanubeClient;
    private readonly ILogger _logger;

    public AlanubeOfficeClient(IAlanubeClient alanubeClient, ILogger logger)
    {
        _alanubeClient = alanubeClient;
        _logger = logger;
    }

    public async Task<AlanubeApiResponse<IReadOnlyList<OfficeResponseDto>>> GetOfficesAsync(string companyId, CancellationToken cancellationToken = default)
    {
        EnsureId(companyId, nameof(companyId));
        var endpoint = $"companies/{Uri.EscapeDataString(companyId)}/offices";
        var response = await _alanubeClient.GetAsync<JsonElement>(endpoint, cancellationToken: cancellationToken);
        var offices = response.IsSuccessStatusCode ? DeserializeOffices(response.Body) : Array.Empty<OfficeResponseDto>();

        if (response.IsSuccessStatusCode)
            await _logger.InformationAsync($"Alanube API GET /offices -> HTTP {response.StatusCode} {response.ReasonPhrase}. Offices loaded: {offices.Count}.");

        return new AlanubeApiResponse<IReadOnlyList<OfficeResponseDto>>
        {
            StatusCode = response.StatusCode,
            ReasonPhrase = response.ReasonPhrase,
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            Body = offices,
            Error = response.Error,
            RawResponse = response.RawResponse
        };
    }

    public async Task<AlanubeApiResponse<OfficeResponseDto>> GetMainOfficeAsync(string companyId, CancellationToken cancellationToken = default)
    {
        EnsureId(companyId, nameof(companyId));
        return await _alanubeClient.GetAsync<OfficeResponseDto>($"companies/{Uri.EscapeDataString(companyId)}/office", cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<OfficeResponseDto>> CreateOfficeAsync(string companyId, OfficeRequestDto request, CancellationToken cancellationToken = default)
    {
        EnsureId(companyId, nameof(companyId));
        return await _alanubeClient.PostAsync<OfficeRequestDto, OfficeResponseDto>($"companies/{Uri.EscapeDataString(companyId)}/offices", request, cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<OfficeResponseDto>> UpdateOfficeAsync(string companyId, string officeId, OfficeRequestDto request, CancellationToken cancellationToken = default)
    {
        EnsureId(companyId, nameof(companyId));
        EnsureId(officeId, nameof(officeId));
        return await _alanubeClient.PatchAsync<OfficeRequestDto, OfficeResponseDto>($"companies/{Uri.EscapeDataString(companyId)}/offices/{Uri.EscapeDataString(officeId)}", request, cancellationToken: cancellationToken);
    }

    private static void EnsureId(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("An Alanube identifier is required.", name);
    }

    private static IReadOnlyList<OfficeResponseDto> DeserializeOffices(JsonElement body)
    {
        if (body.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
            return Array.Empty<OfficeResponseDto>();

        if (body.ValueKind == JsonValueKind.Array)
            return JsonSerializer.Deserialize<IReadOnlyList<OfficeResponseDto>>(body.GetRawText(), _jsonSerializerOptions) ?? Array.Empty<OfficeResponseDto>();

        if (body.ValueKind == JsonValueKind.Object)
        {
            foreach (var wrapperName in new[] { "data", "items", "results", "offices" })
            {
                if (!body.TryGetProperty(wrapperName, out var wrapper))
                    continue;

                if (wrapper.ValueKind == JsonValueKind.Array)
                    return JsonSerializer.Deserialize<IReadOnlyList<OfficeResponseDto>>(wrapper.GetRawText(), _jsonSerializerOptions) ?? Array.Empty<OfficeResponseDto>();

                if (wrapper.ValueKind == JsonValueKind.Object)
                {
                    var wrappedOffice = JsonSerializer.Deserialize<OfficeResponseDto>(wrapper.GetRawText(), _jsonSerializerOptions);
                    return wrappedOffice is null ? Array.Empty<OfficeResponseDto>() : new[] { wrappedOffice };
                }
            }

            var office = JsonSerializer.Deserialize<OfficeResponseDto>(body.GetRawText(), _jsonSerializerOptions);
            return office is null ? Array.Empty<OfficeResponseDto>() : new[] { office };
        }

        return Array.Empty<OfficeResponseDto>();
    }
}
