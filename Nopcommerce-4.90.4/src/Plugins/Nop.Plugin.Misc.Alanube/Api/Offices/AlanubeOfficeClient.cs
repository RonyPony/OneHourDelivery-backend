using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Offices;

public sealed class AlanubeOfficeClient : IAlanubeOfficeClient
{
    private readonly IAlanubeClient _alanubeClient;

    public AlanubeOfficeClient(IAlanubeClient alanubeClient)
    {
        _alanubeClient = alanubeClient;
    }

    public async Task<AlanubeApiResponse<IReadOnlyList<OfficeResponseDto>>> GetOfficesAsync(string companyId, CancellationToken cancellationToken = default)
    {
        EnsureId(companyId, nameof(companyId));
        return await _alanubeClient.GetAsync<IReadOnlyList<OfficeResponseDto>>($"companies/{Uri.EscapeDataString(companyId)}/offices", cancellationToken: cancellationToken);
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
}
