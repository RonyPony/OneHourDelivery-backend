namespace Nop.Plugin.Misc.Alanube.Api;

/// <summary>
/// Represents one query parameter for an Alanube request.
/// </summary>
public sealed class AlanubeQueryParameter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AlanubeQueryParameter"/> class.
    /// </summary>
    /// <param name="name">Parameter name.</param>
    /// <param name="value">Parameter value.</param>
    public AlanubeQueryParameter(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Gets the parameter name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the parameter value.
    /// </summary>
    public string Value { get; }
}
