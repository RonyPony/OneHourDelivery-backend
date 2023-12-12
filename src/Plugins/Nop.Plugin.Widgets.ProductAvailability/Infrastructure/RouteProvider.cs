using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Widgets.ProductAvailability.Infrastructure
{
    /// <summary>
    /// Plugin routes provider
    /// </summary>
    public class RouteProvider : IRouteProvider
    {
        #region Properties

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => int.MaxValue;

        #endregion

        #region Methods

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
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

            endpointRouteBuilder.MapControllerRoute(ProductAvailabilityDefault.ConfigurationRouteName,
                pattern + "Admin/ProductAvailability/Configure",
                new { controller = "ProductAvailability", action = "Configure", area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute("InventorySearch",
                pattern + ProductAvailabilityDefault.InventorySearchTopicPageSeName,
                new { controller = "ProductAvailability", action = "SearchInventory" });

            endpointRouteBuilder.MapControllerRoute("MultiStepCheckoutShippingMethod",
                pattern + "checkout/shippingmethod",
                new { controller = "AvailabilityCheckout", action = "ShippingMethod" });

            endpointRouteBuilder.MapControllerRoute("AvailabilityCheckoutOnePage",
                pattern + "onepagecheckout/",
                new { controller = "AvailabilityCheckout", action = "OnePageCheckout" });
        }

        #endregion
    }
}