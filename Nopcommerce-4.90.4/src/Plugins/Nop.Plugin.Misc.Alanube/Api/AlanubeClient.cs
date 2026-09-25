using System.Net.Http.Headers;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nop.Plugin.Misc.Alanube.Configuration;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Provides HTTP transport for the Alanube API.
/// </summary>
public sealed class AlanubeClient : IAlanubeClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly AlanubeSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeClient"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client managed by the HTTP client factory.</param>
    /// <param name="settings">Alanube plugin settings.</param>
    public AlanubeClient(HttpClient httpClient, ILogger logger, AlanubeSettings settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings;
    }

    /// <inheritdoc />
    public Task<AlanubeApiResponse<TResponse>> GetAsync<TResponse>(
        string relativeEndpoint,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Get,
            relativeEndpoint,
            null,
            queryParameters,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<AlanubeApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string relativeEndpoint,
        TRequest request,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Post,
            relativeEndpoint,
            request,
            queryParameters,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<AlanubeApiResponse<TResponse>> PatchAsync<TRequest, TResponse>(
        string relativeEndpoint,
        TRequest request,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<TRequest, TResponse>(
            HttpMethod.Patch,
            relativeEndpoint,
            request,
            queryParameters,
            cancellationToken);
    }

    /// <inheritdoc />
    public Task<AlanubeApiResponse<TResponse>> DeleteAsync<TResponse>(
        string relativeEndpoint,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        return SendAsync<object, TResponse>(
            HttpMethod.Delete,
            relativeEndpoint,
            null,
            queryParameters,
            cancellationToken);
    }

    private async Task<AlanubeApiResponse<TResponse>> SendAsync<TRequest, TResponse>(
        HttpMethod method,
        string relativeEndpoint,
        TRequest request,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters,
        CancellationToken cancellationToken)
    {
        var requestUri = BuildRequestUri(relativeEndpoint, queryParameters);
        var token = GetApiToken();
        var endpointForLog = GetEndpointForLog(requestUri);
        var stopwatch = Stopwatch.StartNew();

        using var requestMessage = new HttpRequestMessage(method, requestUri);
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (request is not null && method != HttpMethod.Get && method != HttpMethod.Delete)
        {
            string json;
            try
            {
                json = JsonSerializer.Serialize(request, _jsonSerializerOptions);
            }
            catch (Exception exception) when (exception is JsonException or NotSupportedException)
            {
                throw new AlanubeApiException("The Alanube request body could not be serialized.", innerException: exception);
            }

            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage httpResponse;
        try
        {
            httpResponse = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            stopwatch.Stop();
            await _logger.ErrorAsync($"Alanube API {method.Method} {endpointForLog} failed in {stopwatch.ElapsedMilliseconds} ms.", exception);
            throw new AlanubeApiException("The request to Alanube could not be completed.", innerException: exception);
        }

        using (httpResponse)
        {
            var rawResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            stopwatch.Stop();
            var response = new AlanubeApiResponse<TResponse>
            {
                StatusCode = (int)httpResponse.StatusCode,
                ReasonPhrase = httpResponse.ReasonPhrase,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                RawResponse = rawResponse
            };

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Error = DeserializeError(rawResponse, response.StatusCode, httpResponse.ReasonPhrase);
                await _logger.ErrorAsync($"Alanube API {method.Method} {endpointForLog} -> HTTP {response.StatusCode} {response.ReasonPhrase} in {stopwatch.ElapsedMilliseconds} ms. Body: {Truncate(rawResponse, 4000)}");
                return response;
            }

            await _logger.InformationAsync($"Alanube API {method.Method} {endpointForLog} -> HTTP {response.StatusCode} {response.ReasonPhrase} in {stopwatch.ElapsedMilliseconds} ms. Body: {Truncate(rawResponse, 4000)}");

            if (string.IsNullOrWhiteSpace(rawResponse))
                return response;

            try
            {
                response.Body = JsonSerializer.Deserialize<TResponse>(rawResponse, _jsonSerializerOptions);
                return response;
            }
            catch (JsonException exception)
            {
                await _logger.ErrorAsync($"Alanube API {method.Method} {endpointForLog} deserialization failed after HTTP {response.StatusCode} {response.ReasonPhrase}. Body: {Truncate(rawResponse, 4000)}", exception);
                throw new AlanubeApiException(
                    "The successful Alanube response could not be deserialized.",
                    response.StatusCode,
                    rawResponse: rawResponse,
                    innerException: exception);
            }
        }
    }

    private static string GetEndpointForLog(Uri requestUri)
    {
        if (requestUri is null)
            return string.Empty;

        var path = requestUri.AbsolutePath;
        var marker = "/pan/v1";
        var markerIndex = path.IndexOf(marker, StringComparison.OrdinalIgnoreCase);
        if (markerIndex >= 0)
            path = path[(markerIndex + marker.Length)..];

        if (string.IsNullOrWhiteSpace(path))
            path = "/";

        return string.IsNullOrWhiteSpace(requestUri.Query) ? path : $"{path}{requestUri.Query}";
    }

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;

    private Uri BuildRequestUri(
        string relativeEndpoint,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters)
    {
        if (string.IsNullOrWhiteSpace(relativeEndpoint))
            throw new ArgumentException("A relative Alanube endpoint is required.", nameof(relativeEndpoint));

        if (Uri.TryCreate(relativeEndpoint, UriKind.Absolute, out _))
            throw new ArgumentException("The Alanube endpoint must be relative.", nameof(relativeEndpoint));

        var baseUrl = _settings.Environment switch
        {
            AlanubeEnvironment.Sandbox => AlanubeDefaults.SandboxApiBaseUrl,
            AlanubeEnvironment.Production => AlanubeDefaults.ProductionApiBaseUrl,
            _ => throw new AlanubeApiException("The configured Alanube environment is invalid.")
        };

        var endpoint = relativeEndpoint.TrimStart('/');
        var uriBuilder = new StringBuilder(new Uri(new Uri(baseUrl), endpoint).AbsoluteUri);

        if (queryParameters?.Count > 0)
        {
            var separator = uriBuilder.ToString().Contains('?') ? '&' : '?';
            foreach (var parameter in queryParameters)
            {
                if (parameter is null || string.IsNullOrWhiteSpace(parameter.Name))
                    continue;

                uriBuilder.Append(separator);
                uriBuilder.Append(Uri.EscapeDataString(parameter.Name));
                uriBuilder.Append('=');
                uriBuilder.Append(Uri.EscapeDataString(parameter.Value ?? string.Empty));
                separator = '&';
            }
        }

        return new Uri(uriBuilder.ToString(), UriKind.Absolute);
    }

    private string GetApiToken()
    {
        var token = _settings.Environment switch
        {
            AlanubeEnvironment.Sandbox => _settings.SandboxApiToken,
            AlanubeEnvironment.Production => _settings.ProductionApiToken,
            _ => throw new AlanubeApiException("The configured Alanube environment is invalid.")
        };

        if (string.IsNullOrWhiteSpace(token))
            throw new AlanubeApiException("The API token for the configured Alanube environment is missing.");

        return token;
    }

    private static AlanubeApiError DeserializeError(string rawResponse, int statusCode, string reasonPhrase)
    {
        if (!string.IsNullOrWhiteSpace(rawResponse))
        {
            try
            {
                var error = JsonSerializer.Deserialize<AlanubeApiError>(rawResponse, _jsonSerializerOptions);
                if (error is not null &&
                    (!string.IsNullOrWhiteSpace(error.Code) ||
                     !string.IsNullOrWhiteSpace(error.Message) ||
                     error.ValidationErrors?.Count > 0))
                {
                    return error;
                }
            }
            catch (JsonException)
            {
                // The raw response is preserved for diagnostics when Alanube returns an unknown error shape.
            }
        }

        return new AlanubeApiError
        {
            Message = !string.IsNullOrWhiteSpace(reasonPhrase)
                ? reasonPhrase
                : $"Alanube returned HTTP status {statusCode}."
        };
    }
}
