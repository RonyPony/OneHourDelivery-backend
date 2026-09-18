using Nop.Plugin.Misc.Alanube.Api.Webhooks;

namespace Nop.Plugin.Misc.Alanube.Services;

public interface IAlanubeWebhookService
{
    Task<AlanubeWebhookResult> ProcessEmissionFinishedAsync(AlanubeDocumentWebhookDto payload, CancellationToken cancellationToken = default);
}
