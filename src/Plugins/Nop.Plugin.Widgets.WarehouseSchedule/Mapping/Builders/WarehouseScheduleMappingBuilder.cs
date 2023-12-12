using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Shipping;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Widgets.WarehouseSchedule.Domains;

namespace Nop.Plugin.Widgets.WarehouseSchedule.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="WarehouseScheduleMapping" />.
    /// </summary>
    public class PluginBuilderWarehouseScheduleMapping : NopEntityBuilder<WarehouseScheduleMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration for <see cref="WarehouseScheduleMapping" />.
        /// </summary>
        ///<param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseScheduleMapping), nameof(WarehouseScheduleMapping.WarehouseId)))
                    .AsInt32().ForeignKey<Warehouse>().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseScheduleMapping), nameof(WarehouseScheduleMapping.DayId)))
                    .AsInt32().ForeignKey<Day>().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseScheduleMapping), nameof(WarehouseScheduleMapping.IsActive)))
                    .AsBoolean()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseScheduleMapping), nameof(WarehouseScheduleMapping.BeginTime)))
                    .AsTime()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(WarehouseScheduleMapping), nameof(WarehouseScheduleMapping.EndTime)))
                    .AsTime();
        }

        #endregion
    }
}
