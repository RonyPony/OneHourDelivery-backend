using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.RegionsOnRegisterPage.Models;

namespace Nop.Plugin.Widgets.RegionsOnRegisterPage.Mapping.Builder
{
    /// <summary>
    /// Represent the CustomerRegion builder
    /// </summary>
    public class CustomerRegionBuilder : NopEntityBuilder<CustomerRegion>
    {
        /// <inheritdoc />
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(CustomerRegion.Id)).AsInt32().PrimaryKey().Identity();

            table.WithColumn(nameof(CustomerRegion.CustomerID)).AsInt32();
            table.WithColumn(nameof(CustomerRegion.RegionID)).AsInt32();
        }
    }
}