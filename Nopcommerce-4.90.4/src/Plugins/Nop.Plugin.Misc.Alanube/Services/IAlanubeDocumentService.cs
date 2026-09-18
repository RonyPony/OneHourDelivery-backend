using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeDocumentService
{
    Task InsertAsync(AlanubeDocument document);
    Task UpdateAsync(AlanubeDocument document);
    Task<AlanubeDocument> GetByIdAsync(int id);
    Task<AlanubeDocument> GetByAlanubeIdAsync(string alanubeId);
    Task<IList<AlanubeDocument>> GetByOrderAsync(int orderId);
    Task<IList<AlanubeDocument>> GetByOrderAndTypeAsync(int orderId, ElectronicDocumentType documentType);
    Task<bool> ExistsAsync(int orderId, ElectronicDocumentType documentType, int version);
    Task<IList<AlanubeDocument>> GetDocumentsForReconciliationAsync(int maxCount = 100);
    Task<AlanubeRetryDecision> RegisterFailureAsync(AlanubeDocument document, AlanubeFailureCategory failureCategory,
        string errorCode = null, string errorMessage = null, DateTime? utcNow = null);
}
