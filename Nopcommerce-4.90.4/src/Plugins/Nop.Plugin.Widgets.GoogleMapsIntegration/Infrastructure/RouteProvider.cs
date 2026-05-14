using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Helpers;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Infrastructure
{
    /// <summary>
    /// Plugin routes provider.
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
            if (DataSettingsManager.IsDatabaseInstalled())
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
                pattern + "Plugins/Widgets/GoogleMapsIntegration/Configure",
                new { controller = "GoogleMapsIntegration", action = "Configure", area = AreaNames.ADMIN });

            endpointRouteBuilder.MapControllerRoute("CustomerAddressDelete",
                pattern + "customer/addressdelete",
                new { controller = "GoogleMapsIntegration", action = "AddressDelete" });

            endpointRouteBuilder.MapControllerRoute("CustomerAddressEdit",
                pattern + "customer/addressedit/{addressId:min(0)}",
                new { controller = "GoogleMapsIntegration", action = "AddressEdit" });

            endpointRouteBuilder.MapControllerRoute("CustomerAddressAdd",
                pattern + "customer/addressadd",
                new { controller = "GoogleMapsIntegration", action = "AddressAdd" });

            endpointRouteBuilder.MapControllerRoute("GMICheckoutBillingAdd",
                pattern + "checkout/billingaddress",
                new { controller = "GoogleMapsIntegration", action = "BillingAddress" });

            endpointRouteBuilder.MapControllerRoute("GMICheckoutShippinggAdd",
                pattern + "checkout/shippingaddress",
                new { controller = "GoogleMapsIntegration", action = "ShippingAddress" });

            endpointRouteBuilder.MapControllerRoute("GMICheckoutOnePage",
                pattern + "onepagecheckout/",
                new { controller = "GoogleMapsIntegration", action = "OnePageCheckout" });

            endpointRouteBuilder.MapControllerRoute(name: "GMICreateWarehouse",
                pattern: "Admin/Shipping/CreateWarehouse",
                new { controller = "AdminGoogleMapsIntegration", action = "CreateWarehouse", area = AreaNames.ADMIN });

            endpointRouteBuilder.MapControllerRoute(name: "GMIEditWarehouse",
                pattern: "Admin/Shipping/EditWarehouse/{id:min(0)}",
                new { controller = "AdminGoogleMapsIntegration", action = "EditWarehouse", area = AreaNames.ADMIN });

            endpointRouteBuilder.MapControllerRoute(name: "GMIDeleteWarehouse",
                pattern: "Admin/Shipping/DeleteWarehouse/{id:min(0)}",
                new { controller = "AdminGoogleMapsIntegration", action = "DeleteWarehouse", area = AreaNames.ADMIN });
        }

        #endregion
    }
}
