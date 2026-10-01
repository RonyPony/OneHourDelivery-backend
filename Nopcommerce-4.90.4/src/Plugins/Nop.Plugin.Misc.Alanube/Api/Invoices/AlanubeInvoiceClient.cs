using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nop.Plugin.Misc.Alanube.Api;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Api.Invoices;

public sealed class AlanubeInvoiceClient : IAlanubeInvoiceClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private readonly IAlanubeClient _alanubeClient;
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly AlanubeSettings _settings;

    public AlanubeInvoiceClient(IAlanubeClient alanubeClient, HttpClient httpClient, ILogger logger, AlanubeSettings settings)
    {
        _alanubeClient = alanubeClient;
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings;
    }

    public Task<AlanubeApiResponse<CreateInvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _alanubeClient.PostAsync<CreateInvoiceRequest, CreateInvoiceResponse>("invoices", request, BuildCompanyOfficeParameters(), cancellationToken);
    }

    public async Task<AlanubeApiResponse<DirectFiscalInvoiceResponse>> CreateDirectFiscalInvoiceAsync(DirectFiscalInvoiceRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var endpoint = new Uri(new Uri(GetDirectApiBaseUrl()), "fiscal-invoices");
        var stopwatch = Stopwatch.StartNew();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GetApiToken());
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(request, _jsonSerializerOptions), Encoding.UTF8, "application/json");

        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            stopwatch.Stop();
            await _logger.ErrorAsync($"Alanube API POST /fiscal-invoices failed in {stopwatch.ElapsedMilliseconds} ms.", exception);
            throw new AlanubeApiException("The direct fiscal invoice request to Alanube could not be completed.", innerException: exception);
        }

        using (httpResponse)
        {
            var rawResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            stopwatch.Stop();
            var response = new AlanubeApiResponse<DirectFiscalInvoiceResponse>
            {
                StatusCode = (int)httpResponse.StatusCode,
                ReasonPhrase = httpResponse.ReasonPhrase,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                RawResponse = rawResponse
            };

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Error = new AlanubeApiError { Message = string.IsNullOrWhiteSpace(httpResponse.ReasonPhrase) ? $"Alanube returned HTTP status {response.StatusCode}." : httpResponse.ReasonPhrase };
                await _logger.ErrorAsync($"Alanube API POST /fiscal-invoices -> HTTP {response.StatusCode} {response.ReasonPhrase} in {stopwatch.ElapsedMilliseconds} ms. Body: {Truncate(rawResponse, 4000)}");
                return response;
            }

            await _logger.InformationAsync($"Alanube API POST /fiscal-invoices -> HTTP {response.StatusCode} {response.ReasonPhrase} in {stopwatch.ElapsedMilliseconds} ms. Body: {Truncate(rawResponse, 4000)}");

            if (!string.IsNullOrWhiteSpace(rawResponse))
                response.Body = JsonSerializer.Deserialize<DirectFiscalInvoiceResponse>(rawResponse, _jsonSerializerOptions);

            return response;
        }
    }

    public Task<AlanubeApiResponse<GetInvoiceResponse>> GetInvoiceAsync(string alanubeId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(alanubeId))
            throw new ArgumentException("An Alanube invoice identifier is required.", nameof(alanubeId));
        return _alanubeClient.GetAsync<GetInvoiceResponse>($"invoices/{Uri.EscapeDataString(alanubeId)}", BuildCompanyParameters(), cancellationToken);
    }

    private IReadOnlyCollection<AlanubeQueryParameter> BuildCompanyOfficeParameters()
    {
        var parameters = new List<AlanubeQueryParameter>();
        if (!string.IsNullOrWhiteSpace(_settings.CompanyId))
            parameters.Add(new AlanubeQueryParameter("idCompany", _settings.CompanyId));
        if (!string.IsNullOrWhiteSpace(_settings.OfficeId))
            parameters.Add(new AlanubeQueryParameter("idOffice", _settings.OfficeId));
        return parameters;
    }

    private IReadOnlyCollection<AlanubeQueryParameter> BuildCompanyParameters() =>
        string.IsNullOrWhiteSpace(_settings.CompanyId)
            ? []
            : [new AlanubeQueryParameter("idCompany", _settings.CompanyId)];

    private string GetDirectApiBaseUrl()
    {
        var baseUrl = string.IsNullOrWhiteSpace(_settings.DirectApiBaseUrl)
            ? "https://sandbox.alanube.co/dom/v1/"
            : _settings.DirectApiBaseUrl.Trim();

        return baseUrl.EndsWith('/') ? baseUrl : $"{baseUrl}/";
    }

    private string GetApiToken()
    {
        var token = _settings.Environment switch
        {
            AlanubeEnvironment.Sandbox => _settings.SandboxApiToken,
            AlanubeEnvironment.Production => _settings.ProductionApiToken,
            _ => null
        };

        if (string.IsNullOrWhiteSpace(token))
            throw new AlanubeApiException("The API token for the configured Alanube environment is missing.");

        return token;
    }

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;
}
