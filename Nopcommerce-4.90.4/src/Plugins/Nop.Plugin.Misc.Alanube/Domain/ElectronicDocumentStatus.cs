namespace Nop.Plugin.Misc.Alanube.Domain;

public enum ElectronicDocumentStatus
{
    Pending = 1,
    Processing = 2,
    Authorized = 3,
    Rejected = 4,
    Failed = 5,
    CancellationPending = 6,
    Cancelled = 7,
    ManualReview = 8
}
