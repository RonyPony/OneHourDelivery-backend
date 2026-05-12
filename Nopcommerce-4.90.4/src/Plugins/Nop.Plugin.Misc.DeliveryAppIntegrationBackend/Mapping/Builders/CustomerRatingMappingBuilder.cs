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
    /// Represents a <see cref="CustomerRatingMapping"/> entity builder.
    /// </summary>
    public class CustomerRatingMappingBuilder : NopEntityBuilder<CustomerRatingMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerRatingMapping), nameof(CustomerRatingMapping.CreatorCustomerId)))
                    .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerRatingMapping), nameof(CustomerRatingMapping.RatedCustomerId)))
                    .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerRatingMapping), nameof(CustomerRatingMapping.Rate)))
                    .AsDecimal().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerRatingMapping), nameof(CustomerRatingMapping.Comment)))
                    .AsString(1000).Nullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(CustomerRatingMapping), nameof(CustomerRatingMapping.CreateOnUtc)))
                    .AsDateTime().NotNullable();
        }
        #endregion
    }
}
