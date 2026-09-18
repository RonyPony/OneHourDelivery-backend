namespace Nop.Plugin.Misc.Alanube.Domain;

public enum AlanubeFailureCategory
{
    LocalValidation = 1,
    ExplicitRejection = 2,
    TemporaryStatusQuery = 3,
    NetworkFailureBeforeSendConfirmed = 4,
    AmbiguousPostTimeout = 5
}
