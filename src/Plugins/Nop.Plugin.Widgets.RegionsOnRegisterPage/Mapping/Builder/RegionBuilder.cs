using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Models;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Mapping
{
    /// <summary>
    /// Represent the Region entity builder
    /// </summary>
    public class RegionBuilder : NopEntityBuilder<Region>
    {
        /// <inheritdoc />
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(Region.Id)).AsInt32().PrimaryKey().Identity();

            table.WithColumn(nameof(Region.Name)).AsString(80);
            table.WithColumn(nameof(Region.Comment)).AsString(200).Nullable();
            table.WithColumn(nameof(Region.Cost)).AsDecimal(18,2).Nullable();
            table.WithColumn(nameof(Region.Active)).AsBoolean().Nullable();
        }
    }
}