namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Provides the transport abstraction used to communicate with Alanube.
/// </summary>
public interface IAlanubeClient
{
    /// <summary>
    /// Sends a GET request to an Alanube endpoint.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response body.</typeparam>
    /// <param name="relativeEndpoint">Endpoint relative to the configured Alanube base URL.</param>
    /// <param name="queryParameters">Optional query parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    Task<AlanubeApiResponse<TResponse>> GetAsync<TResponse>(
        string relativeEndpoint,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request with a JSON body to an Alanube endpoint.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request body.</typeparam>
    /// <typeparam name="TResponse">Type of the response body.</typeparam>
    /// <param name="relativeEndpoint">Endpoint relative to the configured Alanube base URL.</param>
    /// <param name="request">Request body to serialize as JSON.</param>
    /// <param name="queryParameters">Optional query parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    Task<AlanubeApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string relativeEndpoint,
        TRequest request,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PATCH request with a JSON body to an Alanube endpoint.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request body.</typeparam>
    /// <typeparam name="TResponse">Type of the response body.</typeparam>
    /// <param name="relativeEndpoint">Endpoint relative to the configured Alanube base URL.</param>
    /// <param name="request">Request body to serialize as JSON.</param>
    /// <param name="queryParameters">Optional query parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    Task<AlanubeApiResponse<TResponse>> PatchAsync<TRequest, TResponse>(
        string relativeEndpoint,
        TRequest request,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a DELETE request to an Alanube endpoint.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response body.</typeparam>
    /// <param name="relativeEndpoint">Endpoint relative to the configured Alanube base URL.</param>
    /// <param name="queryParameters">Optional query parameters.</param>
    /// <param name="cancellationToken">Token used to cancel the request.</param>
    Task<AlanubeApiResponse<TResponse>> DeleteAsync<TResponse>(
        string relativeEndpoint,
        IReadOnlyCollection<AlanubeQueryParameter> queryParameters = null,
        CancellationToken cancellationToken = default);
}
