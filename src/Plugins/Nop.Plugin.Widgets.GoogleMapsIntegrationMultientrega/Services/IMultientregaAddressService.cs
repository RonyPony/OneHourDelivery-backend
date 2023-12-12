using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services
{
    /// <summary>
    /// Represents a contract for Multientrega address services.
    /// </summary>
    public interface IMultientregaAddressService
    {
        /// <summary>
        /// Retrieves if the Multientrega service is configured correctly.
        /// </summary>
        /// <returns><see cref="true"/> if Multientrega is configured; <see cref="false"/> otherwise.</returns>
        bool MultientregaIsConfigured();

        /// <summary>
        /// Inserts or updates territorial structure from Multientrega service.
        /// </summary>
        void RegisterMultientregaTerritorialStructure();

        /// <summary>
        /// Retrieves an access token for Multientrega services.
        /// </summary>
        /// <returns>An string containing the token.</returns>
        string GetToken();

        /// <summary>
        /// Retrieves the tax rate for an address.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        /// <returns>A <see cref="decimal"/> with the tax rate amount.</returns>
        decimal GetTaxRateByAddressId(int addressId);

        /// <summary>
        /// Retrieves the rgistered Multientrega address structure for an address.
        /// </summary>
        /// <param name="addressId">The address id.</param>
        /// <returns>An instance of <see cref="MultientregaAddressStructureModel"/>.</returns>
        MultientregaAddressStructureModel GetMultientregaAddressStructureByAddressId(int addressId);

        /// <summary>
        /// Retrieves a list of Multientrega provinces.
        /// </summary>
        /// <param name="countryId">The country id used to validate that the selected country is Panama.</param>
        /// <param name="validateCountryId">A <see cref="bool"/> to indicate if country validation is required.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.</returns>
        IList<SelectListItem> GetProvincesSelectListItems(string countryId, bool validateCountryId = true);

        /// <summary>
        /// Retrieves a list of Multientrega districts by province id.
        /// </summary>
        /// <param name="provinceId">The province id.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.</returns>
        IList<SelectListItem> GetDistrictsSelectListItems(string provinceId);

        /// <summary>
        /// Retrieves a list of Multientrega townships by district id.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.</returns>
        IList<SelectListItem> GetTownshipsSelectListItems(string districtId);

        /// <summary>
        /// Retrieves a list of Multientrega neighborhoods by township id.
        /// </summary>
        /// <param name="townshipId">The township id.</param>
        /// <returns>An implementation of <see cref="IList{T}"/> where T is <see cref="SelectListItem"/>.</returns>
        IList<SelectListItem> GetNeigborhoodsSelectListItems(string townshipId);

        /// <summary>
        /// Retrieves a list model for Multientrega provinces.
        /// </summary>
        /// <param name="searchModel">An instance of <see cref="MultientregaSearchModel"/>.</param>
        /// <returns>An instance of <see cref="ProvinceMappingListModel"/>.</returns>
        ProvinceMappingListModel PrepareProvinceListModel(MultientregaSearchModel searchModel);

        /// <summary>
        /// Retrieves a list model for Multientrega districts.
        /// </summary>
        /// <param name="searchModel">An instance of <see cref="MultientregaSearchModel"/>.</param>
        /// <returns>An instance of <see cref="DistrictMappingListModel"/>.</returns>
        DistrictMappingListModel PrepareDistrictListModel(MultientregaSearchModel searchModel);

        /// <summary>
        /// Retrieves a list model for Multientrega townships.
        /// </summary>
        /// <param name="searchModel">An instance of <see cref="MultientregaSearchModel"/>.</param>
        /// <returns>An instance of <see cref="TownshipMappingListModel"/>.</returns>
        TownshipMappingListModel PrepareTownshipListModel(MultientregaSearchModel searchModel);

        /// <summary>
        /// Retrieves a list model for Multientrega neighborhoods.
        /// </summary>
        /// <param name="searchModel">An instance of <see cref="MultientregaSearchModel"/>.</param>
        /// <returns>An instance of <see cref="NeighborhoodMappingListModel"/>.</returns>
        NeighborhoodMappingListModel PrepareNeighborhoodListModel(MultientregaSearchModel searchModel);

        /// <summary>
        /// Retrieves a procince's name by it's id.
        /// </summary>
        /// <param name="provinceId">The province id.</param>
        /// <returns>A <see cref="string"/> with the name of the province or a <see cref="string.Empty"/> if not found.</returns>
        string GetProvinceNameById(string provinceId);

        /// <summary>
        /// Retrieves the <see cref="StateProvince"/> id by it's Multientrega's province id.
        /// </summary>
        /// <param name="provinceId">The Multientrega's province id.</param>
        /// <returns>An <see cref="int"/> with the state province id or <see cref="null"/> if not found.</returns>
        int? GetStateProvinceIdByProvinceId(string provinceId);
    }
}
