using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="MultientregaTownshipMapping"/>.
    /// </summary>
    public sealed class MultientregaTownshipMappingBuilder : NopEntityBuilder<MultientregaTownshipMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaTownshipMapping), nameof(MultientregaTownshipMapping.MultientregaId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaTownshipMapping), nameof(MultientregaTownshipMapping.MultientregaDistrictId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaTownshipMapping), nameof(MultientregaTownshipMapping.Name)))
                    .AsString(50).NotNullable();
        }
    }
}
