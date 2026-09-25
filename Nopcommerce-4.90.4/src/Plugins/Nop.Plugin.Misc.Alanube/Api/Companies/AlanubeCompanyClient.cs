using Nop.Plugin.Misc.Alanube.Api;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Services.Logging;
using System.Diagnostics;

namespace Nop.Plugin.Misc.Alanube.Api.Companies;

/// <summary>
/// Provides read-only company operations against Alanube.
/// </summary>
public sealed class AlanubeCompanyClient : IAlanubeCompanyClient
{
    private const string CompaniesEndpoint = "companies";

    private readonly IAlanubeClient _alanubeClient;
    private readonly AlanubeSettings _settings;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeCompanyClient"/> class.
    /// </summary>
    /// <param name="alanubeClient">Alanube transport client.</param>
    public AlanubeCompanyClient(IAlanubeClient alanubeClient, AlanubeSettings settings, ILogger logger)
    {
        _alanubeClient = alanubeClient;
        _settings = settings;
        _logger = logger;
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

        var response = await _alanubeClient.GetAsync<CompanyResponseDto>(
            CompaniesEndpoint,
            queryParameters,
            cancellationToken);

        return new AlanubeApiResponse<IReadOnlyList<CompanyResponseDto>>
        {
            StatusCode = response.StatusCode,
            ReasonPhrase = response.ReasonPhrase,
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            Body = response.Body is null ? Array.Empty<CompanyResponseDto>() : new[] { response.Body },
            Error = response.Error,
            RawResponse = response.RawResponse
        };
    }

