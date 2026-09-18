using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Provides read-only company operations against Alanube.
/// </summary>
public sealed class AlanubeCompanyClient : IAlanubeCompanyClient
{
    private readonly IAlanubeClient _alanubeClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeCompanyClient"/> class.
    /// </summary>
    /// <param name="alanubeClient">Alanube transport client.</param>
    public AlanubeCompanyClient(IAlanubeClient alanubeClient)
    {
        _alanubeClient = alanubeClient;
    }

    /// <inheritdoc />
    public async Task<AlanubeApiResponse<IReadOnlyList<CompanyResponseDto>>> GetCompaniesAsync(
        bool? active = null,
        AlanubeCompanyType? type = null,
        CancellationToken cancellationToken = default)
    {
        var queryParameters = new List<AlanubeQueryParameter>();

        if (active.HasValue)
            queryParameters.Add(new AlanubeQueryParameter("active", active.Value.ToString().ToLowerInvariant()));

        if (type.HasValue)
        {
            var typeValue = type.Value == AlanubeCompanyType.Main ? "main" : "associated";
            queryParameters.Add(new AlanubeQueryParameter("type", typeValue));
        }

        return await _alanubeClient.GetAsync<IReadOnlyList<CompanyResponseDto>>(
            "companies",
            queryParameters,
            cancellationToken);
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> CreateCompanyAsync(
        CreateCompanyRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return await _alanubeClient.PostAsync<CreateCompanyRequestDto, CompanyResponseDto>(
            "companies",
            request,
            cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> GetCompanyAsync(string companyId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            throw new ArgumentException("A company identifier is required.", nameof(companyId));
        return await _alanubeClient.GetAsync<CompanyResponseDto>($"companies/{Uri.EscapeDataString(companyId)}", cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> UpdateCompanyAsync(
        string companyId,
        UpdateCompanyRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            throw new ArgumentException("A company identifier is required.", nameof(companyId));

        return await _alanubeClient.PatchAsync<UpdateCompanyRequestDto, CompanyResponseDto>(
            $"companies/{Uri.EscapeDataString(companyId)}",
            request,
            cancellationToken: cancellationToken);
    }
}
