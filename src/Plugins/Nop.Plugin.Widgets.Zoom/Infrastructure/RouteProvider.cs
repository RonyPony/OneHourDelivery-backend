using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Plugin.Widgets.Zoom.Helpers;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Widgets.Zoom.Infrastructure
{
    /// <summary>
    /// Represents an implementation of <see cref="IRouteProvider"/> for Widget.Zoom plugin.
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        #region Properties

        ///<inheritdoc/>
        public int Priority => int.MaxValue;

        #endregion

        #region Methods

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

            endpointRouteBuilder.MapControllerRoute(ZoomDefaults.ConfigurationRouteName,
                pattern + "Plugins/Widgets/Zoom/Configure",
                new { controller = "ZoomWidget", action = "Configure", area = AreaNames.Admin });
        }

        #endregion
    }
}
