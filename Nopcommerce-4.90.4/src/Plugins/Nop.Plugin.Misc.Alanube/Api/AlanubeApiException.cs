namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Represents an exception raised while communicating with the Alanube API.
/// </summary>
public sealed class AlanubeApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeApiException"/> class.
    /// </summary>
    public AlanubeApiException(
        string message,
        int? statusCode = null,
        AlanubeApiError error = null,
        string rawResponse = null,
        Exception innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Error = error;
        RawResponse = rawResponse;
    }

    /// <summary>
    /// Gets the HTTP status code returned by Alanube, when available.
    /// </summary>
    public int? StatusCode { get; }

    /// <summary>
    /// Gets the structured Alanube error, when available.
    /// </summary>
    public AlanubeApiError Error { get; }

    /// <summary>
    /// Gets the raw response body for diagnostics, when available.
    /// </summary>
    public string RawResponse { get; }
}
