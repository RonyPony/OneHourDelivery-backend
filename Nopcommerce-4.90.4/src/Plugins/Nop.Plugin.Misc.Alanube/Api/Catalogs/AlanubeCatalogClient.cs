using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Catalogs;

public sealed class AlanubeCatalogClient : IAlanubeCatalogClient
{
    private readonly IAlanubeClient _alanubeClient;

    public AlanubeCatalogClient(IAlanubeClient alanubeClient) => _alanubeClient = alanubeClient;

    public Task<AlanubeApiResponse<IReadOnlyList<CountryResponseDto>>> GetCountriesAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<CountryResponseDto>>("dgi/countries", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<ProvinceResponseDto>>> GetProvincesAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<ProvinceResponseDto>>("dgi/provinces", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<DistrictResponseDto>>> GetDistrictsAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<DistrictResponseDto>>("dgi/districts", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<CorregimientoResponseDto>>> GetCorregimientosAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<CorregimientoResponseDto>>("dgi/corrections", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<LocationResponseDto>>> GetLocationsAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<LocationResponseDto>>("dgi/locations", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<MeasurementUnitResponseDto>>> GetMeasurementUnitsAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<MeasurementUnitResponseDto>>("dgi/measurement-units", cancellationToken: cancellationToken);
    public Task<AlanubeApiResponse<IReadOnlyList<GoodsAndServicesResponseDto>>> GetGoodsAndServicesAsync(CancellationToken cancellationToken = default) =>
        _alanubeClient.GetAsync<IReadOnlyList<GoodsAndServicesResponseDto>>("dgi/goods-and-services", cancellationToken: cancellationToken);
}
