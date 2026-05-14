using Nop.Core.Domain.Common;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Services
{
    /// <summary>
    /// Represents a contract for the delivery app address services.
    /// </summary>
    public interface IDeliveryAppAddressService
    {
        /// <summary>
        /// Calculates the distance in between the geo-coordinates of a given address and a given geo-coordinates.
        /// </summary>
        /// <param name="address">The adrees greo-coordinates.</param>
        /// <param name="latitud">The latitude of the geo-coordinate.</param>
        /// <param name="longitud">The latitude of the geo-coordinate.</param>
        /// <returns>A <see cref="decimal"/> with distance in meters.</returns>
        double GetDistanceOnMeters(AddressGeoCoordinatesMapping address, decimal latitud, decimal longitud);

        /// <summary>
        /// Evaluates if two addresses have the same values.
        /// </summary>
        /// <param name="addressA">The address A to evaluate.</param>
        /// <param name="addressB">The address B to evluate.</param>
        /// <returns>A <see cref="bool"/> indicating if both addresses have the same values.</returns>
        /// <exception cref="System.ArgumentException">Thrown when either <paramref name="addressA"/> or <paramref name="addressB"/> have null value.</exception>
        bool AreAddressesDuplicated(Address addressA, Address addressB);

        /// <summary>
        /// Retrieves a list of addresses by the customer id.
        /// </summary>
        /// <param name="id">The customer id.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="DeliveryAppAddressModel"/>.</returns>
        IList<DeliveryAppAddressModel> GetAddressesByCustomerId(int id);

        /// <summary>
        /// Inserts a new customer address and returns the id of the new address.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <param name="model">An instance of <see cref="DeliveryAppAddressModel"/>.</param>
        /// <returns>The id of the new address.</returns>
        int InsertNewCustomerAddress(int customerId, DeliveryAppAddressModel model);

        /// <summary>
        /// Update customer address and returns the id of the address updated.
        /// </summary>
        /// <param name="customerId">The customer id</param>
        /// <param name="model">An instance of <see cref="DeliveryAppAddressModel"/>.</param>
        /// <returns>the id of the address updated.</returns>
        int UpdateCustomerAddress(int customerId, DeliveryAppAddressModel model);
    }
}
