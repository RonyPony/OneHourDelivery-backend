using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Invoices;

public interface IAlanubeInvoiceClient
{
    Task<AlanubeApiResponse<CreateInvoiceResponse>> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<GetInvoiceResponse>> GetInvoiceAsync(string alanubeId, CancellationToken cancellationToken = default);
}
