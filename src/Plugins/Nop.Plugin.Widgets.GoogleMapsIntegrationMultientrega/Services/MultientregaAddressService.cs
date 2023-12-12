using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Nop.Core.Domain.Directory;
using Nop.Data;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Models;
using Nop.Services.Directory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Models.Extensions;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Helpers;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Services
{
    /// <summary>
    /// Multi entrega Address Service.
    /// </summary>
    public class MultientregaAddressService : IMultientregaAddressService
    {
        #region Fields

        private readonly IAddressGeoCoordinatesService _addressGeoCoordinatesService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IRepository<MultientregaDistrictMapping> _multientregaDistrictMappingRepository;
        private readonly IRepository<MultientregaNeighborhoodMapping> _multientregNeighborhoodMappingRepository;
        private readonly IRepository<MultientregaProvinceMapping> _multientregaProvinceMappingRepository;
        private readonly IRepository<MultientregaProvinceStateProvinceMapping> _multientregaProvinceStateProvinceMappingRepository;
        private readonly IRepository<MultientregaTownshipMapping> _multientregaTownshipMappingRepository;
        private readonly PluginConfigurationSettings _pluginConfigurationSettings;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        /// <summary>
        /// Initializes a new instance of <see cref="MultientregaAddressService"/>.
        /// </summary>
        /// <param name="addressGeoCoordinatesService">An implementation of <see cref="IAddressGeoCoordinatesService"/>.</param>
        /// <param name="countryService">An implementation of <see cref="ICountryService"/>.</param>
        /// <param name="stateProvinceService">An implementation of <see cref="IStateProvinceService"/>.</param>
        /// <param name="multientregaDistrictMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> whee TEntity is <see cref="MultientregaDistrictMapping"/>.</param>
        /// <param name="multientregNeighborhoodMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> whee TEntity is <see cref="MultientregaNeighborhoodMapping"/>.</param>
        /// <param name="multientregaProvinceMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> whee TEntity is <see cref="MultientregaProvinceMapping"/>.</param>
        /// <param name="multientregaProvinceStateProvinceMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> whee TEntity is <see cref="MultientregaProvinceStateProvinceMapping"/>.</param>
        /// <param name="multientregaTownshipMappingRepository">An implementation of <see cref="IRepository{TEntity}"/> whee TEntity is <see cref="MultientregaTownshipMapping"/>.</param>
        /// <param name="pluginConfigurationSettings">An implementation of <see cref="PluginConfigurationSettings"/>.</param>
        /// <param name="localizationService">An implementation of <see cref="ILocalizationService"/>.</param>
        public MultientregaAddressService(
            IAddressGeoCoordinatesService addressGeoCoordinatesService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IRepository<MultientregaDistrictMapping> multientregaDistrictMappingRepository,
            IRepository<MultientregaNeighborhoodMapping> multientregNeighborhoodMappingRepository,
            IRepository<MultientregaProvinceMapping> multientregaProvinceMappingRepository,
            IRepository<MultientregaProvinceStateProvinceMapping> multientregaProvinceStateProvinceMappingRepository,
            IRepository<MultientregaTownshipMapping> multientregaTownshipMappingRepository,
            PluginConfigurationSettings pluginConfigurationSettings,
            ILocalizationService localizationService)
        {
            _addressGeoCoordinatesService = addressGeoCoordinatesService;
            _countryService = countryService;
            _stateProvinceService = stateProvinceService;
            _multientregaDistrictMappingRepository = multientregaDistrictMappingRepository;
            _multientregNeighborhoodMappingRepository = multientregNeighborhoodMappingRepository;
            _multientregaProvinceMappingRepository = multientregaProvinceMappingRepository;
            _multientregaProvinceStateProvinceMappingRepository = multientregaProvinceStateProvinceMappingRepository;
            _multientregaTownshipMappingRepository = multientregaTownshipMappingRepository;
            _pluginConfigurationSettings = pluginConfigurationSettings;
            _localizationService = localizationService;
        }

        #endregion

        #region Utilities

        private bool IsPanamaCountryId(string countryId)
        {
            bool result = int.TryParse(countryId, out int id);
            if (!result) return false;
            Country country = _countryService.GetCountryById(id);
            if (country is null) return false;

            return country.Name == "Panama";
        }

        private IList<MultientregaProvince> GetProvinces(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", token);
            Task<HttpResponseMessage> response = client.GetAsync($"{_pluginConfigurationSettings.BaseUrl}market/zonas/provincias");
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception($"Error getting provinces from multientrega API. Status code: {response.Result.StatusCode}");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var provinces = JsonConvert.DeserializeObject<List<MultientregaProvince>>(jsonResponseTask.Result);

            return provinces;
        }

        private IList<MultientregaDistrict> GetDistrictsByProvinceId(string provinceId, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", token);
            string jsonBody = JsonConvert.SerializeObject(new { id_provincia = provinceId });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync($"{_pluginConfigurationSettings.BaseUrl}market/zonas/distritos", content);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception($"Error getting distritos from multientrega API. Status code: {response.Result.StatusCode}");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var districts = JsonConvert.DeserializeObject<List<MultientregaDistrict>>(jsonResponseTask.Result);

            return districts;
        }

        private IList<MultientregaCorregimiento> GetTownshipsByDistrictId(string districtId, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", token);
            string jsonBody = JsonConvert.SerializeObject(new { id_distrito = districtId });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync($"{_pluginConfigurationSettings.BaseUrl}market/zonas/corregimientos", content);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception($"Error getting corregimientos from multientrega API. Status code: {response.Result.StatusCode}");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var townships = JsonConvert.DeserializeObject<List<MultientregaCorregimiento>>(jsonResponseTask.Result);

            return townships;
        }

        private IList<MultientregaNeighborhood> GetNeighborhoodsByTownshipId(string townshipId, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", token);
            string jsonBody = JsonConvert.SerializeObject(new { id_corregimiento = townshipId });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client.PostAsync($"{_pluginConfigurationSettings.BaseUrl}market/zonas/barrios", content);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception($"Error getting barrios from multientrega API. Status code: {response.Result.StatusCode}");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var neighborhoods = JsonConvert.DeserializeObject<List<MultientregaNeighborhood>>(jsonResponseTask.Result);

            return neighborhoods;
        }

        private void RegisterProvinces(IList<MultientregaProvince> provinces, string token)
        {
            foreach (MultientregaProvince province in provinces)
            {
                MultientregaProvinceMapping foundMapping = _multientregaProvinceMappingRepository.Table
                    .FirstOrDefault(mapping => mapping.MultientregaId == province.Id.ToString());

                if (foundMapping == null)
                {
                    _multientregaProvinceMappingRepository.Insert(new MultientregaProvinceMapping
                    {
                        MultientregaId = province.Id,
                        Name = province.Nombre
                    });
                }

                RegisterMultientregaProvinceStateProvinceMapping(province);

                IList<MultientregaDistrict> districts = GetDistrictsByProvinceId(province.Id, token);
                RegisterDistricts(districts, token);
            }
        }

        private void RegisterMultientregaProvinceStateProvinceMapping(MultientregaProvince province)
        {
            if (_multientregaProvinceStateProvinceMappingRepository.Table.FirstOrDefault(mapping => mapping.MultientregaProvinceId == province.Id) is null)
            {
                Country country = _countryService.GetCountryByTwoLetterIsoCode("PA");
                if (country is null) return;
                IList<StateProvince> stateProvinces = _stateProvinceService.GetStateProvincesByCountryId(country.Id);

                if (stateProvinces.FirstOrDefault(state => state.Name == province.Nombre) is StateProvince state)
                {
                    _multientregaProvinceStateProvinceMappingRepository.Insert(new MultientregaProvinceStateProvinceMapping
                    {
                        StateProvinceId = state.Id,
                        MultientregaProvinceId = province.Id
                    });
                }
                else
                {
                    var newState = new StateProvince
                    {
                        Name = province.Nombre,
                        Abbreviation = $"PAN-{province.Id}",
                        CountryId = country.Id,
                        Published = true,
                        DisplayOrder = 0
                    };

                    _stateProvinceService.InsertStateProvince(newState);

                    _multientregaProvinceStateProvinceMappingRepository.Insert(new MultientregaProvinceStateProvinceMapping
                    {
                        StateProvinceId = newState.Id,
                        MultientregaProvinceId = province.Id
                    });
                }
            }
        }

        private void RegisterDistricts(IList<MultientregaDistrict> districts, string token)
        {
            foreach (MultientregaDistrict district in districts)
            {
                MultientregaDistrictMapping foundMapping = _multientregaDistrictMappingRepository.Table
                    .FirstOrDefault(mapping => mapping.MultientregaId == district.Id.ToString());

                if (foundMapping == null)
                {
                    _multientregaDistrictMappingRepository.Insert(new MultientregaDistrictMapping
                    {
                        MultientregaId = district.Id.ToString(),
                        MultientregaProvinceId = district.ProvinciaId.ToString(),
                        Name = district.Nombre
                    });
                }

                IList<MultientregaCorregimiento> townships = GetTownshipsByDistrictId(district.Id.ToString(), token);
                RegisterTownships(townships, token);
            }
        }

        private void RegisterTownships(IList<MultientregaCorregimiento> townships, string token)
        {
            foreach (MultientregaCorregimiento township in townships)
            {
                MultientregaTownshipMapping foundMapping = _multientregaTownshipMappingRepository.Table
                    .FirstOrDefault(mapping => mapping.MultientregaId == township.Id.ToString());

                if (foundMapping == null)
                {
                    _multientregaTownshipMappingRepository.Insert(new MultientregaTownshipMapping
                    {
                        MultientregaId = township.Id.ToString(),
                        MultientregaDistrictId = township.DistritoId.ToString(),
                        Name = township.Nombre
                    });
                }

                IList<MultientregaNeighborhood> neighborhoods = GetNeighborhoodsByTownshipId(township.Id.ToString(), token);
                RegisterNeigborhoods(neighborhoods);
            }
        }

        private void RegisterNeigborhoods(IList<MultientregaNeighborhood> neighborhoods)
        {
            foreach (MultientregaNeighborhood neighborhood in neighborhoods)
            {
                MultientregaNeighborhoodMapping foundMapping = _multientregNeighborhoodMappingRepository.Table
                    .FirstOrDefault(mapping => mapping.MultientregaId == neighborhood.Id.ToString());

                if (foundMapping == null)
                {
                    _multientregNeighborhoodMappingRepository.Insert(new MultientregaNeighborhoodMapping
                    {
                        MultientregaId = neighborhood.Id.ToString(),
                        MultientregaTownshipId = neighborhood.CorregimientoId.ToString(),
                        Name = neighborhood.Nombre,
                        FullName = neighborhood.FullName,
                        Status = neighborhood.Estado,
                        LocXLon = neighborhood.LocXLon,
                        LocYLat = neighborhood.LocYLat,
                        UsrCrea = neighborhood.UsrCrea,
                        DateCrea = neighborhood.FechaCrea.ToString(),
                        UsrModifica = neighborhood.UsrModifica,
                        DateModifica = neighborhood.FechaModifica.ToString()
                    });
                }
            }
        }

        private IList<SelectListItem> GetSelectListItemFromList(IList objects)
        {
            var selectListItem = new List<SelectListItem>();

            if (objects.Count > 0)
            {
                selectListItem.Add(new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true, Selected = true });
                foreach (object obj in objects)
                {
                    PropertyInfo nameProperty = obj.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                    PropertyInfo idProperty = obj.GetType().GetProperty("MultientregaId", BindingFlags.Public | BindingFlags.Instance);
                    selectListItem.Add(new SelectListItem
                    {
                        Value = idProperty.GetValue(obj).ToString(),
                        Text = nameProperty.GetValue(obj).ToString(),
                        Disabled = false,
                        Selected = false
                    });
                }
            }
            else
            {
                selectListItem.Add(new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true });
            }

            return selectListItem;
        }

        #endregion

        #region Methods

        ///<inheritdoc/>
        public bool MultientregaIsConfigured()
        {
            return !string.IsNullOrWhiteSpace(_pluginConfigurationSettings.Email)
                && !string.IsNullOrWhiteSpace(_pluginConfigurationSettings.Password)
                && !string.IsNullOrWhiteSpace(_pluginConfigurationSettings.BaseUrl);
        }

        ///<inheritdoc/>
        public void RegisterMultientregaTerritorialStructure()
        {
            if (!MultientregaIsConfigured())
                throw new Exception("Multientrega is not configured correctly.");
            var token = GetToken();
            IList<MultientregaProvince> provinces = GetProvinces(token);
            RegisterProvinces(provinces, token);
        }

        ///<inheritdoc/>
        public string GetToken()
        {
            using var client = new HttpClient();
            Task<HttpResponseMessage> response = client.PostAsync($"{_pluginConfigurationSettings.BaseUrl}token/generate?email={_pluginConfigurationSettings.Email}&password={_pluginConfigurationSettings.Password}", null);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception($"Error getting token for multientrega API. Status code: {response.Result.StatusCode}");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var responseResult = JsonConvert.DeserializeObject<MultientregaTokenResponse>(jsonResponseTask.Result);
            if (!responseResult.Success)
                throw new Exception($"Error getting token for multientrega API. API error message: {responseResult.Error}");

            return $"Bearer {responseResult.Data.Token}";
        }

        ///<inheritdoc/>
        public decimal GetTaxRateByAddressId(int addressId)
        {
            if (addressId == 0) throw new Exception("The address is invalid.");
            AddressGeoCoordinatesMapping mapping = _addressGeoCoordinatesService.GetAddressGeoCoordinates(addressId);
            if (mapping is null) throw new Exception("The address is invalid.");
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Access-Token", GetToken());
            string jsonBody = JsonConvert.SerializeObject(new { nit = _pluginConfigurationSettings.Nit });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> response = client
                .PostAsync($"{_pluginConfigurationSettings.BaseUrl}market/zonas/shipping_value?id_barrio={mapping.NeighborhoodId}&id_sucursal={_pluginConfigurationSettings.BranchOffice}", content);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
                throw new Exception("Error getting tax rate from Multientrega's API.");
            Task<string> jsonResponseTask = response.Result.Content.ReadAsStringAsync();
            jsonResponseTask.Wait();
            var responseResult = JsonConvert.DeserializeObject<MultientregaTaxRateResponse>(jsonResponseTask.Result);

            return responseResult.Tarifa;
        }

        ///<inheritdoc/>
        public MultientregaAddressStructureModel GetMultientregaAddressStructureByAddressId(int addressId)
        {
            AddressGeoCoordinatesMapping mapping = _addressGeoCoordinatesService.GetAddressGeoCoordinates(addressId);
            if (mapping == null) return null;
            return new MultientregaAddressStructureModel
            {
                ProvinceName = _multientregaProvinceMappingRepository.Table.FirstOrDefault(p => p.MultientregaId == mapping.ProvinceId)?.Name ?? "",
                DistrictName = _multientregaDistrictMappingRepository.Table.FirstOrDefault(d => d.MultientregaId == mapping.DistrictId)?.Name ?? "",
                TownshipName = _multientregaTownshipMappingRepository.Table.FirstOrDefault(t => t.MultientregaId == mapping.TownshipId)?.Name ?? "",
                NeighborhoodName = _multientregNeighborhoodMappingRepository.Table.FirstOrDefault(n => n.MultientregaId == mapping.NeighborhoodId)?.Name ?? ""
            };
        }

        ///<inheritdoc/>
        public string GetProvinceNameById(string provinceId)
        {
            if (string.IsNullOrWhiteSpace(provinceId)) return string.Empty;
            return _multientregaProvinceMappingRepository.Table.FirstOrDefault(p => p.MultientregaId == provinceId)?.Name ?? string.Empty;
        }

        ///<inheritdoc/>
        public int? GetStateProvinceIdByProvinceId(string provinceId)
            => _multientregaProvinceStateProvinceMappingRepository.Table.FirstOrDefault(mapping => mapping.MultientregaProvinceId == provinceId)?.StateProvinceId;

        #region Select List Items

        ///<inheritdoc/>
        public IList<SelectListItem> GetProvincesSelectListItems(string countryId, bool validateCountry = true)
        {
            if (validateCountry && !IsPanamaCountryId(countryId))
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true }
                };

            List<MultientregaProvinceMapping> provinces = _multientregaProvinceMappingRepository.Table.ToList();

            return GetSelectListItemFromList(provinces);
        }

        ///<inheritdoc/>
        public IList<SelectListItem> GetDistrictsSelectListItems(string provinceId)
        {
            if (provinceId.Equals("0"))
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true }
                };

            List<MultientregaDistrictMapping> districts = _multientregaDistrictMappingRepository.Table
                .Where(mapping => mapping.MultientregaProvinceId == provinceId).ToList();

            return GetSelectListItemFromList(districts);
        }

        ///<inheritdoc/>
        public IList<SelectListItem> GetTownshipsSelectListItems(string districtId)
        {
            if (districtId.Equals("0"))
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true }
                };

            List<MultientregaTownshipMapping> townships = _multientregaTownshipMappingRepository.Table
                .Where(mapping => mapping.MultientregaDistrictId == districtId).ToList();

            return GetSelectListItemFromList(townships);
        }

        ///<inheritdoc/>
        public IList<SelectListItem> GetNeigborhoodsSelectListItems(string townshipId)
        {
            if (townshipId.Equals("0"))
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "0", Text = _localizationService.GetResource($"{Defaults.ResourcesNamePrefix}.Inputs.SelectAnOption"), Disabled = true }
                };

            List<MultientregaNeighborhoodMapping> neighborhoods = _multientregNeighborhoodMappingRepository.Table
                .Where(mapping => mapping.MultientregaTownshipId == townshipId).ToList();

            return GetSelectListItemFromList(neighborhoods);
        }

        #endregion

        #region List Models for Grids

        ///<inheritdoc/>
        public ProvinceMappingListModel PrepareProvinceListModel(MultientregaSearchModel searchModel)
        {
            var provinces = new PagedList<MultientregaProvinceMapping>(_multientregaProvinceMappingRepository.Table, searchModel.Page - 1, searchModel.PageSize);
            var model = new ProvinceMappingListModel().PrepareToGrid(searchModel, provinces, () =>
            {
                return provinces.Select(province =>
                {
                    return new ProvinceMappingModel
                    {
                        MultientregaId = province.MultientregaId,
                        Name = province.Name
                    };
                });
            });

            return model;
        }

        ///<inheritdoc/>
        public DistrictMappingListModel PrepareDistrictListModel(MultientregaSearchModel searchModel)
        {
            var districts = new PagedList<MultientregaDistrictMapping>(_multientregaDistrictMappingRepository.Table, searchModel.Page - 1, searchModel.PageSize);
            var model = new DistrictMappingListModel().PrepareToGrid(new MultientregaSearchModel(), districts, () =>
            {
                return districts.Select(district =>
                {
                    return new DistrictMappingModel
                    {
                        MultientregaId = district.MultientregaId,
                        MultientregaProvinceId = district.MultientregaProvinceId,
                        Name = district.Name
                    };
                });
            });

            return model;
        }

        ///<inheritdoc/>
        public TownshipMappingListModel PrepareTownshipListModel(MultientregaSearchModel searchModel)
        {
            var townships = new PagedList<MultientregaTownshipMapping>(_multientregaTownshipMappingRepository.Table, searchModel.Page - 1, searchModel.PageSize);
            var model = new TownshipMappingListModel().PrepareToGrid(new MultientregaSearchModel(), townships, () =>
            {
                return townships.Select(township =>
                {
                    return new TownshipMappingModel
                    {
                        MultientregaId = township.MultientregaId,
                        MultientregaDistrictId = township.MultientregaDistrictId,
                        Name = township.Name
                    };
                });
            });

            return model;
        }

        ///<inheritdoc/>
        public NeighborhoodMappingListModel PrepareNeighborhoodListModel(MultientregaSearchModel searchModel)
        {
            var neighborhoods = new PagedList<MultientregaNeighborhoodMapping>(_multientregNeighborhoodMappingRepository.Table, searchModel.Page - 1, searchModel.PageSize);
            var model = new NeighborhoodMappingListModel().PrepareToGrid(new MultientregaSearchModel(), neighborhoods, () =>
            {
                return neighborhoods.Select(neighborhood =>
                {
                    return new NeighborhoodMappingModel
                    {
                        MultientregaId = neighborhood.MultientregaId,
                        MultientregaTownshipId = neighborhood.MultientregaTownshipId,
                        Name = neighborhood.Name,
                        FullName = neighborhood.FullName,
                        Status = neighborhood.Status,
                        LocXLon = neighborhood.LocXLon,
                        LocYLat = neighborhood.LocYLat,
                        UsrCrea = neighborhood.UsrCrea,
                        DateCrea = DateTime.Parse(neighborhood.DateCrea),
                        UsrModifica = neighborhood.UsrModifica,
                        DateModifica = DateTime.Parse(neighborhood.DateCrea)
                    };
                });
            });

            return model;
        }

        #endregion

        #endregion
    }
}
