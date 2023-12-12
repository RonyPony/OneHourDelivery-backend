using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;
using Nop.Data.Mapping;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Domains;

namespace Nop.Plugin.Misc.DeliveryAppIntegrationBackend.Mapping.Builders
{
    /// <summary>
    /// Represents an entity builder for <see cref="VendorDiscount"/>.
    /// </summary>
    public sealed class VendorDiscountBuilder : NopEntityBuilder<VendorDiscount>
    {
        ///<inheritdoc/>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorDiscount), nameof(VendorDiscount.VendorId)))
                    .AsInt32().ForeignKey<Vendor>()
                .WithColumn(NameCompatibilityManager.GetColumnName(typeof(VendorDiscount), nameof(VendorDiscount.DiscountId)))
                    .AsInt32().ForeignKey<Discount>();
        }
    }
}
