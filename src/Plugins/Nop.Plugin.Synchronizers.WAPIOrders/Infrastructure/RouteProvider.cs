using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Plugin.Synchronizers.WAPIOrders.Helpers;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Synchronizers.WAPIOrders.Infrastructure
{
    /// <summary>
    /// Represents a route provider for WAPI Orders Synchronizer.
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Gets a priority for this route provider.
        /// </summary>
        public int Priority => int.MaxValue;

        /// <summary>
        /// Register the required routes for this plugin.
        /// </summary>
        /// <param name="endpointRouteBuilder">An implementation of <see cref="IEndpointRouteBuilder"/>.</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var pattern = string.Empty;
            if (DataSettingsManager.DatabaseIsInstalled)
            {
                var localizationSettings = endpointRouteBuilder.ServiceProvider.GetRequiredService<LocalizationSettings>();
                if (localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
                {
                    var langservice = endpointRouteBuilder.ServiceProvider.GetRequiredService<ILanguageService>();
                    var languages = langservice.GetAllLanguages().ToList();
                    pattern = "{language:lang=" + languages.FirstOrDefault().UniqueSeoCode + "}/";
                }
            }

            endpointRouteBuilder.MapControllerRoute(Defaults.ConfigurationRouteName,
                pattern + "Plugins/Synchronizer/WapiOrders/Configure",
                new { controller = "WapiOrders", action = "Configure", area = AreaNames.Admin });
        }
    }
}
