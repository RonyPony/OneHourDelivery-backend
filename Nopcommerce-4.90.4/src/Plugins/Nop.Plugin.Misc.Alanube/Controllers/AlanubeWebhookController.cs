using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Misc.Alanube.Api.Webhooks;
using Nop.Plugin.Misc.Alanube.Services;
using Nop.Services.Logging;

namespace Nop.Plugin.Misc.Alanube.Controllers;

[ApiController]
[IgnoreAntiforgeryToken]
[Route("plugins/alanube/webhooks")]
public sealed class AlanubeWebhookController : ControllerBase
{
    private readonly AlanubeSettings _settings;
    private readonly IAlanubeWebhookService _webhookService;
    private readonly ILogger _logger;

    public AlanubeWebhookController(AlanubeSettings settings, IAlanubeWebhookService webhookService, ILogger logger)
    {
        _settings = settings;
        _webhookService = webhookService;
        _logger = logger;
    }

    [HttpPost("documents")]
    [HttpPost]
    public async Task<IActionResult> Documents([FromBody] AlanubeDocumentWebhookDto payload, CancellationToken cancellationToken)
    {
        if (!_settings.Enabled || !_settings.EnableWebhook || string.IsNullOrWhiteSpace(_settings.WebhookSecret))
        {
            await _logger.WarningAsync("Alanube webhook rejected because webhooks are disabled or its secret is not configured.");
            return Unauthorized();
        }

        if (!Request.Headers.TryGetValue(AlanubeDefaults.WebhookHeaderName, out var headerValues) ||
            headerValues.Count != 1 || !SecretsEqual(headerValues[0], _settings.WebhookSecret))
            return Unauthorized();

        var result = await _webhookService.ProcessEmissionFinishedAsync(payload, cancellationToken);
        if (!result.DocumentFound && !string.IsNullOrWhiteSpace(payload?.Id))
            return NotFound(new { error = result.ErrorMessage });
        if (!result.Succeeded)
            return BadRequest(new { error = result.ErrorMessage });

        return Ok(new { received = true });
    }

    private static bool SecretsEqual(string supplied, string configured)
    {
        var suppliedHash = SHA256.HashData(Encoding.UTF8.GetBytes(supplied ?? string.Empty));
        var configuredHash = SHA256.HashData(Encoding.UTF8.GetBytes(configured ?? string.Empty));
        return CryptographicOperations.FixedTimeEquals(suppliedHash, configuredHash);
    }
}
