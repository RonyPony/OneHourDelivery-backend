using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nop.Plugin.Misc.Alanube.Configuration;

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
    private readonly AlanubeSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeClient"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client managed by the HTTP client factory.</param>
    /// <param name="settings">Alanube plugin settings.</param>
    public AlanubeClient(HttpClient httpClient, AlanubeSettings settings)
    {
        _httpClient = httpClient;
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
            throw new AlanubeApiException("The request to Alanube could not be completed.", innerException: exception);
        }

        using (httpResponse)
        {
            var rawResponse = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            var response = new AlanubeApiResponse<TResponse>
            {
                StatusCode = (int)httpResponse.StatusCode,
                IsSuccessStatusCode = httpResponse.IsSuccessStatusCode,
                RawResponse = rawResponse
            };

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Error = DeserializeError(rawResponse, response.StatusCode, httpResponse.ReasonPhrase);
                return response;
            }

            if (string.IsNullOrWhiteSpace(rawResponse))
                return response;

            try
            {
                response.Body = JsonSerializer.Deserialize<TResponse>(rawResponse, _jsonSerializerOptions);
                return response;
            }
            catch (JsonException exception)
            {
                throw new AlanubeApiException(
                    "The successful Alanube response could not be deserialized.",
                    response.StatusCode,
                    rawResponse: rawResponse,
                    innerException: exception);
            }
        }
    }

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
