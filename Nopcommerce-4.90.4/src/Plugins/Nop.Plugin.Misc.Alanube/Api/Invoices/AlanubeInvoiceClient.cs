using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Invoices;

public sealed class AlanubeInvoiceClient : IAlanubeInvoiceClient
{
    private readonly IAlanubeClient _alanubeClient;
    private readonly AlanubeSettings _settings;

    public AlanubeInvoiceClient(IAlanubeClient alanubeClient, AlanubeSettings settings)
    {
        _alanubeClient = alanubeClient;
        _settings = settings;
    }

    public Task<AlanubeApiResponse<CreateInvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _alanubeClient.PostAsync<CreateInvoiceRequest, CreateInvoiceResponse>("invoices", request, BuildCompanyOfficeParameters(), cancellationToken);
    }

    public Task<AlanubeApiResponse<GetInvoiceResponse>> GetInvoiceAsync(string alanubeId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(alanubeId))
            throw new ArgumentException("An Alanube invoice identifier is required.", nameof(alanubeId));
        return _alanubeClient.GetAsync<GetInvoiceResponse>($"invoices/{Uri.EscapeDataString(alanubeId)}", BuildCompanyParameters(), cancellationToken);
    }

    private IReadOnlyCollection<AlanubeQueryParameter> BuildCompanyOfficeParameters()
    {
        var parameters = new List<AlanubeQueryParameter>();
        if (!string.IsNullOrWhiteSpace(_settings.CompanyId))
            parameters.Add(new AlanubeQueryParameter("idCompany", _settings.CompanyId));
        if (!string.IsNullOrWhiteSpace(_settings.OfficeId))
            parameters.Add(new AlanubeQueryParameter("idOffice", _settings.OfficeId));
        return parameters;
    }

    private IReadOnlyCollection<AlanubeQueryParameter> BuildCompanyParameters() =>
        string.IsNullOrWhiteSpace(_settings.CompanyId)
            ? []
            : [new AlanubeQueryParameter("idCompany", _settings.CompanyId)];
}
