using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="DriverRatingMapping"/> entity builder.
    /// </summary>
    public class DriverRatingMappingBuilder : NopEntityBuilder<DriverRatingMapping>
    {
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.DriverId)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.CustomerId)))
                    .AsInt32()
                    .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.OrderId)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.Rating)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.RatingType)))
                    .AsInt32()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(DriverRatingMapping), nameof(DriverRatingMapping.CreatedOnUtc)))
                    .AsDateTime();
        }
    }
}
