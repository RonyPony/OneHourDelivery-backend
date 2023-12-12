using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="MultientregaNeighborhoodMapping"/>.
    /// </summary>
    public sealed class MultientregaNeighborhoodMappingBuilder : NopEntityBuilder<MultientregaNeighborhoodMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.MultientregaId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.MultientregaTownshipId)))
                    .AsString(5).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.Name)))
                    .AsString(50).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.FullName)))
                    .AsString(200).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.LocXLon)))
                    .AsString(10).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.LocYLat)))
                    .AsString(10).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.Status)))
                    .AsString(1).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.UsrCrea)))
                    .AsString(100).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.DateCrea)))
                    .AsString(25).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.UsrModifica)))
                    .AsString(100).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaNeighborhoodMapping), nameof(MultientregaNeighborhoodMapping.DateModifica)))
                    .AsString(25).NotNullable();
        }
    }
}
