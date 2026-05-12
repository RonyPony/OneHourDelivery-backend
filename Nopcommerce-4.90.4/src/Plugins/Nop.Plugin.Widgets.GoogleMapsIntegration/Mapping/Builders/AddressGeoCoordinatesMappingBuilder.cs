using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegration.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegration.Mapping.Builders
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
                .AsDecimal(10, 7)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(AddressGeoCoordinatesMapping), nameof(AddressGeoCoordinatesMapping.Longitude)))
                .AsDecimal(10, 7);
        }

        #endregion
    }
}