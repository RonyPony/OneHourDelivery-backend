using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.Alanube.Api;
using Nop.Plugin.Misc.Alanube.Api.Companies;
using Nop.Plugin.Misc.Alanube.Api.Offices;
using Nop.Plugin.Misc.Alanube.Api.Catalogs;
using Nop.Plugin.Misc.Alanube.Api.Invoices;
using Nop.Web.Framework.Infrastructure.Extensions;
using Nop.Plugin.Misc.Alanube.Services;
using Nop.Plugin.Misc.Alanube.Mapping;

namespace Nop.Plugin.Misc.Alanube.Infrastructure;

/// <summary>
/// Represents the Alanube plugin startup configuration.
/// </summary>
public sealed class NopStartup : INopStartup
{
    /// <summary>
    /// Registers Alanube plugin services.
    /// </summary>
    /// <param name="services">Collection of service descriptors.</param>
    /// <param name="configuration">Application configuration.</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IAlanubeClient, AlanubeClient>().WithProxy();
        services.AddScoped<IAlanubeCompanyClient, AlanubeCompanyClient>();
        services.AddScoped<IAlanubeOfficeClient, AlanubeOfficeClient>();
        services.AddScoped<IAlanubeCatalogClient, AlanubeCatalogClient>();
        services.AddScoped<IAlanubeInvoiceClient, AlanubeInvoiceClient>();
        services.AddScoped<IAlanubeCatalogService, AlanubeCatalogService>();
        services.AddScoped<IAlanubeProductMappingService, AlanubeProductMappingService>();
        services.AddScoped<IAlanubeAddressMapper, AlanubeAddressMapper>();
        services.AddScoped<IAlanubeReceiverMapper, AlanubeReceiverMapper>();
        services.AddScoped<IAlanubeOrderItemMapper, AlanubeOrderItemMapper>();
        services.AddScoped<IAlanubeDocumentService, AlanubeDocumentService>();
        services.AddScoped<IAlanubeFiscalSequenceService, AlanubeFiscalSequenceService>();
        services.AddSingleton<IAlanubeRetryPolicy, AlanubeRetryPolicy>();
        services.AddScoped<IAlanubeDocumentLogService, AlanubeDocumentLogService>();
        services.AddScoped<IAlanubeWebhookService, AlanubeWebhookService>();
    }

    /// <summary>
    /// Configures the application pipeline for the Alanube plugin.
    /// </summary>
    /// <param name="application">Application builder.</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// Gets the startup configuration order.
    /// </summary>
    public int Order => 1;
}
