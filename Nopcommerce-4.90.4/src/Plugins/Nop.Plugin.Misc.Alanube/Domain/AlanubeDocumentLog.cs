using Nop.Core;

namespace Nop.Plugin.Misc.Alanube.Domain;

/// <summary>
/// Represents an audit record for an Alanube document operation.
/// </summary>
public sealed class AlanubeDocumentLog : BaseEntity
{
    public int AlanubeDocumentId { get; set; }
    public string Operation { get; set; }
    public string Endpoint { get; set; }
    public string HttpMethod { get; set; }
    public string RequestJson { get; set; }
    public string ResponseJson { get; set; }
    public int? HttpStatusCode { get; set; }
    public bool Success { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
