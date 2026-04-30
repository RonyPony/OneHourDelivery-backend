using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="Day" />.
    /// </summary>
    public class DayBuilder : NopEntityBuilder<Day>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration for <see cref="Day" />.
        /// </summary>
        ///<param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Day), nameof(Day.Id)))
                    .AsInt32().PrimaryKey()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(Day), nameof(Day.Name)))
                    .AsString();
        }

        #endregion
    }
}
