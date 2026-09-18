using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeRetryPolicy : IAlanubeRetryPolicy
{
    private static readonly TimeSpan[] RetryDelays =
    [
        TimeSpan.FromMinutes(1),
        TimeSpan.FromMinutes(5),
        TimeSpan.FromMinutes(15),
        TimeSpan.FromMinutes(30),
        TimeSpan.FromHours(1)
    ];

    public AlanubeRetryDecision Evaluate(AlanubeDocument document, AlanubeFailureCategory failureCategory, DateTime utcNow)
    {
        ArgumentNullException.ThrowIfNull(document);

        if (failureCategory == AlanubeFailureCategory.LocalValidation)
            return Final(ElectronicDocumentStatus.Failed, document.RetryCount);

        if (failureCategory == AlanubeFailureCategory.ExplicitRejection)
            return Final(ElectronicDocumentStatus.Rejected, document.RetryCount);

        if (failureCategory == AlanubeFailureCategory.AmbiguousPostTimeout)
            return Final(ElectronicDocumentStatus.ManualReview, document.RetryCount);

        // A caller may use this category only when it can positively establish that no POST was sent.
        if (failureCategory == AlanubeFailureCategory.NetworkFailureBeforeSendConfirmed)
            return Final(ElectronicDocumentStatus.ManualReview, document.RetryCount);

        if (document.RetryCount >= RetryDelays.Length)
            return Final(ElectronicDocumentStatus.ManualReview, document.RetryCount);

        var retryCount = document.RetryCount + 1;

        return new AlanubeRetryDecision
        {
            Status = document.DocumentStatus,
            RetryAllowed = true,
            RetryCount = retryCount,
            RetryOnUtc = utcNow.Add(RetryDelays[retryCount - 1])
        };
    }

    public bool IsRetryDue(AlanubeDocument document, DateTime utcNow)
    {
        ArgumentNullException.ThrowIfNull(document);
        if (document.DocumentStatus == ElectronicDocumentStatus.ManualReview || document.RetryCount <= 0 || document.RetryCount > RetryDelays.Length || !document.LastRetryUtc.HasValue)
            return false;
        return utcNow >= document.LastRetryUtc.Value.Add(RetryDelays[document.RetryCount - 1]);
    }

    private static AlanubeRetryDecision Final(ElectronicDocumentStatus status, int retryCount) => new()
    {
        Status = status,
        RetryAllowed = false,
        RetryCount = retryCount
    };
}