    public async Task<AlanubeConnectionTestResult> TestConnectionAsync(
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        correlationId = string.IsNullOrWhiteSpace(correlationId)
            ? Guid.NewGuid().ToString("N")[..8]
            : correlationId;

        var logPrefix = $"Alanube Test [{correlationId}]";
        var stopwatch = Stopwatch.StartNew();
        var result = new AlanubeConnectionTestResult
        {
            CorrelationId = correlationId,
            Endpoint = $"/{CompaniesEndpoint}",
            HttpMethod = HttpMethod.Get.Method,
            BaseUrl = GetBaseUrl(),
            MaskedApiToken = MaskApiToken(GetApiToken())
        };

        await _logger.InformationAsync($"{logPrefix} Starting connection test.");
        await _logger.InformationAsync($"{logPrefix} Base URL: {result.BaseUrl}");
        await _logger.InformationAsync($"{logPrefix} {result.HttpMethod} {result.Endpoint}");
        await _logger.InformationAsync($"{logPrefix} Configured API Key: {result.MaskedApiToken}");

        try
        {
            var response = await GetCompaniesAsync(cancellationToken: cancellationToken);
            stopwatch.Stop();

            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            result.StatusCode = response.StatusCode;
            result.ReasonPhrase = response.ReasonPhrase;
            result.ResponseBody = response.RawResponse;
            result.Success = response.IsSuccessStatusCode;

            await _logger.InformationAsync($"{logPrefix} HTTP {response.StatusCode} {response.ReasonPhrase} in {stopwatch.ElapsedMilliseconds} ms.");
            await _logger.InformationAsync($"{logPrefix} Response body: {Truncate(response.RawResponse, 4000)}");

            if (!response.IsSuccessStatusCode)
            {
                result.ErrorMessage = response.Error?.Message ?? $"Alanube returned HTTP status {response.StatusCode}.";
                await _logger.ErrorAsync($"{logPrefix} Alanube HTTP error: {response.StatusCode} {response.ReasonPhrase}. Body: {Truncate(response.RawResponse, 4000)}");
                return result;
            }

            if (string.IsNullOrWhiteSpace(response.RawResponse))
            {
                result.ErrorMessage = "Alanube returned an empty response.";
                await _logger.WarningAsync($"{logPrefix} Alanube returned HTTP 200 but the response body is empty.");
                return result;
            }

            result.Companies = response.Body ?? Array.Empty<CompanyResponseDto>();
            await _logger.InformationAsync($"{logPrefix} Deserialized DTO: CompanyResponseDto.");
            await _logger.InformationAsync($"{logPrefix} Companies returned: {result.Companies.Count}.");

            if (result.Companies.Count == 0)
            {
                await _logger.WarningAsync($"{logPrefix} Alanube returned a successful response with zero companies.");
                return result;
            }

            foreach (var company in result.Companies)
            {
                await _logger.InformationAsync($"{logPrefix} Company: Id={Sanitize(company.Id)}, Name={Sanitize(company.TradeName)}, RUC={Sanitize(company.Ruc)}.");
            }

            return result;
        }
        catch (AlanubeApiException exception)
        {
            stopwatch.Stop();
            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            result.StatusCode = exception.StatusCode;
            result.ResponseBody = exception.RawResponse;
            result.ErrorMessage = exception.Error?.Message ?? exception.Message;
            result.Success = false;

            if (exception.InnerException is System.Text.Json.JsonException)
                await _logger.ErrorAsync($"{logPrefix} Alanube response deserialization failed. Body: {Truncate(exception.RawResponse, 4000)}", exception);
            else
                await _logger.ErrorAsync($"{logPrefix} Alanube connection test failed.", exception);

            return result;
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            stopwatch.Stop();
            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            result.ErrorMessage = exception is TaskCanceledException
                ? "The request to Alanube timed out."
                : exception.Message;
            result.Success = false;
            await _logger.ErrorAsync($"{logPrefix} Alanube network, DNS or timeout error.", exception);
            return result;
        }
        catch (Exception exception)
        {
            stopwatch.Stop();
            result.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            result.ErrorMessage = exception.Message;
            result.Success = false;
            await _logger.ErrorAsync($"{logPrefix} Unexpected Alanube connection test error.", exception);
            return result;
        }
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> CreateCompanyAsync(
        CreateCompanyRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return await _alanubeClient.PostAsync<CreateCompanyRequestDto, CompanyResponseDto>(
            CompaniesEndpoint,
            request,
            cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> GetCompanyAsync(string companyId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            throw new ArgumentException("A company identifier is required.", nameof(companyId));
        return await _alanubeClient.GetAsync<CompanyResponseDto>($"{CompaniesEndpoint}/{Uri.EscapeDataString(companyId)}", cancellationToken: cancellationToken);
    }

    public async Task<AlanubeApiResponse<CompanyResponseDto>> UpdateCompanyAsync(
        string companyId,
        UpdateCompanyRequestDto request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyId))
            throw new ArgumentException("A company identifier is required.", nameof(companyId));

        return await _alanubeClient.PatchAsync<UpdateCompanyRequestDto, CompanyResponseDto>(
            $"{CompaniesEndpoint}/{Uri.EscapeDataString(companyId)}",
            request,
            cancellationToken: cancellationToken);
    }

    private string GetBaseUrl() => _settings.Environment switch
    {
        AlanubeEnvironment.Sandbox => AlanubeDefaults.SandboxApiBaseUrl,
        AlanubeEnvironment.Production => AlanubeDefaults.ProductionApiBaseUrl,
        _ => "Invalid environment"
    };

    private string GetApiToken() => _settings.Environment switch
    {
        AlanubeEnvironment.Sandbox => _settings.SandboxApiToken,
        AlanubeEnvironment.Production => _settings.ProductionApiToken,
        _ => null
    };

    private static string MaskApiToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return "not configured";

        var trimmed = token.Trim();
        return trimmed.Length <= 4
            ? "****"
            : $"****{trimmed[^4..]}";
    }

    private static string Sanitize(string value) => string.IsNullOrWhiteSpace(value)
        ? string.Empty
        : Truncate(value.Replace("\r", " ").Replace("\n", " ").Trim(), 300);

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;
}
