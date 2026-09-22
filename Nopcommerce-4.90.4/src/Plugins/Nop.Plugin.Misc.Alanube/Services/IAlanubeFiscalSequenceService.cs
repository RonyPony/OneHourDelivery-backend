namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeFiscalSequenceService
{
    /// <summary>Reserves the next number in a fiscal series. A reserved number is never reused.</summary>
    Task<string> ReserveNextAsync(string companyId, string officeId, string billingPoint, string documentType,
        long initialNextNumber, CancellationToken cancellationToken = default);
}
