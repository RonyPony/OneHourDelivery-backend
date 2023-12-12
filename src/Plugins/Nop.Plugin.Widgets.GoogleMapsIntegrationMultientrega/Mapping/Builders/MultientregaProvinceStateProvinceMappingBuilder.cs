using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Directory;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Domains;

namespace Nop.Plugin.Widgets.GoogleMapsIntegrationMultientrega.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="MultientregaProvinceStateProvinceMapping"/>.
    /// </summary>
    public sealed class MultientregaProvinceStateProvinceMappingBuilder : NopEntityBuilder<MultientregaProvinceStateProvinceMapping>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaProvinceStateProvinceMapping), nameof(MultientregaProvinceStateProvinceMapping.StateProvinceId)))
                .AsInt32().ForeignKey<StateProvince>().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(MultientregaProvinceStateProvinceMapping), nameof(MultientregaProvinceStateProvinceMapping.MultientregaProvinceId)))
                .AsString(5).PrimaryKey();
        }
    }
}
