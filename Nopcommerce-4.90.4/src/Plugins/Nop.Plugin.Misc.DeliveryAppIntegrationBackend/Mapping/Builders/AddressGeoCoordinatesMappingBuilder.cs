using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Common;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for address geo-coordinate mappings.
    /// </summary>
    public sealed class AddressGeoCoordinatesMappingBuilder : NopEntityBuilder<AddressGeoCoordinatesMapping>
    {
        /// <inheritdoc/>
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
    }
}
