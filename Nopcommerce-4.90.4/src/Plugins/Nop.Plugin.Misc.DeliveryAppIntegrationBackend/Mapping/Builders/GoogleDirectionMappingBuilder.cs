using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Orders;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;
using Nop.Data.Extensions;
using System.Data;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents a <see cref="GoogleDirectionMappingBuilder"/> entity builder.
    /// </summary>
    public class GoogleDirectionMappingBuilder : NopEntityBuilder<GoogleDirectionMapping>
    {
        #region Methods
        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(GoogleDirectionMapping), nameof(GoogleDirectionMapping.OrderId)))
                    .AsInt32().ForeignKey<Order>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(GoogleDirectionMapping), nameof(GoogleDirectionMapping.GoogleResource)))
                    .AsString(5000).NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(GoogleDirectionMapping), nameof(GoogleDirectionMapping.DestinationType)))
                    .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(GoogleDirectionMapping), nameof(GoogleDirectionMapping.RequestNumber)))
                    .AsInt32().NotNullable()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(GoogleDirectionMapping), nameof(GoogleDirectionMapping.CreateOnUtc)))
                    .AsDateTime().NotNullable();
        }
        #endregion
    }
}
