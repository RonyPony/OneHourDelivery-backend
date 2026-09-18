using Nop.Data;
using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeDocumentService : IAlanubeDocumentService
{
    private readonly IRepository<AlanubeDocument> _documentRepository;
    private readonly IAlanubeRetryPolicy _retryPolicy;

    public AlanubeDocumentService(IRepository<AlanubeDocument> documentRepository, IAlanubeRetryPolicy retryPolicy)
    {
        _documentRepository = documentRepository;
        _retryPolicy = retryPolicy;
    }

    public async Task InsertAsync(AlanubeDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var now = DateTime.UtcNow;
        if (document.CreatedOnUtc == default)
            document.CreatedOnUtc = now;
        document.UpdatedOnUtc = now;

        await _documentRepository.InsertAsync(document);
    }

    public async Task UpdateAsync(AlanubeDocument document)
    {
        ArgumentNullException.ThrowIfNull(document);

        document.UpdatedOnUtc = DateTime.UtcNow;
        await _documentRepository.UpdateAsync(document);
    }

    public async Task<AlanubeDocument> GetByIdAsync(int id)
    {
        return id <= 0 ? null : await _documentRepository.GetByIdAsync(id);
    }

    public async Task<AlanubeDocument> GetByAlanubeIdAsync(string alanubeId)
    {
        if (string.IsNullOrWhiteSpace(alanubeId))
            return null;

        return await _documentRepository.Table
            .FirstOrDefaultAsync(document => document.AlanubeId == alanubeId);
    }

    public async Task<IList<AlanubeDocument>> GetByOrderAsync(int orderId)
    {
        if (orderId <= 0)
            return new List<AlanubeDocument>();

        return await _documentRepository.Table
            .Where(document => document.OrderId == orderId)
            .OrderBy(document => document.DocumentType)
            .ThenBy(document => document.Version)
            .ToListAsync();
    }

    public async Task<IList<AlanubeDocument>> GetByOrderAndTypeAsync(int orderId, ElectronicDocumentType documentType)
    {
        if (orderId <= 0)
            return new List<AlanubeDocument>();

        return await _documentRepository.Table
            .Where(document => document.OrderId == orderId && document.DocumentType == documentType)
            .OrderBy(document => document.Version)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int orderId, ElectronicDocumentType documentType, int version)
    {
        if (orderId <= 0 || version <= 0)
            return false;

        return await _documentRepository.Table.AnyAsync(document =>
            document.OrderId == orderId &&
            document.DocumentType == documentType &&
            document.Version == version);
    }

    public async Task<IList<AlanubeDocument>> GetDocumentsForReconciliationAsync(int maxCount = 100)
    {
        if (maxCount <= 0)
            return new List<AlanubeDocument>();

        return await _documentRepository.Table
            .Where(document => document.DocumentStatus == ElectronicDocumentStatus.Pending ||
                               document.DocumentStatus == ElectronicDocumentStatus.Processing)
            .OrderBy(document => document.UpdatedOnUtc)
            .ThenBy(document => document.Id)
            .Take(maxCount)
            .ToListAsync();
    }

    public async Task<AlanubeRetryDecision> RegisterFailureAsync(AlanubeDocument document,
        AlanubeFailureCategory failureCategory, string errorCode = null, string errorMessage = null, DateTime? utcNow = null)
    {
        ArgumentNullException.ThrowIfNull(document);
        var now = utcNow ?? DateTime.UtcNow;
        var decision = _retryPolicy.Evaluate(document, failureCategory, now);
        document.DocumentStatus = decision.Status;
        document.RetryCount = decision.RetryCount;
        document.LastRetryUtc = decision.RetryAllowed ? now : document.LastRetryUtc;
        document.ErrorCode = errorCode;
        document.ErrorMessage = errorMessage;
        await UpdateAsync(document);
        return decision;
    }
}
