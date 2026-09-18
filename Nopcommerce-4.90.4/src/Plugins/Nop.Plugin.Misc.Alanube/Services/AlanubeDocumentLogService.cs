using Nop.Data;
using Nop.Plugin.Misc.Alanube.Domain;
using System.Text.RegularExpressions;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeDocumentLogService : IAlanubeDocumentLogService
{
    private readonly IRepository<AlanubeDocumentLog> _logRepository;

    public AlanubeDocumentLogService(IRepository<AlanubeDocumentLog> logRepository)
    {
        _logRepository = logRepository;
    }

    public async Task InsertAsync(AlanubeDocumentLog log)
    {
        ArgumentNullException.ThrowIfNull(log);

        log.Endpoint = SanitizeEndpoint(log.Endpoint);
        log.RequestJson = SanitizeJson(log.RequestJson);
        log.ResponseJson = SanitizeJson(log.ResponseJson);
        log.ErrorMessage = SanitizeText(log.ErrorMessage);

        if (log.CreatedOnUtc == default)
            log.CreatedOnUtc = DateTime.UtcNow;

        await _logRepository.InsertAsync(log);
    }

    public async Task<IList<AlanubeDocumentLog>> GetByDocumentIdAsync(int alanubeDocumentId)
    {
        if (alanubeDocumentId <= 0)
            return new List<AlanubeDocumentLog>();

        return await _logRepository.Table
            .Where(log => log.AlanubeDocumentId == alanubeDocumentId)
            .OrderBy(log => log.CreatedOnUtc)
            .ThenBy(log => log.Id)
            .ToListAsync();
    }

    private static string SanitizeJson(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return Regex.Replace(value,
            "(?i)(\\\"[^\\\"]*(?:authorization|token|secret|password|api[_-]?key)[^\\\"]*\\\"\\s*:\\s*\\\")[^\\\"]*(\\\")",
            "$1[REDACTED]$2");
    }

    private static string SanitizeEndpoint(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return Regex.Replace(value,
            "(?i)([?&](?:authorization|token|secret|password|api[_-]?key)=)[^&]*",
            "$1[REDACTED]");
    }

    private static string SanitizeText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return Regex.Replace(value, "(?i)(bearer\\s+)[^\\s]+", "$1[REDACTED]");
    }
}
