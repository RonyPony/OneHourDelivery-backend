using System.Data;
using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    ///// <summary>
    ///// Represents a <see cref="VendorReviewMapping"/> entity builder.
    ///// </summary>
    public class VendorReviewMappingBuilder : NopEntityBuilder<VendorReviewMapping>
    {
        #region Methods

        /// <summary>
        /// Applies entity configuration.
        /// </summary>
        /// <param name="table">An instance of <see cref="CreateTableExpressionBuilder"/>.</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {

            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorReviewMapping), nameof(VendorReviewMapping.VendorId)))
                    .AsInt32().ForeignKey<Vendor>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorReviewMapping), nameof(VendorReviewMapping.CustomerId)))
                    .AsInt32().ForeignKey<Customer>(onDelete: Rule.None)
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorReviewMapping), nameof(VendorReviewMapping.Rating)))
                   .AsDecimal()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorReviewMapping), nameof(VendorReviewMapping.Comment)))
                .AsString(200);

        }

        #endregion
    }
}
