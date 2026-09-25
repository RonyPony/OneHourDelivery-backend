using AlanubeApiResponse = Nop.Plugin.Misc.Alanube.Api.AlanubeApiResponse<System.Collections.Generic.IReadOnlyList<Nop.Plugin.Misc.Alanube.Api.Companies.CompanyResponseDto>>;

namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Provides read-only company operations against Alanube.
/// </summary>
public interface IAlanubeCompanyClient
{
    /// <summary>
    /// Gets companies available to the configured Alanube token.
    /// </summary>
    /// <param name="active">Optional filter for active companies.</param>
    /// <param name="type">Optional filter for main or associated companies.</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    Task<AlanubeApiResponse> GetCompaniesAsync(
        bool? active = null,
        AlanubeCompanyType? type = null,
        CancellationToken cancellationToken = default);

    Task<AlanubeConnectionTestResult> TestConnectionAsync(
        string correlationId,
        CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<CompanyResponseDto>> CreateCompanyAsync(
        CreateCompanyRequestDto request,
        CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<CompanyResponseDto>> GetCompanyAsync(string companyId, CancellationToken cancellationToken = default);

    Task<AlanubeApiResponse<CompanyResponseDto>> UpdateCompanyAsync(
        string companyId,
        UpdateCompanyRequestDto request,
        CancellationToken cancellationToken = default);
}
