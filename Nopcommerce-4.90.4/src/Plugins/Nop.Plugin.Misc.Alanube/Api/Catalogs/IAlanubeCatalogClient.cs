using Nop.Plugin.Misc.Alanube.Api;

namespace Nop.Plugin.Misc.Alanube.Api.Catalogs;

public interface IAlanubeCatalogClient
{
    Task<AlanubeApiResponse<IReadOnlyList<CountryResponseDto>>> GetCountriesAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<ProvinceResponseDto>>> GetProvincesAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<DistrictResponseDto>>> GetDistrictsAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<CorregimientoResponseDto>>> GetCorregimientosAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<LocationResponseDto>>> GetLocationsAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<MeasurementUnitResponseDto>>> GetMeasurementUnitsAsync(CancellationToken cancellationToken = default);
    Task<AlanubeApiResponse<IReadOnlyList<GoodsAndServicesResponseDto>>> GetGoodsAndServicesAsync(CancellationToken cancellationToken = default);
}
