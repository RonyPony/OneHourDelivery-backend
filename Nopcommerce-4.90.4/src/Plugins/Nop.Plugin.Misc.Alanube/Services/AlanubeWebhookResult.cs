namespace Nop.Plugin.Misc.Alanube.Services;

public sealed class AlanubeWebhookResult
{
    public bool Succeeded { get; init; }
    public bool DocumentFound { get; init; }
    public string ErrorMessage { get; init; }
}
