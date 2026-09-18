using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Offices;

public interface IAlanubeOfficeClient
{
    Task<AlanubeApiResponse<IReadOnlyList<OfficeResponseDto>>> GetOfficesAsync(string companyId, CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<OfficeResponseDto>> GetMainOfficeAsync(string companyId, CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<OfficeResponseDto>> CreateOfficeAsync(string companyId, OfficeRequestDto request, CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<OfficeResponseDto>> UpdateOfficeAsync(string companyId, string officeId, OfficeRequestDto request, CancellationToken cancellationToken = default);
}
