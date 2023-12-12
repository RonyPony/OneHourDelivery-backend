using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="MultientregaDistrictMapping"/>.
    /// </summary>
    public sealed class MultientregaDistrictMappingBuilder : NopEntityBuilder<MultientregaDistrictMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaDistrictMapping), nameof(MultientregaDistrictMapping.MultientregaId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaDistrictMapping), nameof(MultientregaDistrictMapping.MultientregaProvinceId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaDistrictMapping), nameof(MultientregaDistrictMapping.Name)))
                    .AsString(50).NotNullable();
        }
    }
}
