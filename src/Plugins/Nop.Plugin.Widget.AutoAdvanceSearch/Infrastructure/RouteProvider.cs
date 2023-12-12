using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Widget.AutoAdvanceSearch.Infrastructure
{
    /// <summary>
    /// Represents an implementation of <see cref="IRouteProvider"/> for this plugin. 
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        ///<inheritdoc/>
        public int Priority => int.MaxValue;

        ///<inheritdoc/>
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

            //product search
            endpointRouteBuilder.MapControllerRoute("ProductAutoAdvanceSearch", $"{pattern}search/",
                    new { controller = "AutoAdvanceSearch", action = "Search" });
        }

    }
}
