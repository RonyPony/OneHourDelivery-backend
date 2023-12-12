using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for address and geo-coordinates mappings.
    /// </summary>
    public sealed class AddressGeoCoordinatesMappingBuilder : NopEntityBuilder<AddressGeoCoordinatesMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration for <see cref="AddressGeoCoordinatesMapping"/>.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.AddressId)))
                .AsInt32().ForeignKey<Address>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.Latitude)))
                .AsDecimal(10, 7).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.Longitude)))
                .AsDecimal(10, 7).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.ProvinceId)))
                .AsString(5).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.DistrictId)))
                .AsString(5).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.TownshipId)))
                .AsString(5).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.NeighborhoodId)))
                .AsString(5).Nullable();
        }

        #endregion
    }
}