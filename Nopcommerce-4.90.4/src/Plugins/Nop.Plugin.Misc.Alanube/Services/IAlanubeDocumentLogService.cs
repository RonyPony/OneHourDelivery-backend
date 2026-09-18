using Nop.Plugin.Misc.Alanube.Domain;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeDocumentLogService
{
    Task InsertAsync(AlanubeDocumentLog log);

    Task<IList<AlanubeDocumentLog>> GetByDocumentIdAsync(int alanubeDocumentId);
}
