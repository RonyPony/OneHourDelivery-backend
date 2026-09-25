namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Represents a response received from the Alanube API.
/// </summary>
/// <typeparam name="T">Type of the successful response body.</typeparam>
public sealed class AlanubeApiResponse<T>
{
    /// <summary>
    /// Gets or sets the HTTP status code returned by Alanube.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the HTTP reason phrase returned by Alanube.
    /// </summary>
    public string ReasonPhrase { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the HTTP response was successful.
    /// </summary>
    public bool IsSuccessStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the deserialized successful response body.
    /// </summary>
    public T Body { get; set; }

    /// <summary>
    /// Gets or sets the Alanube error returned by the API, when available.
    /// </summary>
    public AlanubeApiError Error { get; set; }

    /// <summary>
    /// Gets or sets the raw response body for diagnostics.
    /// </summary>
    public string RawResponse { get; set; }
}
