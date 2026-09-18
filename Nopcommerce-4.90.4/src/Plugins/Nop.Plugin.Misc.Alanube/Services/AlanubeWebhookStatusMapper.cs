using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public static class AlanubeWebhookStatusMapper
{
    public static ElectronicDocumentStatus? Map(string status, string legalStatus)
    {
        return legalStatus?.Trim().ToUpperInvariant() switch
        {
            "DGI_AUTHORIZED" => ElectronicDocumentStatus.Authorized,
            "PAC_AUTHORIZED" => ElectronicDocumentStatus.Processing,
            "DGI_REJECTED" => ElectronicDocumentStatus.Rejected,
            "PAC_REJECTED" => ElectronicDocumentStatus.Rejected,
            "REGISTERED" => ElectronicDocumentStatus.Processing,
            _ when string.Equals(status?.Trim(), "FINISHED", StringComparison.OrdinalIgnoreCase) => ElectronicDocumentStatus.ManualReview,
            _ => null
        };
    }

    public static bool CanTransition(ElectronicDocumentStatus current, ElectronicDocumentStatus target)
    {
        if (current == target)
            return true;
        if (current is ElectronicDocumentStatus.Authorized or ElectronicDocumentStatus.Rejected or ElectronicDocumentStatus.Cancelled)
            return false;
        return target != ElectronicDocumentStatus.Pending;
    }
}
