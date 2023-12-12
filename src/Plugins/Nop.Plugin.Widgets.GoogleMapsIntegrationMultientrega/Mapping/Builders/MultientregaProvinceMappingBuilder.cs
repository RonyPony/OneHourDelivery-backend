using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Directory;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="MultientregaProvinceMapping"/>.
    /// </summary>
    public sealed class MultientregaProvinceMappingBuilder : NopEntityBuilder<MultientregaProvinceMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaProvinceMapping), nameof(MultientregaProvinceMapping.Name)))
                    .AsString(50).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaProvinceMapping), nameof(MultientregaProvinceMapping.MultientregaId)))
                    .AsString(5).PrimaryKey();
        }
    }
}
