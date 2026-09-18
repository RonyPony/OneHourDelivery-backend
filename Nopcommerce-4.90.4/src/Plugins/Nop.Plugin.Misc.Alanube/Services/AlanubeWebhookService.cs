using System.Text.Json;
using Nop.Plugin.Misc.Alanube.Api.Webhooks;
using Nop.Plugin.Misc.Alanube.Domain;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeWebhookService : IAlanubeWebhookService
{
    private const string EventType = "documents.emissionFinished";
    private readonly IAlanubeDocumentService _documentService;
    private readonly IAlanubeDocumentLogService _logService;
    private readonly ILogger _logger;

    public AlanubeWebhookService(IAlanubeDocumentService documentService, IAlanubeDocumentLogService logService, ILogger logger)
    {
        _documentService = documentService;
        _logService = logService;
        _logger = logger;
    }

    public async Task<AlanubeWebhookResult> ProcessEmissionFinishedAsync(AlanubeDocumentWebhookDto payload, CancellationToken cancellationToken = default)
    {
        if (payload == null)
            return Failure("The webhook payload is required.");
        if (string.IsNullOrWhiteSpace(payload.Id))
            return Failure("The webhook document identifier is required.");
        cancellationToken.ThrowIfCancellationRequested();
        var document = await _documentService.GetByAlanubeIdAsync(payload.Id);
        if (document == null)
        {
            await _logger.WarningAsync($"Alanube webhook ignored because document '{Sanitize(payload.Id)}' was not found.");
            return new AlanubeWebhookResult { DocumentFound = false, ErrorMessage = "The Alanube document was not found." };
        }

        var mappedStatus = AlanubeWebhookStatusMapper.Map(payload.Status, payload.LegalStatus);
        var transitionApplied = mappedStatus.HasValue && AlanubeWebhookStatusMapper.CanTransition(document.DocumentStatus, mappedStatus.Value);
        if (transitionApplied)
            document.DocumentStatus = mappedStatus.Value;

        if (!string.IsNullOrWhiteSpace(payload.DocumentNumber))
            document.DocumentNumber = payload.DocumentNumber;
        if (!document.IssueDateUtc.HasValue && payload.StampDate.HasValue)
            document.IssueDateUtc = payload.StampDate.Value.UtcDateTime;
        if (document.DocumentStatus == ElectronicDocumentStatus.Authorized && !document.AuthorizedDateUtc.HasValue)
            document.AuthorizedDateUtc = (payload.SignatureDate ?? payload.StampDate)?.UtcDateTime;

        var errorText = GetErrorText(payload.Error);
        if (!string.IsNullOrWhiteSpace(errorText))
        {
            document.ErrorCode = "ALANUBE_WEBHOOK_ERROR";
            document.ErrorMessage = "Alanube reported an error in the emission-finished webhook.";
        }

        await _documentService.UpdateAsync(document);

        var unknownFinishedStatus = mappedStatus == ElectronicDocumentStatus.ManualReview;
        await _logService.InsertAsync(new AlanubeDocumentLog
        {
            AlanubeDocumentId = document.Id,
            Operation = EventType,
            Endpoint = "/plugins/alanube/webhooks/documents",
            HttpMethod = "POST",
            RequestJson = JsonSerializer.Serialize(new
            {
                payload.Type,
                payload.Id,
                payload.StampDate,
                payload.Status,
                payload.LegalStatus,
                payload.CompanyIdentification,
                payload.TrackId,
                payload.DocumentNumber,
                payload.SequenceConsumed,
                payload.SignatureDate,
                payload.DocumentStampUrl,
                HasXml = !string.IsNullOrWhiteSpace(payload.Xml),
                HasResumeXml = !string.IsNullOrWhiteSpace(payload.ResumeXml),
                HasPdf = !string.IsNullOrWhiteSpace(payload.Pdf),
                payload.GovernmentResponse,
                payload.Error
            }),
            ResponseJson = JsonSerializer.Serialize(new { Status = document.DocumentStatus.ToString(), TransitionApplied = transitionApplied }),
            Success = true,
            ErrorCode = unknownFinishedStatus ? "UNKNOWN_LEGAL_STATUS" : document.ErrorCode,
            ErrorMessage = unknownFinishedStatus ? $"Unrecognized legal status: {Sanitize(payload.LegalStatus)}" : document.ErrorMessage
        });

        return new AlanubeWebhookResult { Succeeded = true, DocumentFound = true };
    }

    private static AlanubeWebhookResult Failure(string message) => new() { ErrorMessage = message };

    private static string GetErrorText(JsonElement? error) => error.HasValue && error.Value.ValueKind is not JsonValueKind.Null and not JsonValueKind.Undefined
        ? error.Value.GetRawText()
        : null;

    private static string Sanitize(string value) => string.IsNullOrWhiteSpace(value) ? string.Empty : Truncate(value.Replace("\r", " ").Replace("\n", " "), 200);

    private static string Truncate(string value, int length) => value?.Length > length ? value[..length] : value;
}
