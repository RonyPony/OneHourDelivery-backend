namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Represents an error returned by the Alanube API.
/// </summary>
public sealed class AlanubeApiError
{
    /// <summary>
    /// Gets or sets the Alanube error code, when provided.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the Alanube error message, when provided.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets validation errors returned by Alanube.
    /// </summary>
    public IList<AlanubeValidationError> ValidationErrors { get; set; } = new List<AlanubeValidationError>();
}

/// <summary>
/// Represents one validation error returned by the Alanube API.
/// </summary>
public sealed class AlanubeValidationError
{
    /// <summary>
    /// Gets or sets the field or path related to the validation error.
    /// </summary>
    public string Field { get; set; }

    /// <summary>
    /// Gets or sets the validation error message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the validation error code, when provided.
    /// </summary>
    public string Code { get; set; }
}
