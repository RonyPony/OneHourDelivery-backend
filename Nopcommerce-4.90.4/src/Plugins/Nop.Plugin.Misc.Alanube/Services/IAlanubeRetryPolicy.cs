using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeRetryPolicy
{
    AlanubeRetryDecision Evaluate(AlanubeDocument document, AlanubeFailureCategory failureCategory, DateTime utcNow);
    bool IsRetryDue(AlanubeDocument document, DateTime utcNow);
}

public sealed class AlanubeRetryDecision
{
    public ElectronicDocumentStatus Status { get; init; }
    public bool RetryAllowed { get; init; }
    public DateTime? RetryOnUtc { get; init; }
    public int RetryCount { get; init; }
}
