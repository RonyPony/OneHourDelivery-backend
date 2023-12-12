using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.contracts;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app google direction services.
    /// </summary>
    public interface IGoogleDirectionService
    {
        /// <summary>
        /// Retrives a google direction resource to consume in frontend.
        /// </summary>
        /// <param name="directionRequest"></param>
        /// <returns>json google source.</returns>
        IOperationResult<GoogleDirectionResourceModel> GetGoogleDirections(GoogleDirecctionRequest directionRequest);

        /// <summary>
        ///  Retrives a google direction polyline resource to consume in frontend.
        /// </summary>
        /// <param name="orderDeliveryStatus"></param>
        /// <returns>google polyline source.</returns>
        string GetGoogleDirectionPolyline(OrderDeliveryStatusMapping orderDeliveryStatus);

        /// <summary>
        /// Gets distance info from one point to other point
        /// </summary>
        /// <param name="latitudePoint1">Origin place latitude</param>
        /// <param name="longitudePoint1">Origin place longitude</param>
        /// <param name="latitudePoint2">Destination place latitude</param>
        /// <param name="longitudePoint2">Destination place longitude</param>
        /// <returns>An implementation of <see cref="IOperationResult{decimal}"/></returns>
        IOperationResult<decimal> GetDistanceBetweenPoints(decimal latitudePoint1, decimal longitudePoint1, decimal latitudePoint2, decimal longitudePoint2);
    }
}
