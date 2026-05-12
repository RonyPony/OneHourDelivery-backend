using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Domain.Localization;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using System.Linq;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Infrastructure
{
    /// <summary>
    /// Represents a route providerforthis plugin.
    /// </summary>
    public sealed class RouteProvider : IRouteProvider
    {
        ///<inheritdoc/>
        public int Priority => int.MaxValue;

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

            endpointRouteBuilder.MapControllerRoute("AppDeliveryWarehouses",
                pattern: "Admin/DeliveryAppShipping/SelectVendorWarehouse",
                new { controller = "DeliveryAppShipping", action = "SelectVendorWarehouse", area = AreaNames.ADMIN });

            endpointRouteBuilder.MapControllerRoute("GetPaymentFormByOrderId",
                pattern + "payment-form/{orderId:min(0)}",
                new { controller = "PaymentForm", action = "GetByOrderId" });
        }
    }
}
