using System.Collections.Concurrent;
using System.Transactions;
using Nop.Data;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

/// <summary>Allocates fiscal numbers under a serializable transaction.</summary>
public sealed class AlanubeFiscalSequenceService : IAlanubeFiscalSequenceService
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _seriesLocks = new(StringComparer.Ordinal);
    private readonly IRepository<AlanubeFiscalSequence> _repository;

    public AlanubeFiscalSequenceService(IRepository<AlanubeFiscalSequence> repository) => _repository = repository;

    public async Task<string> ReserveNextAsync(string companyId, string officeId, string billingPoint, string documentType,
        long initialNextNumber, CancellationToken cancellationToken = default)
    {
        Validate(companyId, officeId, billingPoint, documentType, initialNextNumber);
        var key = $"{companyId}|{officeId}|{billingPoint}|{documentType}";
        var seriesLock = _seriesLocks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await seriesLock.WaitAsync(cancellationToken);
        try
        {
            using var transaction = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.Serializable },
                TransactionScopeAsyncFlowOption.Enabled);

            var sequence = await _repository.Table.FirstOrDefaultAsync(item =>
                item.CompanyId == companyId && item.OfficeId == officeId &&
                item.BillingPoint == billingPoint && item.DocumentType == documentType);

            if (sequence is null)
            {
                sequence = new AlanubeFiscalSequence
                {
                    CompanyId = companyId,
                    OfficeId = officeId,
                    BillingPoint = billingPoint,
                    DocumentType = documentType,
                    LastNumber = initialNextNumber - 1,
                    CreatedOnUtc = DateTime.UtcNow
                };
                await _repository.InsertAsync(sequence, publishEvent: false);
            }

            if (sequence.LastNumber >= 9_999_999_999)
                throw new InvalidOperationException("The fiscal numbering series has reached its maximum value.");

            sequence.LastNumber++;
            sequence.UpdatedOnUtc = DateTime.UtcNow;
            await _repository.UpdateAsync(sequence, publishEvent: false);
            transaction.Complete();
            return sequence.LastNumber.ToString("D10");
        }
        finally
        {
            seriesLock.Release();
        }
    }

    private static void Validate(string companyId, string officeId, string billingPoint, string documentType, long initialNextNumber)
    {
        if (companyId?.Length != 26) throw new ArgumentException("Company ID must contain exactly 26 characters.", nameof(companyId));
        if (officeId?.Length != 26) throw new ArgumentException("Office ID must contain exactly 26 characters.", nameof(officeId));
        if (billingPoint?.Length != 3 || !billingPoint.All(char.IsAsciiDigit)) throw new ArgumentException("Billing point must contain exactly three digits.", nameof(billingPoint));
        if (documentType?.Length != 2) throw new ArgumentException("Document type must contain exactly two characters.", nameof(documentType));
        if (initialNextNumber is < 1 or > 9_999_999_999) throw new ArgumentOutOfRangeException(nameof(initialNextNumber));
    }
}
